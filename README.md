# x86-encoder
Explores the encoding of x86 machine instructions.

The code is structured as a compiler-backend API.

## Resources

These are some of the resources that was used as a technical reference:

* [Encoding Real x86 Instructions](http://www.c-jump.com/CIS77/CPU/x86/lecture.html)
* [Intel x86 Assembler Instruction Set Opcode Table](http://sparksandflames.com/files/x86InstructionChart.html)

Assemblers and disassemblers used:

* [Compiler Explorer - C++](https://gcc.godbolt.org/)
* [disassembler.io](https://www.onlinedisassembler.com/)
* [Online x86 / x64 Assembler and Disassembler](https://defuse.ca/online-x86-assembler.htm#disassembly)

Other resources that might be useful in the future:
* [Sandpile.org](http://www.sandpile.org/) - The world's leading source for technical x86 processor information
* [x86 Disassembly](https://en.wikibooks.org/wiki/X86_Disassembly) at Wikibooks
* [X86-64 Instruction Encoding](http://wiki.osdev.org/X86-64_Instruction_Encoding)
* [X86 Opcode and Instruction Reference](http://ref.x86asm.net/)
* [x86 Instruction Set Reference](http://x86.renejeschke.de/)
* [x86 Instruction Encoding](https://events.linuxfoundation.org/sites/events/files/slides/bpetkov-x86-hacks.pdf) (SUSE)
* [x86 Registers](http://www.eecg.toronto.edu/~amza/www.mindsec.com/files/x86regs.html)

## Code sample

```csharp
var gen = new InstructionGenerator();
var main = gen.DefineLabel("main");
gen.EmitLabel(main);
gen.Emit(OpCode.Create(OpType.Push_EBP));
gen.Emit(OpCode.Create(OpType.Mov2, OpCodeDirection.RegToRM, true), new ModRegRM(Mod.E4, Register.ESP, Register.EBP));
gen.Emit(OpCode.Create(OpType.Mov, OpCodeDirection.RMToReg), new ModRegRM(Mod.E2, Register.EAX, Register.EBP, + 8));
gen.Emit(OpCode.Create(OpType.Mov, OpCodeDirection.RMToReg), new ModRegRM(Mod.E2, Register.EDX, Register.EBP, + 12));
//gen.Emit(OpCode.Create(OpType.Mov_eAX_lv, OpCodeDirection.RegToRM, isImmediate: true), constant: 2);
//gen.Emit(OpCode.Create(OpType.Mov_eDX_lv, OpCodeDirection.RegToRM, isImmediate: true), constant: 3);
gen.Emit(OpCode.Create(OpType.Add2, OpCodeDirection.RegToRM), new ModRegRM(Mod.E4, Register.EDX, Register.EAX));
gen.Emit(OpCode.Create(OpType.Pop_EBP));
gen.Emit(OpCode.Create(OpType.Ret1, is32bit: false), 8);

var bytes = gen.GetBytes();

Printer.PrintBytes(bytes);
```
