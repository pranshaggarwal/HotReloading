using System;
using System.Collections.Generic;
using System.Linq;
using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using StatementConverter.Extensions;

namespace StatementConverter.StatementInterpreter
{
    public class ParameterInterpreter : IStatementInterpreter
    {
        private readonly MethodDeclarationSyntax methodDeclarationSyntax;
        private readonly ParameterSyntax parameterSyntax;
        private readonly SemanticModel semanticModel;

        public ParameterInterpreter(ParameterSyntax parameterSyntax, SemanticModel semanticModel)
        {
            this.parameterSyntax = parameterSyntax;
            this.semanticModel = semanticModel;
        }

        public Statement GetStatement()
        {
            var parameterName = parameterSyntax.Identifier.Text;
            ClassType classType = null;
            if(parameterSyntax.Type != null)
            {
                var typeInfo = semanticModel.GetTypeInfo(parameterSyntax.Type);
                classType = typeInfo.GetClassType();
            }
            else
            {
                //For lamda expression
                var lamdaExpressionSymbolInfo = semanticModel.GetSymbolInfo(parameterSyntax.Parent.Parent);
                if(lamdaExpressionSymbolInfo.Symbol is IMethodSymbol ms)
                {
                    classType = ms.Parameters.First(x => x.Name == parameterName).Type.GetClassType();
                }
                else
                {
                    throw new Exception("Unable to find type for parameter: " + parameterName);
                }
            }

            return new Parameter
            {
                Name = parameterName,
                Type =  classType
            };
        }
    }
}