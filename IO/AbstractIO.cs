using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //This is an abstract class for input and output moduls
    //It's a base class for two types of input: from console and from file
    //Both classes use default output method, but fileinput overrides it and adds writing to file
    public abstract class AbstractIO
    {
        //the input string that program gets from user
        protected string input;
        //method in which program gets input from user
        public abstract string getInput();
        //method which sets the final output - as default, prints it in console
        //(because in both ways of input we need it on console)
        public virtual void setOutput(string output)
        {

            Console.WriteLine(output);

        }

    }
}
