using System;
using FluentAssertions;
using NUnit.Framework;

namespace StatementConverter.Test
{
    [TestFixture]
    public class BinaryTest
    {
        [SetUp]
        public void Setup()
        {
            Tracker.Reset();
        }

        [Test]
        public void TestAdd()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void AddString1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "AddString1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello");
        }

        [Test]
        public void AddString2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "AddString2");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("hello1");
        }

        [Test]
        public void AddString3()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "AddString3");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be("1hello");
        }

        [Test]
        public void TestAddAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "AddAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestSub()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Sub");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0);
        }

        [Test]
        public void TestSubAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "SubAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0);
        }

        [Test]
        public void TestMultiply()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Multiply");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(25);
        }

        [Test]
        public void TestMultipleAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "MultiplyAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(25);
        }

        [Test]
        public void TestDivide()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Divide");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(1);
        }

        [Test]
        public void TestDivideAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "DivideAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(1);
        }

        [Test]
        public void TestModulo()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Modulo");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(1);
        }

        [Test]
        public void TestModuloAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "ModuloAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(1);
        }

        [Test]
        public void TestEqual()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Equal");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestNotEqual()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "NotEqual");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestAnd()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "And");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestOr()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Or");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestGreaterThan()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "GreaterThan");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestGreaterThanOrEqual()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "GreaterThanOrEqual");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestLessThan()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LessThan");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestLessThanOrEqual()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LessThanOrEqual");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(true);
        }

        [Test]
        public void TestBitwiseAnd()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseAnd");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0x0000);
        }

        [Test]
        public void TestBitwiseAndAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseAndAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0x0000);
        }

        [Test]
        public void TestBitwiseOr()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseOr");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b1111);
        }

        [Test]
        public void TestBitwiseOrAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseOrAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b1111);
        }

        [Test]
        public void TestXor()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Xor");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b1100);
        }

        [Test]
        public void TestXorAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "XorAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b1100);
        }

        [Test]
        public void TestLeftShift()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LeftShift");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b11110);
        }

        [Test]
        public void TestLeftShiftAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LeftShiftAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b11110);
        }

        [Test]
        public void TestRightShift()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "RightShift");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b0111);
        }

        [Test]
        public void TestRightShiftAssign()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "RightShiftAssign");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b0111);
        }

        [Test]
        public void TestCoalesce()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Coalesce");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(2);
        }
    }
}
