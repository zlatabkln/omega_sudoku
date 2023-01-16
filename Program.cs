using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku
{
    class Program
    {
        public static void Main()
        {
            //gets input until user wants to exit
            bool exit = false;
            while (!exit)
            {
                exit = run();
            }
        }
        public static bool run()
        {
            //bool that shows if we need to start another run
            bool exit = false;

            Console.WriteLine("Hello! This is SudokuSolver. Please, choose one of the options below:");
            Console.WriteLine("1.New Game\n2.Exit\nYour choice:\n");
            string choice = Console.ReadLine();
            //validate that user stays in these two options
            while (!new string[2] { "1", "2" }.Contains(choice))
            {
                Console.WriteLine("Please, enter option's number:");
                choice = Console.ReadLine();
            }
            if (choice == "2")
                //exit - stop the run, return to main and end the program
                exit = true;
            else
            {
                //new game
                try
                {
                    //ask user for way to get input and get input
                    IOHandler ih = new IOHandler();
                    //convert to matrix
                    Converter c = new Converter(ih.getInput());
                    //validate board
                    SudokuBoard board = new SudokuBoard(c.getSize(), c.getMat());
                    //solve
                    Solution sol = new Solution(board.getMat(), board.getBoxSize());
                    //return to user
                    string output = c.matToString(sol.getSolution(), c.getSize());
                    ih.handler.setOutput(output);
                }
                //in case of exception, start a new run (messages are shown before)
                catch (Exception e)
                {
                    return exit;
                }
            }
            return exit;
        }


    }
}
