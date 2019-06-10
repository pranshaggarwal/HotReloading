using System;
using System.Diagnostics;
using Gtk;
using Ide.Core;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;

namespace VisualStudio.Mac
{
    public class StartupHandler : CommandHandler
    {
        private const string IDE_NAME = "Visual Studio Mac";
        protected override void Run()
        {
            var version = IdeApp.Version.ToString();
            Gtk.Application.Invoke(delegate
            {

            });
            Initializer.Init(new VisualStudioIde(), $"{IDE_NAME} - {version}");
        }
    }
}