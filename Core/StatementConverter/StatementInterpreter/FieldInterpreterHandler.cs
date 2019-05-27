using System.Collections.Generic;
using HotReloading.Core;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class FieldInterpreterHandler
    {
        private readonly FieldDeclarationSyntax fds;
        private readonly SemanticModel semanticModel;

        public FieldInterpreterHandler(FieldDeclarationSyntax fds, SemanticModel semanticModel)
        {
            this.fds = fds;
            this.semanticModel = semanticModel;
        }

        public IEnumerable<Field> GetFields()
        {
            var fields = new List<Field>();
            var typeInfo = semanticModel.GetTypeInfo(fds.Declaration.Type);
            var type = typeInfo.GetClassType();
            var accessModifier = Helper.GetAccessModifer(fds.Modifiers);

            var parentNamedType = (INamedTypeSymbol)semanticModel.GetDeclaredSymbol(fds.Parent);
            var parentType = parentNamedType.GetClassType();
            var isStatic = fds.Modifiers.Any(SyntaxKind.StaticKeyword);


            foreach (var variable in fds.Declaration.Variables)
            {
                var field = new Field
                {
                    Name = variable.Identifier.Text,
                    Type = type,
                    ParentType = parentType,
                    AccessModifier = accessModifier,
                    IsStatic = isStatic
                };

                fields.Add(field);
            }

            return fields;
        }
    }
}