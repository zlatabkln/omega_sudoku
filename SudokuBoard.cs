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
        private Dictionary<char,int> boardDict;
        public SudokuBoard(int size, string str) 
        {
            double boxSize = Math.Sqrt(size);
            //validate size
            this.boxSize = (int)boxSize;
            this.boardDict = makeDictInSize(str, this.boxSize);
            this.boardMat = matFromString(str, str.Length, this.boardDict);
        }
        public Dictionary<char,int> getDict()
        {
            return this.boardDict;
        }
        public int getBoxSize()
        {
            return this.boxSize;
        }
        public int[,] getMat()
        {
            return this.boardMat;
        }
        public Dictionary<char,int> makeDictInSize(string str, int size) {
            Dictionary<char,int> dict = new Dictionary<char,int>();
            int index = 1;
            foreach (char ch in str)
            {
                if (!dict.ContainsKey(ch))
                {
                    if (dict.Count == size)
                    {
                        throw new IOException("Too much different characters");
                    }
                    dict.Add(ch,index);
                    index++;
                }
            }
            while (index < size)
            {
                dict.Add((char)index,index);
                index++;
            }
            return dict;
        }
        public int[,] matFromString(string str, int size, Dictionary<char,int> dict) 
        {
            int[,] mat = new int[size,size];
            int i,j,index=0;
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    mat[i, j] = dict[str[index]];
                    index++;
                }
            }
            return mat;
        }
    }
}
