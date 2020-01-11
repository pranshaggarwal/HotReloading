using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class StaticEventMemberStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var staticPropertyMember = new StaticPropertyMemberStatement
            {
                Name = "StaticEvent",
                ParentType = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                },
            };

            var generator = CodeGeneratorFactory.Create(staticPropertyMember);

            var actualOutput = generator.Generate(staticPropertyMember);
            var expectedOutput = "System.Int32.StaticEvent";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}