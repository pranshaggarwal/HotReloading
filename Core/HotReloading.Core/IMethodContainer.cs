using System;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Syntax;

namespace HotReloading.Core
{
    public interface IMethodContainer
    {
        Method Method { get; set; }
        CSharpLamdaExpression GetExpression();
        Delegate GetDelegate();
    }
}