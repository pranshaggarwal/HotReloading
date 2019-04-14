using System;
using System.Linq.Expressions;
using Microsoft.CSharp.Expressions;

namespace CSharpExpressions.Microsoft.CSharp
{
    public class CSharpLamdaExpression
    {
        private readonly LambdaExpression lambdaExpression;
        private readonly AsyncLambdaCSharpExpression asyncLambdaCSharpExpression;

        public CSharpLamdaExpression(LambdaExpression lambdaExpression)
        {
            this.lambdaExpression = lambdaExpression;
        }

        public CSharpLamdaExpression(AsyncLambdaCSharpExpression asyncLambdaCSharpExpression)
        {
            this.asyncLambdaCSharpExpression = asyncLambdaCSharpExpression;
        }

        public Delegate Compile()
        {
            if (lambdaExpression != null)
                return lambdaExpression.Compile();
            else
                return asyncLambdaCSharpExpression.Compile();
        }
    }
}
