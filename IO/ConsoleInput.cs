using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //this is console type of io
    //inheritates from abstract io type
    public class ConsoleInput : AbstractIO
    {

        public ConsoleInput()
        {
            //in case user wants to enter string from console, he's asked for it here
            Console.WriteLine("Please, enter board's string:\n");

            this.input = Console.ReadLine();

        }
        public override string getInput()
        {
            return this.input;
        }

    }
}
