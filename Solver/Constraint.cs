using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //constraints are rules for the board
    //there are four of them:
    //1.In single cell there is a single value
    //2.In single row each character appears exactly once
    //3.In single column each character appears exactly once
    //4.In single box each character appears exactly once
    //this class represents those constraints
    public class Constraint
    {
        //there are four types( as said before):
        //rc - cell constraint(1)
        //rn - row constraint(2)
        //cn - column constraint(3)
        //bn - box constraint(4)
        private string conType;
        //in rc par1 is the row index, par2 - column index
        //in other types, par1 - the constraint index, par2 - a required value
        private int par1;
        private int par2;
        public Constraint(string conType, int par1, int par2)
        {
            this.conType = conType;
            this.par1 = par1;
            this.par2 = par2;
        }
        public int getPar1()
        {
            return this.par1;
        }
        public int getPar2()
        {
            return this.par2;
        }
        public string getType()
        {
            return this.conType;
        }

        //override equals and getHashCode to use the dictionary with a constraint key
        public override bool Equals(object obj)
        {
            Constraint c = obj as Constraint;

            if (c == null)
            {
                return false;
            }
            if (c == this)
            {
                return true;
            }
            return c.getPar1() == this.getPar1() && c.getPar2() == this.getPar2() && c.getType().Equals(this.getType());
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + par1.GetHashCode();
                hash = hash * 23 + par2.GetHashCode();
                hash = hash * 23 + conType.GetHashCode();
                return hash;
            }
        }
    }
}
