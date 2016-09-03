using System;
using System.Linq;

namespace Test
{
    public class Displacement
    {
        public Displacement(int value)
        {
            Value = value;
        }

        public int Value
        {
            get;
            private set;
        }

        public byte[] GetBytes(ModRegRM rm)
        {
            switch (rm.Mod)
            {
                case Mod.E1:
                    return new byte[0];
                case Mod.E2:
                    return BitConverter.GetBytes((sbyte)Value).Take(1).ToArray();
                case Mod.E3:
                    return BitConverter.GetBytes((int)Value);
                case Mod.E4:
                    /*
					var reg = rm.RegisterOrMemory; // Correct?
					switch (reg.Size) 
					{
						case Size.S8:
							return BitConverter.GetBytes((sbyte)Value).Take(1).ToArray();

						case Size.S16:
							return BitConverter.GetBytes((short)Value).ToArray();

						case Size.S32:
							return BitConverter.GetBytes((int)Value);
					}
					break;
					*/
                    return BitConverter.GetBytes((sbyte)Value).Take(1).ToArray();
            }

            throw new Exception();
        }

        public static implicit operator Displacement(int value)
        {
            return new Displacement(value);
        }

        public override string ToString()
        {
            return string.Format(Value.ToString());
        }
    }
}