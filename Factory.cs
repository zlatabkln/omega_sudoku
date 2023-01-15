using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Factory
    {
        public Constraint createConstraint(string cType, int par1, int par2)
        {
            return new Constraint(cType, par1, par2);
        }
        public Variant createVariant(int row, int col, int val)
        {
            return new Variant(row, col, val);
        }
    }
}
