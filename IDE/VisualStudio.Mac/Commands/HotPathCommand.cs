using MonoDevelop.Components.Commands;
using VisualStudio.Mac.hotswap;

namespace VisualStudio.Mac.Commands
{
    public class HotPathCommand : CommandHandler
    {
        protected override void Run()
        {
            UnityPlayModePatching.HotPatch(null);
        }
    }
}
