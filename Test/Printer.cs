using System;
using System.Text;

namespace Test
{
    public static class Printer
    {
        public static void PrintBits(params byte[] b)
        {
            foreach (var bs in b)
            {
                Console.Write(Bits.ToBitString(bs));
            }

            Console.WriteLine();
        }

        public static void PrintBytes(params byte[] b)
        {
            foreach (var b2 in b)
            {
                Console.WriteLine(Convert.ToString(b2, 16).ToString().PadLeft(2, '0'));
            }
        }

        public static void PrintInstructions(byte[][] b)
        {
            foreach (var b1 in b)
            {
                var sb = new StringBuilder();
                foreach (var b2 in b1)
                {
                    sb.Append(Bits.ToBitString(b2) + ' ');
                }

                Console.WriteLine(sb.ToString());
            }
        }

        public static void PrintInstructions2(byte[][] b, bool f = false)
        {
            int offset = 0;
            foreach (var b1 in b)
            {
                var sb = new StringBuilder();
                if (f)
                {
                    sb.Append(Convert.ToString(offset, 16).ToString().PadLeft(4, '0') + "    ");
                }

                foreach (var b2 in b1)
                {
                    sb.Append(Convert.ToString(b2, 16).PadLeft(2, '0').PadRight(3, ' '));
                    offset += 1;
                }

                Console.WriteLine(sb.ToString());
            }
        }

        public static void PrintInstructions(Instruction[] instructions, bool f = false)
        {
            int offset = 0;
            foreach (var b1 in instructions)
            {
                var sb = new StringBuilder();
                if (f)
                {
                    sb.Append(Convert.ToString(offset, 16).ToString().PadLeft(4, '0') + "    ");
                }

                var b = b1.Encode();
                foreach (var b2 in b)
                {
                    sb.Append(Convert.ToString(b2, 16).PadLeft(2, '0').PadRight(3, ' '));
                }

                offset += b1.Size;
                Console.WriteLine(sb.ToString());
            }
        }
    }
}