using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class ThisStatementCodeGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var thisStatement = new ThisStatement();

            var generator = CodeGeneratorFactory.Create(thisStatement);

            var actualOutput = generator.Generate(thisStatement);
            var expectedOutput = "this";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}