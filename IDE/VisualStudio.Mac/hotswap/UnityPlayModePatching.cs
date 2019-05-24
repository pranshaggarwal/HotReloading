using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VisualStudio.Mac.hotswap
{
    public static class UnityPlayModePatching
    {
        static ILDynaRec.HotPatcher patcher;
        private static List<string> loadedAssemblies = new List<string>();
        private static Dictionary<string, DateTime> assemblyTimestamps = new Dictionary<string, DateTime>();

        public static string ScriptAssembliesDir
        {
            get
            {
                throw new NotImplementedException();
                //return Path.GetFullPath(Application.dataPath + "/../Library/ScriptAssemblies/");
            }
        }

        static void LoadAllAssemblies()
        {
            var dir = new DirectoryInfo(ScriptAssembliesDir);
            var fileInfos = dir.GetFileSystemInfos("*.dll");

            int i = 0;
            foreach (var fileInfo in fileInfos)
            {
                patcher.LoadLocalAssembly(fileInfo.FullName);
                loadedAssemblies.Add(fileInfo.Name);
                assemblyTimestamps[fileInfo.Name] = fileInfo.LastWriteTime;
                UnityCompiler.Trace($"Read {fileInfo.Name} ({fileInfo.FullName})");
            }
        }

        public static void HotPatch(string assemblyName)
        {
            try
            {
                if (patcher == null)
                {
                    patcher = new ILDynaRec.HotPatcher();
                    LoadAllAssemblies();
                }

                var checkAssemblies = loadedAssemblies.ToArray();
                if (assemblyName != null)
                {
                    checkAssemblies = checkAssemblies.Where((name) => name == assemblyName).ToArray();
                }

                int i = 0;
                foreach (var assembly in checkAssemblies)
                {
                    var compiler = new UnityCompiler
                    {
                        assemblyModifiedTime = assemblyTimestamps[assembly],
                    };
                    var outputName = Path.GetFileNameWithoutExtension(assembly) + "--hotpatch.dll";
                    if (!compiler.InvokeCompiler(assembly, outputName))
                    {
                        UnityCompiler.Trace($"Did not compile compile {assembly}.");
                        continue;
                    }

                    UnityCompiler.Trace($"Compiled assembly {assembly} as {compiler.OutputAssemblyPath}, running hot patcher.");
                    patcher.HotPatch(compiler.OutputAssemblyPath);
                    assemblyTimestamps[assembly] = DateTime.Now;
                }
            }
            finally
            {
                //EditorUtility.ClearProgressBar();
            }
        }
    }
}
