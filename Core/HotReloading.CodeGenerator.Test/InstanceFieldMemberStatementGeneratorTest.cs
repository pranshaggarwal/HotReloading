using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class InstanceFieldMemberStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var instanceFieldMember = new InstanceFieldMemberStatement
            {
                Name = "test2",
                Parent = new LocalIdentifierStatement
                {
                    Name = "test1"
                }
            };

            var generator = CodeGeneratorFactory.Create(instanceFieldMember);

            var actualOutput = generator.Generate(instanceFieldMember);
            var expectedOutput = "test1.test2";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}