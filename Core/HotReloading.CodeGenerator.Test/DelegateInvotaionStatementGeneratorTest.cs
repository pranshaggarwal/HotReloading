using System.Collections.Generic;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class DelegateInvotaionStatementGeneratorTest
    {
        [Test]
        public void Generate_WithNoArguments()
        {
            var delegateInvocation = new DelegateInvocationStatement
            {
                Delegate = new DelegateIdentifierStatement
                {
                    Target = new LocalIdentifierStatement
                    {
                        Name = "test"
                    }
                }
            };

            var generator = CodeGeneratorFactory.Create(delegateInvocation);

            var actualOutput = generator.Generate(delegateInvocation);
            var expectedOutput = "test()";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WithOneArguments()
        {
            var delegateInvocation = new DelegateInvocationStatement
            {
                Delegate = new DelegateIdentifierStatement
                {
                    Target = new LocalIdentifierStatement
                    {
                        Name = "test"
                    }
                },
                Arguments = new List<IStatementCSharpSyntax>
                {
                    new ConstantStatement(1)
                    {
                        Type = new HrType
                        {
                            Name = typeof(int).FullName,
                            AssemblyName = typeof(int).Assembly.FullName
                        }
                    }
                }
            };

            var generator = CodeGeneratorFactory.Create(delegateInvocation);

            var actualOutput = generator.Generate(delegateInvocation);
            var expectedOutput = "test(1)";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WithTwoArguments()
        {
            var delegateInvocation = new DelegateInvocationStatement
            {
                Delegate = new DelegateIdentifierStatement
                {
                    Target = new LocalIdentifierStatement
                    {
                        Name = "test"
                    }
                },
                Arguments = new List<IStatementCSharpSyntax>
                {
                    new ConstantStatement(1)
                    {
                        Type = new HrType
                        {
                            Name = typeof(int).FullName,
                            AssemblyName = typeof(int).Assembly.FullName
                        }
                    },
                    new ConstantStatement(2)
                    {
                        Type = new HrType
                        {
                            Name = typeof(int).FullName,
                            AssemblyName = typeof(int).Assembly.FullName
                        }
                    }
                }
            };

            var generator = CodeGeneratorFactory.Create(delegateInvocation);

            var actualOutput = generator.Generate(delegateInvocation);
            var expectedOutput = "test(1, 2)";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}