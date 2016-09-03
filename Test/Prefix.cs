namespace Test
{
    public static class Prefix
    {
        // Repeat/lock
        public static readonly byte Lock = 0xF0;
        // String manipulation
        public static readonly byte Rep = 0xF1;
        public static readonly byte Repne = 0xF2;
        // Segment
        public static readonly byte CS = 0x2E;
        public static readonly byte SS = 0x36;
        public static readonly byte DS = 0x3E;
        public static readonly byte ES = 0x26;
        public static readonly byte FS = 0x64;
        public static readonly byte GS = 0x65;
        // Operand size
        public static readonly byte TwoByteOpCode = 0x0F;
    }
}