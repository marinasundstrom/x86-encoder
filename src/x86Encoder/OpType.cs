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

        public static readonly OpType Add = new OpType(nameof(Add), new byte[]{0x00}, true, true, true, true);
    }
}