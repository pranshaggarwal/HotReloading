using System.Diagnostics;
using HotReloading.Core;
using NUnit.Framework;

namespace Tests
{
    public class MethodGeneratorTest
    {
        [Test]
        public void Generate_WhenPrivateAccessModifier()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Private,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
                var expectedOutput = @"private void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenPublicAccessModifier()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenInternalAccessModifier()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Internal,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"internal void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenProtectedAccessModifier()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Protected,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"protected void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenProtectedInternalAccessModifier()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.ProtectedInternal,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"protected internal void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsAsync()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsAsync = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public async void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsStatic()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsStatic = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public static void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsStaticAndAsync()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsStatic = true,
                IsAsync = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public static async void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsVirtual()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsVirtual = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public virtual void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsVirtualAndAsync()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsVirtual = true,
                IsAsync = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public virtual async void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsAbstract()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsAbstract = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public abstract void Test();";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsOverrided()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsOverrided = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public override void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsOverridedAndAsync()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsOverrided = true,
                IsAsync = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public override async void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsOverridedAndSealed()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsSealed = true,
                IsOverrided = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public sealed override void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }

        [Test]
        public void Generate_WhenMethodIsOverridedSealedAndAsync()
        {
            var method = new Method
            {
                AccessModifier = AccessModifier.Public,
                IsSealed = true,
                IsOverrided = true,
                IsAsync = true,
                Name = "Test",
                ReturnType = new HrType
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                }
            };

            var generator = CodeGeneratorFactory.Create(method);


            var actualOutput = generator.Generate(method);
            var expectedOutput = @"public sealed override async void Test()
{
}";

            Assert.AreEqual(expectedOutput, actualOutput);
        }
    }
}