using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class InstructionGenerator
    {
        public InstructionGenerator()
        {
            labels = new List<Label>();
        }

        #region Emit

        public void Emit(byte data)
        {
            Append(new byte[] { data });
        }

        public void Emit(byte[] data)
        {
            Append(data);
        }

        public void Emit(OpCode opCode)
        {
            var data = new Instruction(opCode);
            Append(Encode(opCode));
        }

        public void Emit(OpCode opCode, ModRegRM modRegRm)
        {
            var data = new Instruction(opCode, modRegRm);
            Append(Encode(opCode, modRegRm));
        }

        public void Emit(OpCode opCode, ModRegRM modRegRm, Constant constant)
        {
            var data = new Instruction(opCode, modRegRm, constant);
            Append(Encode(opCode, modRegRm, constant));
        }

        public void Emit(OpCode opCode, Constant constant)
        {
            var data = new Instruction(opCode, constant: constant);
            Append(Encode(opCode, constant: constant));
        }

        #endregion

        #region Labels

        public Label DefineLabel(string name)
        {
            var label = new Label(name);
            labels.Add(label);
            return label;
        }

        public void EmitLabel(Label label)
        {
            label.Value = Offset;
        }
        public IEnumerable<Label> Labels
        {
            get
            {
                return labels;
            }
        }

        #endregion

        #region Buffer

        public uint Offset
        {
            get
            {
                return offset;
            }
        }

        public byte[] GetBytes()
        {
            var bufferCopy = new byte[Offset];
            Array.Copy(buffer, bufferCopy, Offset);
            PrepareLabels(bufferCopy);
            return bufferCopy;
        }

        private void PrepareLabels(byte[] buffer)
        {
            foreach (var label in Labels)
            {
                //var bytes = BitConverter.GetBytes((uint)label.Value);
                foreach (var reference in label.References)
                {
                    var origin = (int)reference;
                    var destination = (int)(uint)label.Value;
                    int relAddress = 0;
                    if (destination > Offset)
                    {
                        relAddress = destination - origin;
                    }
                    else
                    {
                        relAddress = origin - destination;
                    }
                    byte[] bytes = null;
                    
                    bytes = BitConverter.GetBytes(relAddress + 5);
                    Array.Copy(bytes, 0, buffer, reference, bytes.Length);
                }
            }
        }

        #endregion

        #region Buffer Internals

        private byte[] buffer;
        private uint offset;
        private List<Label> labels;

        private void Append(byte[] data)
        {
            if (buffer == null)
            {
                buffer = new byte[20];
                offset = 0;
            }

            var dataCount = data.Length;

            if (offset + dataCount > buffer.Length)
            {
                var oldBuffer = buffer;
                int increasedSize = oldBuffer.Length / 2;
                buffer = new byte[oldBuffer.Length + increasedSize];

                Array.Copy(oldBuffer, buffer, oldBuffer.Length);
            }
      
            Array.Copy(data, 0, buffer, offset, data.Length);

            offset += (uint)dataCount;
        }

        #endregion

        private byte[] Encode(OpCode opCode, ModRegRM modReg = null, Constant constant = null)
        {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(opCode.GetBytes());

            if (opCode.Type.HasModRegRM)
            {
                bytes.AddRange(modReg.Encode());
            }

            if (constant != null)
            {
                var label = constant as Label;

                if (label != null)
                {
                    var size = 0;
                    size += opCode.Size;
                    if (opCode.Type.HasModRegRM)
                    {
                        size += modReg.Size;
                    }

                    if (opCode.Is32Bit)
                    {
                        bytes.AddRange(BitConverter.GetBytes((uint)0));
                    }
                    else
                    {
                        bytes.AddRange(BitConverter.GetBytes((ushort)0));
                    }

                    label.References.Add((uint)(Offset + size));
                }
                else
                {
                    if (opCode.Is32Bit)
                    {
                        bytes.AddRange(BitConverter.GetBytes((uint)constant));
                    }
                    else
                    {
                        bytes.AddRange(BitConverter.GetBytes((ushort)constant));
                    }
                }
            }

            return bytes.ToArray();
        }
    }
}
