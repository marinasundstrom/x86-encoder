using System;
using System.Linq;
using System.Runtime.InteropServices;
using static Test.Memory2;

namespace Test
{
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int Add(int a, int b);
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate int GetValue();
    class MainClass
    {
        public unsafe static void Main(string[] args)
        {
            //NewMethod();

            //return;

            /* 

                https://godbolt.org/g/j7qYrJ

                int add(int a, int b) {
                    return a + b;
                }

            */

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

            var mem = AllocateMemory(bytes, (uint)bytes.Length);
            var func = CreateDelegate<Add>(mem);

            try
            {
                var v = func(2, 3);
                Console.WriteLine(v);
            }
            finally
            {
                VirtualFree(mem, (uint)bytes.Length, FreeType.Release);
            }

            //var gen = new InstructionGenerator();
            //var main = gen.DefineLabel("main");
            //var end = gen.DefineLabel("end");
            //gen.EmitLabel(main);
            //gen.Emit(OpCode.Create(OpType.Push_EBP));
            //gen.Emit(OpCode.Create(OpType.Mov2, OpCodeDirection.RegToRM, true), new ModRegRM(Mod.E4, Register.ESP, Register.EBP));

            //gen.Emit(OpCode.Create(OpType.Mov, OpCodeDirection.RegToRM), new ModRegRM(Mod.E2, Register.EDI, Register.EBP, -4));
            //gen.Emit(OpCode.Create(OpType.Mov, OpCodeDirection.RegToRM), new ModRegRM(Mod.E2, Register.ESI, Register.EBP, -8));
            //gen.Emit(OpCode.Create(OpType.Mov, OpCodeDirection.RMToReg), new ModRegRM(Mod.E2, Register.EBX, Register.EBP, -4));
            //gen.Emit(OpCode.Create(OpType.Mov, OpCodeDirection.RMToReg), new ModRegRM(Mod.E2, Register.EDX, Register.EBP, -8));

            //var jmp1 = gen.DefineLabel("jpm1");

            //gen.Emit(OpCode.Create(OpType.Cmp2), new ModRegRM(Mod.E4, Register.EDX, Register.EAX));
            //gen.Emit(OpCode.Create(OpType.Jz), jmp1);

            //gen.Emit(OpCode.Create(OpType.Mov2, OpCodeDirection.RegToRM), new ModRegRM(Mod.E4, Register.EDX, Register.EAX));

            //gen.Emit(OpCode.Create(OpType.Jmp), end);
            //gen.EmitLabel(jmp1);

            //gen.Emit(OpCode.Create(OpType.Mov2, OpCodeDirection.RegToRM), new ModRegRM(Mod.E4, Register.EBX, Register.EAX));

            //gen.EmitLabel(end);

            //gen.Emit(OpCode.Create(OpType.Pop_EBP));
            //gen.Emit(OpCode.Create(OpType.Ret));
            //var bytes = gen.GetBytes();

            //Printer.PrintBytes(bytes);
        }

        private static unsafe void NewMethod()
        {
            var gen = new InstructionGenerator();
            var main = gen.DefineLabel("main");
            gen.EmitLabel(main);
            gen.Emit(OpCode.Create(OpType.Push_EBP));
            gen.Emit(OpCode.Create(OpType.Dec_EAX));
            gen.Emit(OpCode.Create(OpType.Mov2, OpCodeDirection.RegToRM, true), new ModRegRM(Mod.E4, Register.ESP, Register.EBP));
            gen.Emit(OpCode.Create(OpType.Mov_eAX_lv, OpCodeDirection.RegToRM, isImmediate: true), constant: 42);
            gen.Emit(OpCode.Create(OpType.Pop_EBP));
            gen.Emit(OpCode.Create(OpType.Ret));
            var bytes = gen.GetBytes();

            Printer.PrintBytes(bytes);

            var mem = AllocateMemory(bytes,(uint) bytes.Length + 50);
            var func = CreateDelegate<GetValue>(mem);

            try
            {
                var v = func();
                Console.WriteLine(v);
            }
            finally
            {
                VirtualFree(mem, (uint)bytes.Length, FreeType.Release);
            }
        }

        private static unsafe UIntPtr AllocateMemory(byte[] bytes, uint length)
        {
            var mem = UIntPtr.Zero;
            // http://stackoverflow.com/questions/959087/is-it-possible-to-execute-an-x86-assembly-sequence-from-within-c
            mem = VirtualAlloc(UIntPtr.Zero, length, AllocationType.COMMIT, MemoryProtection.EXECUTE_READWRITE);
            Marshal.Copy(bytes, 0, (IntPtr)(void*)mem, bytes.Length);
            return mem;
        }

        private static unsafe TDelegate CreateDelegate<TDelegate>(UIntPtr mem)
        {
            return (TDelegate)(object)Marshal.GetDelegateForFunctionPointer((IntPtr)(void*)mem, typeof(TDelegate));
        }
    }
}