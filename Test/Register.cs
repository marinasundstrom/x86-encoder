namespace Test
{
    public class Register
    {
        byte _byte;
        public Register(string name, Size size, byte b)
        {
            Name = name;
            Size = size;
            _byte = b;
        }

        public Register(string name, Size size, byte b, Register bas, bool isSegment = false)
        {
            Name = name;
            Size = size;
            _byte = b;
            Base = bas;
            IsSegmentRegister = isSegment;
        }

        public string Name
        {
            get;
            private set;
        }

        public Size Size
        {
            get;
            private set;
        }

        public Register Base
        {
            get;
            private set;
        }

        public bool IsSegmentRegister
        {
            get;
            private set;
        }

        public byte GetByte()
        {
            return _byte;
        }

        public override string ToString()
        {
            return string.Format(Name);
        }

#region Registers
        public static readonly Register None = null;
        public static readonly Register EAX = new Register(nameof(EAX), Size.S32, 0);
        public static readonly Register ECX = new Register(nameof(ECX), Size.S32, 1);
        public static readonly Register EDX = new Register(nameof(EDX), Size.S32, 2);
        public static readonly Register EBX = new Register(nameof(EBX), Size.S32, 3);
        public static readonly Register ESP = new Register(nameof(ESP), Size.S32, 4);
        public static readonly Register EBP = new Register(nameof(EBP), Size.S32, 5);
        public static readonly Register ESI = new Register(nameof(ESI), Size.S32, 6);
        public static readonly Register EDI = new Register(nameof(EDI), Size.S32, 7);
        public static readonly Register AX = new Register(nameof(AX), Size.S16, 0, EAX);
        public static readonly Register CX = new Register(nameof(CX), Size.S16, 1, ECX);
        public static readonly Register DX = new Register(nameof(DX), Size.S16, 2, EDX);
        public static readonly Register BX = new Register(nameof(BX), Size.S16, 3, EBX);
        public static readonly Register SP = new Register(nameof(SP), Size.S16, 4, ESP);
        public static readonly Register BP = new Register(nameof(BP), Size.S16, 5, BP);
        public static readonly Register SI = new Register(nameof(SI), Size.S16, 6, SI);
        public static readonly Register DI = new Register(nameof(DI), Size.S16, 7, DI);
#endregion
    }
}