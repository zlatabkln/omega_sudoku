using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    //Here are stored some useful functions for solving
    //The solution is based on algorithm X - the sudoku board is represented as exact cover problem
    partial class Solution
    {
        //The function adds constraints of a specified type to the list
        //in this way the program fills the list of all existing constractions
        public void addConstraints(List<Constraint> lst, int N, string type, Factory f)
        {
            //lst - destination to add constraints, list of constraints 
            //N - size^2 of the board (number of cells)
            //type - required type of constraint
            //f - factory to create the constraints
            //par1 goes in range 0-N in all constraints
            //it represents row in "rc" type constraints (row-column - the cells of the board)
            //and index of constraint in over types (rows, columns and boxes)
            int par1 = 0;
            //par2 goes in range 0-N in "rc" constraints - represents the column
            //in other types it represents a possible value in constraint and goes in range 1-(N+1)
            int startPar2;
            //end - the last possible value for par2 (N - for "rc", and N+1 - for others) 
            int end = N;
            //check the type of the constraints and match the indexes
            if (type == "rc")
                startPar2 = 0;
            else
            {
                startPar2 = 1;
                end++;
            }
            //create constraints for the board and add to list - each type has size^2 constraints
            for (par1 = 0; par1 < N; par1++)
            {
                for (int par2 = startPar2; par2 < end; par2++)
                    lst.Add(f.createConstraint(type, par1, par2));
            }
        }

        //The function adds pairs key-value to the dictionary that represents the constraints that specific value
        //in cell can cover
        //key - the variant: cell, represented by row and column, and a value in this cell
        //value - list of constraints this variant covers
        public void addVariants(Dictionary<Variant, List<Constraint>> dict, List<Constraint> X, int N, int R, int C, Factory f)
        {
            Variant key;
            //each value can cover at most 4 constraints - cell, row, column and box
            //so when we fill the dictionary, each value will be a list of 4 constraints
            Constraint rc, rn, cn, bn;
            //in each row there are N columns
            //in each column there are N cells
            //each cell has N possible values
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    for (int n = 1; n < (N + 1); n++)
                    {
                        //b is an index of a current box
                        int b = (r / R) * R + (c / C);
                        // key - variant: cell in row r column c with value n
                        key = f.createVariant(r, c, n);
                        //cell constraint - the coordinates of the cell we filled with this variant
                        rc = f.createConstraint("rc", r, c);
                        //row constraint - row index of the cell and the value
                        rn = f.createConstraint("rn", r, n);
                        //column constraint - column index of the cell and the value
                        cn = f.createConstraint("cn", c, n);
                        //box constraint - box index of the cell and the value
                        bn = f.createConstraint("bn", b, n);
                        //adding new pair to the dictionary
                        dict.Add(key, new List<Constraint> { rc, rn, cn, bn });
                    }
                }
            }
        }
        //the exact cover function - this function is used to represent the board as exact cover problem
        //the function takes list of all existing constraits and dictionary of values and constraints they cover
        //it returns a dictionary that shows for each constraint the values that cover it 
        //key: constraint 
        //value: list of covering values for the constraint
        public Dictionary<Constraint, List<Variant>> exact_cover(List<Constraint> X, Dictionary<Variant, List<Constraint>> Y)
        {
            //convert list of constraints to dictionary
            //now each constraint has an empty list of values that cover it
            Dictionary<Constraint, List<Variant>> XNew = X.ToDictionary(x => x, x => new List<Variant>());
            //the program goes through the constraints in dictionary y, and adds its key (the covering variant) to the list
            foreach (var (i, row) in Y)
            {
                foreach (Constraint j in row)
                {
                    XNew[j].Add(i);
                }
            }
            return XNew;
        }

        //select function is used when we fill a cell with value
        //now all the constraints that were covered with this value don't need to be covered
        //so the program erases them from a dictionary of uncovered constraints and updates the possible values of other constraints
        //it returns a list of possible values to cover these constraits
        //(in case the first attempt to solve the board didn't work out and we need to go back)
        public List<List<Variant>> select(Dictionary<Constraint, List<Variant>> X, Dictionary<Variant, List<Constraint>> Y, Variant r)
        {
            //define a list of deleted values from the cover dictionary
            List<List<Variant>> cols = new List<List<Variant>>();
            //temp is the other possible values of that exact constrait - it has to be safe in case we need to restart the solving
            List<Variant> temp = new List<Variant>();
            //go through the dictionaries and move irrelevant variant lists to cols
            foreach (Constraint c1 in Y[r])
            {
                foreach (Variant v in X[c1])
                {
                    foreach (Constraint c2 in Y[v])
                    {
                        if (!c1.Equals(c2))
                            X[c2].Remove(v);
                    }
                }
                X.Remove(c1, out temp);
                cols.Add(temp);
            }
            return cols;
        }

        //deselect function - cancels the choice of value
        //it resets the pairs of constraints and variant lists that were deleted because of variant r
        //back to the uncovered dictionary
        public void deselect(Dictionary<Constraint, List<Variant>> X, Dictionary<Variant, List<Constraint>> Y, Variant r, List<List<Variant>> cols)
        {
            foreach (Constraint c1 in Y[r])
            {
                //reset deleted keyValue Pair of the cell in Variant r back to the dictionary
                X[c1] = cols[0];
                cols.RemoveAt(0);
                //reset rest of the constraint
                foreach (Variant v in X[c1])
                {
                    foreach (Constraint c2 in Y[v])
                    {
                        //checking that the constraint isn't the cell we returned
                        if (!c1.Equals(c2))
                        {
                            //if this is the first Variant to reassign to this constraint, we need to create a new value 0 new list of variants
                            if (X.ContainsKey(c2))
                                X[c2].Add(v);
                            else
                                X.Add(c2, new List<Variant> { v });
                        }


                    }
                }
            }

        }


        //the solving function - reccursive function that solves the board as exact cover problem
        List<Variant> solve(Dictionary<Constraint, List<Variant>> X, Dictionary<Variant, List<Constraint>> Y, List<Variant> solutions)
        {
            //if uncovered dictionary is null - the solution is found
            if (!X.Any())
            {
                return solutions;
            }
            else
            {
                //start with the constraint that has the least count of possible values
                KeyValuePair<Constraint, List<Variant>> c = X.OrderBy(t => X[t.Key].Count).First();
                //start to go through the variants of that constraint and tries to solve the board
                foreach (Variant r in c.Value.ToList())
                {
                    //add current variant to the solutions
                    solutions.Add(r);
                    //update the possibilities of other constraints and erase the current one
                    var cols = select(X, Y, r);

                    //go in reccursion
                    var s = solve(X, Y, solutions);
                    //if s isn't null - the solution's found
                    if (s.Count() > 0)
                    {
                        return s;
                    }
                    //if not - the solution way must be wrong
                    //we need to start over with another value
                    deselect(X, Y, r, cols);
                    if (c.Value.Count > 1)
                        c.Value.Remove(r);
                    solutions.RemoveAt(solutions.Count - 1);
                }
            }
            if (!X.Any())
                return solutions;
            return new List<Variant>();
        }
    }


}


