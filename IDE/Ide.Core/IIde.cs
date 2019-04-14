using System;

namespace Ide.Core
{
    public interface IIde
    {
        event EventHandler<DocumentSavedEventArgs> DocumentSaved;
        event EventHandler<DocumentChangedEventArgs> DocumentChanged;
    }
}