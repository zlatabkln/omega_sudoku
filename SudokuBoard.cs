using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuBoard
    {
        //class validates the board - checks if there are no dublicates in rows, columns and boxes
        private int boxSize;
        private int[,] boardMat;
        public SudokuBoard(int size, int[,] boardMat)
        {
            try
            {
                double boxSize = Math.Sqrt(size);
                //check the dimentions of matrix
                if (Math.Round(boxSize) != boxSize)
                    throw new SudokuError("Bad dimensions");
                this.boxSize = (int)boxSize;
                //validations
                validateRows(boardMat, size);
                validateCols(boardMat, size);
                validateBoxes(boardMat, this.boxSize);
                this.boardMat = boardMat;
            }
            catch (SudokuError se)
            {
                Console.WriteLine(string.Format("{0}:{1}", se.GetType(), se.Message));
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

        //validate rows - check about each row that there is no duplicates
        public static void validateRows(int[,] board, int size)
        {
            List<int> valInRow = new List<int>();
            for (int i = 0; i < size; i++)
            {
                valInRow.Clear();
                for (int j = 0; j < size; j++)
                {
                    //add all the existing values to the list
                    if (board[i, j] != 0)
                        valInRow.Add(board[i, j]);
                }
                //check if all the values are unique
                if (valInRow.Count != valInRow.Distinct().Count())
                    throw new SudokuError("Wrong board - there are duplicates in same row");
            }
        }

        //validate columns - check about each column that there is no duplicates
        public static void validateCols(int[,] board, int size)
        {
            List<int> valInCol = new List<int>();
            for (int i = 0; i < size; i++)
            {
                valInCol.Clear();
                for (int j = 0; j < size; j++)
                {
                    //add all the existing values to the list
                    if (board[j, i] != 0)
                        valInCol.Add(board[j, i]);
                }
                //check if all the values are unique
                if (valInCol.Count != valInCol.Distinct().Count())
                    throw new SudokuError("Wrong board - there are duplicates in same column");
            }
        }

        //validate boxess - check about each box that there is no duplicates
        public static void validateBoxes(int[,] board, int size)
        {
            for (int i = 0; i < Math.Pow(size, 2); i += size)
            {
                for (int j = 0; j < Math.Pow(size, 2); j += size)
                {
                    //check single box
                    validateSingleBox(board, i, j, size);
                }
            }
        }

        //check about a single box that there is no duplicates
        public static void validateSingleBox(int[,] board, int startRow, int startCol, int size)
        {
            List<int> valInBox = new List<int>();
            for (int i = startRow; i < size; i++)
            {
                for (int j = startCol; j < size; j++)
                {
                    //add all the existing values to the list
                    if (board[i, j] != 0)
                        valInBox.Add(board[i, j]);
                }

            }
            //check if all the values are unique
            if (valInBox.Count != valInBox.Distinct().Count())
                throw new SudokuError("Wrong board - there are duplicates in same box");
        }
    }
}
