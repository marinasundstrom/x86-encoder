using System;
using System.Linq;

namespace x86Encoder
{
    public struct OpCode
    {
        byte[] bytes;
        private OpCode(OpType opType, OpCodeDirection direction = OpCodeDirection.RegToRM, bool is32bit = true, bool isImmediate = false)
        {
            Type = opType;
            Direction = direction;
            Is32Bit = is32bit;
            IsImmediate = isImmediate;
            this.bytes = (byte[])opType.GetBytes().Clone();
            if (opType.SupportsImmediate && IsImmediate)
            {
                bytes = SetImmediateMode(bytes, true);
            }

            if (opType.CanSet32Bit)
            {
                bytes = Modify(bytes, (byte)Direction == (byte)OpCodeDirection.RMToReg ? true : false, is32bit);
            }
        }

        public int Size
        {
            get
            {
                return bytes.Length;
            }
        }

        public byte[] GetBytes()
        {
            return bytes;
        }

        public OpCodeDirection Direction
        {
            get;
            private set;
        }

        public bool Is32Bit
        {
            get;
            private set;
        }

        public OpType Type
        {
            get;
            private set;
        }

        public bool IsImmediate
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return string.Format(Type.Name);
        }

        public static OpCode Create(OpType opType, OpCodeDirection direction = OpCodeDirection.RegToRM, bool is32bit = false, bool isImmediate = false)
        {
            return new OpCode(opType, direction, is32bit, isImmediate);
        }

        /// <summary>
        /// Modify the specified opCode, d and s.
        /// </summary>
        /// <param name = "opCode">Op code.</param>
        /// <param name = "d">
        /// 	d = 0 if adding from Register to R/M.
        /// 	d = 1 if adding from R/M to Register.
        /// </param>
        /// <param name = "s">
        /// 	s = 0 if adding 8-bit operands.
        /// 	s = 1 if adding 16-bit or 32-bit operands.
        /// </param>
        public static byte[] Modify(byte[] opCode, bool d, bool s)
        {
            var b = opCode.Last();
            b = Bits.SetBit(b, 0, s);
            b = Bits.SetBit(b, 1, d);
            if (opCode.Length == 1)
            {
                return new byte[]{b};
            }
            else if (opCode.Length == 2)
            {
                return new[]{opCode[0], b};
            }

            throw new Exception("Invalid OpCode");
        }

        public static byte[] SetImmediateMode(byte[] opCode, bool value)
        {
            var b = opCode.First();
            b = Bits.SetBit(b, 7, value);
            if (opCode.Length == 1)
            {
                return new byte[]{b};
            }
            else if (opCode.Length == 2)
            {
                return new[]{b, opCode[1]};
            }

            throw new Exception("Invalid OpCode");
        }

        public static bool HasImmediateMode(byte[] opCode)
        {
            var b = opCode.First();
            return Bits.IsBitSet(b, 7);
        }
    }
}