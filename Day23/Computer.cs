namespace Day23
{
    internal class Computer
    {
        public int PC { get; set; }
        public int A { get; set; }
        public int B { get; set; }
        public List<string> Program { get; set; }

        public Computer()
        {
            PC = 0;
            A = 0;
            B = 0;
            Program = new List<string>();
        }

        public void Reset()
        {
            PC = 0;
            A = 0;
            B = 0;
        }

        public void ReadProgram(string[] program)
        {
            Program = program.ToList();
        }

        public void RunProgram()
        {
            PC = 0;
            
            while (PC < Program.Count)
            {
                string instruction = Program[PC].Replace(",", "");
                string[] parts = instruction.Split(' ');
                
                int reg = parts[1] == "a" ? 0 : 1;
                int offset;

                //Console.Write($"PC:{PC} A:{A,-5} B:{B,-5} -- ");
                switch (parts[0])
                {
                    case "hlf":
                        //Console.WriteLine($"opcode:{parts[0]} reg:{reg}");
                        ExecHLF(reg);
                        break;
                    case "tpl":
                        //Console.WriteLine($"opcode:{parts[0]} reg:{reg}");
                        ExecTPL(reg);
                        break;
                    case "inc":
                        //Console.WriteLine($"opcode:{parts[0]} reg:{reg}");
                        ExecINC(reg);
                        break;
                    case "jmp":
                        offset = Convert.ToInt32(parts[1]);
                        //Console.WriteLine($"opcode:{parts[0]} offset:{offset}");
                        ExecJMP(offset);
                        break;
                    case "jie":
                        offset = Convert.ToInt32(parts[2]);
                        //Console.WriteLine($"opcode:{parts[0]} reg:{reg} offset:{offset}");
                        ExecJIE(reg, offset);
                        break;
                    case "jio":
                        offset = Convert.ToInt32(parts[2]);
                        //Console.WriteLine($"opcode:{parts[0]} reg:{reg} offset:{offset}");
                        ExecJIO(reg, offset);
                        break;
                    default:
                        Console.WriteLine("Error: Unknown instruction");
                        break;
                }

            }

            //Console.WriteLine("Run complete");
        }

        private void ExecHLF(int reg)
        {
            if (reg == 0)
                A /= 2;
            else
                B /= 2;

            PC++;
        }

        private void ExecTPL(int reg)
        {
            if (reg == 0)
                A *= 3;
            else
                B *= 3;

            PC++;
        }

        private void ExecINC(int reg)
        {
            if (reg == 0)
                A++;
            else 
                B++;

            PC++;
        }

        private void ExecJMP(int offset)
        {
            int tmpPC = PC + offset;

            if (tmpPC < 0 || tmpPC > Program.Count)
            {
                Console.WriteLine($"Error: Invalid address {tmpPC}");
                return;
            }

            // jump
            PC = tmpPC;
        }

        private void ExecJIE(int reg, int offset)
        {
            int tmpPC = PC + offset;
            int tmpReg = (reg == 0) ? A : B;

            if (tmpReg % 2 != 0)
            {
                PC++;
                return;
            }

            if (tmpPC < 0 || tmpPC > Program.Count)
            {
                Console.WriteLine($"Error: Invalid address {tmpPC}");
                return;
            }

            // reg value is even, jump
            PC = tmpPC;
        }

        private void ExecJIO(int reg, int offset)
        {
            int tmpPC = PC + offset;
            int tmpReg = (reg == 0) ? A : B;

            if (tmpReg != 1)
            {
                PC++;
                return;
            }

            if (tmpPC < 0 || tmpPC > Program.Count)
            {
                Console.WriteLine($"Error: Invalid address {tmpPC}");
                return;
            }

            // reg value is 1, jump
            PC = tmpPC;
        }
    }
}
