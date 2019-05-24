using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace VisualStudio.Mac.hotswap
{
    internal class UnityCompiler
    {
        public UnityCompiler() { }

        public DateTime? assemblyModifiedTime = null;

        public static string TempPath {
            get {
                throw new NotImplementedException();
                //return Path.GetFullPath(Application.dataPath + "/../Temp/");
            }
        }

        public static string MainAssemblyFilename {
            get
            {
                throw new NotImplementedException();
                //return Path.GetFullPath(Application.dataPath + "/../Library/ScriptAssemblies/Assembly-CSharp.dll");
            }
        }

        public static void Trace(string str) {
            ILDynaRec.Debug.Trace(str);
        }

        List<string> m_sourceFiles = new List<string>();

        private string GetMonoCompileParams(string assembly, string outFilename) {
            var dir = new DirectoryInfo(TempPath);
            var fileInfos = dir.GetFileSystemInfos("UnityTempFile-*");

            Array.Sort(fileInfos, (a, b) => b.CreationTime.CompareTo(a.CreationTime));

            var sourceFilePattern = new Regex(@"^Assets/[a-zA-Z\\/]+\.cs\s?$");
            var asmPath = $"-out:Temp/{assembly}";
            foreach (var fileInfo in fileInfos) {
                try {
                    var text = File.ReadAllText(fileInfo.FullName);
                    if (!text.Contains(asmPath)) {
                        continue;
                    }

                    // Found param file for current assembly, grab source file names while at it.
                    foreach (var line in text.Split('\n')) {
                        if (sourceFilePattern.IsMatch(line)) {
                            m_sourceFiles.Add(line.Trim());
                        }
                    }

                    return text.Replace(asmPath, "-out:" + outFilename);
                }
                catch { }
            }

            return null;
        }

        public bool InvokeCompiler(string assemblyName, string outputName) {
            var compileParams = GetMonoCompileParams(assemblyName, $"Temp/{outputName}");
            if (compileParams == null) {
                ILDynaRec.Debug.Log($"No compile params for {assemblyName}. " +
                    "It's likely that assembly has yet to be compiled during current session");
                return false;
            }

            if (assemblyModifiedTime != null) {
                var asmTime = assemblyModifiedTime.Value;
                bool anyChanges = m_sourceFiles.Any((fileName) => {
                    var fileTime = File.GetLastWriteTime(fileName);
                    if (fileTime > asmTime) {
                        Trace($"Found changed source: {fileName} in {assemblyName}");
                        return true;
                    }
                    else {
                        return false;
                    }
                });
                if (!anyChanges) {
                    Trace($"Ignoring {assemblyName} - no changes in source files");
                    return false;
                }
            }

            var file = File.CreateText(TempPath + "HotPatchTemp");
            file.Write(compileParams);
            file.Close();

            var process = new Process();

            OutputAssemblyPath = Path.GetFullPath(TempPath + "/" + outputName);

            if (File.Exists(OutputAssemblyPath)) {
                File.Delete(OutputAssemblyPath);
            }

            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;

            //  Command line:
            // "C:\Program Files\Unity\Editor\Data\MonoBleedingEdge\bin\mono.exe"
            // "C:\Program Files\Unity\Editor\Data\MonoBleedingEdge\lib\mono\4.5\mcs.exe"
            // @Temp/UnityTempFile-2dd7cda01d4f8c6428fa4253a6886c18

            process.StartInfo.FileName = @"C:\Program Files\Unity\Editor\Data\MonoBleedingEdge\bin\mono.exe";
            process.StartInfo.Arguments = "\"" + @"C:\Program Files\Unity\Editor\Data\MonoBleedingEdge\lib\mono\4.5\mcs.exe" + "\"" + " @Temp/HotPatchTemp";

            Trace($"Running {process.StartInfo.FileName} {process.StartInfo.Arguments}");

            process.Start();
            process.WaitForExit();

            return File.Exists(OutputAssemblyPath);
        }

        public string OutputAssemblyPath { get; private set; }
    }
}
