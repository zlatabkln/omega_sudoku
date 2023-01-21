using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //IOHandler works with the user - asks for input and creates the matching io object (file or console)
    //connecting between main program and input moduls
    public class IOHandler
    {
        //given input
        private string input;
        //matching io object to work with the io
        public AbstractIO handler;
        //list of special tabs that are not supposed to be in input
        private static List<char> specialTabs = new List<char> { '\n', '\t', '\v', '\r', ' ' };

        //test function - almost similar to a regular constructor, but without interaction with the user
        //used to test correct exceptions throwing
        public IOHandler(string input)
        {
            this.input = input;
            try
            {
                if (this.input == "")
                    throw new IOError("Input was null");
                foreach (char st in specialTabs)
                {
                    if (this.input.Contains(st))
                        throw new IOError("Special tabs in input");
                }
            }
            catch (IOError ioe)
            {
                Console.WriteLine(string.Format("{0}:{1}", ioe.GetType(), ioe.Message));
                throw;
            }
        }
        public IOHandler()
        {
            //ask for way to get input and create a matching object
            Console.WriteLine("1.Solve from console\n2.Solve from file\nYour choice:\n");
            string choice = Console.ReadLine();
            //check that user chooses one of the options
            while (!new string[2] { "1", "2" }.Contains(choice))
            {
                Console.WriteLine("Please, enter option's number:");
                choice = Console.ReadLine();
            }
            switch (choice)
            {
                //input from console
                case "1":
                    this.handler = new ConsoleInput();
                    this.input = this.handler.getInput();
                    break;
                //input from file
                case "2":
                    this.handler = new FileInput();
                    this.input = this.handler.getInput();
                    break;
            }
            try
            {
                //check that input is not null and doesn't contain special tabs - else throw matching exception
                if (this.input == "")
                    throw new IOError("Input was null");
                foreach (char st in specialTabs)
                {
                    if (this.input.Contains(st))
                        throw new IOError("Special tabs in input");
                }
            }
            catch (IOError ioe)
            {
                Console.WriteLine(string.Format("{0}:{1}", ioe.GetType(), ioe.Message));
                throw;
            }
        }

        public string getInput()
        {
            return this.input;
        }
    }
}
