using HotReloading.Syntax;

namespace HotReloading.Core.Statements
{
    public class StaticFieldMemberStatement : MemberAccessStatement
    {
        public HrType ParentType { get; set; }
        public string Name { get; set; }
    }
}