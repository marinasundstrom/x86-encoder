using System.Text;
using System.Runtime.CompilerServices;

namespace Test
{
    public static class Bits
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte SetBit(byte b, int index, bool value)
        {
            byte mask = (byte)(1 << (byte)index);
            if (!value)
            {
                return b &= (byte)~mask;
            }
            else
            {
                return b |= mask;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBitSet(byte b, int index)
        {
            return (b & (1 << index)) != 0;
        }

        public static string ToBitString(byte b)
        {
            var sb = new StringBuilder();
            for (int i = 7; i >= 0; i--)
            {
                var r = Bits.IsBitSet(b, i);
                sb.Append(r ? 1 : 0);
            }

            return sb.ToString();
        }
    }
}