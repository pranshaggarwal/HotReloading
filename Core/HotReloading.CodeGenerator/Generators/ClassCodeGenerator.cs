using System.Text;
using HotReloading.CodeGenerator.Extensions;
using HotReloading.Syntax;

namespace HotReloading.CodeGenerator.Generators
{
    public class ClassCodeGenerator : ICSharpCodeGenerator
    {
        public string Generate(ICSharpSyntax cSharpSyntax)
        {
            var @class = (Class)cSharpSyntax;

            var classStrBuilder = new StringBuilder();
            classStrBuilder.Append(@class.AccessModifier.GenerateCode());

            if (@class.IsStatic)
                classStrBuilder.Append(" static");

            if (@class.IsSealed)
                classStrBuilder.Append(" sealed");

            if (@class.IsAbstract)
                classStrBuilder.Append(" abstract");

            classStrBuilder.Append(" class ");

            classStrBuilder.Append(@class.Name);


            classStrBuilder.Append("\n{");


            bool isTheirAnyMember = false;

            foreach (var field in @class.Fields)
            {
                if (isTheirAnyMember)
                    classStrBuilder.Append("\n\t");
                isTheirAnyMember = true;

                var fieldCodeGenerator = CodeGeneratorFactory.Create(field);

                classStrBuilder.Append("\n\t" + fieldCodeGenerator.Generate(field));
            }

            foreach (var property in @class.Properties)
            {
                if (isTheirAnyMember)
                    classStrBuilder.Append("\n\t");
                isTheirAnyMember = true;

                var propertyCodeGenerator = CodeGeneratorFactory.Create(property);

                classStrBuilder.Append("\n\t" + propertyCodeGenerator.Generate(property));
            }

            foreach (var method in @class.Methods)
            {
                if (isTheirAnyMember)
                    classStrBuilder.Append("\n\t");
                isTheirAnyMember = true;

                var methodCodeGenerator = CodeGeneratorFactory.Create(method);

                classStrBuilder.Append("\n\t" + methodCodeGenerator.Generate(method).Replace("\n", "\n\t"));
            }

            classStrBuilder.Append("\n}");

            return classStrBuilder.ToString();
        }
    }
}
