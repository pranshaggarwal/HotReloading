using System;
using FluentAssertions;
using NUnit.Framework;

namespace CSharpCodeAnalysis.Test
{
    [TestFixture]
    public class MethodChangeTest
    {
        [Test]
        public void Test_NewMethodAddedToClass()
        {
            var newSyntaxTree = Helper.GetSyntaxTree("Test_NewMethodAddedToClass");
            var oldSyntaxTree = Helper.GetSyntaxTree("Test_NewMethodAddedToClass.Old");

            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, oldSyntaxTree);

            codeChangesVisitor.UpdatedMethods.Count.Should().Be(1);
            codeChangesVisitor.NewClassses.Count.Should().Be(0);
        }

        [Test]
        public void Test_MethodUpdate_NewLine()
        {
            var newSyntaxTree = Helper.GetSyntaxTree("Test_MethodUpdate_NewLine");
            var oldSyntaxTree = Helper.GetSyntaxTree("Test_MethodUpdate_NewLine.Old");

            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, oldSyntaxTree);

            codeChangesVisitor.UpdatedMethods.Count.Should().Be(1);
            codeChangesVisitor.NewClassses.Count.Should().Be(0);
        }

        [Test]
        public void Test_MethodUpdate_UpdateLine()
        {
            var newSyntaxTree = Helper.GetSyntaxTree("Test_MethodUpdate_UpdateLine");
            var oldSyntaxTree = Helper.GetSyntaxTree("Test_MethodUpdate_UpdateLine.Old");

            var codeChangesVisitor = new CodeChangesVisitor();

            codeChangesVisitor.Visit(newSyntaxTree, oldSyntaxTree);

            codeChangesVisitor.UpdatedMethods.Count.Should().Be(1);
            codeChangesVisitor.NewClassses.Count.Should().Be(0);
        }
    }
}
