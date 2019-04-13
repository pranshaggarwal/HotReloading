using Xunit;

namespace HotReloading.BuildTask.Test
{
    public class MethodInjectorTest
    {
        public MethodInjectorTest()
        {
            methodInjectorTask = new MethodInjector(new TestLogger());
            methodInjectorTask.ProjectDirectory = "/Users/pranshuaggarwal/Xenolt/HotReloading/Build/BuildSample";
            methodInjectorTask.AssemblyFile = "obj/Debug/netstandard2.0/BuildSample.dll";
        }

        private readonly MethodInjector methodInjectorTask;

        [Fact]
        public void Execute_Test()
        {
            methodInjectorTask.Execute();
        }
    }
}