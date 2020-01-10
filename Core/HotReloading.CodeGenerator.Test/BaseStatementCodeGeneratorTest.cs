using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class BaseStatementCodeGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var baseStatement = new BaseStatement();

            var generator = CodeGeneratorFactory.Create(baseStatement);

            var actualOutput = generator.Generate(baseStatement);
            var expectedOutput = "base";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}