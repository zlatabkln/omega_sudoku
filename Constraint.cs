using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Constraint
    {
        private string conType;
        private int par1;
        private int par2;
        public Constraint(string conType, int par1, int par2)
        {
            this.conType = conType;
            this.par1 = par1;
            this.par2 = par2;
        }
    }
}
