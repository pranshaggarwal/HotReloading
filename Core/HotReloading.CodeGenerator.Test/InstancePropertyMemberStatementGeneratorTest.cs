using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class InstancePropertyMemberStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var instancePropertyMember = new InstancePropertyMemberStatement
            {
                Name = "Test2",
                Parent = new LocalIdentifierStatement
                {
                    Name = "test1"
                }
            };

            var generator = CodeGeneratorFactory.Create(instancePropertyMember);

            var actualOutput = generator.Generate(instancePropertyMember);
            var expectedOutput = "test1.Test2";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}