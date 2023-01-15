using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuBoard
    {
        private int boxSize;
        private int[,] boardMat;
        private HashSet<int> possibleValues;
        public SudokuBoard(int size, int[,] boardMat)
        {
            try
            {
                double boxSize = Math.Sqrt(size);
                if (Math.Round(boxSize) != boxSize)
                    throw new SudokuError("Bad dimensions");
                this.boxSize = (int)boxSize;
                validateRows(boardMat, size);
                validateCols(boardMat,size);
                validateBoxes(boardMat,this.boxSize);
                this.boardMat = boardMat;
            }
            catch (SudokuError se)
            {
                Console.WriteLine(se.Message);
                throw;
            }
        }
        public int[,] getMat()
        {
            return this.boardMat;
        }
        public int getBoxSize()
        {
            return this.boxSize;
        }
        public static void validateRows(int[,] board, int size)
        {
            List<int> valInRow = new List<int>();
            for (int i = 0; i < size; i++)
            {
                valInRow.Clear();
                for (int j = 0; j < size; j++)
                {
                    valInRow.Add(board[i, j]);
                }
                if (valInRow.Count != valInRow.Distinct().Count())
                    throw new SudokuError("Wrong board - there are duplicates in same row");
            }
        }
        public static void validateCols(int[,] board, int size)
        {
            List<int> valInCol = new List<int>();
            for (int i = 0; i < size; i++)
            {
                valInCol.Clear();
                for (int j = 0; j < size; j++)
                {
                    valInCol.Add(board[j, i]);
                }
                if (valInCol.Count != valInCol.Distinct().Count())
                    throw new SudokuError("Wrong board - there are duplicates in same column");
            }
        }
        public static void validateBoxes(int[,] board, int size)
        {
            for(int i=0; i < Math.Pow(size,2); i+=size)
            {
                for (int j = 0; j < Math.Pow(size, 2); j += size)
                {
                    validateSingleBox(board, i, j, size);
                }
            }
        }
        public static void validateSingleBox(int[,]board, int startRow,int startCol, int size)
        {
            List<int> valInBox = new List<int>();
            for (int i = startRow; i < size; i++)
            {
                valInBox.Clear();
                for (int j = startCol; j < size; j++)
                {
                    valInBox.Add(board[j, i]);
                }
                if (valInBox.Count != valInBox.Distinct().Count())
                    throw new SudokuError("Wrong board - there are duplicates in same box");
            }
        }
    }    
}
