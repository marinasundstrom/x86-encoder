namespace x86Encoder
{
    public class OpType
    {
        byte[] bytes;
        internal OpType(string name, byte[] bytes, bool supportsImmediate = false, bool hasModeRegRM = false, bool supportsDirection = false, bool canSet32Bit = false)
        {
            Name = name;
            SupportsImmediate = supportsImmediate;
            HasModRegRM = hasModeRegRM;
            SupportsDirection = supportsDirection;
            CanSet32Bit = canSet32Bit;
            this.bytes = (byte[])bytes.Clone();
        }

        public string Name
        {
            get;
            private set;
        }

        public int Size
        {
            get
            {
                return bytes.Length;
            }
        }

        public bool SupportsImmediate
        {
            get;
            private set;
        }

        public bool TakesShortAddress
        {
            get;
            private set;
        }

        public bool TakesNearAddress
        {
            get;
            private set;
        }

        public bool HasModRegRM
        {
            get;
            private set;
        }

        public bool SupportsDirection
        {
            get;
            private set;
        }

        public bool CanSet32Bit
        {
            get;
            private set;
        }

        public byte[] GetBytes()
        {
            return bytes;
        }

        #region Op Types

        public static readonly OpType Push = new OpType(nameof(Push), new byte[] { 0x6A }, true, true, true, true);

        public static readonly OpType Pop = new OpType(nameof(Pop), new byte[] { 0x8F }, true, true, true, true);

        public static readonly OpType Add = new OpType(nameof(Add), new byte[]{0x00}, true, true, true, true);

        public static readonly OpType Sub = new OpType(nameof(Sub), new byte[] { 0x28 }, true, true, true, true);

        public static readonly OpType IMul = new OpType(nameof(IMul), new byte[] { 0x6B }, true, true, true, true);

        public static readonly OpType Div = new OpType(nameof(Div), new byte[] { 0xF6 }, true, true, true, true);

        public static readonly OpType Inc = new OpType(nameof(Inc), new byte[] { 0xFE }, true, true, true, true);

        public static readonly OpType Dec = new OpType(nameof(Dec), new byte[] { 0xFF }, true, true, true, true);

        public static readonly OpType Mov = new OpType(nameof(Mov), new byte[] { 0x88 }, true, true, true, true);

        public static readonly OpType Ret = new OpType(nameof(Ret), new byte[] { 0xC2 }, true, true, true, true);

        public static readonly OpType IRet = new OpType(nameof(IRet), new byte[] { 0xCF }, true, true, true, true);

        public static readonly OpType Call = new OpType(nameof(Call), new byte[] { 0xE8 }, true, true, true, true);

        public static readonly OpType Jmp = new OpType(nameof(Jmp), new byte[] { 0xE9 }, true, false, false, true);

        public static readonly OpType Jz = new OpType(nameof(Jz), new byte[] { 0x74 }, true, false, false, true);

        public static readonly OpType Jnz = new OpType(nameof(Jnz), new byte[] { 0x75 }, true, false, false, true);

        public static readonly OpType Cmp = new OpType(nameof(Cmp), new byte[] { 0x38 }, true, true, true, false);

        #endregion
    }
}