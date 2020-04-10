using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            Debug.WriteLine("ReferenceAddedToProject");
        }


        void Workspace_FileChangedInProject(object sender, MonoDevelop.Projects.ProjectFileEventArgs e)
        {
            Debug.WriteLine("FiledChangedInProject");
        }


        async void Workspace_FileAddedToProject(object sender, MonoDevelop.Projects.ProjectFileEventArgs e)
        {
            Debug.WriteLine("FileAddedToProject");
            var project = GetRoslynProject(e.First().Project);
            var path = e.First().ProjectFile.FilePath;
            Microsoft.CodeAnalysis.Document document;
            while ((document = project.Documents.FirstOrDefault(x => x.FilePath == path)) == null)
            {
                Debug.WriteLine("Trying to get analysed document");
                await Task.Delay(50);
                project = GetRoslynProject(e.First().Project);
            }
            DocumentAdded?.Invoke(this, new DocumentAddedEventArgs
            {
                Document = document
            });
        }


        void Workspace_SolutionLoaded(object sender, MonoDevelop.Projects.SolutionEventArgs e)
        {
            var pad = IdeApp.Workbench.GetPad<HotReloadingPad>();
            pad.Visible = true;
        }


        public event EventHandler<DocumentSavedEventArgs> DocumentSaved;
        public event EventHandler<DocumentChangedEventArgs> DocumentChanged;
        public event EventHandler<DocumentAddedEventArgs> DocumentAdded;

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

            Microsoft.CodeAnalysis.Document document;
            while ((document = GetRoslynDocument()) == null)
            {
                Debug.WriteLine("Trying to get analysed document");
                await Task.Delay(50);
            }

            return document;
        }

        private Microsoft.CodeAnalysis.Document GetRoslynDocument()
        {
            var project = GetRoslynProject(activeDocument.DocumentContext?.Project);
            return project.Documents.FirstOrDefault(x => x.FilePath == activeDocument.FilePath);
        }

        private Microsoft.CodeAnalysis.Project GetRoslynProject(MonoDevelop.Projects.Project activeProject)
        {
            if (activeProject == null)
                return null;
            var currentWorkspace = IdeServices.TypeSystemService.Workspace;
            return currentWorkspace.CurrentSolution.Projects.FirstOrDefault<Microsoft.CodeAnalysis.Project>((p =>
            {
                if (((p.FilePath) == (activeProject).FileName))
                    return p.Name == (activeProject).Name;
                return false;
            }));
        }

    }
}