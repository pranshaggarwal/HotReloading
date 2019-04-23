using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.ExpressionInterpreter;
using StatementConverter.Extensions;
using StatementConverter.StatementInterpreter;

namespace HotReloading.Test
{
    public static class Helper
    {
        public static Method GetMethod(string codeFile, string methodKey)
        {
            var syntaxTree = GetSyntaxTree(codeFile);

            var semanticModel = GetSemanticModel(syntaxTree);

            var mds = GetMethodDeclarationSyntax((CompilationUnitSyntax)syntaxTree.GetRoot(), methodKey, semanticModel);

            var statementInterpreterHandler = new StatementInterpreterHandler(mds, semanticModel);

            return statementInterpreterHandler.GetMethod();
        }

        public static SyntaxTree GetSyntaxTree(string codeFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream($"HotReloading.Test.TestCodes.{codeFileName}.cs"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var code = streamReader.ReadToEnd();

                    System.Diagnostics.Debug.WriteLine("Code: " + code);

                    return CSharpSyntaxTree.ParseText(code);
                }
            }
        }

        public static MethodDeclarationSyntax GetMethodDeclarationSyntax(CompilationUnitSyntax compilationUnit, string methodKey, SemanticModel semanticModel)
        {
            return compilationUnit.Members.OfType<NamespaceDeclarationSyntax>().First().Members.OfType<ClassDeclarationSyntax>().First()
                .Members.OfType<MethodDeclarationSyntax>().Cast<MethodDeclarationSyntax>().FirstOrDefault(x => HotReloading.Runtime.GetMethodKey(x.Identifier.Text,
                                    x.ParameterList.Parameters.Select(p => ((Type)p.Type.GetClassType(semanticModel)).FullName).ToArray()) == methodKey);
        }


        public static SemanticModel GetSemanticModel(SyntaxTree syntaxTree)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var compilation = CSharpCompilation.Create("HotReloading.Test")
                            .AddReferences(MetadataReference.CreateFromFile(
                            assembly.Location))
                            .AddReferences(MetadataReference.CreateFromFile(
                            typeof(object).Assembly.Location))
                            .AddSyntaxTrees(syntaxTree);

            return compilation.GetSemanticModel(syntaxTree);
        }

        public static void Setup()
        {
            Tracker.Reset();
            RuntimeMemory.Reset();
            StatementConverter.CodeChangeHandler.GetMethod = Runtime.GetMethod;
        }
    }
}
