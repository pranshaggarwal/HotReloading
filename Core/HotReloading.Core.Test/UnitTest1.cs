using System.Diagnostics;
using HotReloading.Core;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var ty = typeof(void);
            var method1 = new Method1
            {
                AccessModifier = AccessModifier.Public,
                IsAsync = true,
                IsStatic = true,
                Name = "TestMethod",
                ReturnType = new Type
                {
                    Name = typeof(void).FullName,
                    AssemblyName = typeof(void).Assembly.FullName
                },
                Parameters = new System.Collections.Generic.List<Parameter1>
                {
                    
                }
            };

            Debug.WriteLine(method1.ToString());
            Assert.Pass();
        }
    }
}