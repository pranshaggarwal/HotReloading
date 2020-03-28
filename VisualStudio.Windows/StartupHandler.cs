using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using Task = System.Threading.Tasks.Task;
using Ide.Core;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using System.Linq;

namespace VisualStudio.Windows
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(StartupHandler.PackageGuidString)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class StartupHandler : AsyncPackage, IIde
    {
        public const string PackageGuidString = "87e788de-5f09-49bf-871f-d4142b3c30eb";

        private DTE dte;
        private WindowEvents windowEvents;
        private DocumentEvents documentEvents;
        private Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace workspace;
        private Document activeDocument;

        public event EventHandler<DocumentSavedEventArgs> DocumentSaved;
        public event EventHandler<DocumentChangedEventArgs> DocumentChanged;

        public StartupHandler()
        {

        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            try
            {
                await Initializer.Init(this, "VisualStudio.Windows");
                //await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
                //var componentModel = (IComponentModel)GetGlobalService(typeof(SComponentModel));
                //workspace = componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();
                //this.dte = (DTE)(await GetServiceAsync(typeof(DTE)));
                //windowEvents = dte.Events.WindowEvents;
                //documentEvents = dte.Events.DocumentEvents;
                //windowEvents.WindowActivated += WindowEvents_WindowActivated;
                //documentEvents.DocumentSaved += DocumentEvents_DocumentSaved;

                //ActiveDocumentChanged(dte.ActiveDocument);
            }
            catch(Exception ex)
            {

            }
        }

        private async void WindowEvents_WindowActivated(EnvDTE.Window GotFocus, EnvDTE.Window LostFocus)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync();

            if (GotFocus.Type == vsWindowType.vsWindowTypeDocument)
            {
                ActiveDocumentChanged(GotFocus.Document);
            }
        }

        private void ActiveDocumentChanged(Document document)
        {
            if (document != null && document.Language == "CSharp")
            {
                activeDocument = document;
                var analysedDocument = GetAnalysisDocument(document);

                if (analysedDocument != null)
                    DocumentChanged?.Invoke(this, new DocumentChangedEventArgs { NewDocument = analysedDocument });
            }
        }

        private void DocumentEvents_DocumentSaved(Document document)
        {
            var analysedDocument = GetAnalysisDocument(document);

            if (analysedDocument != null)
                DocumentSaved?.Invoke(this, new DocumentSavedEventArgs { Document = analysedDocument });
        }

        private Microsoft.CodeAnalysis.Document GetAnalysisDocument(Document document)
        {
            var solution = workspace.CurrentSolution;
            var documentIds = solution.GetDocumentIdsWithFilePath(document.FullName);

            if (documentIds.Length > 0)
            {
                var analysedDocument = solution.GetDocument(documentIds.First());
                return analysedDocument;
            }

            return null;
        }
    }
}
