using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using PubSub.Extension;
using VisualStudio.Mac.Events;

namespace VisualStudio.Mac.Commands
{
    public class EnableCommands : CommandHandler
    {
        private CommandInfo info;

        protected override void Run()
        {
            Settings.Enabled = !info.Checked;
            this.Publish<EnableHotReloadingChangeEvent>();
        }

        protected override void Update(CommandInfo info)
        {
            this.info = info;
            info.Enabled = true;
            info.Checked = Settings.Enabled;
        }
    }
}
