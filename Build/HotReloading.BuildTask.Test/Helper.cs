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

            methodInjectorTask.References = "/Users/pranshuaggarwal/.nuget/packages/xamarin.forms/3.2.0.839982/lib/netstandard2.0/Xamarin.Forms.Core.dll;/Users/pranshuaggarwal/.nuget/packages/prism.forms/7.1.0.431/lib/netstandard2.0/Prism.Forms.dll;/Users/pranshuaggarwal/.nuget/packages/prism.core/7.1.0.431/lib/netstandard2.0/Prism.dll";

            var assemblyPath = Path.Combine(assemblyLocation, $"{assemblyToTest}.dll");

            var tempAssemblyPath = Path.Combine(Path.GetDirectoryName(assemblyPath), $"{Path.GetFileNameWithoutExtension(assemblyPath)}.temp.{Path.GetExtension(assemblyPath)}");

            methodInjectorTask.InjectCode(assemblyPath, tempAssemblyPath);
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
