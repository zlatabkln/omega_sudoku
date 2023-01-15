using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    partial class Solution
    {
        public void addConstraints(List<Constraint> lst,int N, string type, Factory f)
        {
            int par1 = 0;
            int startPar2;
            int end = N;
            if (type == "rc")
                startPar2 = 0;
            else
            {
                startPar2 = 1;
                end++;
            }
            for (par1 = 0; par1 < N; par1++)
            {
                for (int par2 = startPar2; par2 < end; par2++)
                   lst.Add( f.createConstraint(type, par1, par2));
            }
        }
        public void addVariants(Dictionary<Variant,List<Constraint>> dict, List<Constraint> X,int N,int R,int C, Factory f)
        {
            List<Constraint> lst = new List<Constraint>();
            int interval = (int)Math.Pow(N, 2);
            int indexRC = 0;
            int indexRN = interval;
            int indexCN = interval * 2;
            int indexBN = interval * 3;
            Variant key;
            for (int r = 0; r < N; r++)
            {
                indexCN = interval * 2;
                indexRC = 0;
                for (int c = 0; c < N; c++)
                {
                    indexBN = interval * 3;
                    indexRN = interval;
                    for (int n = 1; n < (N + 1); n++)
                    {                       
                        int b = (r / R) * R + (c / C);
                        key = f.createVariant(r, c, n);
                        dict.Add(key, new List<Constraint> { X[indexRC], X[indexRN], X[indexCN], X[indexBN+b]});
                        indexRN++;
                        indexCN++;
                        indexBN++;                       
                    }
                    indexRC++;
                }
                
            }
        }
        public Dictionary<Constraint,HashSet<Variant>> exact_cover(List<Constraint> X, Dictionary<Variant, List<Constraint>> Y)
        {
            Dictionary<Constraint, HashSet<Variant>> XNew = X.ToDictionary(x => x, x => new HashSet<Variant>());
            foreach (var (i, row) in Y)
            {
                foreach (Constraint j in row)
                {
                    XNew[j].Add(i);
                }
            }
            return XNew;
        }

        public List<HashSet<Variant>> select(Dictionary<Constraint, HashSet<Variant>> X, Dictionary<Variant, List<Constraint>> Y, Variant r)
        {
            /*var XNew = new List<Tuple<string, Tuple<int, int>>>();
            var YNew = new Dictionary<Tuple<int, int, int>, List<Tuple<string, Tuple<int, int>>>>();
            foreach (var c in Y[r])
            {
                XNew.Add(c);
                foreach (var r2 in X.Where(x => x.Equals(c)).SelectMany(x => Y[x]))
                {
                    YNew[r2] = Y[r2].Where(x => !x.Equals(c)).ToList();
                }
            }
            return XNew;*/
            List<HashSet<Variant>> cols = new List<HashSet<Variant>>();
            HashSet<Variant> temp=new HashSet<Variant>();
            foreach (Constraint c1 in Y[r])
            {
                foreach (Variant v in X[c1])
                {
                    foreach (Constraint c2 in Y[v])
                    {
                        if (!Equals(c1, c2))
                            X[c2].Remove(v);
                    }
                    X.Remove(c1, out temp);
                }
                cols.Add(temp);
            }
            return cols;
        }

        public void deselect(Dictionary<Constraint, HashSet<Variant>> X, Dictionary<Variant, List<Constraint>> Y, Variant r, List<HashSet<Variant>> cols)
        {
            foreach (Constraint c in Y[r])
            {
                
            }
        }
        /* private void rangeOfCells(List<BasicCell> X, Range r1, Range r2, string cellType)
         {
             for(int val1 = r1.Start.Value; val1 < r1.End.Value; val1++)
             {
                 for(int val2 = r2.Start.Value; val2 < r2.End.Value; val2++)
                 {
                     X.Add(new Cell(cellType,val1,val2))
                 }
             }
         }*/
        public IEnumerable<Variant> solve(Dictionary<Constraint, HashSet<Variant>> X, Dictionary<Variant, List<Constraint>> Y, List<Variant> solution)
        {
            if (!X.Any())
            {
                foreach (Variant r in solution)
                {
                    yield return r;
                }
                    
            }
            else
            {
                List<HashSet<Variant>> cols;
                KeyValuePair<Constraint,HashSet<Variant>> c = X.OrderBy(t => X[t.Key].Count).First();
                foreach (Variant r in X[c.Key].ToList())
                {
                    solution.Add(r);
                    cols=select(X, Y, r);
                    foreach (Variant result in solve(X, Y, solution))
                    {
                        yield return result;
                    }
                    deselect(X, Y, r,cols);
                    solution.Remove(r);
                }
            }
        }

    }
}
