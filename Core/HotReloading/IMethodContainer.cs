using System;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;

namespace HotReloading
{
    public interface IMethodContainer
    {
        Method Method { get; set; }
        CSharpLamdaExpression GetExpression();
        Delegate GetDelegate();
    }
}