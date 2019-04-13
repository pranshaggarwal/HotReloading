using FluentAssertions;
using NUnit.Framework;

namespace CSharpCodeAnalysis.Test
{
    [TestFixture]
    public class FieldChangeTest
    {
        [Test]
        public void Test_NewFieldAdded()
        {
            var newSyntaxTree = Helper.GetSyntaxTree("Test_NewFieldAdded");
            var oldSyntaxTree = Helper.GetSyntaxTree("Test_NewFieldAdded.Old");

            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, oldSyntaxTree);

            codeChangesVisitor.UpdatedMethods.Count.Should().Be(0);
            codeChangesVisitor.NewClassses.Count.Should().Be(0);
            codeChangesVisitor.NewFields.Count.Should().Be(1);
        }
    }
}
