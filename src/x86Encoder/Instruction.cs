using System;
using System.Collections.Generic;

namespace x86Encoder
{
    public class Instruction
    {
        private int size;
        public Instruction(OpCode opCode, ModRegRM modReg = null, Constant constant = null)
        {
            OpCode = opCode;
            ModRegRM = modReg;
            Constant = constant;
        }

        public Instruction(ICollection<byte> prefixes, OpCode opCode, ModRegRM modReg = null, Constant constant = null)
        {
            Prefixes = prefixes;
            OpCode = opCode;
            ModRegRM = modReg;
            Constant = constant;
        }

        public IEnumerable<byte> Prefixes
        {
            get;
            private set;
        }

        public OpCode OpCode
        {
            get;
            private set;
        }

        public ModRegRM ModRegRM
        {
            get;
            private set;
        }

        public Constant Constant
        {
            get;
            private set;
        }

        public byte[] Encode()
        {
            var bytes = new List<byte>();
            bytes.AddRange(OpCode.GetBytes());
            if (OpCode.Type.HasModRegRM && ModRegRM != null)
            {
                bytes.AddRange(ModRegRM.Encode());
            }

            if (Constant != null)
            {
                var label = Constant as Label;
                if (label != null)
                {
                    var size = 0;
                    size += OpCode.Size;
                    if (OpCode.Type.HasModRegRM)
                    {
                        size += ModRegRM.Size;
                    }

                    label.References.Add((uint)size);
                    if (OpCode.Is32Bit)
                    {
                        bytes.AddRange(BitConverter.GetBytes((uint)0));
                    }
                    else
                    {
                        bytes.AddRange(BitConverter.GetBytes((ushort)0));
                    }
                }
                else
                {
                    if (OpCode.Is32Bit)
                    {
                        bytes.AddRange(BitConverter.GetBytes((uint)Constant));
                    }
                    else
                    {
                        bytes.AddRange(BitConverter.GetBytes((ushort)Constant));
                    }
                }
            }

            return bytes.ToArray();
        }

        public int Size
        {
            get
            {
                if (size == 0)
                {
                    size += OpCode.Size;
                    if (OpCode.Type.HasModRegRM)
                    {
                        size += ModRegRM.Size;
                    }

                    if (Constant != null)
                    {
                        if (OpCode.Is32Bit)
                        {
                            size += 4;
                        }
                        else
                        {
                            size += 2;
                        }
                    }
                }

                return size;
            }
        }
    }
}