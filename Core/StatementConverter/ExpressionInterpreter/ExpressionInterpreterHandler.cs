using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CSharpExpressions.Microsoft.CSharp;
using HotReloading.Core;
using HotReloading.Core.Statements;
using Microsoft.CSharp.Expressions;

namespace StatementConverter.ExpressionInterpreter
{
    public class ExpressionInterpreterHandler
    {
        private readonly Method method;
        private readonly ParameterExpression[] parameterExpressions;
        private readonly List<ParameterExpression> scopedLocalVariable = new List<ParameterExpression>();
        private readonly ParameterExpression thisExpression;
        private LabelTarget brk;
        private LabelTarget cont;

        public ExpressionInterpreterHandler(Method method)
        {
            this.method = method;
            parameterExpressions = GetParameterExpressions(method.Parameters);

            if (!method.IsStatic) thisExpression = Expression.Parameter(method.ParentType, "obj");
        }

        public Expression GetExpression(Statement statement)
        {
            Expression expression = null;
            switch (statement)
            {
                case Block block:
                    expression = new BlockExpressionInterpreter(this, scopedLocalVariable, block).GetExpression();
                    return expression;
                case InvocationStatement invocationStatement:
                    expression = new InvocationExpressionInterpreter(this, invocationStatement).GetExpression();
                    return expression;
                case LocalVariableDeclaration localVariableDeclaration:
                    expression = new LocalVariableDeclarationExpressionInterpreter(localVariableDeclaration).GetExpression();
                    return expression;
                case ObjectCreationStatement creationStatement:
                    expression = new ObjectCreationExpressionInterpreter(this, creationStatement).GetExpression();
                    return expression;
                case ConstantStatement constantStatement:
                    expression = new ConstantExpressionInterpreter(constantStatement).GetExpression();
                    return expression;
                case StaticFieldMemberStatement staticFieldMemberStatement:
                    expression = new StaticFieldExpressionInterpreter(staticFieldMemberStatement).GetExpression();
                    return expression;
                case InstanceFieldMemberStatement instanceFieldMemberStatement:
                    expression = new InstanceFieldExpressioInterpreter(this, instanceFieldMemberStatement).GetExpression();
                    return expression;
                case StaticPropertyMemberStatement staticPropertyMemberStatement:
                    expression = new StaticPropertyExpressionInterpreter(staticPropertyMemberStatement).GetExpression();
                    return expression;
                case InstancePropertyMemberStatement instancePropertyMemberStatement:
                    expression = new InstancePropertyExpressionInterpreter(this, instancePropertyMemberStatement).GetExpression();
                    return expression;
                case LocalIdentifierStatement localIdentifierStatement:
                    expression = new LocalIdentifierExpressionInterpreter(localIdentifierStatement, scopedLocalVariable).GetExpression();
                    return expression;
                case ParameterIdentifierStatement parameterIdentifierStatement:
                    expression = new ParameterIdentifierExpressionInterpreter(parameterIdentifierStatement,
                        parameterExpressions).GetExpression();
                    return expression;
                case ThisStatement thisStatement:
                    expression = new ThisExpressionInterpreter(thisStatement, thisExpression).GetExpression();
                    return expression;
                case BinaryStatement binaryStatement:
                    expression = new BinaryExpressionInterpreter(this, binaryStatement).GetExpression();
                    return expression;
                case ConditionalStatement conditionalStatement:
                    expression = new ConditionalExpressionInterpreter(this, conditionalStatement).GetExpression();
                    return expression;
                case IfStatement ifStatement:
                    expression = new IfExpressionInterpreter(this, ifStatement).GetExpression();
                    return expression;
                case DefaultStatement defaultStatement:
                    expression = new DefaultExpressionInterpreter(defaultStatement).GetExpression();
                    return expression;
                case IsTypeStatement isTypeStatement:
                    expression = new IsTypeExpressionInterpreter(this, isTypeStatement).GetExpression();
                    return expression;
                case AsStatement asTypeStatement:
                    expression = new AsTypeExpressionInterpreter(this, asTypeStatement).GetExpression();
                    return expression;
                case ObjectEqualStatement objectEqualStatement:
                    expression = new ObjectEqualExpressionInterpreter(this, objectEqualStatement).GetExpression();
                    return expression;
                case SwitchStatement switchStatement:
                    expression = new SwitchExpressionInterpreter(this, switchStatement).GetExpression();
                    return expression;
                case AwaitStatement awaitStatement:
                    expression = new AwaitExpressionInterpreter(this, awaitStatement).GetExpression();
                    return expression;
                case ReturnStatement returnStatement:
                    expression = new ReturnExpressionInterpreter(this, returnStatement).GetExpression();
                    return expression;
                case UnaryStatement unaryStatement:
                    expression = new UnaryExpressionInterpreter(this, unaryStatement).GetExpression();
                    return expression;
                case CastStatement castStatement:
                    expression = new CastExpressionInterpreter(this, castStatement).GetExpression();
                    return expression;
                case ArrayCreationStatement arrayCreationStatement:
                    expression = new ArrayCreationExpresstionInterpreter(this, arrayCreationStatement).GetExpression();
                    return expression;
                case ElementAccessStatement arrayAccessStatement:
                    expression = new ElementAccessExpressionInterpreter(this, arrayAccessStatement).GetExpression();
                    return expression;
                case WhileStatement whileStatement:
                    brk = Expression.Label();
                    cont = Expression.Label();
                    expression = new WhileExpressionInterpreter(this, whileStatement, brk, cont).GetExpression();
                    brk = null;
                    cont = null;
                    return expression;
                case DoWhileStatement doWhileStatement:
                    brk = Expression.Label();
                    cont = Expression.Label();
                    expression = new DoWhileExpressionInterpreter(this, doWhileStatement, brk, cont).GetExpression();
                    brk = null;
                    cont = null;
                    return expression;
                case ForStatement forStatement:
                    brk = Expression.Label();
                    cont = Expression.Label();
                    expression = new ForExpressionInterpreter(this, forStatement, brk, cont).GetExpression();
                    brk = null;
                    cont = null;
                    return expression;
                case ForEachStatement forEachStatement:
                    brk = Expression.Label();
                    cont = Expression.Label();
                    expression = new ForEachExpressionInterpreter(this, forEachStatement, scopedLocalVariable, brk, cont).GetExpression();
                    brk = null;
                    cont = null;
                    return expression;
                case BreakStatement breakStatement:
                    expression = new BreakExpressionInterpreter(brk).GetExpression();
                    return expression;
                case ContinueStatement continueStatement:
                    expression = new ContinueExpressionInterpreter(cont).GetExpression();
                    return expression;
                case UsingStatement usingStatement:
                    expression = new UsingExpressionInterpreter(this, usingStatement, scopedLocalVariable).GetExpression();
                    return expression;
                default:
                    throw new NotImplementedException(statement.GetType() + " is not supported yet.");
            }
        }

        public CSharpLamdaExpression GetLamdaExpression()
        {
            var interpreter = GetExpression(method.Block);

            var expression = interpreter;

            ParameterExpression[] parameters;

            if (method.IsStatic)
            {
                parameters = parameterExpressions;
            }
            else
            {
                var list = parameterExpressions.ToList();
                list.Insert(0, thisExpression);
                parameters = list.ToArray();
            }

            if(method.IsAsync)
                return new CSharpLamdaExpression(CSharpExpression.AsyncLambda(expression, parameters));

            return new CSharpLamdaExpression(Expression.Lambda(expression, parameters));
        }

        private ParameterExpression[] GetParameterExpressions(IEnumerable<Parameter> parameters)
        {
            return parameters.Select(x => Expression.Parameter(x.Type, x.Name)).ToArray();
        }
    }
}