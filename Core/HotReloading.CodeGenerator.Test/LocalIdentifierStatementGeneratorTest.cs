using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class LocalIdentifierStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var localIdentifierStatement = new LocalIdentifierStatement
            {
                Name = "test",
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(localIdentifierStatement);

            var actualOutput = generator.Generate(localIdentifierStatement);
            var expectedOutput = "test";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}