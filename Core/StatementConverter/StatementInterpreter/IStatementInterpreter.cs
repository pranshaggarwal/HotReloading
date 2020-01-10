using HotReloading.Syntax.Statements;

namespace StatementConverter.StatementInterpreter
{
    public interface IStatementInterpreter
    {
        IStatementCSharpSyntax GetStatement();
    }
}