using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class NamedTypeStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var namedType = new NamedTypeStatement
            {
                Name = "Int32",
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                },
            };

            var generator = CodeGeneratorFactory.Create(namedType);

            var actualOutput = generator.Generate(namedType);
            var expectedOutput = "System.Int32";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}