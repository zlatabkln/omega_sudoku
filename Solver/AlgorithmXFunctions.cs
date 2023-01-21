using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    //Here are stored some useful functions for solving
    //The solution is based on algorithm X - the sudoku board is represented as exact cover problem
    public partial class Solution
    {
        //The function adds constraints of a specified type to the list
        //in this way the program fills the list of all existing constractions
        public void addConstraints(List<Constraint> lst, int size, string type, Factory f)
        {
            //lst - destination to add constraints, list of constraints 
            //size - box size^2 of the board (size of each dimension)
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
            int end = size;
            //check the type of the constraints and match the indexes
            if (type == "rc")
                startPar2 = 0;
            else
            {
                startPar2 = 1;
                end++;
            }
            //create constraints for the board and add to list - each type has size^2 constraints
            for (par1 = 0; par1 < size; par1++)
            {
                for (int par2 = startPar2; par2 < end; par2++)
                    lst.Add(f.createConstraint(type, par1, par2));
            }
        }

        //The function adds pairs key-value to the dictionary that represents the constraints that specific value
        //in cell can cover
        //key - the variant: cell, represented by row and column, and a value in this cell
        //value - list of constraints this variant covers
        public void addVariants(Dictionary<Variant, List<Constraint>> varDict, int size, int boxHeight, int boxWidth, Factory f)
        {
            Variant key;
            //each value can cover at most 4 constraints - cell, row, column and box
            //so when we fill the dictionary, each value will be a list of 4 constraints
            Constraint rc, rn, cn, bn;
            //in each row there are (size) columns
            //in each column there are (size) cells
            //each cell has (size) possible values
            for (int r = 0; r < size; r++)
            {
                for (int c = 0; c < size; c++)
                {
                    for (int n = 1; n < (size + 1); n++)
                    {
                        //b is an index of a current box
                        int b = (r / boxHeight) * boxWidth + (c / boxWidth);
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
                        varDict.Add(key, new List<Constraint> { rc, rn, cn, bn });
                    }
                }
            }
        }
        //the exact cover function - this function is used to represent the board as exact cover problem
        //the function takes list of all existing constraits and dictionary of values and constraints they cover
        //it returns a dictionary that shows for each constraint the values that cover it 
        //key: constraint 
        //value: list of covering values for the constraint
        public Dictionary<Constraint, List<Variant>> exact_cover(List<Constraint> listConstr, Dictionary<Variant, List<Constraint>> varDict)
        {
            //convert list of constraints to dictionary
            //now each constraint has an empty list of values that cover it
            Dictionary<Constraint, List<Variant>> constrDict = listConstr.ToDictionary(x => x, x => new List<Variant>());
            //the program goes through the constraints in dictionary of variants, and adds its key (the covering variant) to list of every constraint existing in variant's value list
            foreach (var (i, row) in varDict)
            {
                foreach (Constraint j in row)
                {
                    constrDict[j].Add(i);
                }
            }
            return constrDict;
        }

        //select function is used when we fill a cell with value
        //now all the constraints that were covered with this value don't need to be covered
        //so the program erases them from a dictionary of uncovered constraints and updates the possible values of other constraints
        //it returns a list of possible values to cover these constraits
        //(in case the first attempt to solve the board didn't work out and we need to go back)
        public List<List<Variant>> select(Dictionary<Constraint, List<Variant>> constrDict, Dictionary<Variant, List<Constraint>> varDict, Variant currVariant)
        {
            //define a list of deleted values from the cover dictionary (dictionary of constraints)
            List<List<Variant>> cols = new List<List<Variant>>();
            //temp is the other possible values of that exact constrait - it has to be safe in case we need to restart the solving
            List<Variant> temp = new List<Variant>();
            //go through the dictionaries and move irrelevant variant lists to cols
            foreach (Constraint c1 in varDict[currVariant])
            {
                foreach (Variant v in constrDict[c1])
                {
                    foreach (Constraint c2 in varDict[v])
                    {
                        if (!c1.Equals(c2))
                            constrDict[c2].Remove(v);
                    }
                }
                constrDict.Remove(c1, out temp);
                cols.Add(temp);
            }
            return cols;
        }

        //deselect function - cancels the choice of value
        //it resets the pairs of constraints and variant lists that were deleted because of variant r
        //back to the uncovered dictionary
        public void deselect(Dictionary<Constraint, List<Variant>> constrDict, Dictionary<Variant, List<Constraint>> varDict, Variant currVariant, List<List<Variant>> cols)
        {
            //to undo the select operation, we must go backwards
            List<Constraint> rY=new List<Constraint>(varDict[currVariant]);
            rY.Reverse();
            foreach (Constraint c1 in rY)
            {
                //reset deleted keyValue Pair of the cell of current variant back to the dictionary
                constrDict[c1]=cols[cols.Count-1];
                cols.RemoveAt(cols.Count - 1);
                //reset rest of the constraint
                foreach (Variant v in constrDict[c1])
                {
                    foreach (Constraint c2 in varDict[v])
                    {
                        //checking that the constraint isn't the cell we returned
                        if (!c1.Equals(c2))
                        {
                            //if this is the first Variant to reassign to this constraint, we need to create a new value (new list of variants)
                            if (constrDict.ContainsKey(c2))
                                constrDict[c2].Add(v);
                            else
                                constrDict.Add(c2, new List<Variant> { v });
                        }


                    }
                }
            }

        }


        //the solving function - reccursive function that solves the board as exact cover problem
        List<Variant> solve(Dictionary<Constraint, List<Variant>> constrDict, Dictionary<Variant, List<Constraint>> varDict, List<Variant> solutions)
        {
            //if uncovered dictionary is null - the solution is found
            if (!constrDict.Any())
            {
                return solutions;
            }
            else
            {
                //start with the constraint that has the least count of possible values
                KeyValuePair<Constraint, List<Variant>> c = constrDict.OrderBy(t => constrDict[t.Key].Count).First();
                //start to go through the variants of that constraint and tries to solve the board
                foreach (Variant curr in c.Value.ToList())
                {
                    //add current variant to the solutions
                    solutions.Add(curr);
                    //update the possibilities of other constraints and erase the current one
                    var cols = select(constrDict, varDict, curr);

                    //go in reccursion
                    var s = solve(constrDict, varDict, solutions);
                    //if s isn't null - the solution's found
                    if (s.Count() > 0)
                    {
                        return s;
                    }
                    //if not - the solution way must be wrong
                    //we need to start over with another value
                    deselect(constrDict, varDict, curr, cols);
                    solutions.RemoveAt(solutions.Count - 1);
                }
            }
            if (!constrDict.Any())
                return solutions;
            return new List<Variant>();
        }
    }


}


