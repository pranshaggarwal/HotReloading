using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using ILVerify;
using Internal.TypeSystem.Ecma;
using Xunit;

namespace HotReloading.BuildTask.Test
{
    public class MethodInjectorTest : ILVerify.ResolverBase
    {
        private Verifier _verifier;
        private string projectPath;
        private string filePath;

        public MethodInjectorTest()
        {
             projectPath = "/Users/pranshuaggarwal/Xenolt/HotReloading/Build/BuildSample";
             filePath = "obj/Debug/netstandard2.0/BuildSample.dll";
        }



        [Fact]
        public void Execute_Test()
        {
            _verifier = new Verifier(this);

            var path = Path.Combine(projectPath, filePath);


            var test = new AssemblyName("mscorlib");
            _verifier.SetSystemModuleName(test);
            VerifyAssembly(path);
        }

        protected override PEReader ResolveCore(string simpleName)
        {
            return new PEReader(File.OpenRead(simpleName));
        }

        private void VerifyAssembly(string path)
        {
            PEReader peReader = Resolve(path);

            var getModuleMethod = typeof(Verifier).GetMethods();

            //EcmaModule module = _verifier.GetModule(peReader);
            //EcmaModule module = (EcmaModule)getModuleMethod[0].Invoke(_verifier, new PEReader[] { peReader });

            VerifyAssembly(peReader, null, path);
        }

        private void VerifyAssembly(PEReader peReader, EcmaModule module, string path)
        {
            int numErrors = 0;
            int verifiedMethodCounter = 0;
            int methodCounter = 0;
            int verifiedTypeCounter = 0;
            int typeCounter = 0;

            VerifyMethods(peReader, module, path, ref numErrors, ref verifiedMethodCounter, ref methodCounter);
            //VerifyTypes(peReader, module, path, ref numErrors, ref verifiedTypeCounter, ref typeCounter);

            if (numErrors > 0)
                WriteLine(numErrors + " Error(s) Verifying " + path);
            else
                WriteLine("All Classes and Methods in " + path + " Verified.");

            WriteLine($"Types found: {typeCounter}");
            WriteLine($"Types verified: {verifiedTypeCounter}");

            WriteLine($"Methods found: {methodCounter}");
            WriteLine($"Methods verified: {verifiedMethodCounter}");
        }

        private void VerifyMethods(PEReader peReader, EcmaModule module, string path, ref int numErrors, ref int verifiedMethodCounter, ref int methodCounter)
        {
            numErrors = 0;
            verifiedMethodCounter = 0;
            methodCounter = 0;

            MetadataReader metadataReader = peReader.GetMetadataReader();
            foreach (var methodHandle in metadataReader.MethodDefinitions)
            {
                // get fully qualified method name
                var methodName = GetQualifiedMethodName(metadataReader, methodHandle);

                if (!methodName.Contains("UpdateCornerRadius"))
                    continue;

                bool verifying = true;
                    WriteLine(verifying ? "Verifying " : "Skipping ");
                    WriteLine(methodName);

                if (verifying)
                {
                    var results = _verifier.Verify(peReader, methodHandle);
                    foreach (var result in results)
                    {
                        PrintVerifyMethodsResult(result, path);
                        numErrors++;
                    }

                    verifiedMethodCounter++;
                }

                methodCounter++;
            }
        }

        private void VerifyTypes(PEReader peReader, EcmaModule module, string path, ref int numErrors, ref int verifiedTypeCounter, ref int typeCounter)
        {
            MetadataReader metadataReader = peReader.GetMetadataReader();

            foreach (TypeDefinitionHandle typeHandle in metadataReader.TypeDefinitions)
            {
                // get fully qualified type name
                var className = GetQualifiedClassName(metadataReader, typeHandle);
                bool verifying = true;
                    WriteLine(verifying ? "Verifying " : "Skipping ");
                    WriteLine(className);
                if (verifying)
                {
                    var results = _verifier.Verify(peReader, typeHandle);
                    foreach (VerificationResult result in results)
                    {
                        System.Diagnostics.Debug.WriteLine(result.Message);
                        numErrors++;
                    }

                    typeCounter++;
                }

                verifiedTypeCounter++;
            }
        }

        private string GetQualifiedClassName(MetadataReader metadataReader, TypeDefinitionHandle typeHandle)
        {
            var typeDef = metadataReader.GetTypeDefinition(typeHandle);
            var typeName = metadataReader.GetString(typeDef.Name);

            var namespaceName = metadataReader.GetString(typeDef.Namespace);
            var assemblyName = metadataReader.GetString(metadataReader.IsAssembly ? metadataReader.GetAssemblyDefinition().Name : metadataReader.GetModuleDefinition().Name);

            StringBuilder builder = new StringBuilder();
            builder.Append($"[{assemblyName}]");
            if (!string.IsNullOrEmpty(namespaceName))
                builder.Append($"{namespaceName}.");
            builder.Append($"{typeName}");

            return builder.ToString();
        }

        private string GetQualifiedMethodName(MetadataReader metadataReader, MethodDefinitionHandle methodHandle)
        {
            var methodDef = metadataReader.GetMethodDefinition(methodHandle);
            var typeDef = metadataReader.GetTypeDefinition(methodDef.GetDeclaringType());

            var methodName = metadataReader.GetString(metadataReader.GetMethodDefinition(methodHandle).Name);
            var typeName = metadataReader.GetString(typeDef.Name);
            var namespaceName = metadataReader.GetString(typeDef.Namespace);
            var assemblyName = metadataReader.GetString(metadataReader.IsAssembly ? metadataReader.GetAssemblyDefinition().Name : metadataReader.GetModuleDefinition().Name);

            StringBuilder builder = new StringBuilder();
            builder.Append($"[{assemblyName}]");
            if (!string.IsNullOrEmpty(namespaceName))
                builder.Append($"{namespaceName}.");
            builder.Append($"{typeName}.{methodName}");

            return builder.ToString();
        }

        private static void PrintMethod(EcmaMethod method)
        {
            Write(method.Name);
            Write("(");

            if (method.Signature.Length > 0)
            {
                bool first = true;
                for (int i = 0; i < method.Signature.Length; i++)
                {
                    Internal.TypeSystem.TypeDesc parameter = method.Signature[0];
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        Write(", ");
                    }

                    Write(parameter.ToString());
                }
            }

            Write(")");
        }

        private void PrintVerifyMethodsResult(VerificationResult result, string pathOrModuleName)
        {
            Write("[IL]: Error: ");

            Write("[");
            Write(pathOrModuleName);
            Write(" : ");

            //MetadataReader metadataReader = module.MetadataReader;

            //TypeDefinition typeDef = metadataReader.GetTypeDefinition(metadataReader.GetMethodDefinition(result.Method).GetDeclaringType());
            //string typeName = metadataReader.GetString(typeDef.Name);
            //Write(typeName);

            //Write("::");
            //var method = (EcmaMethod)module.GetMethod(result.Method);
            //PrintMethod(method);
            Write("]");

            if (result.Code != VerifierError.None)
            {
                Write("[offset 0x");
                Write(result.GetArgumentValue<int>("Offset").ToString("X8"));
                Write("]");

                if (result.TryGetArgumentValue("Found", out string found))
                {
                    Write("[found ");
                    Write(found);
                    Write("]");
                }

                if (result.TryGetArgumentValue("Expected", out string expected))
                {
                    Write("[expected ");
                    Write(expected);
                    Write("]");
                }

                if (result.TryGetArgumentValue("Token", out int token))
                {
                    Write("[token  0x");
                    Write(token.ToString("X8"));
                    Write("]");
                }
            }

            Write(" ");
            WriteLine(result.Message);
        }

        private static void Write(string message)
        {
            System.Diagnostics.Debug.Write(message);
        }

        private static void WriteLine(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}