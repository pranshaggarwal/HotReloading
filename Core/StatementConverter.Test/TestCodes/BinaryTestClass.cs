using System;
namespace StatementConverter.Test
{
    public class BinaryTestClass
    {
        public static void Add()
        {
            var t = 5 + 5;
            Tracker.Call(t);
        }

        public static void Add1()
        {
            Int16 x = 5;
            Int32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add2()
        {
            Int16 x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add3()
        {
            Int32 x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add4()
        {
            UInt16 x = 5;
            Int16 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add5()
        {
            UInt16 x = 5;
            Int32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add6()
        {
            UInt16 x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add7()
        {
            UInt16 x = 5;
            UInt32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add8()
        {
            UInt16 x = 5;
            UInt64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add9()
        {
            UInt32 x = 5;
            Int16 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add10()
        {
            UInt32 x = 5;
            Int32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add11()
        {
            UInt32 x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add12()
        {
            UInt32 x = 5;
            UInt64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add13()
        {
            float x = 5;
            Int16 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add14()
        {
            float x = 5;
            Int32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add15()
        {
            float x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add16()
        {
            float x = 5;
            UInt16 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add17()
        {
            float x = 5;
            UInt32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add18()
        {
            float x = 5;
            UInt64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add19()
        {
            float x = 5;
            double y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add20()
        {
            double x = 5;
            Int16 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add21()
        {
            double x = 5;
            Int32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add22()
        {
            double x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add23()
        {
            decimal x = 5;
            Int16 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add24()
        {
            decimal x = 5;
            Int32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add25()
        {
            decimal x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add26()
        {
            decimal x = 5;
            UInt16 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add27()
        {
            decimal x = 5;
            UInt32 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add28()
        {
            decimal x = 5;
            UInt64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add29()
        {
            Byte x = 5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add30()
        {
            char x = (char)5;
            Int64 y = 5;
            var t = x + y;
            Tracker.Call(t);
        }

        public static void Add31()
        {
            var t = (byte)5 + (byte)5;
            Tracker.Call(t);
        }

        public static void Add32()
        {
            var t = (sbyte)5 + (sbyte)5;
            Tracker.Call(t);
        }

        public static void Add33()
        {
            var t = (char)5 + (char)5;
            Tracker.Call(t);
        }

        public static void AddString1()
        {
            var t = "hel" + "lo";
            Tracker.Call(t);
        }

        public static void AddString2()
        {
            var t = "hello" + 1;
            Tracker.Call(t);
        }

        public static void AddString3()
        {
            var t = 1 + "hello";
            Tracker.Call(t);
        }

        public static void AddAssign()
        {
            var t = 5;
            t += 5;
            Tracker.Call(t);
        }

        public static void AddAssign1()
        {
            byte t = 5;
            t += 5;
            Tracker.Call(t);
        }

        public static void Sub()
        {
            var t = 5 - 5;
            Tracker.Call(t);
        }

        public static void Sub1()
        {
            byte x = 5;
            int y = 5;
            var t = x - y;
            Tracker.Call(t);
        }

        public static void SubAssign()
        {
            var t = 5;
            t -= 5;
            Tracker.Call(t);
        }

        public static void SubAssign1()
        {
            byte t = 5;
            t -= 5;
            Tracker.Call(t);
        }

        public static void Multiply()
        {
            var t = 5 * 5;
            Tracker.Call(t);
        }

        public static void Multiply1()
        {
            byte x = 5;
            int y = 5;
            var t = x * y;
            Tracker.Call(t);
        }

        public static void MultiplyAssign()
        {
            var t = 5;
            t *= 5;
            Tracker.Call(t);
        }

        public static void MultiplyAssign1()
        {
            byte t = 5;
            t *= 5;
            Tracker.Call(t);
        }

        public static void Divide()
        {
            var t = 5 / 5;
            Tracker.Call(t);
        }

        public static void Divide1()
        {
            byte x = 5;
            int y = 5;
            var t = x / y;
            Tracker.Call(t);
        }

        public static void DivideAssign()
        {
            var t = 5;
            t /= 5;
            Tracker.Call(t);
        }

        public static void DivideAssign1()
        {
            byte t = 5;
            t /= 5;
            Tracker.Call(t);
        }

        public static void Modulo()
        {
            var t = 5 % 2;
            Tracker.Call(t);
        }

        public static void Modulo1()
        {
            byte x = 5;
            int y = 2;
            var t = x % y;
            Tracker.Call(t);
        }

        public static void ModuloAssign()
        {
            var t = 5;
            t %= 2;
            Tracker.Call(t);
        }

        public static void ModuloAssign1()
        {
            byte t = 5;
            t %= 2;
            Tracker.Call(t);
        }

        public static void Equal()
        {
            var t = 5 == 5;
            Tracker.Call(t);
        }

        public static void Equal1()
        {
            byte x = 5;
            int y = 5;
            var t = x == y;
            Tracker.Call(t);
        }

        public static void NotEqual()
        {
            var t = 5 != 2;
            Tracker.Call(t);
        }

        public static void NotEqual1()
        {
            byte x = 5;
            int y = 2;
            var t = x != y;
            Tracker.Call(t);
        }

        public static void And()
        {
            var t = true && true;
            Tracker.Call(t);
        }

        public static void Or()
        {
            var t = true || false;
            Tracker.Call(t);
        }

        public static void GreaterThan()
        {
            var t = 6 > 5;
            Tracker.Call(t);
        }

        public static void GreaterThan1()
        {
            byte x = 6;
            int y = 5;
            var t = x > y;
            Tracker.Call(t);
        }

        public static void GreaterThanOrEqual()
        {
            var t = 5 >= 5;
            Tracker.Call(t);
        }

        public static void GreaterThanOrEqual1()
        {
            byte x = 5;
            int y = 5;
            var t = x >= y;
            Tracker.Call(t);
        }

        public static void LessThan()
        {
            var t = 4 < 5;
            Tracker.Call(t);
        }

        public static void LessThan1()
        {
            byte x = 4;
            int y = 5;
            var t = x < y;
            Tracker.Call(t);
        }

        public static void LessThanOrEqual()
        {
            var t = 5 <= 5;
            Tracker.Call(t);
        }

        public static void LessThanOrEqual1()
        {
            byte x = 5;
            int y = 5;
            var t = x <= y;
            Tracker.Call(t);
        }

        public static void BitwiseAnd()
        {
            var t = 0b0101 & 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseAnd1()
        {
            var t = (Int64)0b0101 & 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseAndAssign()
        {
            var t = 0b0101;
            t &= 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseAndAssign1()
        {
            Int64 t = 0b0101;
            t &= 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseOr()
        {
            var t = 0b0101 | 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseOr1()
        {
            var t = (Int64)0b0101 | 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseOrEnum()
        {
            CustomEnum t = CustomEnum.Value1 | CustomEnum.Value2;
            Tracker.Call((int)t);
        }

        public static void BitwiseOrAssign()
        {
            var t = 0b0101;
            t |= 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseOrAssign1()
        {
            Int64 t = 0b0101;
            t |= 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseOrAddEnumValue()
        {
            CustomEnum t = CustomEnum.Value1;
            t |= CustomEnum.Value2;
            Tracker.Call((int)t);
        }

        public static void BitwiseOrRemoveEnumValue()
        {
            CustomEnum t = CustomEnum.Value1 | CustomEnum.Value2;
            t &= ~CustomEnum.Value2;
            Tracker.Call(t);
        }

        public static void Xor()
        {
            var t = 0b1001 ^ 0b0101;
            Tracker.Call(t);
        }

        public static void Xor1()
        {
            var t = (Int64)0b1001 ^ 0b0101;
            Tracker.Call(t);
        }

        public static void XorAssign()
        {
            var t = 0b1001;
            t ^= 0b0101;
            Tracker.Call(t);
        }

        public static void XorAssign1()
        {
            Int64 t = 0b1001;
            t ^= 0b0101;
            Tracker.Call(t);
        }

        public static void LeftShift()
        {
            var t = 0b1111 << 1;
            Tracker.Call(t);
        }

        public static void LeftShift1()
        {
            var t = 0b1111 << (byte)1;
            Tracker.Call(t);
        }

        public static void LeftShiftAssign()
        {
            var t = 0b1111;
            t <<= 1;
            Tracker.Call(t);
        }

        public static void LeftShiftAssign1()
        {
            var t = 0b1111;
            t <<= (byte)1;
            Tracker.Call(t);
        }

        public static void RightShift()
        {
            var t = 0b1111 >> 1;
            Tracker.Call(t);
        }

        public static void RightShift1()
        {
            var t = 0b1111 >> (byte)1;
            Tracker.Call(t);
        }

        public static void RightShiftAssign()
        {
            var t = 0b1111;
            t >>= 1;
            Tracker.Call(t);
        }

        public static void RightShiftAssign1()
        {
            var t = 0b1111;
            t >>= (byte)1;
            Tracker.Call(t);
        }

        public static void Coalesce()
        {
            int? t = null;
            Tracker.Call(t ?? 2);
        }

        public static void Coalesce1()
        {
            byte? t = null;
            Tracker.Call(t ?? 2);
        }
    }
}
