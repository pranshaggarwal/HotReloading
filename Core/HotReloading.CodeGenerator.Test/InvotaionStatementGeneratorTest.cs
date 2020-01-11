using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class InvotaionStatementGeneratorTest
    {
        [Test]
        public void Generate_InstanceMethodWithNoArguments()
        {
            var invocationStatement = new InvocationStatement
            {
                Method = new InstanceMethodMemberStatement
                {
                    Name = "Method",
                    Parent = new LocalIdentifierStatement
                    {
                        Name = "test"
                    }
                }
            };

            var generator = CodeGeneratorFactory.Create(invocationStatement);

            var actualOutput = generator.Generate(invocationStatement);
            var expectedOutput = "test.Method()";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_InstanceMethodWithOneArguments()
        {
            var invocationStatement = new InvocationStatement
            {
                Method = new InstanceMethodMemberStatement
                {
                    Name = "Method",
                    Parent = new LocalIdentifierStatement
                    {
                        Name = "test"
                    }
                }
            };

            invocationStatement.Arguments.Add(new ConstantStatement(1)
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            });

            var generator = CodeGeneratorFactory.Create(invocationStatement);

            var actualOutput = generator.Generate(invocationStatement);
            var expectedOutput = "test.Method(1)";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_InstanceMethodWithTwoArguments()
        {
            var invocationStatement = new InvocationStatement
            {
                Method = new InstanceMethodMemberStatement
                {
                    Name = "Method",
                    Parent = new LocalIdentifierStatement
                    {
                        Name = "test"
                    }
                }
            };

            invocationStatement.Arguments.Add(new ConstantStatement(1)
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            });

            invocationStatement.Arguments.Add(new ConstantStatement(2)
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            });

            var generator = CodeGeneratorFactory.Create(invocationStatement);

            var actualOutput = generator.Generate(invocationStatement);
            var expectedOutput = "test.Method(1, 2)";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_StaticMethodWithNoArguments()
        {
            var invocationStatement = new InvocationStatement
            {
                Method = new StaticMethodMemberStatement
                {
                    Name = "Method",
                    ParentType = new HrType
                    {
                        Name = typeof(int).FullName,
                        AssemblyName = typeof(int).Assembly.FullName
                    }
                }
            };

            var generator = CodeGeneratorFactory.Create(invocationStatement);

            var actualOutput = generator.Generate(invocationStatement);
            var expectedOutput = "System.Int32.Method()";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_StaticMethodWithOneArguments()
        {
            var invocationStatement = new InvocationStatement
            {
                Method = new StaticMethodMemberStatement
                {
                    Name = "Method",
                    ParentType = new HrType
                    {
                        Name = typeof(int).FullName,
                        AssemblyName = typeof(int).Assembly.FullName
                    }
                }
            };

            invocationStatement.Arguments.Add(new ConstantStatement(1)
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            });

            var generator = CodeGeneratorFactory.Create(invocationStatement);

            var actualOutput = generator.Generate(invocationStatement);
            var expectedOutput = "System.Int32.Method(1)";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_StaticMethodWithTwoArguments()
        {
            var invocationStatement = new InvocationStatement
            {
                Method = new StaticMethodMemberStatement
                {
                    Name = "Method",
                    ParentType = new HrType
                    {
                        Name = typeof(int).FullName,
                        AssemblyName = typeof(int).Assembly.FullName
                    }
                }
            };

            invocationStatement.Arguments.Add(new ConstantStatement(1)
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            });

            invocationStatement.Arguments.Add(new ConstantStatement(2)
            {
                Type = new HrType
                {
                    Name = typeof(int).FullName,
                    AssemblyName = typeof(int).Assembly.FullName
                }
            });

            var generator = CodeGeneratorFactory.Create(invocationStatement);

            var actualOutput = generator.Generate(invocationStatement);
            var expectedOutput = "System.Int32.Method(1, 2)";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}