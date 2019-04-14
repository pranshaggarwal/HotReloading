using System;
namespace StatementConverter.Test
{
    public partial class InvocationTestClass
    {
        private string field;

        private string Field
        {
            get => field;
        }

        public static void InvokeOtherClassStaticMethod()
        {
            Tracker.Call();
        }

        public static void InvokeStaticMethodWithArrowFunction() => Tracker.Call();

        public static void InvokeStaticMethodWithConstantArgument()
        {
            Tracker.Call("hello");
        }

        public static void InvokeStaticMethodWithAddExpression()
        {
            Tracker.Call(5 + 5);
        }

        public static void InvokeStaticMethodWithParameterArgument(string test)
        {
            Tracker.Call(test);
        }

        public static void InokeStaticMethodWithLocalVariableArgument()
        {
            string test = "hello";
            Tracker.Call(test);
        }

        public void InokeStaticMethodWithFieldArgument()
        {
            Tracker.Call(field);
        }

        public void InokeStaticMethodWithPropertyArgument()
        {
            Tracker.Call(Field);
        }

        public static void InvokeOtherClassInstanceMethod()
        {
            TestInstanceMemberClass testInstance = new TestInstanceMemberClass();
            testInstance.Test();
        }

        public static void InvokeStaticMethod()
        {
            StaticMemberMethod();
        }

        public static void StaticMemberMethod()
        {
            Tracker.Call();
        }

        public void InvokeInstanceMethod()
        {
            InstanceMemberMethod();
        }

        public void InvokeInstanceMethodWithThisKeyword()
        {
            this.InstanceMemberMethod();
        }

        public void InvokeOptionalParameterMethod1()
        {
            OptionalParameterMethod1();
        }

        public void OptionalParameterMethod1(string str = null)
        {
        }

        public void InvokeOptionalParameterMethod2()
        {
            OptionalParameterMethod2();
        }

        public void OptionalParameterMethod2(string str = "default")
        {
        }

        public void InvokeOptionalNamedParameterMethod()
        {
            OptionalNamedParameterMethod(i: 1, str: "hello");
        }

        public void OptionalNamedParameterMethod(string str = "", int i = 0)
        {
        }

        public void InstanceMemberMethod()
        {
            Tracker.Call(field);
        }

        public void InvokeInternalClassMember()
        {
            InternalClassTestClass.Internal.Call();
        }

        public void InvokeMemberMethod()
        {
            var testClass = new TestInstanceMemberClass();
            testClass.Object.Call();
        }

        public void InvokeChainMethod5Times()
        {
            var chainClass = new ChainingTestClass();
            chainClass.Chain().Chain().Chain()
                .Chain().Chain();
        }

        public void InvokeMethodWithNamespace()
        {
            StatementConverter.Test.Tracker.Call("hello");
        }

        public string ReturnValue()
        {
            return "hello";
        }
    }
}
