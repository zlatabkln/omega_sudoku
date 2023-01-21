using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //this class creates a solution from board
    public partial class Solution
    {
        private int[,] solution;
        public Solution(int[,] grid, int boxSize)
        {
            //factory - to fill the dictionaries
            Factory f = new Factory();
            //square root from number of rows - box height
            int boxHeight = boxSize;
            //square root from number of columns - box width
            int boxWidth = boxSize;
            //size of each dimension
            int size = boxHeight * boxWidth;
            //list of all the constraints in board
            List<Constraint> listOfConstraints = new List<Constraint>();
            //fill the list
            addConstraints(listOfConstraints, size, "rc", f);
            addConstraints(listOfConstraints, size, "rn", f);
            addConstraints(listOfConstraints, size, "cn", f);
            addConstraints(listOfConstraints, size, "bn", f);
            //dictionary of variants and lists of constraints each variant covers
            Dictionary<Variant, List<Constraint>> varDict = new Dictionary<Variant, List<Constraint>>();
            //fill the dictionary
            addVariants(varDict, size, boxHeight, boxWidth, f);
            //create a dictionary of constraints and lists of values that cover them
            Dictionary<Constraint, List<Variant>> constrDict = exact_cover(listOfConstraints, varDict);
            //delete all the existing values and match the constraints
            //stay with dictionary of uncovered constraints and lists of possible variants to cover them
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] != 0)
                    {
                        select(constrDict, varDict, new Variant(i, j, grid[i, j]));
                    }
                }
            }
            if (constrDict.Any())
            {
                //list for solution
                List<Variant> solutions = new List<Variant>();
                try
                {
                    //solve the board
                    solutions = solve(constrDict, varDict, solutions);
                    //solution not found
                    if (solutions.Count == 0)
                        throw new SudokuError("Impossible to solve");
                    //fill the board with solution
                    foreach (Variant solution in solve(constrDict, varDict, solutions))
                    {
                        grid[solution.getRow(), solution.getCol()] = solution.getValue();
                    }
                }
                //impossible board exception
                catch (SudokuError se)
                {
                    Console.WriteLine(string.Format("{0}:{1}", se.GetType(), se.Message));
                    throw;
                }
            }
            this.solution = grid;
        }
        public int[,] getSolution()
        {
            return this.solution;
        }
    }
}
