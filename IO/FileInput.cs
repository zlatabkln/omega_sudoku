using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //for input which is given from file
    //inheritates from abstract io
    public class FileInput : AbstractIO
    {
        //path of file with the input
        private string path;
        public FileInput(string path)
        {
            this.path = path;
            try
            {
                //try reading from file
                StreamReader sr = new StreamReader(this.path);
                string input = sr.ReadLine();
                while (input != null)
                {
                    this.input += input;
                    input = sr.ReadLine();
                }
                sr.Close();
            }
            //wrong filepath
            catch (FileNotFoundException e)
            {
                Console.WriteLine("failed to read file");
                Console.WriteLine(e.Message);
                throw;
            }
            
        }
        public FileInput()
        {
            //user is asked for file path tp get the input from
            Console.WriteLine("Please, enter file's path:\n");
            this.path = Console.ReadLine();
            try
            {
                //try reading from file
                StreamReader sr = new StreamReader(this.path);
                string input = sr.ReadLine();
                while (input != null)
                {
                    this.input += input;
                    input = sr.ReadLine();
                }
                sr.Close();
            }
            //possible exceptions - wrong filepath or file without write premission

            catch (FileNotFoundException e)
            {
                Console.WriteLine("failed to read file");
                Console.WriteLine(e.Message);
                throw;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("failed to read file");
                Console.WriteLine(e.Message);
                throw;
            }

        }

        public override string getInput()
        {
            return this.input;
        }

        //when the board is solved we need to show the solution
        //in any case the solution will be shown on console
        //but if the input was from file, the output'll be also stored to that or other file
        public override void setOutput(string output)
        {
            //print input to the console
            base.setOutput(output);
            //ask for filepath to store the output
            Console.WriteLine("The solution is ready. Please, choose one of the options:\n");
            Console.WriteLine("1.Write to source file\n2.New path");
            string choice = Console.ReadLine();
            //user should choose one of the options
            while (!new string[2] { "1", "2" }.Contains(choice))
            {
                Console.WriteLine("Please, enter option's number:");
                choice = Console.ReadLine();
            }
            string currPath = "";
            StreamWriter sw = null;
            try
            {
                //use the old path
                if (choice == "1")
                    currPath = this.path;
                else
                {
                    //get new path
                    Console.WriteLine("Please, enter file's path:\n");
                    currPath = Console.ReadLine();
                }
                //try to write to the file
                sw = new StreamWriter(currPath);
                sw.Write(output);
            }
            //catch possible exceptions
            catch (FileNotFoundException e)
            {
                Console.WriteLine("failed to read file");
                Console.WriteLine(e.Message);
                throw;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("failed to read file");
                Console.WriteLine(e.Message);
                throw;
            }
            catch (UnauthorizedAccessException e)
            {
                FileAttributes attr = (new FileInfo(currPath)).Attributes;
                Console.Write("UnAuthorizedAccessException: Unable to access file. ");
                if ((attr & FileAttributes.ReadOnly) > 0)
                    Console.Write("The file is read-only.");
                throw;
            }

            //close the file
            finally
            {
                if (sw != null) sw.Close();
            }
        }

        //test function - alike to setOutput function, but without interaction with user
        //only to test throwing UnauthorizedAccessException 
        public void testSetOutput(string path)
        {
            string currPath = "";
            StreamWriter sw = null;
            try
            {
                currPath = path;
                //try to write to the file
                sw = new StreamWriter(currPath);
                sw.Write("Test");
            }
            //catch possible exceptions
            catch (FileNotFoundException e)
            {
                Console.WriteLine("failed to read file");
                Console.WriteLine(e.Message);
                throw;
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("failed to read file");
                Console.WriteLine(e.Message);
                throw;
            }
            catch (UnauthorizedAccessException e)
            {
                FileAttributes attr = (new FileInfo(currPath)).Attributes;
                Console.Write("UnAuthorizedAccessException: Unable to access file.\n ");
                if ((attr & FileAttributes.ReadOnly) > 0)
                    Console.Write("The file is read-only.\n");
                throw;
            }

            //close the file
            finally
            {
                if (sw != null) sw.Close();
            }
        }
    }
}

