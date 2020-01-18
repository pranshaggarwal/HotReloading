using HotReloading.Syntax;
using NUnit.Framework;

namespace HotReloading.CodeGenerator.Test
{
    [TestFixture]
    public class ClassGeneratorTest
    {
        [Test]
        public void Generate_WhenPrivateAccessModifier()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.Private,
                Name = "TestClass",
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"private class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenPublicAccessModifier()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.Public,
                Name = "TestClass"
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"public class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenInternalAccessModifier()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.Internal,
                Name = "TestClass"
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"internal class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenProtectedAccessModifier()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.Protected,
                Name = "TestClass"
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"protected class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenProtectedInternalAccessModifier()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.ProtectedInternal,
                Name = "TestClass"
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"protected internal class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenClassIsStatic()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.Public,
                IsStatic = true,
                Name = "TestClass"
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"public static class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenClassIsAbstract()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.Public,
                IsAbstract = true,
                Name = "TestClass"
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"public abstract class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenClassIsealed()
        {
            var @class = new Class
            {
                AccessModifier = AccessModifier.Public,
                IsSealed = true,
                Name = "TestClass"
            };

            var generator = CodeGeneratorFactory.Create(@class);


            var actualOutput = generator.Generate(@class);
            var expectedOutput = @"public sealed class TestClass
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenClassWithMethod()
        {
            var test = new Method
            {
                AccessModifier = AccessModifier.Public,
                Name = "TestMethod",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };
            var @class = new Class
            {
                AccessModifier = AccessModifier.Public,
                Name = "TestClass",
                Methods = new System.Collections.Generic.List<Method>
                {
                    new Method
                    {
                        AccessModifier = AccessModifier.Public,
                        Name = "TestMethod",
                        ReturnType = new HrType
                        {
                            Name = typeof(void).FullName,
                            AssemblyName = typeof(void).Assembly.FullName
                        },
                        Body = new Syntax.Statements.Block()
                    }
                }
            };

            var generator = CodeGeneratorFactory.Create(@class);

            var actualOutput = generator.Generate(@class);
            var expectedOutput = "public class TestClass\n{\n\tpublic void TestMethod()\n\t{\n\t}\n}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}