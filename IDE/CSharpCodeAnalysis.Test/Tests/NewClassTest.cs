using FluentAssertions;
using NUnit.Framework;

namespace CSharpCodeAnalysis.Test
{
    [TestFixture]
    public class NewClassTest
    {
        [Test]
        public void Test_NewClassAddedToExistingFile()
        {
            var newSyntaxTree = Helper.GetSyntaxTree("Test_NewClassAddedToExistingFile");
            var oldSyntaxTree = Helper.GetSyntaxTree("Test_NewClassAddedToExistingFile.Old");

            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, oldSyntaxTree);

            codeChangesVisitor.UpdatedMethods.Count.Should().Be(0);
            codeChangesVisitor.NewClassses.Count.Should().Be(1);
        }

        [Test]
        public void Test_NewClassAddedToNewFile()
        {
            var newSyntaxTree = Helper.GetSyntaxTree("Test_NewClassAddedToNewFile");

            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, null);

            codeChangesVisitor.UpdatedMethods.Count.Should().Be(0);
            codeChangesVisitor.NewClassses.Count.Should().Be(1);
        }
    }
}
