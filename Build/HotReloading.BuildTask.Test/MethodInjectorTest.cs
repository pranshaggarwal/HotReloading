using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace HotReloading.BuildTask.Test
{
    public class MethodInjectorTest
    {
        private static readonly string assemblyLocation = System.IO.Directory.GetCurrentDirectory();

        [Fact]
        public void Test()
        {
            var methodInjectorTask = new MethodInjector(new TestLogger());

            var assemblyPath = Path.Combine(assemblyLocation, "BuildTestAssembly.dll");

            var classToTest = "BuildTestAssembly.IInstanceClassInterfaceImplementationTest";

            var newAssemblyPath = methodInjectorTask.InjectCode(assemblyPath, classToTest);

            var assembly = Assembly.LoadFrom(newAssemblyPath);

            var type = assembly.GetType(classToTest);

            var interface1 = type.GetInterfaces();

            var obj = Activator.CreateInstance(type);
            var instanceClass = obj.Should().BeAssignableTo<IInstanceClass>().Subject;

            instanceClass.InstanceMethods.Count();
        }
    }
}