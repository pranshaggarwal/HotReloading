using System.Collections.Generic;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class ParameterInterpreter
    {
        private readonly MethodDeclarationSyntax methodDeclarationSyntax;
        private readonly SemanticModel semanticModel;

        public ParameterInterpreter(MethodDeclarationSyntax methodDeclarationSyntax, SemanticModel semanticModel)
        {
            this.methodDeclarationSyntax = methodDeclarationSyntax;
            this.semanticModel = semanticModel;
        }

        public List<Parameter> GetParameters()
        {
            var retVal = new List<Parameter>();
            var syntaxes = methodDeclarationSyntax.ParameterList.ChildNodes().Cast<ParameterSyntax>();

            foreach (var ps in syntaxes)
            {
                var node = ps.ChildNodes().ElementAt(0);
                var typeInfo = semanticModel.GetTypeInfo(node);
                var str = typeInfo.GetClassType();
                retVal.Add(new Parameter
                {
                    Name = ps.Identifier.Text,
                    Type = str
                });
            }

            return retVal;
        }
    }
}