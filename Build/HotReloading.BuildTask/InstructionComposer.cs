using System;
using System.Collections.Generic;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace HotReloading.BuildTask
{
    public class InstructionComposer
    {
        private readonly ModuleDefinition moduleDefinition;

        public InstructionComposer(ModuleDefinition moduleDefinition)
        {
            this.moduleDefinition = moduleDefinition;

            Instructions = new List<Instruction>();
        }

        public List<Instruction> Instructions { get; }

        public InstructionComposer LoadArg(ParameterDefinition parameterDefinition)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldarg, parameterDefinition));
            return this;
        }

        public InstructionComposer LoadArg_0()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
            return this;
        }

        public InstructionComposer LoadArg_1()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldarg_1));
            return this;
        }

        public InstructionComposer LoadArg_2()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldarg_2));
            return this;
        }

        public InstructionComposer LoadArg_3()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldarg_3));
            return this;
        }

        public InstructionComposer Load(TypeReference typeReference)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldtoken, typeReference));
            return this;
        }

        public InstructionComposer Load(string str)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldstr, str));
            return this;
        }

        public InstructionComposer Load(VariableDefinition variableDefinition)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldloc, variableDefinition));
            return this;
        }

        public InstructionComposer Load(FieldDefinition field)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldfld, field));
            return this;
        }

        public InstructionComposer Load(ParameterDefinition value)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldarg, value));
            return this;
        }

        public InstructionComposer LoadNull()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldnull));
            return this;
        }

        public InstructionComposer LoadArray(int length, Type type, string[] elements)
        {
            SetupArray(length, type);

            for (var i = 0; i < elements.Length; i++)
            {
                AddArrayElement(i, elements[i]);
            }

            return this;
        }

        private void SetupArray(int length, Type type)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, length));
            var typeReference = moduleDefinition.ImportReference(type);
            Instructions.Add(Instruction.Create(OpCodes.Newarr, typeReference));
        }

        private void AddArrayElement(int index, string element)
        {
            Instructions.Add(Instruction.Create(OpCodes.Dup));
            Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, index));
            Instructions.Add(Instruction.Create(OpCodes.Ldstr, element));
            Instructions.Add(Instruction.Create(OpCodes.Stelem_Ref));
        }

        public InstructionComposer LoadArray(ParameterDefinition[] array, bool isInstance = false)
        {
            var offset = 0;

            if (isInstance)
                offset = 1;

            SetupArray(array.Length + offset, typeof(object));

            if (isInstance)
            {
                Instructions.Add(Instruction.Create(OpCodes.Dup));
                Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, 0));
                Instructions.Add(Instruction.Create(OpCodes.Ldarg_0));
                Instructions.Add(Instruction.Create(OpCodes.Stelem_Ref));
            }

            for (var i = 0; i < array.Length; i++)
            {
                Instructions.Add(Instruction.Create(OpCodes.Dup));
                Instructions.Add(Instruction.Create(OpCodes.Ldc_I4, i + offset));
                Instructions.Add(Instruction.Create(OpCodes.Ldarg, array[i]));
                if(array[i].ParameterType.IsValueType)
                {
                    Instructions.Add(Instruction.Create(OpCodes.Box, array[i].ParameterType));
                }
                Instructions.Add(Instruction.Create(OpCodes.Stelem_Ref));
            }

            return this;
        }

        public InstructionComposer Load_0()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldloc_0));
            return this;
        }

        public InstructionComposer Load_1()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldloc_1));
            return this;
        }

        public InstructionComposer Load_2()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldloc_2));
            return this;
        }

        public InstructionComposer Load_3()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldloc_3));
            return this;
        }

        public InstructionComposer Load_a_S()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldloca_S));
            return this;
        }

        public InstructionComposer Load_S()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldloc_S));
            return this;
        }

        public InstructionComposer StaticCall(Method method)
        {
            var methodInfo = method.ParentType.GetMethod(method.MethodName, method.ParameterSignature);
            var methodReference = moduleDefinition.ImportReference(methodInfo);
            Instructions.Add(Instruction.Create(OpCodes.Call, methodReference));
            return this;
        }

        public InstructionComposer BaseCall(MethodReference methodReference)
        {
            Instructions.Add(Instruction.Create(OpCodes.Call, methodReference));
            return this;
        }

        public InstructionComposer InstanceCall(Method method)
        {
            var methodInfo = method.ParentType.GetMethod(method.MethodName, method.ParameterSignature);
            var methodReference = moduleDefinition.ImportReference(methodInfo);
            Instructions.Add(Instruction.Create(OpCodes.Callvirt, methodReference));
            return this;
        }

        public InstructionComposer InstanceCall(MethodDefinition setMethod)
        {
            Instructions.Add(Instruction.Create(OpCodes.Call, setMethod));
            return this;
        }

        public InstructionComposer Store(VariableDefinition variableDefinition)
        {
            Instructions.Add(Instruction.Create(OpCodes.Stloc, variableDefinition));
            return this;
        }

        public InstructionComposer Store(FieldDefinition field)
        {
            Instructions.Add(Instruction.Create(OpCodes.Stfld, field));
            return this;
        }

        public InstructionComposer Store_0()
        {
            Instructions.Add(Instruction.Create(OpCodes.Stloc_0));
            return this;
        }

        public InstructionComposer Store_1()
        {
            Instructions.Add(Instruction.Create(OpCodes.Stloc_1));
            return this;
        }

        public InstructionComposer Store_2()
        {
            Instructions.Add(Instruction.Create(OpCodes.Stloc_2));
            return this;
        }

        public InstructionComposer Store_3()
        {
            Instructions.Add(Instruction.Create(OpCodes.Stloc_3));
            return this;
        }

        public InstructionComposer Store_S()
        {
            Instructions.Add(Instruction.Create(OpCodes.Stloc_S));
            return this;
        }

        public InstructionComposer IsNotNull()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldnull));
            Instructions.Add(Instruction.Create(OpCodes.Cgt_Un));
            return this;
        }

        public InstructionComposer LoadStr(string name)
        {
            Instructions.Add(Instruction.Create(OpCodes.Ldstr, name));
            return this;
        }

        public InstructionComposer MoveToWhenFalse(Instruction targetInstruction)
        {
            Instructions.Add(Instruction.Create(OpCodes.Brfalse, targetInstruction));
            return this;
        }

        public InstructionComposer MoveTo(Instruction targetInstruction)
        {
            Instructions.Add(Instruction.Create(OpCodes.Br, targetInstruction));
            return this;
        }

        public InstructionComposer CompareEqual()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ceq));
            return this;
        }

        public InstructionComposer Unbox_Any(TypeReference type)
        {
            Instructions.Add(Instruction.Create(OpCodes.Unbox_Any, type));
            return this;
        }

        public InstructionComposer Cast(TypeReference type)
        {
            Instructions.Add(Instruction.Create(OpCodes.Castclass, type));
            return this;
        }

        public InstructionComposer NoOperation()
        {
            Instructions.Add(Instruction.Create(OpCodes.Nop));
            return this;
        }

        public InstructionComposer Pop()
        {
            Instructions.Add(Instruction.Create(OpCodes.Pop));
            return this;
        }

        public InstructionComposer Return()
        {
            Instructions.Add(Instruction.Create(OpCodes.Ret));
            return this;
        }

        public InstructionComposer Append(Instruction instruction)
        {
            Instructions.Add(instruction);
            return this;
        }

        public InstructionComposer Append(InstructionComposer composer2)
        {
            Instructions.AddRange(composer2.Instructions);
            return this;
        }
    }
}