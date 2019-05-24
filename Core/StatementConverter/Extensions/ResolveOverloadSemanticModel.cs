using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace StatementConverter.Extensions
{
    public static class ResolveOverloadSemanticModel
    {
        public static ISymbol ResolveOverload(this SemanticModel semanticModel, SymbolInfo symbolInfo, SyntaxNode syntaxNode)
        {
            var notMethodSymbol = symbolInfo.CandidateSymbols.FirstOrDefault(x => !(x is IMethodSymbol));
            if (notMethodSymbol != null)
            {
                throw new Exception("Cannot resolve overrload candidate for: " + notMethodSymbol.GetType());
            }
            else
            {
                if (syntaxNode.Parent is InvocationExpressionSyntax invocationExpressionSyntax)
                {
                    var arguments = invocationExpressionSyntax.ArgumentList.Arguments;
                    var candidates = new List<IMethodSymbol>();
                    foreach (var method in symbolInfo.CandidateSymbols.OfType<IMethodSymbol>())
                    {
                        if (method.Parameters.Length < arguments.Count)
                            continue;
                        var newArguments = new ArgumentSyntax[method.Parameters.Length];

                        foreach (var arg in arguments)
                        {
                            if (arg.NameColon != null)
                            {
                                var anyParameter = method.Parameters.FirstOrDefault(x => x.Name == arg.NameColon.Name.Identifier.ValueText);
                                if (anyParameter != null)
                                {
                                    newArguments[method.Parameters.IndexOf(anyParameter)] = arg;
                                }
                            }
                            else
                            {
                                newArguments[arguments.IndexOf(arg)] = arg;
                            }
                        }

                        bool isMatch = true;

                        for (int i = 0; i < newArguments.Length; i++)
                        {
                            var argument = newArguments[i];
                            if (argument == null)
                            {
                                if (!method.Parameters[i].IsOptional)
                                {
                                    isMatch = false;
                                    break;
                                }
                            }
                            else
                            {
                                var argumentTypeInfo = semanticModel.GetTypeInfo(argument.Expression);
                                var type = argumentTypeInfo.Type ?? argumentTypeInfo.ConvertedType;
                                if (!type.IsAssignableTo(method.Parameters[i].Type))
                                {
                                    isMatch = false;
                                    break;
                                }
                            }
                        }

                        if (isMatch)
                            candidates.Add(method);
                    }
                    if (candidates.Count != 1)
                    {
                        candidates.Sort((item1, item2) =>
                        {
                            for(int i = 0; i < item1.Parameters.Length; i++)
                            {
                                if(item1.Parameters[i].Type.IsSubClassOf(item2.Parameters[i].Type))
                                {
                                    return 1;
                                }
                            }
                            return -1;
                        });

                        return candidates.LastOrDefault();
                    }
                    else
                        return candidates.FirstOrDefault();
                }
            }

            throw new Exception("Unable to resolve overload candidate for: " + syntaxNode.GetType());
        }
    }
}