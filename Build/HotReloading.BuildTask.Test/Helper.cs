using System;
using System.IO;

namespace HotReloading.BuildTask.Test
{
    public static class Helper
    {
        private static readonly string assemblyLocation = System.IO.Directory.GetCurrentDirectory();

        public static string GetInjectedAssembly(string classToTest)
        {
            var methodInjectorTask = new MethodInjector(new TestLogger());

            var assemblyPath = Path.Combine(assemblyLocation, "BuildTestAssembly.dll");

            var newAssemblyPath = methodInjectorTask.InjectCode(assemblyPath, GetFullClassname(classToTest));
            return newAssemblyPath;
        }

        public static string GetFullClassname(string className)
        {
            return $"BuildTestAssembly.{className}";
        }

        public static void Reset()
        {
            CodeChangeHandler.instanceClasses.Clear();
            CodeChangeHandler.Methods.Clear();
        }
    }
}
