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

        public bool TakeShortAddress
        {
            get;
            private set;
        }

        public bool TakeNearAddress
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

        public static readonly OpType Dec_EAX = new OpType(nameof(Dec_EAX), new byte[]{0x48}, false, false, false, false);
        public static readonly OpType Push_EBP = new OpType(nameof(Push_EBP), new byte[]{0x55}, false, false, false, false);
        public static readonly OpType Pop_EBP = new OpType(nameof(Pop_EBP), new byte[]{0x5d}, false, false, false, false);
        public static readonly OpType Ret = new OpType(nameof(Ret), new byte[]{0xC3}, false, false, false, false);
        public static readonly OpType Ret1 = new OpType(nameof(Ret1), new byte[] { 0xC2 }, true, false, false, false);
        public static readonly OpType Mov = new OpType(nameof(Mov), new byte[]{0x88}, true, true, true, true);
        public static readonly OpType Mov2 = new OpType(nameof(Mov2), new byte[]{0x89}, true, true, true, true);
        public static readonly OpType Mov_eAX_lv = new OpType(nameof(Mov_eAX_lv), new byte[]{0xB8}, true, false, true, false);
        public static readonly OpType Mov_eDX_lv = new OpType(nameof(Mov_eDX_lv), new byte[] { 0xBA }, true, false, true, false);
        public static readonly OpType Add = new OpType(nameof(Add), new byte[]{0x00}, true, true, true, true);
        public static readonly OpType Add2 = new OpType(nameof(Add2), new byte[]{0x01}, true, true, true, true);
        public static readonly OpType Add3 = new OpType(nameof(Add3), new byte[]{0x02}, true, true, true, true);
        public static readonly OpType Sub = new OpType(nameof(Sub), new byte[]{0x28}, true, true, true, true);
        public static readonly OpType Call = new OpType(nameof(Call), new byte[]{0xE8}, true, false, false, true);
        public static readonly OpType Jmp = new OpType(nameof(Jmp), new byte[]{0xE9}, true, false, false, true);
        public static readonly OpType Jz = new OpType(nameof(Jz), new byte[]{0x74}, true, false, false, true);
        public static readonly OpType Jnz = new OpType(nameof(Jnz), new byte[]{0x75}, true, false, false, true);
        public static readonly OpType Cmp2 = new OpType(nameof(Cmp2), new byte[]{0x39}, true, true, true, false);
    }
}