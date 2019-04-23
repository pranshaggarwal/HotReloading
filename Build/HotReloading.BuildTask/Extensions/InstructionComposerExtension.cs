using System;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace HotReloading.BuildTask.Extensions
{
    public static class InstructionComposerExtension
    {
        public static void AddAutoPropertyGetterInstruction(this InstructionComposer composer, FieldDefinition field)
        {
            composer.LoadArg_0()
                .Load(field)
                .Return();
        }

        public static void AddAutoPropertySetterInstruction(this InstructionComposer composer, FieldDefinition field)
        {
            composer.LoadArg_0()
                .LoadArg_1()
                .Store(field)
                .Return();
        }

        public static Instruction GetStoreInstruction(this Instruction loadInstructionForReturn)
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

        public static bool IsLoadInstruction(this Instruction loadInstructionForReturn)
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
    }
}