using Microsoft.CodeAnalysis;

namespace Ide.Core
{
    public class DocumentChangedEventArgs
    {
        public Document NewDocument { get; set; }
    }
}