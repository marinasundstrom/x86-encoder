namespace Test
{
    public class Constant
    {
        public uint ? Value;

        public static implicit operator uint (Constant constant)
        {
            return (uint)constant.Value;
        }

        public static implicit operator Constant(int value)
        {
            return new ConstantNo((uint)value);
        }

        public static implicit operator Constant(uint value)
        {
            return new ConstantNo(value);
        }
    }
}