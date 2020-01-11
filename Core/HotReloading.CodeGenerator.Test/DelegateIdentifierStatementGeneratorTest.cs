using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class DelegateIdentifierStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var staticPropertyMember = new DelegateIdentifierStatement
            {
                Target = new InstanceEventMemberStatement
                {
                    Name = "TestEvent",
                    Parent = new LocalIdentifierStatement
                    {
                        Name = "test"
                    }
                }
            };

            var generator = CodeGeneratorFactory.Create(staticPropertyMember);

            var actualOutput = generator.Generate(staticPropertyMember);
            var expectedOutput = "test.TestEvent";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}