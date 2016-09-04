using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace x86Encoder.Tests.OpCodes
{
    /// <summary>
    /// http://x86.renejeschke.de/html/file_module_x86_id_5.html
    /// </summary>
    public class AddOpCodeTest
    {
        [Fact(DisplayName = nameof(Add))]
        public void Add()
        {
            byte expectedOpByte = 0x00;

            var opType = OpType.Add;

            var opByte = opType.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_Eb_Iv))]
        public void Add_Eb_Iv()
        {
            byte expectedOpByte = 0x80;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RegToRM, isImmediate: true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_Ev_Iv))]
        public void Add_Ev_Iv()
        {
            byte expectedOpByte = 0x81;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RegToRM, true, isImmediate: true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_Gv_Iv))]
        public void Add_Gv_Iv()
        {
            byte expectedOpByte = 0x83;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RMToReg, true, isImmediate: true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_Eb_Gb))]
        public void Add_Eb_Gb()
        {
            byte expectedOpByte = 0x00;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RegToRM);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_Ev_Gv))]
        public void Add_Ev_Gv()
        {
            byte expectedOpByte = 0x01;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RegToRM, true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_Gb_Eb))]
        public void Add_Gb_Eb()
        {
            byte expectedOpByte = 0x02;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RMToReg);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_Gv_Ev))]
        public void Add_Gv_Ev()
        {
            byte expectedOpByte = 0x03;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RMToReg, true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_AL_Ib), Skip = "Is this wrong?")]
        public void Add_AL_Ib()
        {
            byte expectedOpByte = 0x04;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RegToRM, true, isImmediate: true);
            var instruction = new Instruction(opCode, new ModRegRM(Mod.E4, Register.None, Register.AL), constant: 42);

            var opByte = instruction.Encode().First();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Add_rAX_Iz), Skip = "Is this wrong?")]
        public void Add_rAX_Iz()
        {
            byte expectedOpByte = 0x05;

            var opCode = OpCode.Create(OpType.Add, OpCodeDirection.RegToRM, true, isImmediate: true);
            var instruction = new Instruction(opCode, new ModRegRM(Mod.E4, Register.None, Register.AX), constant: 42);

            var opByte = instruction.Encode().First();
            Assert.Equal(expectedOpByte, opByte);
        }
    }
}
