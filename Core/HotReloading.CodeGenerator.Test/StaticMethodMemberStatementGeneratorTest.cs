using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class StaticMethodMemberStatementGeneratorTest
    {
        [Test]
        public void Generate_Test()
        {
            var staticMethodMember = new StaticMethodMemberStatement
            {
                Name = "Method",
                ParentType = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(staticMethodMember);

            var actualOutput = generator.Generate(staticMethodMember);
            var expectedOutput = "System.Int32.Method";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}