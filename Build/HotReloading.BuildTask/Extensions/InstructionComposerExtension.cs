using Mono.Cecil;

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
    }
}