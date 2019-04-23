using System;
using CSharpExpressions.Microsoft.CSharp;

namespace HotReloading.Core
{
    public interface IMethodContainer
    {
        Method Method { get; set; }
        CSharpLamdaExpression GetExpression();
        Delegate GetDelegate();
    }
}