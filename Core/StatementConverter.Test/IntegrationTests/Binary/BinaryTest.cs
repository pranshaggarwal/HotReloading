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
            Helper.Setup();
        }

        [Test]
        public void TestParenthesisExpression()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "ParenthesisExpression");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(50);
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
        public void TestAdd1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd2()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add2");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd3()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add3");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd4()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add4");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd5()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add5");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd6()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add6");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd7()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add7");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd8()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add8");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd9()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add9");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd10()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add10");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd11()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add11");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd12()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add12");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd13()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add13");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd14()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add14");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd15()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add15");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd16()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add16");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd17()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add17");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd18()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add18");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd19()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add19");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd20()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add20");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd21()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add21");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd22()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add22");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd23()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add23");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd24()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add24");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd25()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add25");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd26()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add26");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd27()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add27");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd28()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add28");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd29()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add29");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd30()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add30");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd31()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add31");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd32()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add32");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(10);
        }

        [Test]
        public void TestAdd33()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Add33");

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
        public void TestAddAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "AddAssign1");

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
        public void TestSub1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Sub1");

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
        public void TestSubAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "SubAssign1");

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
        public void TestMultiply1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Multiply1");

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
        public void TestMultipleAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "MultiplyAssign1");

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
        public void TestDivide1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Divide1");

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
        public void TestDivideAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "DivideAssign1");

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
        public void TestModulo1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Modulo1");

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
        public void TestModuloAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "ModuloAssign1");

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
        public void TestEqual1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Equal1");

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
        public void TestNotEqual1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "NotEqual1");

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
        public void TestGreaterThan1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "GreaterThan1");

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
        public void TestGreaterThanOrEqual1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "GreaterThanOrEqual1");

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
        public void TestLessThan1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LessThan1");

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
        public void TestLessThanOrEqual1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LessThanOrEqual1");

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
        public void TestBitwiseAnd1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseAnd1");

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
        public void TestBitwiseAndAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseAndAssign1");

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
        public void TestBitwiseOr1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseOr1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b1111);
        }

        [Test]
        public void TestBitwiseOrEnum()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseOrEnum");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(3);
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
        public void TestBitwiseOrAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseOrAssign1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(0b1111);
        }

        [Test]
        public void TestBitwiseOrAddEnumValue()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseOrAddEnumValue");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(3);
        }

        [Test]
        public void TestBitwiseOrRemoveEnumValue()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "BitwiseOrRemoveEnumValue");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(1);
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
        public void TestXor1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Xor1");

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
        public void TestXorAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "XorAssign1");

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
        public void TestLeftShift1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LeftShift1");

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
        public void TestLeftShiftAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "LeftShiftAssign1");

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
        public void TestRightShift1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "RightShift1");

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
        public void TestRightShiftAssign1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "RightShiftAssign1");

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

        [Test]
        public void TestCoalesce1()
        {
            var lamdaExpression = Helper.GetLamdaExpression("BinaryTestClass", "Coalesce1");

            var del = lamdaExpression.Compile();

            del.DynamicInvoke();

            Tracker.LastValue.Should().Be(2);
        }
    }
}
