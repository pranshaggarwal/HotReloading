using System;
using System.IO;
using System.Reflection;
using BaseAssembly;

namespace HotReloading.BuildTask.Test
{
    public static class Helper
    {
        private static readonly string assemblyLocation = System.IO.Directory.GetCurrentDirectory();

        public static string GetInjectedAssembly(string assemblyToTest)
        {

            var methodInjectorTask = new MethodInjector(new TestLogger());

            var assemblyPath = Path.Combine(assemblyLocation, $"{assemblyToTest}.dll");

            var tempAssemblyPath = Path.Combine(Path.GetDirectoryName(assemblyPath), $"{Path.GetFileNameWithoutExtension(assemblyPath)}.temp.{Path.GetExtension(assemblyPath)}");

            methodInjectorTask.InjectCode(assemblyPath, tempAssemblyPath, GetFullClassname(assemblyToTest));
            return tempAssemblyPath;
        }

        public static string GetFullClassname(string assemblyName)
        {
            return $"{assemblyName}.TestClass";
        }

        public static void Reset()
        {
            CodeChangeHandler.instanceClasses.Clear();
            CodeChangeHandler.Methods.Clear();
            Tracker.LastValue = null;
        }
    }
}
