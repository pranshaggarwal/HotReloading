using Ide.Core;
using MonoDevelop.Components.Commands;

namespace VisualStudio.Mac
{
    public class StartupHandler : CommandHandler
    {
        protected override void Run()
        {
            CodeChangeHandler.Init(new VisualStudioIde());
        }
    }
}