# x86-encoder
Explores the econding of x86 machine instructions.

The code is structured as a compiler-backend API.


```
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
