using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class StaticFieldMemberStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var staticFieldMember = new StaticFieldMemberStatement
            {
                Name = "StaticField",
                ParentType = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                },
            };

            var generator = CodeGeneratorFactory.Create(staticFieldMember);

            var actualOutput = generator.Generate(staticFieldMember);
            var expectedOutput = "System.Int32.StaticField";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}