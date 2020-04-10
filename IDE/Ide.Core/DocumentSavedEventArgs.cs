using Microsoft.CodeAnalysis;

namespace Ide.Core
{
    public class DocumentSavedEventArgs
    {
        public Document Document { get; set; }
    }

    public class DocumentAddedEventArgs
    {
        public Document Document { get; set; }
    }
}