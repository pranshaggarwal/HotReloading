//using System;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Threading;
//using EnvDTE;
//using Microsoft.VisualStudio;
//using Microsoft.VisualStudio.ComponentModelHost;
//using Microsoft.VisualStudio.Shell;
//using Task = System.Threading.Tasks.Task;
//using Ide.Core;

//namespace VisualStudio.Window
//{
//    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
//    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
//    [Guid(PackageGuidString)]
//    [ProvideAutoLoad(VSConstants.UICONTEXT.SolutionExists_string, PackageAutoLoadFlags.BackgroundLoad)]
//    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
//    public sealed class SolutionOpenPackage : AsyncPackage, IIde
//    {
//        public const string PackageGuidString = "e6a44be9-0d5d-4a4f-9ff6-82fd360fddae";

//        private DTE dte;
//        private WindowEvents windowEvents;
//        private DocumentEvents documentEvents;
//        private Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace workspace;
//        private Document activeDocument;

//        public event EventHandler<DocumentSavedEventArgs> DocumentSaved;
//        public event EventHandler<DocumentChangedEventArgs> DocumentChanged;

//        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
//        {
//            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
//            var componentModel = (IComponentModel)GetGlobalService(typeof(SComponentModel));
//            workspace = componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();
//            this.dte = (DTE)(await GetServiceAsync(typeof(DTE)));
//            windowEvents = dte.Events.WindowEvents;
//            documentEvents = dte.Events.DocumentEvents;
//            windowEvents.WindowActivated += WindowEvents_WindowActivated;
//            documentEvents.DocumentSaved += DocumentEvents_DocumentSaved;

//            ActiveDocumentChanged(dte.ActiveDocument);
//        }

//        private async void WindowEvents_WindowActivated(EnvDTE.Window GotFocus, EnvDTE.Window LostFocus)
//        {
//            await this.JoinableTaskFactory.SwitchToMainThreadAsync();

//            if (GotFocus.Type == vsWindowType.vsWindowTypeDocument)
//            {
//                ActiveDocumentChanged(GotFocus.Document);
//            }
//        }

//        private void ActiveDocumentChanged(Document document)
//        {
//            if (document != null && document.Language == "CSharp")
//            {
//                activeDocument = document;
//                var analysedDocument = GetAnalysisDocument(document);

//                if(analysedDocument != null)
//                 DocumentChanged?.Invoke(this, new DocumentChangedEventArgs { NewDocument = analysedDocument });
//            }
//        }

//        private void DocumentEvents_DocumentSaved(Document document)
//        {
//            var analysedDocument = GetAnalysisDocument(document);

//            if (analysedDocument != null)
//                DocumentSaved?.Invoke(this, new DocumentSavedEventArgs { Document = analysedDocument });
//        }

//        private Microsoft.CodeAnalysis.Document GetAnalysisDocument(Document document)
//        {
//            var solution = workspace.CurrentSolution;
//            var documentIds = solution.GetDocumentIdsWithFilePath(document.FullName);

//            if (documentIds.Length > 0)
//            {
//                var analysedDocument = solution.GetDocument(documentIds.First());
//                return analysedDocument;
//            }

//            return null;
//        }
//    }
//}
