using System;
using System.Text;
using HotReloading.Syntax;
using HotReloading.Syntax.Statements;

namespace HotReloading.CodeGenerator.Generators
{
    public class BlockCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var block = (Block)cSharpSyntax;

            var codeStrBuilder = new StringBuilder();

            codeStrBuilder.Append("\n{");

            foreach (var statement in block.Statements)
            {
                var statementFactory = CodeGeneratorFactory.Create(statement);
                codeStrBuilder.Append($"\n\t{statementFactory.Generate(statement)}");
                if (!(statement is Block))
                {
                    codeStrBuilder.Append(";");   
                }
            }

            codeStrBuilder.Append("\n}");

            return codeStrBuilder.ToString();
        }
    }
}
