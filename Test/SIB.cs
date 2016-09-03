namespace Test
{
    public class SIB
    {
        public SIB(uint scale, Register index, Register bas)
        {
            Scale = scale;
            Index = index;
            Base = bas;
        }

        public uint Scale
        {
            get;
            private set;
        }

        public Register Index
        {
            get;
            private set;
        }

        public Register Base
        {
            get;
            private set;
        }

        public static byte EncodeSIB(byte scale, byte index, byte bas)
        {
            byte b = 0;
            bool mod0 = Bits.IsBitSet((byte)scale, 0);
            bool mod1 = Bits.IsBitSet((byte)scale, 1);
            bool reg0 = Bits.IsBitSet((byte)index, 0);
            bool reg1 = Bits.IsBitSet((byte)index, 1);
            bool reg2 = Bits.IsBitSet((byte)index, 2);
            bool rm0 = Bits.IsBitSet((byte)bas, 0);
            bool rm1 = Bits.IsBitSet((byte)bas, 1);
            bool rm2 = Bits.IsBitSet((byte)bas, 2);
            b = Bits.SetBit(b, 0, rm0);
            b = Bits.SetBit(b, 1, rm1);
            b = Bits.SetBit(b, 2, rm2);
            b = Bits.SetBit(b, 3, reg0);
            b = Bits.SetBit(b, 4, reg1);
            b = Bits.SetBit(b, 5, reg2);
            b = Bits.SetBit(b, 6, mod0);
            b = Bits.SetBit(b, 7, mod1);
            return b;
        }

        public byte Encode()
        {
            return EncodeSIB((byte)Scale, Index?.GetByte() ?? 0, Base?.GetByte() ?? 0);
        }
    }
}