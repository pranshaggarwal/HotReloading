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
            var assemblyToTest = "IInstanceClassInterfaceImplementationTest";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var interfaces = type.GetInterfaces();

            var obj = Activator.CreateInstance(type);
            var instanceClass = obj.Should().BeAssignableTo<IInstanceClass>().Subject;

            instanceClass.InstanceMethods.Count().Should().Be(0);

            File.Delete(newAssemblyPath);
        } 

        [Test]
        public void Test_IInstanceClass_AlreadyImplemented()
        {
            var assemblyToTest = "IInstanceClassAleadyImplmentedTest";
            string newAssemblyPath = Helper.GetInjectedAssembly(assemblyToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(Helper.GetFullClassname(assemblyToTest));

            var iInstanceInterfaces = type.GetInterfaces().Where(x => x == typeof(IInstanceClass));

            iInstanceInterfaces.Count().Should().Be(1);

            var obj = Activator.CreateInstance(type);
            var instanceClass = obj.Should().BeAssignableTo<IInstanceClass>().Subject;

            instanceClass.InstanceMethods.Count().Should().Be(0);

            File.Delete(newAssemblyPath);
        }
    }
}