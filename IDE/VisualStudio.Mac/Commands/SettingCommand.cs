using MonoDevelop.Components.Commands;

namespace VisualStudio.Mac.Commands
{
    public class SettingCommand : CommandHandler
    {
        protected override void Update(CommandInfo info)
        {
            base.Update(info);
            info.Enabled = true;
            info.Visible = true;
        }

        protected override void Run()
        {
            base.Run();

            //new SettingWindow().Show();
        }
    }
}
