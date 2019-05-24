using MonoDevelop.Components.Commands;
using VisualStudio.Mac.hotswap;

namespace VisualStudio.Mac.Commands
{
    public class HotSwapCommand : CommandHandler
    {
        protected override void Run()
        {
            AssemblyPostprocessor.HotPatch();
        }
    }
}
