using System.IO;
using System.Linq;
using System.Reflection;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CSharp.Expressions;
using StatementConverter.ExpressionInterpreter;
using StatementConverter.StatementInterpreter;

namespace StatementConverter.Test
{
    public static class Helper
    {
        public static SemanticModel GetSemanticModel(SyntaxTree syntaxTree)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var compilation = CSharpCompilation.Create("StatementConverter.Test")
                            .AddReferences(MetadataReference.CreateFromFile(
                            assembly.Location))
                            .AddReferences(MetadataReference.CreateFromFile(
                            typeof(object).Assembly.Location))
                            .AddSyntaxTrees(syntaxTree);

            return compilation.GetSemanticModel(syntaxTree);
        }

        public static MethodDeclarationSyntax GetMethodDeclarationSyntax(CompilationUnitSyntax compilationUnit, string methodName)
        {
            return compilationUnit.Members.OfType<NamespaceDeclarationSyntax>().First().Members.OfType<ClassDeclarationSyntax>().First()
                .Members.OfType<MethodDeclarationSyntax>().Cast<MethodDeclarationSyntax>().First(x => x.Identifier.Text == methodName);
        }

        public static SyntaxTree GetSyntaxTree(string codeFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream($"StatementConverter.Test.TestCodes.{codeFileName}.cs"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var code = streamReader.ReadToEnd();

                    System.Diagnostics.Debug.WriteLine("Code: " + code);

                    return CSharpSyntaxTree.ParseText(code);
                }
            }
        }

        public static CSharpLamdaExpression GetLamdaExpression(string codeFile, string methodName)
        {
            var syntaxTree = GetSyntaxTree(codeFile);

            var mds = GetMethodDeclarationSyntax((CompilationUnitSyntax)syntaxTree.GetRoot(), methodName);

            var semanticModel = GetSemanticModel(syntaxTree);
            var statementInterpreterHandler = new StatementInterpreterHandler(mds, semanticModel);

            var method = statementInterpreterHandler.GetMethod();

            var interpreterHandler = new ExpressionInterpreterHandler(method);

            var lamdaExpression = interpreterHandler.GetLamdaExpression();
            return lamdaExpression;
        }

        public static ITypeSymbol GetTypeSymbol(string codeFile, string methodName)
        {
            var syntaxTree = GetSyntaxTree(codeFile);

            var mds = GetMethodDeclarationSyntax((CompilationUnitSyntax)syntaxTree.GetRoot(), methodName);
            var semanticModel = GetSemanticModel(syntaxTree);

            var statement = (LocalDeclarationStatementSyntax)mds.Body.Statements.FirstOrDefault();
            var test = semanticModel.GetSymbolInfo(statement.Declaration.Type).Symbol;

            return (ITypeSymbol)semanticModel.GetSymbolInfo(statement.Declaration.Type).Symbol;
        }

        public static void Setup()
        {
            Tracker.Reset();
            RuntimeMemory.Methods.Clear();
            HotReloading.CodeChangeHandler.instanceClasses.Clear();
            CodeChangeHandler.GetMethod = HotReloading.CodeChangeHandler.GetMethod;
        }
    }
}
