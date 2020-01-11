using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class InstanceEventMemberStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var instanceEventMember = new InstanceEventMemberStatement
            {
                Name = "TestEvent",
                Parent = new LocalIdentifierStatement
                {
                    Name = "test"
                }
            };

            var generator = CodeGeneratorFactory.Create(instanceEventMember);

            var actualOutput = generator.Generate(instanceEventMember);
            var expectedOutput = "test.TestEvent";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}