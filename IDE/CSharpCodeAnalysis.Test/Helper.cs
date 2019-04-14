using System;
using System.IO;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpCodeAnalysis.Test
{
    public static class Helper
    {
        public static SemanticModel GetSemanticModel(SyntaxTree syntaxTree)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var compilation = CSharpCompilation.Create("CSharpCodeAnalysis.Test")
                            .AddReferences(MetadataReference.CreateFromFile(
                            assembly.Location))
                            .AddReferences(MetadataReference.CreateFromFile(
                            typeof(object).Assembly.Location))
                            .AddSyntaxTrees(syntaxTree);

            return compilation.GetSemanticModel(syntaxTree);
        }

        public static SyntaxTree GetSyntaxTree(string codeFileName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream($"CSharpCodeAnalysis.Test.TestCodes.{codeFileName}.cs"))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    var code = streamReader.ReadToEnd();

                    System.Diagnostics.Debug.WriteLine("Code: " + code);

                    return CSharpSyntaxTree.ParseText(code);
                }
            }
        }
    }
}
