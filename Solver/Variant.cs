using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //variants are representation of a possible value in a specific cell
    public class Variant
    {
        private int row;
        private int col;
        private int val;
        public Variant(int row, int col, int val)
        {
            this.row = row;
            this.col = col;
            this.val = val;
        }
        public int getRow()
        {
            return this.row;
        }
        public int getCol()
        {
            return this.col;
        }
        public int getValue()
        {
            return this.val;
        }

        //override equals and getHashCode to use dictionary with variant key
        public override bool Equals(object obj)
        {
            Variant v = obj as Variant;

            if (v == null)
            {
                return false;
            }
            if (v == this)
            {
                return true;
            }
            return v.getRow() == this.getRow() && v.getCol() == this.getCol() && v.getValue() == this.getValue();
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 37;
                hash = hash * 31 + row.GetHashCode();
                hash = hash * 31 + col.GetHashCode();
                hash = hash * 31 + val.GetHashCode();
                return hash;
            }
        }
    }
}
