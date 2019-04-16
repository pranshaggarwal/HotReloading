using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;

namespace HotReloading.BuildTask.Test
{
    [TestFixture]
    public class InterfaceImplmentationTest
    {
        [SetUp]
        public void Setup()
        {
            Helper.Reset();
        }

        [Test]
        public void Test_IInstanceClass_Implementation()
        {
            var classToTest = "IInstanceClassInterfaceImplementationTest";
            string newAssemblyPath = Helper.GetInjectedAssembly(classToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType($"BuildTestAssembly.{classToTest}");

            var interface1 = type.GetInterfaces();

            var obj = Activator.CreateInstance(type);
            var instanceClass = obj.Should().BeAssignableTo<IInstanceClass>().Subject;

            instanceClass.InstanceMethods.Count();
        }
    }
}