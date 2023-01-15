using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    partial class Solution
    {
        private int[,] solution;
        public Solution(int[,] grid, int size)
        {
            Factory f = new Factory();
            int R = size;
            int C = size;
            int N = R * C;
            List<Constraint> listOfConstraints = new List<Constraint>();
            addConstraints(listOfConstraints, N, "rc",f);
            addConstraints(listOfConstraints, N, "rn",f);
            addConstraints(listOfConstraints, N, "cn",f);
            addConstraints(listOfConstraints, N, "bn",f);
            Dictionary<Variant, List<Constraint>> varDict = new Dictionary<Variant, List<Constraint>>();
            addVariants(varDict,listOfConstraints, N, R, C, f);
            Dictionary<Constraint, HashSet<Variant>> xDict = exact_cover(listOfConstraints, varDict);
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i,j] != 0)
                    {
                        select(xDict, varDict, new Variant(i, j, grid[i,j]));
                    }
                }
            }
            List<Variant> solutions = new List<Variant>();
            foreach (var solution in solve(xDict, varDict, solutions))
            {
                List<Variant> sol = solve(xDict, varDict, solutions).ToList();
                Variant v;
                for (int i = 0; i < sol.Count(); i++)
                {
                    v=sol[i];
                    grid[v.getRow(),v.getCol()] = v.getValue();
                }
                this.solution = grid;
            }
        }
        public int[,] getSolution()
        {
            return this.solution;
        }
    }
}
