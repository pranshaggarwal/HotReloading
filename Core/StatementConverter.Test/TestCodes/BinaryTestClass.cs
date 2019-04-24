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

        public static void Sub()
        {
            var t = 5 - 5;
            Tracker.Call(t);
        }

        public static void SubAssign()
        {
            var t = 5;
            t -= 5;
            Tracker.Call(t);
        }

        public static void Multiply()
        {
            var t = 5 * 5;
            Tracker.Call(t);
        }

        public static void MultiplyAssign()
        {
            var t = 5;
            t *= 5;
            Tracker.Call(t);
        }

        public static void Divide()
        {
            var t = 5 / 5;
            Tracker.Call(t);
        }

        public static void DivideAssign()
        {
            var t = 5;
            t /= 5;
            Tracker.Call(t);
        }

        public static void Modulo()
        {
            var t = 5 % 2;
            Tracker.Call(t);
        }

        public static void ModuloAssign()
        {
            var t = 5;
            t %= 2;
            Tracker.Call(t);
        }

        public static void Equal()
        {
            var t = 5 == 5;
            Tracker.Call(t);
        }

        public static void NotEqual()
        {
            var t = 5 != 2;
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

        public static void GreaterThanOrEqual()
        {
            var t = 5 >= 5;
            Tracker.Call(t);
        }

        public static void LessThan()
        {
            var t = 4 < 5;
            Tracker.Call(t);
        }

        public static void LessThanOrEqual()
        {
            var t = 5 <= 5;
            Tracker.Call(t);
        }

        public static void BitwiseAnd()
        {
            var t = 0b0101 & 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseAndAssign()
        {
            var t = 0b0101;
            t &= 0b1010;
            Tracker.Call(t);
        }

        public static void BitwiseOr()
        {
            var t = 0b0101 | 0b1010;
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

        public static void XorAssign()
        {
            var t = 0b1001;
            t ^= 0b0101;
            Tracker.Call(t);
        }

        public static void LeftShift()
        {
            var t = 0b1111 << 1;
            Tracker.Call(t);
        }

        public static void LeftShiftAssign()
        {
            var t = 0b1111;
            t <<= 1;
            Tracker.Call(t);
        }

        public static void RightShift()
        {
            var t = 0b1111 >> 1;
            Tracker.Call(t);
        }

        public static void RightShiftAssign()
        {
            var t = 0b1111;
            t >>= 1;
            Tracker.Call(t); 
        }

        public static void Coalesce()
        {
            int? t = null;
            Tracker.Call(t ?? 2);
        }
    }
}
