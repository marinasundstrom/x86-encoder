using System.Collections.Generic;

namespace Test
{
    public class ModRegRM
    {
        private int size;
        public ModRegRM(Mod mod, Register register, Register registerOrMemory)
        {
            Mod = mod;
            Register = register;
            RegisterOrMemory = registerOrMemory;
        }

        public ModRegRM(Mod mod, Register register, Register registerOrMemory, Displacement displacement)
        {
            Mod = mod;
            Register = register;
            RegisterOrMemory = registerOrMemory;
            Displacement = displacement;
        }

        public ModRegRM(Mod mod, Register register, Register registerOrMemory, SIB sib)
        {
            Mod = mod;
            Register = register;
            RegisterOrMemory = registerOrMemory;
            SIB = sib;
        }

        public Mod Mod
        {
            get;
            private set;
        }

        public Register Register
        {
            get;
            private set;
        }

        public Register RegisterOrMemory
        {
            get;
            private set;
        }

        public SIB SIB
        {
            get;
            private set;
        }

        public Displacement Displacement
        {
            get;
            private set;
        }

        public int Size
        {
            get
            {
                if (size == 0)
                {
                    size = 1;
                    if (SIB != null)
                    {
                        size += 2;
                    }

                    if (Displacement != null)
                    {
                        size += 1;
                    }
                }

                return size;
            }
        }

        public byte[] Encode()
        {
            var bytes = new List<byte>();
            byte b = 0;
            b = EncodeModRegRM(b, Mod, Register?.GetByte() ?? 0, RegisterOrMemory?.GetByte() ?? 0);
            bytes.Add(b);
            if (SIB != null)
            {
                bytes.Add(SIB.Encode());
            }

            if (Displacement != null)
            {
                bytes.AddRange(Displacement.GetBytes(this));
            }

            return bytes.ToArray();
        }

        public static byte EncodeModRegRM(byte b, Mod mod, byte reg, byte rm)
        {
            bool mod0 = Bits.IsBitSet((byte)mod, 0);
            bool mod1 = Bits.IsBitSet((byte)mod, 1);
            bool reg0 = Bits.IsBitSet((byte)reg, 0);
            bool reg1 = Bits.IsBitSet((byte)reg, 1);
            bool reg2 = Bits.IsBitSet((byte)reg, 2);
            bool rm0 = Bits.IsBitSet((byte)rm, 0);
            bool rm1 = Bits.IsBitSet((byte)rm, 1);
            bool rm2 = Bits.IsBitSet((byte)rm, 2);
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
    }
}