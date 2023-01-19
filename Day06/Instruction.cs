using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    internal class Instruction
    {
        public int cm { get; set; }
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }

        public Instruction(int cm, int x1, int y1, int x2, int y2)
        {
            this.cm = cm;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
    }
}
