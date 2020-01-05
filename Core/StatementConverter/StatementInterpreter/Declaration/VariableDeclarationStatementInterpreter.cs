using System.Collections.Generic;
using HotReloading.Core.Statements;
using HotReloading.Syntax.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class VariableDeclarationStatementInterpreter : IStatementInterpreter
    {
        private readonly List<LocalVariableDeclaration> scopedLocalVariableDeclarations;
        private readonly SemanticModel semanticModel;
        private readonly StatementInterpreterHandler statementInterpreterHandler;
        private readonly VariableDeclarationSyntax variableDeclarationSyntax;

        public VariableDeclarationStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler,
            SemanticModel semanticModel, List<LocalVariableDeclaration> scopedLocalVariableDeclarations,
            VariableDeclarationSyntax variableDeclarationSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.semanticModel = semanticModel;
            this.scopedLocalVariableDeclarations = scopedLocalVariableDeclarations;
            this.variableDeclarationSyntax = variableDeclarationSyntax;
        }

        public IStatementCSharpSyntax GetStatement()
        {
            var retVal = new List<IStatementCSharpSyntax>();
            var variableTypeInfo = semanticModel.GetTypeInfo(variableDeclarationSyntax.Type);
            var type = variableTypeInfo.GetHrType();
            foreach (var variable in variableDeclarationSyntax.Variables)
            {
                if (variable.IsMissing)
                    continue;
                var localdeclaration = new LocalVariableDeclaration();

                localdeclaration.Name = variable.Identifier.ValueText;
                localdeclaration.Type = type;

                scopedLocalVariableDeclarations.Add(localdeclaration);
                retVal.Add(localdeclaration);

                //assignment
                if (variable.Initializer != null)
                {
                    var statement = statementInterpreterHandler.GetStatement(variable.Initializer);

                    retVal.Add(statement);
                }
            }

            return new MultiStatement
            {
                Statements = retVal
            };
        }
    }
}