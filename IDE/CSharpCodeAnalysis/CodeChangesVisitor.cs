using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpCodeAnalysis
{
    /// <summary>
    ///     Compare two syntax tree for changes in methods and classes
    /// </summary>
    public class CodeChangesVisitor : CSharpSyntaxWalker
    {
        private bool newNodeVisiting;
        private readonly List<ClassDeclarationSyntax> oldClassDeclarationSynctaxes;
        private readonly List<MethodDeclarationSyntax> oldMethods;
        private readonly List<FieldDeclarationSyntax> oldFields;
        private readonly List<PropertyDeclarationSyntax> oldProperties;

        public CodeChangesVisitor()
        {
            NewClassses = new List<ClassDeclarationSyntax>();
            oldClassDeclarationSynctaxes = new List<ClassDeclarationSyntax>();
            oldMethods = new List<MethodDeclarationSyntax>();
            oldFields = new List<FieldDeclarationSyntax>();
            oldProperties = new List<PropertyDeclarationSyntax>();
            UpdatedMethods = new List<MethodDeclarationSyntax>();
            NewFields = new List<VariableDeclaratorSyntax>();
            NewProperties = new List<PropertyDeclarationSyntax>();
        }

        public List<ClassDeclarationSyntax> NewClassses { get; set; }
        public List<MethodDeclarationSyntax> UpdatedMethods { get; set; }
        public List<VariableDeclaratorSyntax> NewFields { get; set; }
        public List<PropertyDeclarationSyntax> NewProperties { get; set; }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            if (!newNodeVisiting)
            {
                oldClassDeclarationSynctaxes.Add(node);
            }
            else
            {
                if (!oldClassDeclarationSynctaxes.Any(x => x.Identifier.Text == node.Identifier.Text))
                    NewClassses.Add(node);
            }

            base.VisitClassDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (!newNodeVisiting)
            {
                oldMethods.Add(node);
            }
            else
            {
                if (!oldMethods.Any(x => IsSameMethod(node, x)))
                {
                    UpdatedMethods.Add(node);
                }
                else
                {
                    var oldMethod = oldMethods.FirstOrDefault(x => IsSameMethod(node, x));

                    if (oldMethod.ToFullString().Trim() != node.ToFullString().Trim())
                        UpdatedMethods.Add(node);
                }
            }

            base.VisitMethodDeclaration(node);
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            if (!newNodeVisiting)
            {
                oldFields.Add(node);
            }
            else
            {
                foreach(var variable in node.Declaration.Variables)
                {
                    bool alreadyExists = false;
                    foreach(var oldVariable in oldFields.SelectMany(x => x.Declaration.Variables))
                    {
                        if (oldVariable.Identifier.Text == variable.Identifier.Text)
                            alreadyExists = true;
                    }
                    if(!alreadyExists)
                        NewFields.Add(variable);
                }
            }
            base.VisitFieldDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            if (!newNodeVisiting)
            {
                oldProperties.Add(node);
            }
            else
            {
                if (!oldProperties.Any(x => IsSameProperty(node, x)))
                {
                    NewProperties.Add(node);
                }
            }
            base.VisitPropertyDeclaration(node);
        }

        private static bool IsSameMethod(MethodDeclarationSyntax node1, MethodDeclarationSyntax node2)
        {
            return IdentifierMatched(node1, node2) &&
                   IdentifierMatched(node1.Parent as ClassDeclarationSyntax, node2.Parent as ClassDeclarationSyntax);
        }

        private static bool IsSameProperty(PropertyDeclarationSyntax node1, PropertyDeclarationSyntax node2)
        {
            return IdentifierMatched(node1, node2) &&
                   IdentifierMatched(node1.Parent as ClassDeclarationSyntax, node2.Parent as ClassDeclarationSyntax);
        }

        public void Visit(SyntaxTree newSyntaxTree, SyntaxTree oldSyntaxTree)
        {
            if(oldSyntaxTree != null) Visit(oldSyntaxTree.GetRoot());
            newNodeVisiting = true;
            Visit(newSyntaxTree.GetRoot());
        }

        private static bool IdentifierMatched(ClassDeclarationSyntax node1, ClassDeclarationSyntax node2)
        {
            return node1.Identifier.Text == node2.Identifier.Text;
        }

        private static bool IdentifierMatched(MethodDeclarationSyntax node1, MethodDeclarationSyntax node2)
        {
            return node1.Identifier.Text == node2.Identifier.Text;
        }

        private static bool IdentifierMatched(PropertyDeclarationSyntax node1, PropertyDeclarationSyntax node2)
        {
            return node1.Identifier.Text == node2.Identifier.Text;
        }
    }
}
