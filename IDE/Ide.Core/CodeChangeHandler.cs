using System;
using System.Collections.Generic;
using System.Linq;
using CSharpCodeAnalysis;
using HotReloading.Core;
using Microsoft.CodeAnalysis;
using StatementConverter.StatementInterpreter;
using MQTTnet;
using MQTTnet.Server;
using System.Threading.Tasks;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using System.Reflection;

namespace Ide.Core
{
    public class CodeChangeHandler
    {
        private Document currentDocument;

        public IIde ide;
        private readonly TcpCommunicatorServer server;

        internal CodeChangeHandler(IIde ide)
        {
            this.ide = ide;
            server = new TcpCommunicatorServer(Constants.DEFAULT_PORT);

            ide.DocumentSaved += Ide_DocumentSaved;
            ide.DocumentChanged += Ide_DocumentChanged;
        }

        public static CodeChangeHandler Instance { get; internal set; }

        private void Ide_DocumentChanged(object sender, DocumentChangedEventArgs e)
        {
            currentDocument = e.NewDocument;
        }

        private async void Ide_DocumentSaved(object sender, DocumentSavedEventArgs e)
        {
            CodeChangeMessage request = new CodeChangeMessage();
            try
            {
                var oldDocument = currentDocument;
                var newDocument = e.Document;

                var oldSyntaxTree = await oldDocument.GetSyntaxTreeAsync();

                var semanticModel = await newDocument.GetSemanticModelAsync();

                var newSynctaxTree = await newDocument.GetSyntaxTreeAsync();

                var diagnostics = semanticModel.GetDiagnostics().Where(x => x.Severity == DiagnosticSeverity.Error);

                if (diagnostics.Count() == 0)
                {
                    request.CodeChange = CompareTree(newSynctaxTree, oldSyntaxTree, semanticModel);
                    currentDocument = newDocument;
                }
                else
                {
                    var compileError = "";
                    var errorIndex = 0;
                    foreach (var diagnostic in diagnostics)
                    {
                        errorIndex++;
                        if (compileError != "") compileError += "\n";
                        compileError += errorIndex + ". Error: ";
                        compileError += diagnostic.GetMessage() + "\n";
                        compileError += "    at " + diagnostic.Location.GetLineSpan().Path;
                        compileError += ": " + diagnostic.Location.GetLineSpan().StartLinePosition;
                    }

                    request.Error = new Error
                    {
                        CompileError = compileError
                    };
                }
            }
            catch (Exception ex)
            {
                request.Error = new Error
                {
                    ParsingError = ex.Message,
                };
            }

            await server.Send(request);
        }

        public CodeChange CompareTree(SyntaxTree newSyntaxTree, SyntaxTree oldSyntaxTree,
            SemanticModel semanticModel)
        {
            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, oldSyntaxTree);

            var updatedMethods = new List<Method>();

            foreach (var mds in codeChangesVisitor.UpdatedMethods)
            {
                var interpreterHandler = new StatementInterpreterHandler(mds, semanticModel);

                var method = interpreterHandler.GetMethod();
                updatedMethods.Add(method);
            }

            var request = new CodeChangeMessage();

            return new CodeChange
            {
                Methods = updatedMethods
            };
        }
    }
}