using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class InstanceMethodMemberStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var instanceMethodMember = new InstanceMethodMemberStatement
            {
                Name = "Method",
                Parent = new LocalIdentifierStatement
                {
                    Name = "test"
                }
            };

            var generator = CodeGeneratorFactory.Create(instanceMethodMember);

            var actualOutput = generator.Generate(instanceMethodMember);
            var expectedOutput = "test.Method";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}