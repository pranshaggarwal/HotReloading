using System;
using MonoDevelop.Components.Commands;
using PubSub.Extension;
using VisualStudio.Mac.Events;

namespace VisualStudio.Mac.Commands
{
    public class AllowOverrideCommands : CommandHandler
    {
        private CommandInfo info;
        protected override void Run()
        {
            Settings.AllowOverride = !info.Checked;
            this.Publish<AllowOverrideChangeEvent>();
        }

        protected override void Update(CommandInfo info)
        {
            this.info = info;
            this.info.Enabled = Settings.Enabled;
            this.info.Checked = Settings.AllowOverride;
        }
    }
}
