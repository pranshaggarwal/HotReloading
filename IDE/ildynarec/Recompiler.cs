using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Diagnostics;

using Cecil = Mono.Cecil;
using Reflection = System.Reflection;

namespace ILDynaRec
{
    public class Recompiler
    {
        TypeResolver resolver = new TypeResolver();

        const Reflection.BindingFlags bflags_all =
            Reflection.BindingFlags.NonPublic |
            Reflection.BindingFlags.Instance |
            Reflection.BindingFlags.Static |
            Reflection.BindingFlags.Public |
            Reflection.BindingFlags.FlattenHierarchy;

        const Reflection.BindingFlags bflags_all_instance =
            Reflection.BindingFlags.NonPublic |
            Reflection.BindingFlags.Public |
            Reflection.BindingFlags.Instance;

        private Dictionary<Type, Reflection.MethodInfo> EmitInstructionCache = new Dictionary<Type, Reflection.MethodInfo>();
        private Dictionary<Type, Reflection.MethodInfo> EmitPrimitiveCache = new Dictionary<Type, Reflection.MethodInfo>();

        private static Type[] primitiveOperandTypes = new Type[] {
            typeof(SByte), typeof(Int16), typeof(Int32), typeof(Single), typeof(Double),
            typeof(Byte), typeof(UInt16), typeof(UInt32), typeof(String)
        };


        public Recompiler() {

        }

        public Type FindType(Cecil.TypeReference typeRef) {
            var type = resolver.FindType(typeRef);
            if (type == null) {
                throw new Exception(String.Format("Cannot resolve type {0}", typeRef));
            }

            return type;
        }

        public Reflection.MethodBase FindMethod(Type type, Cecil.MethodReference methodRef) {
            Type[] opParamTypes = methodRef.Parameters.Select((prm) => {
                if (prm.ParameterType is Cecil.GenericParameter) {
                    throw new Exception("Unsupported generic parameter");
                    //return ourType.GenericTypeArguments[((Cecil.GenericParameter)prm.ParameterType).Position];
                }
                if(prm.ParameterType is Cecil.ArrayType) {
                    var arr = (Cecil.ArrayType)prm.ParameterType;
                    if(arr.ElementType.IsGenericParameter) {
                        throw new Exception("Unsupported generic array parameter");
                    }
                }

                return FindType(prm.ParameterType);
            }).ToArray();

            if (methodRef.Name == ".ctor") {
                return type.GetConstructor(
                    Reflection.BindingFlags.Instance |
                    Reflection.BindingFlags.Static /*| Reflection.BindingFlags.NonPublic */|
                    Reflection.BindingFlags.Public,
                    binder: null,
                    types: opParamTypes,
                    modifiers: null);
            }
            else {
                return type.GetMethod(methodRef.Name,
                    bindingAttr: bflags_all,
                    binder: null,
                    types: opParamTypes,
                    modifiers: null);
            }
        }

        public DynamicMethod RecompileMethod(Cecil.MethodDefinition methodDef) {
            Debug.Trace("Recompiling method: {0}", methodDef.FullName);

            var declaringType = FindType(methodDef.DeclaringType);
            var returnType = FindType(methodDef.ReturnType);
            Type[] paramTypes = methodDef.Parameters.Select((paramDef) => {
                return FindType(paramDef.ParameterType);
            }).ToArray();

            if (!methodDef.IsStatic) {
                paramTypes = new Type[] { declaringType }.Concat(paramTypes).ToArray();
            }

            var dynMethod = new DynamicMethod(methodDef.Name, returnType, paramTypes, true);
            ILGenerator il = dynMethod.GetILGenerator();
            dynMethod.InitLocals = methodDef.Body.InitLocals;
            foreach (var variable in methodDef.Body.Variables) {
                var localType = FindType(variable.VariableType);
                Debug.Trace("Declaring local (cecil type: {0}) of type (runtime type: {1})", variable.VariableType, localType);
                il.DeclareLocal(localType);
            }

            var labels = new Dictionary<Cecil.Cil.Instruction, Label>();

            foreach (var inst in methodDef.Body.Instructions) {
                if (inst.Operand != null && inst.Operand.GetType() == typeof(Cecil.Cil.Instruction)) {
                    var opinst = (Cecil.Cil.Instruction)(inst.Operand);
                    labels[opinst] = il.DefineLabel();
                }
            }

            foreach (var inst in Instrument.IterateInstructions(methodDef)) {
                Debug.Trace("Emitting: {0}", inst);

                Label label;
                if (labels.TryGetValue(inst, out label)) {
                    il.MarkLabel(label);
                }

                var ilop = FindOpcode(inst.OpCode);

                if (inst.Operand == null) {
                    // Simple operation without operand.
                    il.Emit(ilop);
                    continue;
                }

                var operand = inst.Operand;
                var operandType = operand.GetType();

                // Dynamic dispatch implementation:

                // We have to run different processing code depending on the type of instruction
                // operand. Visitor pattern cannot be implemented here, because we don't actually
                // own the classes that are the operands (and some of them are primitive or system
                // types). 

                // Therefore, dynamic dispatcher is used. Method for each operand type is implemented
                // in this class, and reflection is used to find correct method to call.

                // In newer .net versions we would be able to do EmitInstruction(il, ilop, (dynamic)operand),
                // but the .net version we are targeting (because of Unity compatibility) does not 
                // have `dynamic`.

                if (operandType == typeof(Cecil.Cil.Instruction)) {
                    //branch location     
                    var operandInst = (Cecil.Cil.Instruction)operand;

                    il.Emit(ilop, labels[operandInst]);
                }
                else if (primitiveOperandTypes.Contains(operandType)) {
                    //if operand is primitive, call il.Emit directly
                    Reflection.MethodInfo method;
                    if (!EmitPrimitiveCache.TryGetValue(operandType, out method)) {
                        method = typeof(ILGenerator).GetMethod("Emit", new Type[] { typeof(OpCode), operandType });
                        EmitPrimitiveCache[operandType] = method;
                    }

                    if (method == null) {
                        throw new Exception(String.Format("Emit method for primitive type {0} not found.", operandType.Name));
                    }

                    try {
                        method.Invoke(il, new object[] { ilop, operand });
                    } catch (Reflection.TargetInvocationException e) {
                        throw e.InnerException;
                    }
                }
                else {
                    //or else, call our EmitInstruction
                    Reflection.MethodInfo method;
                    if (!EmitInstructionCache.TryGetValue(operandType, out method)) {
                        method = GetType().GetMethod("EmitInstruction",
                            bindingAttr: bflags_all_instance,
                            binder: null,
                            modifiers: null,
                            types: new Type[] { typeof(ILGenerator), typeof(OpCode), operandType });
                        EmitInstructionCache[operandType] = method;
                    }

                    if (method == null) {
                        throw new Exception(String.Format("Don't know what to do with operand {0}", operandType.Name));
                    }
                    try {
                        method.Invoke(this, new object[] { il, ilop, operand });
                    } catch (Reflection.TargetInvocationException e) {
                        throw e.InnerException;
                    }
                }
            }

            return dynMethod;
        }

        static OpCode FindOpcode(Cecil.Cil.OpCode cop) {
            if (cop.Name == "constrained.") {
                return OpCodes.Constrained;
            }

            var opType = typeof(OpCodes).GetFields().FirstOrDefault((op) => {
                return op.Name.ToLower().Replace('_', '.') == cop.Name;
            });
            if(opType == null) {
                throw new Exception($"Unsupported opcode: {cop.Name}");
            }

            return (OpCode)opType.GetValue(null);
        }

        void EmitInstruction(ILGenerator il, OpCode opcode, Cecil.MethodReference operandMethod) {
            var ourType = FindType(operandMethod.DeclaringType);
            Type[] opParamTypes = operandMethod.Parameters.Select((prm) => {
                if (prm.ParameterType is Cecil.GenericParameter) {
                    return ourType.GenericTypeArguments[((Cecil.GenericParameter)prm.ParameterType).Position];
                }

                return FindType(prm.ParameterType);
            }).ToArray();

            if (operandMethod.Name == ".ctor") {
                var target = ourType.GetConstructor(
                    Reflection.BindingFlags.Instance |
                    Reflection.BindingFlags.Static /*| Reflection.BindingFlags.NonPublic */|
                    Reflection.BindingFlags.Public,
                    binder: null,
                    types: opParamTypes,
                    modifiers: null);
                if(target == null) {
                    throw new Exception($"Cannot find call target constructor: {operandMethod.Name} {opParamTypes}");
                }
                il.Emit(opcode, target);
            }
            else {
                var target = ourType.GetMethod(operandMethod.Name,
                    bindingAttr: bflags_all,
                    binder: null,
                    types: opParamTypes,
                    modifiers: null);
                if (target == null) {
                    throw new Exception($"Cannot find call target method: {operandMethod.Name} {opParamTypes}");
                }
                il.Emit(opcode, target);
            }
        }

        void EmitInstruction(ILGenerator il, OpCode opcode, Cecil.FieldDefinition operand) {
            EmitInstruction(il, opcode, (Cecil.FieldReference)operand);
        }

        void EmitInstruction(ILGenerator il, OpCode opcode, Cecil.FieldReference operandField) {
            var ourType = FindType(operandField.DeclaringType);

            il.Emit(opcode, ourType.GetField(operandField.Name, bindingAttr: bflags_all));
        }

        void EmitInstruction(ILGenerator il, OpCode opcode, Cecil.GenericInstanceMethod operandGeneric) {
            var ourType = FindType(operandGeneric.DeclaringType);

            Type[] opGenericArgs = operandGeneric.GenericArguments.Select((prm) => {
                return FindType(prm);
            }).ToArray();

            Type[] opParamTypes = operandGeneric.Parameters.Select((prm) => {
                if (prm.ParameterType is Cecil.GenericParameter) {
                    var genericParam = ((Cecil.GenericParameter)prm.ParameterType);
                    return opGenericArgs[genericParam.Position];
                }

                return FindType(prm.ParameterType);
            }).ToArray();

            var methods = ourType.GetMethods(bindingAttr: bflags_all);

            var ourMethod = methods.FirstOrDefault(met => {
                return met.Name == operandGeneric.Name &&
                    met.GetGenericArguments().Length == opGenericArgs.Length &&
                    met.GetParameters().Length == opParamTypes.Length;
            });
            if(ourMethod == null) {
                throw new Exception($"Could not find generic call target for {operandGeneric}");
            }

            var metParams = ourMethod.GetParameters();

            ourMethod = ourMethod.MakeGenericMethod(opGenericArgs);
            il.Emit(opcode, ourMethod);
        }

        void EmitInstruction(ILGenerator il, OpCode opcode, Cecil.TypeReference operand) {
            il.Emit(opcode, FindType(operand));
        }

        void EmitInstruction(ILGenerator il, OpCode opcode, Cecil.Cil.VariableDefinition operand) {
            il.Emit(opcode, operand.Index);
        }
    }
}