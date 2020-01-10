using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class ConstantStatementGeneratorTest
    {
        [Test]
        public void Generate_WhenValueIsNull()
        {
            var constant = new ConstantStatement(null);

            var generator = CodeGeneratorFactory.Create(constant);


            var actualOutput = generator.Generate(constant);
            var expectedOutput = "null";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenTypeIsNumeric()
        {
            var constant = new ConstantStatement(1)
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                },
            };

            var generator = CodeGeneratorFactory.Create(constant);


            var actualOutput = generator.Generate(constant);
            var expectedOutput = "1";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenTypeIsChar()
        {
            var constant = new ConstantStatement('c')
            {
                Type = new HrType
                {
                    Name = typeof(char).FullName,
                    AssemblyName = typeof(char).Assembly.FullName
                },
            };

            var generator = CodeGeneratorFactory.Create(constant);


            var actualOutput = generator.Generate(constant);
            var expectedOutput = "'c'";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenTypeIsString()
        {
            var constant = new ConstantStatement("str")
            {
                Type = new HrType
                {
                    Name = typeof(string).FullName,
                    AssemblyName = typeof(string).Assembly.FullName
                },
            };

            var generator = CodeGeneratorFactory.Create(constant);


            var actualOutput = generator.Generate(constant);
            var expectedOutput = "\"str\"";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}