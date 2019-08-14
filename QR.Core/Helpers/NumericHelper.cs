using System.Collections.Generic;

namespace QR.Core
{
    internal static class NumericHelper
    {
        public static bool GetBit(this byte value, int i) => ((value >> i) & 1) != 0;
        public static bool GetBit(this int value, int i) => (((uint)value >> i) & 1) != 0;

        public static IEnumerable<bool> ToBits(this byte value) => ToBits(value, sizeof(byte) * 8);
        public static IEnumerable<bool> ToBits(this int value, int size)
        {
            uint unsigned = (uint)value;
            for (int i = size - 1; i >= 0; --i)
                yield return (unsigned & (0b1L << i)) != 0;
        }
    }
}
