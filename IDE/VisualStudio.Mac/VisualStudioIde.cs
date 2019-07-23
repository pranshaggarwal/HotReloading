using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Ide.Core;
using MonoDevelop.Core;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;

namespace VisualStudio.Mac
{
    public class VisualStudioIde : IIde
    {
        private Document activeDocument;

        public VisualStudioIde()
        {
            IdeApp.Workbench.ActiveDocumentChanged += Handle_ActiveDocumentChanged;
            IdeApp.Workspace.FileAddedToProject += Workspace_FileAddedToProject;
            IdeApp.Workspace.FileChangedInProject += Workspace_FileChangedInProject;
            IdeApp.Workspace.ReferenceAddedToProject += Workspace_ReferenceAddedToProject;
            IdeApp.Workspace.SolutionLoaded += Workspace_SolutionLoaded;

            ActiveDocumentChanged();
        }

        void Workspace_ReferenceAddedToProject(object sender, MonoDevelop.Projects.ProjectReferenceEventArgs e)
        {
        }


        void Workspace_FileChangedInProject(object sender, MonoDevelop.Projects.ProjectFileEventArgs e)
        {
        }


        void Workspace_FileAddedToProject(object sender, MonoDevelop.Projects.ProjectFileEventArgs e)
        {
        }


        void Workspace_SolutionLoaded(object sender, MonoDevelop.Projects.SolutionEventArgs e)
        {
            var pad = IdeApp.Workbench.GetPad<HotReloadingPad>();
            pad.Visible = true;
        }


        public event EventHandler<DocumentSavedEventArgs> DocumentSaved;
        public event EventHandler<DocumentChangedEventArgs> DocumentChanged;

        private void Handle_ActiveDocumentChanged(object sender, EventArgs e)
        {
            ActiveDocumentChanged();
        }

        private async void ActiveDocumentChanged()
        {
            var doc = IdeApp.Workbench.ActiveDocument;

            if (activeDocument == doc) return;
            if (activeDocument != null)
            {
                activeDocument.Saved -= HandleDocumentSaved;
                activeDocument = null;
            }
            var test = doc?.IsCompileableInProject;
            var file = doc?.IsFile;
            var x = doc?.GetContent<string>();
            var ext = doc?.FileName.Extension;
            if (ext == ".cs")
            {
                activeDocument = doc;
                activeDocument.Saved += HandleDocumentSaved;

                var analysedDocument = await GetAnalysisDocument();

                DocumentChanged?.Invoke(this, new DocumentChangedEventArgs {NewDocument = analysedDocument});
            }
        }

        private async void HandleDocumentSaved(object sender, EventArgs e)
        {
            var analysedDocument = await GetAnalysisDocument();

            if (analysedDocument != null)
                DocumentSaved?.Invoke(this, new DocumentSavedEventArgs {Document = analysedDocument});
        }

        private async Task<Microsoft.CodeAnalysis.Document> GetAnalysisDocument()
        {
            if (activeDocument.FileName.Extension != ".cs") return null;
            while (activeDocument.AnalysisDocument == null)
            {
                Debug.WriteLine("Trying to get analysed document");
                await Task.Delay(50);
            }

            return activeDocument.AnalysisDocument;
        }
    }
}