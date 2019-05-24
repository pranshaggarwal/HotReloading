using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Cecil = Mono.Cecil;
using BindingFlags = System.Reflection.BindingFlags;
using Reflection = System.Reflection;

namespace ILDynaRec
{
    public class HotPatcher
    {
        Recompiler recompiler = new Recompiler();

        public HotPatcher() { }

        Cecil.TypeDefinition FindType(Cecil.AssemblyDefinition asm, string name) {
            return asm.MainModule.Types.FirstOrDefault(type => type.Name == name);
        }

        IEnumerable<Cecil.MethodDefinition> IterateMethods(Cecil.AssemblyDefinition asm) {
            foreach (var module in asm.Modules) {
                foreach (var type in module.Types) {
                    foreach (var method in type.Methods) {
                        if (!method.HasBody) {
                            continue;
                        }

                        yield return method;
                    }
                }
            }
        }

        int GenBodyHashcode(Cecil.MethodDefinition method) {
            return ConcatBody(method).GetHashCode();
        }

        /// <summary>
        /// Generate "position independent" representation of IL.
        /// 
        /// We need to do this extra work because even if we skip 
        /// instrumentation prologue of functions when iterating
        /// instructions, typical "toString()" representation of 
        /// Cecil.Cil.Instructions contains absolute instruction
        /// positions.
        /// </summary>
        string ConcatBody(Cecil.MethodDefinition method) {
            const string skipAddr = "IL_0000: ";

            var sb = new System.Text.StringBuilder();
            foreach (var ins in Instrument.IterateInstructions(method)) {
                if (ins.Operand is Cecil.Cil.Instruction) {
                    // instructions that refer to other instructions, like
                    // branches.

                    var insOperand = (Cecil.Cil.Instruction)(ins.Operand);
                    var offset = insOperand.Offset - ins.Offset;

                    sb.AppendLine(String.Format("{0} {1}", ins.OpCode, offset));
                }
                else {
                    sb.AppendLine(ins.ToString().Substring(skipAddr.Length));
                }
            }
            return sb.ToString();
        }

        class LocalMethod {
            public int BodyHashCode;
            public string BodyString;
            public Reflection.MethodBase Method;
            public Type Type;

            public Cecil.MethodDefinition CurrentMethodDef;
            public Reflection.Emit.DynamicMethod CurrentSwap;
        }

        Dictionary<string, LocalMethod> localMethods = new Dictionary<string, LocalMethod>();

        public void LoadLocalAssembly(string assemblyFilename) {
            var currentAssembly = Cecil.AssemblyDefinition.ReadAssembly(assemblyFilename);
            foreach (var method in IterateMethods(currentAssembly)) {
                var lm = new LocalMethod();
                lm.CurrentMethodDef = method;
                lm.CurrentSwap = null;
                lm.BodyString = ConcatBody(method);
                lm.BodyHashCode = lm.BodyString.GetHashCode();

                try {
                    var localType = recompiler.FindType(method.DeclaringType);
                    lm.Type = localType;
                    if (localType != null) {
                        lm.Method = recompiler.FindMethod(localType, method);
                    }

                    localMethods[method.FullName] = lm;

                    Debug.Trace($"Loading method: {method.FullName}");
                } catch (Exception e) {
                    Debug.Trace($"Failed to load method: {method.FullName}");
                }
            }
        }

        public void HotPatch(string assemblyFilename) {
            Debug.Trace("Started hotpatching {0}", assemblyFilename);

            var newAssembly = Cecil.AssemblyDefinition.ReadAssembly(assemblyFilename);

            foreach (var method in IterateMethods(newAssembly)) {
                Debug.Trace($"Searching for {method.FullName}");
                LocalMethod localMethod;
                if (!localMethods.TryGetValue(method.FullName, out localMethod)) {
                    Debug.Trace($"Did not find loaded method {method.FullName}. New method or a bug.");
                    continue;
                }

                string newBody = ConcatBody(method);
                int newBodyHash = newBody.GetHashCode();
                if (localMethod.BodyHashCode == newBody.GetHashCode()) {
                    Debug.Trace($"Found {method.FullName}, but hash code didn't change.");
                    continue;
                }

                string oldBody = localMethod.BodyString;

                Debug.Log($"<i>Trying to hotpatch {method.FullName}...</i>");

                if(localMethod.Method == null) {
                    Debug.Log($"Can't hotpatch {method.FullName} - local method not found,");
                    continue;
                }

                Debug.Trace("----------");

                Debug.Trace("Hotswapping {0}", method.FullName);
                Debug.Trace("Method body hashcode was: {0}, is: {1}", localMethod.BodyHashCode, newBodyHash);

                Debug.Trace("Method body was:");
                Debug.Trace(oldBody);

                Debug.Trace("Method body is:");
                Debug.Trace(newBody);

                try {
                    var dynmethod = recompiler.RecompileMethod(method);
                    if (dynmethod != null) {
                        SwapMethod(localMethod.Method, dynmethod);

                        localMethod.CurrentSwap = dynmethod;
                        localMethod.BodyString = newBody;
                        localMethod.BodyHashCode = newBodyHash;
                    }
                }
                catch (Exception e) {
                    Debug.LogWarningFormat("Failed to patch {0}. <i>See full stacktrace in Temp/hotpatch.log.</i>\nError is: {1}",
                        method.FullName, e.Message);
                    Debug.Trace(e.StackTrace);
                }
                finally {
                    Debug.Trace("----------");
                }
            }
        }

        private static RuntimeMethodHandle GetDynamicHandle(Reflection.Emit.DynamicMethod dynamicMethod) {
            // MS API
            var descr = typeof(Reflection.Emit.DynamicMethod)
                .GetMethod("GetMethodDescriptor", BindingFlags.Instance | BindingFlags.NonPublic);
            if (descr != null) {
                var res = (RuntimeMethodHandle)descr.Invoke(dynamicMethod, null);
                RuntimeHelpers.PrepareMethod(res);
                return res;
            }

            // Mono API
            var descr2 = typeof(Reflection.Emit.DynamicMethod)
                .GetMethod("CreateDynMethod", BindingFlags.Instance | BindingFlags.NonPublic);
            if(descr2 != null) {
                descr2.Invoke(dynamicMethod, null);
                var res = dynamicMethod.MethodHandle;
                RuntimeHelpers.PrepareMethod(res);
                return res;
            }

            {
                // If everything else fails, force method compilation by creating a delegate of dynamic method.
                // TODO: We have to call with proper delegate, not just Action<>
                var method2 = dynamicMethod.CreateDelegate(typeof(Action)).Method;
                var res = method2.MethodHandle;
                RuntimeHelpers.PrepareMethod(res);
                return res;
            }
        }

        public static void TestPrepareMethod(Reflection.Emit.DynamicMethod method) {
            var borrowed = GetDynamicHandle(method);
            IntPtr pBorrowed = borrowed.GetFunctionPointer();
        }

        private void SwapMethod(Reflection.MethodBase method, Reflection.Emit.DynamicMethod replacement) {
            RuntimeHelpers.PrepareMethod(method.MethodHandle);
            IntPtr pBody = method.MethodHandle.GetFunctionPointer();

            var borrowed = GetDynamicHandle(replacement);
            IntPtr pBorrowed = borrowed.GetFunctionPointer();

            Debug.Trace($"Is 64bit: {Environment.Is64BitProcess}");

            unsafe {
                var ptr = (byte*)pBody.ToPointer();
                var ptr2 = (byte*)pBorrowed.ToPointer();
                var ptrDiff = ptr2 - ptr - 5;
                if (ptrDiff < (long)0xFFFFFFFF && ptrDiff > (long)-0xFFFFFFFF) {
                    // 32-bit relative jump, available on both 32 and 64 bit arch.
                    Debug.Trace($"diff is {ptrDiff} doing relative jmp");
                    Debug.Trace("patching on {0:X}, target: {1:X}", (ulong)ptr, (ulong)ptr2);
                    *ptr = 0xe9; // JMP
                    *((uint*)(ptr + 1)) = (uint)ptrDiff;
                }
                else {
                    Debug.Trace($"diff is {ptrDiff} doing push+ret trampoline");
                    Debug.Trace("patching on {0:X}, target: {1:X}", (ulong)ptr, (ulong)ptr2);
                    if (Environment.Is64BitProcess) {
                        // For 64bit arch and likely 64bit pointers, do:
                        // PUSH bits 0 - 32 of addr
                        // MOV [RSP+4] bits 32 - 64 of addr
                        // RET
                        var cursor = ptr;
                        *(cursor++) = 0x68; // PUSH
                        *((uint*)cursor) = (uint)ptr2;
                        cursor += 4;
                        *(cursor++) = 0xC7; // MOV [RSP+4]
                        *(cursor++) = 0x44;
                        *(cursor++) = 0x24;
                        *(cursor++) = 0x04;
                        *((uint*)cursor) = (uint)((ulong)ptr2 >> 32);
                        cursor += 4;
                        *(cursor++) = 0xc3; // RET
                    }
                    else {
                        // For 32bit arch and 32bit pointers, do: PUSH addr, RET.
                        *ptr = 0x68;
                        *((uint*)(ptr + 1)) = (uint)ptr2;
                        *(ptr + 5) = 0xC3;
                    }
                }

                Debug.LogFormat("Patched 0x{0:X} to 0x{1:X}.", (ulong)ptr, (ulong)ptr2);
            }
        }
    }
}

