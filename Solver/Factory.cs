using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //factory is used to create objects of two types - constraints and variants
    public class Factory
    {
        //function to create constraints with given fields
        public Constraint createConstraint(string cType, int par1, int par2)
        {
            return new Constraint(cType, par1, par2);
        }
        //function to create variants with given fields
        public Variant createVariant(int row, int col, int val)
        {
            return new Variant(row, col, val);
        }
    }
}
