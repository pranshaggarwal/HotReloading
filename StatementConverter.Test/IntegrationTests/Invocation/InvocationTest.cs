using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class InvocationTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public void WhenStaticMethodOfOtherClassCalled_MethodShouldCalled()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeOtherClassStaticMethod");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void InvokeStaticMethodWithArrowFunction()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeStaticMethodWithArrowFunction");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void WhenInstanceMethodOfOtherClassCalled()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeOtherClassInstanceMethod");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void WhenStaticMethodCalled()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeStaticMethod");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void WhenInvokeStaticMethodWithAddExpressionCalled()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeStaticMethodWithAddExpression");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void WhenInstanceMethodCalled()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeInstanceMethod");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass("hello");

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenInstanceMethodCalledWithThisKeyword()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeInstanceMethodWithThisKeyword");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass("hello");

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void InvokeOptionalParameterMethod1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeOptionalParameterMethod1");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void InvokeOptionalParameterMethod2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeOptionalParameterMethod2");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("default");
        }

        [Test]
        public void InvokeOptionalNamedParameterMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeOptionalNamedParameterMethod");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello1");
        }

        [Test]
        public void WhenInvokeStaticMethodWithConstantArgument()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeStaticMethodWithConstantArgument");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenInvokeStaticMethodWithParameterArgument()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeStaticMethodWithParameterArgument");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke("hello");

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenInokeStaticMethodWithLocalVariableArgument()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InokeStaticMethodWithLocalVariableArgument");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenInokeStaticMethodWithFieldArgument()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InokeStaticMethodWithFieldArgument");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass("hello");

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenInokeStaticMethodWithPropertyArgument()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InokeStaticMethodWithPropertyArgument");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass("hello");

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void WhenInvokeInternalClassMemberInvoke()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeInternalClassMember");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            del.DynamicInvoke(instance);

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void WhenInvokeMemberMethod()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeMemberMethod");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            del.DynamicInvoke(instance);

            Tracker.MethodCalled.Should().BeTrue();
        }

        [Test]
        public void WhenChainMethodInvoke5Times()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeChainMethod5Times");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be(5);
        }


        [Test]
        public void InvokeMethodWithNamespace()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "InvokeMethodWithNamespace");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            del.DynamicInvoke(instance);

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void ReturnValue()
        {
            var lamdaExpression = Helper.GetLamdaExpression("InvocationTestClass", "ReturnValue");

            var del = lamdaExpression.Compile();

            var instance = new InvocationTestClass();

            var returnValue = del.DynamicInvoke(instance);

            returnValue.Should().Be("hello");
        }
    }
}
