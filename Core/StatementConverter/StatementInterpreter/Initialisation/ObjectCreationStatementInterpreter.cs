using System.Collections.Generic;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class ObjectCreationStatementInterpreter : IStatementInterpreter
    {
        private readonly ObjectCreationExpressionSyntax objectCreationExpressionSyntax;
        private readonly StatementInterpreterHandler statementInterpreterHandler;
        private readonly SemanticModel semanticModel;

        public ObjectCreationStatementInterpreter(StatementInterpreterHandler statementInterpreterHandler,
            SemanticModel semanticModel,
            ObjectCreationExpressionSyntax objectCreationExpressionSyntax)
        {
            this.statementInterpreterHandler = statementInterpreterHandler;
            this.semanticModel = semanticModel;
            this.objectCreationExpressionSyntax = objectCreationExpressionSyntax;
        }

        public Statement GetStatement()
        {
            var typeInfo = semanticModel.GetTypeInfo(objectCreationExpressionSyntax);

            var objectCreationStatement = new ObjectCreationStatement
            {
                Type = typeInfo.GetClassType()
            };

            if (objectCreationExpressionSyntax.Initializer != null)
            {
                objectCreationStatement.Initializer = new List<Statement>();

                foreach(var expression in objectCreationExpressionSyntax.Initializer.Expressions)
                {
                    var statement = statementInterpreterHandler.GetStatement(expression)
                    ;
                    objectCreationStatement.Initializer.Add(statement);
                }
            }

            if (objectCreationExpressionSyntax.ArgumentList?.Arguments != null)
            {
                var arguments = new List<Statement>();

                foreach (var argumentSyntax in objectCreationExpressionSyntax.ArgumentList.Arguments)
                {
                    arguments.Add(statementInterpreterHandler.GetStatement(argumentSyntax));
                }

                objectCreationStatement.ArgumentList = arguments;
            }

            return objectCreationStatement;
        }
    }
}