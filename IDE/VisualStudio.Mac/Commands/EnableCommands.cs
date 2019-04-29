using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;

namespace VisualStudio.Mac.Commands
{
    public class EnableCommands : CommandHandler
    {
        private CommandInfo info;

        protected override void Run()
        {
            Settings.Enabled = !info.Checked;
        }

        protected override void Update(CommandInfo info)
        {
            this.info = info;
            info.Enabled = true;
            info.Checked = Settings.Enabled;
        }
    }
}
