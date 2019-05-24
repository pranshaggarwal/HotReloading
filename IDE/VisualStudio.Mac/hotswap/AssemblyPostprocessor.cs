using System;

namespace VisualStudio.Mac.hotswap
{
    static class AssemblyPostprocessor
    {
        static ILDynaRec.HotPatcher hotPatcher = new ILDynaRec.HotPatcher();

        static AssemblyPostprocessor()
        {
            SetDynarecLogging();

            var logFilename = UnityCompiler.TempPath + "/hotswap.log";

            //File.Delete(logFilename);

            System.Diagnostics.Trace.Listeners.Clear();
            System.Diagnostics.Trace.Listeners.Add(
                new System.Diagnostics.TextWriterTraceListener(logFilename, "file log"));

            System.Diagnostics.Trace.AutoFlush = true;

            Start();
        }

        public static void Start()
        {

        }

        private static bool ShouldLockReload
        {
            get
            {
                throw new NotImplementedException();
                r//eturn EditorPrefs.GetBool("AutolockRefresh") || EditorPrefs.GetBool("HotPatchInstrument");
            }
        }

        private static bool assembliesLocked = false;
        private static void onPlay()
        {
            //if (EditorApplication.isPlaying)
            //{
            //    if (ShouldLockReload && !assembliesLocked)
            //    {
            //        Debug.Log("Locking reloads");
            //        EditorApplication.LockReloadAssemblies();
            //        EditorPrefs.SetBool("kAutoRefresh", false);
            //        assembliesLocked = true;
            //    }
            //}
            //else
            //{
            //    if (assembliesLocked)
            //    {
            //        //Debug.Log("Unlocking reload");
            //        EditorApplication.UnlockReloadAssemblies();
            //        EditorPrefs.SetBool("kAutoRefresh", true);
            //        assembliesLocked = false;

            //        AssetDatabase.Refresh();
            //    }
            //}
        }

        static void SetDynarecLogging()
        {
            //ILDynaRec.Debug.LogAction = (str) => Debug.Log(str);
            //ILDynaRec.Debug.WarnAction = (str) => Debug.LogWarning(str);
            //ILDynaRec.Debug.ErrorAction = (str) => Debug.LogError(str);
        }

        public static void HotPatch()
        {
            var compiler = new UnityCompiler();
            if (!compiler.InvokeCompiler(null, null))
            {
                //Debug.LogError("Failed to compile assembly.");
                return;
            }

            var assemblyPath = compiler.OutputAssemblyPath;
            hotPatcher.HotPatch(assemblyPath);
        }
    }
}
