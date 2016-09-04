using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace x86Encoder.Tests
{
    public class InstructionGeneratorTest
    {
        [Fact]
        public void EmitOpCode()
        {
            var instructionGenerator = new InstructionGenerator();
            instructionGenerator.Emit(OpCode.Create(OpType.Ret));
        }

        [Fact]
        public void EmitOpCodeWithModRegRM()
        {
            var instructionGenerator = new InstructionGenerator();
            //instructionGenerator.Emit(OpCode.Create(OpType.Ret), new ModRegRM());
        }
    }
}
