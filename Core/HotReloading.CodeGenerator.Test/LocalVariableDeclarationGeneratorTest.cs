using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class LocalVariableDeclarationGeneratorTest
    {
        [Test]
        public void Generate_WhenIntLocalVariableDeclare()
        {
            var localVariableDeclaration = new LocalVariableDeclaration
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                },
                Name = "test"
            };

            var generator = CodeGeneratorFactory.Create(localVariableDeclaration);


            var actualOutput = generator.Generate(localVariableDeclaration);
            var expectedOutput = @"System.Int32 test";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

    }
}