using System.Collections.Generic;

namespace HotReloading.Core.Statements
{
    public class InvocationStatement : Statement
    {
        public InvocationStatement()
        {
            Arguments = new List<Statement>();
        }

        public MethodMemberStatement Method { get; set; }
        public List<Statement> Arguments { get; set; }
    }
}