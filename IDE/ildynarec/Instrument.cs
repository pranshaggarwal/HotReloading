using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Cecil = Mono.Cecil;
using Cil = Mono.Cecil.Cil;
using OpCodes = Mono.Cecil.Cil.OpCodes;
using RefEmit = System.Reflection.Emit;

namespace ILDynaRec
{
    public class Instrument
    {
        public static void InstrumentAssembly(Cecil.AssemblyDefinition assembly) {
            var typeCount = assembly.Modules.Sum(mod => mod.Types.Count);

            foreach (var module in assembly.Modules) {
                foreach (var type in module.Types) {
                    InstrumentType(type);
                }
            }
        }

        public static void InstrumentType(Cecil.TypeDefinition type) {
            foreach (var method in type.Methods) {
                if (!method.HasBody) {
                    continue;
                }

                if (method.IsConstructor) {
                    continue;
                }

                if (method.IsStatic) {
                    continue;
                }

                var fieldName = method.GetHotpatchFieldName();
                if (type.Fields.Any(f => f.Name == fieldName)) {
                    Debug.LogWarningFormat("{0} already instrumented", method.FullName);
                    continue;
                }

                if (method.HasGenericParameters) {
                    Debug.LogWarningFormat("{0} cannot be instrumented - generic parameters", method.FullName);
                    continue;
                }

                if (method.Parameters.Any(p => p.IsOut || p.ParameterType.IsByReference)) {
                    Debug.LogWarningFormat("{0} cannot be instrumented - out/ref arguments", method.FullName);
                    continue;
                }

                //Create delegate type and static field in class
                var del = type.Module.Import(typeof(RefEmit.DynamicMethod));
                var field = new Cecil.FieldDefinition(fieldName, Cecil.FieldAttributes.Private, del);
                field.IsStatic = true;
                type.Fields.Add(field);

                //Instrument method code to check for delegate field and call it instead
                var objType = type.Module.TypeSystem.Object;
                var voidType = type.Module.TypeSystem.Void;
                var objArrayType = type.Module.Import(typeof(object[]));
                var dynamicInvoke = type.Module.Import(typeof(RefEmit.DynamicMethod).GetMethod("Invoke", new Type[] { typeof(object), typeof(object[]) }));

                var argArrayVar = new Cecil.Cil.VariableDefinition(objArrayType);
                method.Body.Variables.Add(argArrayVar);

                var ilproc = method.Body.GetILProcessor();
                var firstins = ilproc.Body.Instructions[0];

                Action<Cecil.Cil.Instruction> emit = (ins) => ilproc.InsertBefore(firstins, ins);

                //var writeLineMethod = typeof(UnityEngine.Debug).GetMethod("Log", new Type[]{typeof(string)});
                //var writeLine = type.Module.Import(writeLineMethod);

                //emit(ilproc.Create(OpCodes.Ldstr, "Hello " + method.FullName));
                //emit(ilproc.Create(OpCodes.Call, writeLine));

                //Check if delegate field is not null
                emit(ilproc.Create(OpCodes.Ldsfld, field));
                emit(ilproc.Create(OpCodes.Ldnull));
                emit(ilproc.Create(OpCodes.Ceq));

                //If null, branch to original function
                emit(ilproc.Create(OpCodes.Brtrue, firstins));

                //Create object[] args array
                emit(ilproc.Create(OpCodes.Ldc_I4, method.Parameters.Count + 1));
                emit(ilproc.Create(OpCodes.Newarr, objType));
                emit(ilproc.Create(OpCodes.Stloc, argArrayVar));

                //Store this at args[0]
                emit(ilproc.Create(OpCodes.Ldloc, argArrayVar));
                emit(ilproc.Create(OpCodes.Ldc_I4_0)); //index[0]
                emit(ilproc.Create(OpCodes.Ldarg_0)); //arg[0] (this)
                emit(ilproc.Create(OpCodes.Stelem_Ref)); //store ref

                for (int i = 0; i < method.Parameters.Count; i++) {
                    var p = method.Parameters[i];
                    //Store parameters in args array
                    emit(ilproc.Create(OpCodes.Ldloc, argArrayVar));
                    emit(ilproc.Create(OpCodes.Ldc_I4, i + 1)); //index[i + 1]
                    emit(ilproc.Create(OpCodes.Ldarg, i + 1)); //arg[i + 1]
                    if (p.ParameterType.IsValueType) {
                        emit(ilproc.Create(OpCodes.Box, p.ParameterType));
                    }
                    emit(ilproc.Create(OpCodes.Stelem_Ref)); //store ref        
                }

                //Call delegate
                emit(ilproc.Create(OpCodes.Ldsfld, field));
                emit(ilproc.Create(OpCodes.Ldarg_0)); //arg[0] (this)
                emit(ilproc.Create(OpCodes.Ldloc, argArrayVar));
                emit(ilproc.Create(OpCodes.Callvirt, dynamicInvoke));

                //Handle return value
                if (method.ReturnType != voidType) {
                    if (method.ReturnType.IsValueType) {
                        emit(ilproc.Create(OpCodes.Unbox_Any, method.ReturnType));
                    }
                }
                else {
                    //Discard dynamicInvoke result
                    emit(ilproc.Create(OpCodes.Pop));
                }

                //Return
                emit(ilproc.Create(OpCodes.Ret));
            }
        }

        /// <summary>
        /// Yields method instructions but skips hotpatch code if the method
        /// is hotpatch-instrumented.
        /// </summary>
        public static IEnumerable<Cil.Instruction> IterateInstructions(Cecil.MethodDefinition method) {
            var hotPatchFieldName = method.GetHotpatchFieldName();
            bool isHotPatched = method.DeclaringType.Fields.Any(field => field.Name == hotPatchFieldName);

            if (isHotPatched) {
                var enumerator = method.Body.Instructions.GetEnumerator();
                enumerator.MoveNext();

                while (enumerator.Current.OpCode != OpCodes.Brtrue) {
                    if (!enumerator.MoveNext()) {
                        yield break;
                    }
                }

                var jumpTo = (Cil.Instruction)(enumerator.Current.Operand);

                while (enumerator.Current != jumpTo) {
                    enumerator.MoveNext();
                }

                //don't forget to emit jump target - it is part of original method
                yield return enumerator.Current;
                while (enumerator.MoveNext()) {
                    yield return enumerator.Current;
                }
            }
            else {
                foreach (var inst in method.Body.Instructions) {
                    yield return inst;
                }
            }
        }
    }
}

