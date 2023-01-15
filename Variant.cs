using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Variant
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
    }
}
