using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //This class translates between string and matrix representation of the board
    public class Converter
    {
        //size - matrix dimentions
        private int size;
        //board in matrix representation
        private int[,] mat;
        //board as string
        public Converter(string str)
        {
            try
            {
                double size = Math.Sqrt(str.Length);
                //check that string is in valid length to create board
                if (Math.Round(size) != size)
                    throw new SudokuError("Bad dimensions");
                this.size = (int)size;
                //convert given string to matrix
                this.mat = matFromString(str, this.size);
            }
            catch (SudokuError se)
            {
                Console.WriteLine(string.Format("{0}:{1}", se.GetType(), se.Message));
                throw;
            }
        }

        public int getSize()
        {
            return this.size;
        }
        public int[,] getMat()
        {
            return this.mat;
        }

        //converts string to matrix
        public static int[,] matFromString(string str, int size)
        {
            //define future board matrix
            int[,] mat = new int[size, size];
            int i, j, index = 0;
            int numRep;
            //go through the mathrix and fill with int representation of chars in string
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    //get number from char
                    numRep = ((int)str[index]) - 48;
                    //check for unexpected characters
                    if (numRep > size || numRep < 0)
                        throw new SudokuError(string.Format("Unexpected char {0}", str[index]));
                    mat[i, j] = numRep;
                    index++;
                }
            }
            return mat;
        }

        //returns string representation of a solved board
        public string matToString(int[,] mat, int size)
        {
            string res = "";
            int i, j;
            //go through the matrix and add characters represented by numbers to the string result
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    res += (char)(mat[i, j] + 48);
                }
            }
            return res;
        }

    }
}
