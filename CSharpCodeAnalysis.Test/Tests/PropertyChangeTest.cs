using FluentAssertions;
using NUnit.Framework;

namespace CSharpCodeAnalysis.Test
{
    [TestFixture]
    public class PropertyChangeTest
    {
        [Test]
        public void Test_NewPropertyAdded()
        {
            var newSyntaxTree = Helper.GetSyntaxTree("Test_NewPropertyAdded");
            var oldSyntaxTree = Helper.GetSyntaxTree("Test_NewPropertyAdded.Old");

            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, oldSyntaxTree);

            codeChangesVisitor.UpdatedMethods.Count.Should().Be(0);
            codeChangesVisitor.NewClassses.Count.Should().Be(0);
            codeChangesVisitor.NewFields.Count.Should().Be(0);
            codeChangesVisitor.NewProperties.Count.Should().Be(1);
        }
    }
}
