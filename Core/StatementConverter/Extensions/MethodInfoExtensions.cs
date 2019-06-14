using System.Linq.Expressions;
using System.Reflection;

namespace StatementConverter.Extensions
{
    public static class MethodInfoExtensions
    {
        public static Expression[] ConvertArguments(this MethodInfo methodInfo, Expression[] arguments)
        {
            Expression[] convertedArguments = new Expression[arguments.Length];

            for (int i = 0; i < convertedArguments.Length; i++)
            {
                var argument = arguments[i];

                var parameter = methodInfo.GetParameters()[i];

                if (parameter.ParameterType != argument.Type)
                {
                    convertedArguments[i] = Expression.Convert(argument, parameter.ParameterType);
                }
                else
                {
                    convertedArguments[i] = argument;
                }
            }

            return convertedArguments;
        }
    }
}
