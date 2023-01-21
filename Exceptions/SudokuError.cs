using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //exception that are relevant to the board - invalid dimentions, wrong board, impossible board
    public class SudokuError : Exception
    {
        public SudokuError(string mes) : base(mes) { }
    }
}
