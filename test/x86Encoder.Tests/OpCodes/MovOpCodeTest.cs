using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace x86Encoder.Tests.OpCodes
{
    /// <summary>
    /// http://x86.renejeschke.de/html/file_module_x86_id_176.html
    /// </summary>
    public class MovOpCodeTest
    {
        [Fact(DisplayName = nameof(Mov))]
        public void Mov()
        {
            byte expectedOpByte = 0x88;

            var opType = OpType.Mov;

            var opByte = opType.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Mov_Eb_Gb))]
        public void Mov_Eb_Gb()
        {
            byte expectedOpByte = 0x88;

            var opCode = OpCode.Create(OpType.Mov, OpCodeDirection.RegToRM);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Mov_Ev_Gv))]
        public void Mov_Ev_Gv()
        {
            byte expectedOpByte = 0x89;

            var opCode = OpCode.Create(OpType.Mov, OpCodeDirection.RegToRM, true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Mov_Gb_Eb))]
        public void Mov_Gb_Eb()
        {
            byte expectedOpByte = 0x8A;

            var opCode = OpCode.Create(OpType.Mov, OpCodeDirection.RMToReg);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Mov_Gv_Ev))]
        public void Mov_Gv_Ev()
        {
            byte expectedOpByte = 0x8B;

            var opCode = OpCode.Create(OpType.Mov, OpCodeDirection.RMToReg, true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }

        [Fact(DisplayName = nameof(Mov_Gw_Ew), Skip = "Why is this wrong?")]
        public void Mov_Gw_Ew()
        {
            byte expectedOpByte = 0x8C;

            var opCode = OpCode.Create(OpType.Mov, OpCodeDirection.RMToReg, true);

            var opByte = opCode.GetBytes().Single();
            Assert.Equal(expectedOpByte, opByte);
        }
    }
}
