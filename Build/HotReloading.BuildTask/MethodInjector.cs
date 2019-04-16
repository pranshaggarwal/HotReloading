using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HotReloading.BuildTask.Extensions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace HotReloading.BuildTask
{
    public class MethodInjector : Task
    {
        public MethodInjector()
        {
            Logger = new Logger(Log);
        }

        public MethodInjector(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        [Required] public string AssemblyFile { get; set; }

        [Required] public string ProjectDirectory { get; set; }

        public bool DebugSymbols { get; set; }
        public string DebugType { get; set; }

        public override bool Execute()
        {
            var assemblyPath = Path.Combine(ProjectDirectory, AssemblyFile);
            var tempAssemblyPath = InjectCode(assemblyPath);

            File.Replace(tempAssemblyPath, assemblyPath, tempAssemblyPath + "1");

            Logger.LogMessage("Injection done");
            return true;
        }

        public string InjectCode(string assemblyPath, string classToInjectCode = null)
        {
            var debug = DebugSymbols || !string.IsNullOrEmpty(DebugType) && DebugType.ToLowerInvariant() != "none";

            var ad = AssemblyDefinition.ReadAssembly(assemblyPath, new ReaderParameters
            {
                ReadWrite = true,
                ReadSymbols = true
            });

            var md = ad.MainModule;

            var types = md.Types.Where(x => x.BaseType != null).ToList();

            var iInstanceClassType = md.ImportReference(typeof(IInstanceClass));

            foreach (var type in types)
            {
                if (classToInjectCode != null && type.FullName != classToInjectCode)
                    continue;
                var methods = type.Methods;

                PropertyDefinition instanceMethods = null;
                MethodDefinition getInstanceMethod = null;
                MethodDefinition instanceMethodGetters = null;

                if (type.IsDelegate(md))
                    continue;

                var hasImplementedIInstanceClass = type.HasImplementedIInstanceClass(iInstanceClassType, out getInstanceMethod, out instanceMethodGetters);

                ImplementIInstanceClass(md, type, ref instanceMethods, ref getInstanceMethod, ref instanceMethodGetters, hasImplementedIInstanceClass);

                foreach (var method in methods)
                {
                    if (method.CustomAttributes
                            .Any(x => x.AttributeType.Name == "CompilerGeneratedAttribute") ||
                        method.CustomAttributes
                            .Any(x => x.AttributeType.Name == "GeneratedCodeAttribute"))
                        continue;

                    if (method == instanceMethodGetters || method.IsConstructor)
                        continue;

                    Logger.LogMessage("Weaving Method " + method.Name);

                    WrapMethod(md, type, method, getInstanceMethod);
                }

                if (!type.IsAbstract & !hasImplementedIInstanceClass)
                    methods.Add(getInstanceMethod);
            }

            var tempAssemblyPath = Path.Combine(Path.GetDirectoryName(assemblyPath), $"{Path.GetFileNameWithoutExtension(assemblyPath)}.temp.{Path.GetExtension(assemblyPath)}");

            ad.Write(tempAssemblyPath, new WriterParameters
            {
                WriteSymbols = debug
            });

            ad.Dispose();

            return tempAssemblyPath;
        }

        private static void ImplementIInstanceClass(ModuleDefinition md, TypeDefinition type, ref PropertyDefinition instanceMethods, ref MethodDefinition getInstanceMethod, ref MethodDefinition instanceMethodGetters, bool hasImplementedIInstanceClass)
        {
            if (!type.IsAbstract && !hasImplementedIInstanceClass)
            {
                type.Interfaces.Add(new InterfaceImplementation(md.ImportReference(typeof(IInstanceClass))));
                CreateInstanceMethodsProperty(md, type, out instanceMethods, out instanceMethodGetters);
                getInstanceMethod = CreateGetInstanceMethod(md, instanceMethods);
            }
        }

        private static void CreateInstanceMethodsProperty(ModuleDefinition md, TypeDefinition type, out PropertyDefinition instanceMethods, out MethodDefinition instanceMethodGetters)
        {
            var instaceMethodType = md.ImportReference(typeof(Dictionary<string, Delegate>));
            var field = type.CreateField(md, "instanceMethods", instaceMethodType);

            var returnComposer = new InstructionComposer(md)
                .Load_1()
                .Return();

            var getFieldComposer = new InstructionComposer(md)
                .LoadArg_0()
                .Load(field)
                .Store_1()
                .MoveTo(returnComposer.Instructions.First());

            var initializeField = new InstructionComposer(md)
                .NoOperation()
                .LoadArg_0()
                .LoadArg_0()
                .StaticCall(new Method
                {
                    ParentType = typeof(CodeChangeHandler),
                    MethodName = nameof(CodeChangeHandler.GetInitialInstanceMethods),
                    ParameterSignature = new[] { typeof(IInstanceClass) }
                })
                .Store(field)
                .NoOperation();

            var getterComposer = new InstructionComposer(md);
            getterComposer.LoadArg_0()
                .Load(field)
                .LoadNull()
                .CompareEqual()
                .Store_0()
                .Load_0()
                .MoveToWhenFalse(getFieldComposer.Instructions.First())
                .Append(initializeField)
                .Append(getFieldComposer)
                .Append(returnComposer);

            instanceMethodGetters = type.CreateGetter(md, "InstanceMethods", instaceMethodType, getterComposer.Instructions, vir: true);

            var boolVariable = new VariableDefinition(md.ImportReference(typeof(bool)));
            instanceMethodGetters.Body.Variables.Add(boolVariable);

            var delegateVariable = new VariableDefinition(md.ImportReference(typeof(Delegate)));
            instanceMethodGetters.Body.Variables.Add(delegateVariable);

            instanceMethods = type.CreateProperty("InstanceMethods", instaceMethodType, instanceMethodGetters, null);
        }

        private static void WrapMethod(ModuleDefinition md, TypeDefinition type, MethodDefinition method, MethodDefinition getInstanceMethod)
        {
            var delegateVariable = new VariableDefinition(md.ImportReference(typeof(Delegate)));
            method.Body.Variables.Add(delegateVariable);

            var boolVariable = new VariableDefinition(md.ImportReference(typeof(bool)));
            method.Body.Variables.Add(boolVariable);

            var instructions = method.Body.Instructions;
            var ilprocessor = method.Body.GetILProcessor();
            Instruction loadInstructionForReturn = null;
            if (method.ReturnType.FullName != "System.Void" && instructions.Count > 1)
            {
                var secondLastInstruction =  instructions.ElementAt(instructions.Count - 2);

                if (IsLoadInstruction(secondLastInstruction))
                    loadInstructionForReturn = secondLastInstruction;
            }

            var retInstruction = instructions.Last();

            var oldInstruction = method.Body.Instructions.Where(x => x != retInstruction &&
                                                                    x != loadInstructionForReturn).ToList();

            var lastInstruction = retInstruction;

            method.Body.Instructions.Clear();

            if (loadInstructionForReturn != null)
            {
                method.Body.Instructions.Add(loadInstructionForReturn);
                lastInstruction = loadInstructionForReturn;
            }

            method.Body.Instructions.Add(retInstruction);

            foreach (var instruction in oldInstruction) ilprocessor.InsertBefore(lastInstruction, instruction);
            method.Body.InitLocals = true;
            var firstInstruction = method.Body.Instructions.First();

            var parameters = method.Parameters.ToArray();

            var composer = new InstructionComposer(md);

            if(method.IsStatic)
                ComposeStaticMethodInstructions(type, method, delegateVariable, boolVariable, firstInstruction, parameters, composer);
            else
                ComposeInstanceMethodInstructions(type, method, delegateVariable, boolVariable, firstInstruction, parameters, composer, getInstanceMethod);

            if (method.ReturnType.FullName == "System.Void") composer.Pop();
            else if (method.ReturnType.IsValueType)
            {
                composer.Unbox_Any(method.ReturnType);
            }
            else
            {
                composer.Cast(method.ReturnType);
            }

            if (loadInstructionForReturn != null)
            {
                Instruction storeInstructionForReturn = GetStoreInstruction(loadInstructionForReturn);
                composer.Append(storeInstructionForReturn);
            }

            composer.MoveTo(lastInstruction);

            foreach (var instruction in composer.Instructions) ilprocessor.InsertBefore(firstInstruction, instruction);
        }

        private static void ComposeInstanceMethodInstructions(TypeDefinition type, MethodDefinition method, VariableDefinition delegateVariable, VariableDefinition boolVariable, Instruction firstInstruction, ParameterDefinition[] parameters, InstructionComposer composer, MethodDefinition getInstanceMethod)
        {
            composer.LoadArg_0()
                .LoadStr(method.Name)
                .InstanceCall(getInstanceMethod)
                .Store(delegateVariable)
                .Load(delegateVariable)
                .IsNotNull()
                .Store(boolVariable)
                .Load(boolVariable)
                .MoveToWhenFalse(firstInstruction)
                .NoOperation()
                .Load(delegateVariable)
                .LoadArray(parameters, true)
                .InstanceCall(new Method
                {
                    ParentType = typeof(Delegate),
                    MethodName = nameof(Delegate.DynamicInvoke),
                    ParameterSignature = new[] { typeof(object[]) }
                });
        }

        private static void ComposeStaticMethodInstructions(TypeDefinition type, MethodDefinition method, VariableDefinition delegateVariable, VariableDefinition boolVariable, Instruction firstInstruction, ParameterDefinition[] parameters, InstructionComposer composer)
        {
            composer.Load(type)
                            .StaticCall(new Method
                            {
                                ParentType = typeof(Type),
                                MethodName = nameof(Type.GetTypeFromHandle),
                                ParameterSignature = new[] { typeof(RuntimeTypeHandle) }
                            })
                            .Load(method.Name)
                            .StaticCall(new Method
                            {
                                ParentType = typeof(CodeChangeHandler),
                                MethodName = nameof(CodeChangeHandler.GetMethodDelegate),
                                ParameterSignature = new[] { typeof(Type), typeof(string) }
                            })
                            .Store(delegateVariable)
                            .Load(delegateVariable)
                            .IsNotNull()
                            .Store(boolVariable)
                            .Load(boolVariable)
                            .MoveToWhenFalse(firstInstruction)
                            .NoOperation()
                            .Load(delegateVariable)
                            .LoadArray(parameters)
                            .InstanceCall(new Method
                            {
                                ParentType = typeof(Delegate),
                                MethodName = nameof(Delegate.DynamicInvoke),
                                ParameterSignature = new[] { typeof(object[]) }
                            });
        }

        private static MethodDefinition CreateGetInstanceMethod(ModuleDefinition md, PropertyDefinition instanceMethods)
        {
            var getInstanceMethod = new MethodDefinition("GetInstanceMethod", MethodAttributes.Family,
                md.ImportReference(typeof(Delegate)));

            var methodName = new ParameterDefinition("methodName", ParameterAttributes.None,
                md.ImportReference(typeof(string)));
            getInstanceMethod.Parameters.Add(methodName);

            var boolVariable = new VariableDefinition(md.ImportReference(typeof(bool)));
            getInstanceMethod.Body.Variables.Add(boolVariable);

            var delegateVariable = new VariableDefinition(md.ImportReference(typeof(Delegate)));
            getInstanceMethod.Body.Variables.Add(delegateVariable);

            var composer1 = new InstructionComposer(md)
                .Load(delegateVariable)
                .Return();

            var composer2 = new InstructionComposer(md).LoadNull()
                .Store(delegateVariable)
                .MoveTo(composer1.Instructions.First());

            var composer = new InstructionComposer(md)
                .LoadArg_0()
                .InstanceCall(instanceMethods.GetMethod)
                .Load(methodName)
                .InstanceCall(new Method
                {
                    ParentType = typeof(Dictionary<string, Delegate>),
                    MethodName = "ContainsKey",
                    ParameterSignature = new[] {typeof(Dictionary<string, Delegate>).GetGenericArguments()[0]}
                })
                .Store(boolVariable)
                .Load(boolVariable)
                .MoveToWhenFalse(composer2.Instructions.First())
                .LoadArg_0()
                .InstanceCall(instanceMethods.GetMethod)
                .Load(methodName)
                .InstanceCall(new Method
                {
                    ParentType = typeof(Dictionary<string, Delegate>),
                    MethodName = "get_Item",
                    ParameterSignature = new[] {typeof(Dictionary<string, Delegate>).GetGenericArguments()[0]}
                })
                .Store(delegateVariable)
                .MoveTo(composer1.Instructions.First())
                .Append(composer2)
                .Append(composer1);

            var ilProcessor = getInstanceMethod.Body.GetILProcessor();

            foreach (var instruction in composer.Instructions) ilProcessor.Append(instruction);

            return getInstanceMethod;
        }

        private static bool IsLoadInstruction(Instruction loadInstructionForReturn)
        {
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_0)
                return true;
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_1)
                return true;
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_2)
                return true;
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_3)
                return true;
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_S)
                return true;
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc)
                return true;
            return false;
        }

        private static Instruction GetStoreInstruction(Instruction loadInstructionForReturn)
        {
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_0)
                return Instruction.Create(OpCodes.Stloc_0);
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_1)
                return Instruction.Create(OpCodes.Stloc_1);
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_2)
                return Instruction.Create(OpCodes.Stloc_2);
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_3)
                return Instruction.Create(OpCodes.Stloc_3);
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc_S)
                return Instruction.Create(OpCodes.Stloc_S, (VariableDefinition)loadInstructionForReturn.Operand);
            if (loadInstructionForReturn.OpCode == OpCodes.Ldloc)
                return Instruction.Create(OpCodes.Stloc, (VariableDefinition)loadInstructionForReturn.Operand);
            throw new Exception("Unable to get store locationn for opcode: " + loadInstructionForReturn.OpCode);
        }
    }
}