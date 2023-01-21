using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.IO;

namespace SudokuTest
{
    [TestClass]
    public class SudokuTest
    {
        [TestMethod]
        public void TestSolve1x1Empty()
        {
            string input = "0";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        
        [TestMethod]
        public void TestSolve4x4Empty()
        {
            string input = "0000000000000000";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolve4x4Regular()
        {
            string input = "0003324003022000";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolve9x9Empty()
        {
            string input = "000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolve9x9Regular()
        {
            string input = "800000070006010053040600000000080400003000700020005038000000800004050061900002000";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolve16x16Empty()
        {
            string input = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolve16x16Regular()
        {
            string input = "00000000000140004900000@:006<7050<70000308@;:0060:>000070002080000003040000@000000:0005<04000?0000?0>06000000490009000000:0=0<00004900;?00:00000010003200;?0000>0000<70030400;?88@00:>00700<0200032008@;>0007100071509008@00>=60:>=05000002000;008000:00<0159000";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolve25x25Empty()
        {
            string input = "0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolve25x25Regular()
        {
            string input = "005DEI0000;9800:070F00000000I0B098G0@F:H0>3?0E0600080B;000F03?0<>5DE20A100I00:0003?=00200000000090G0?=<00D0265A10C00B0900000HF0000000<0005IEBA04C9000;=003?0265I00C0A0098000:00000E2A04C098GH;00@F00=<0000BA0;080H@F0>7030=0200IE000;07@F:0?0<D3I006010CB00DE?0260I00C001090G0F:>0000A2004C000GH003@0:0=0D0?0000098GH0F0>3@E?=0065002G07980F:>3=<D0?A265000000:>00F?0000000A2;040B8G070>0?00=00E050016040B;G00@0D020<65I01CB094@00H7:>0?0IA1654C000G00@8?00000000=0094C8007@:00002=<DE0IA00H700G00>0?<DE0=100IAC0;900@FGH:00?=DE20000IA00;08C0?0:>000000A1408C0;0H7@00E00<D0IA14B;9000G07@030=00145I0B;9800@0G0000?0E00<0080BGH7@F000=:60DE000100";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestSolvedBoard()
        {
            string input = "7=26;@?<4:5>9318;@?<4:5>8931=2674:5>89317=26@?<;89317=26;@?<:5>4=?67@5<;:3>42189@5<;:3>49218?67=:3>49218=?675<;@9218=?67@5<;3>4:2689?<7=5>;@14:3?<7=5>;@314:68925>;@314:2689<7=?314:2689?<7=>;@56792<;=?>4@58:31<;=?>4@518:37926>4@518:36792;=?<18:36792<;=?4@5>";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            Solution sol = new Solution(sb.getMat(), sb.getBoxSize());
            Assert.IsTrue(!(c.matToString(sol.getSolution(), sb.getBoxSize())).Contains("0"));
        }
        [TestMethod]
        public void TestWrongBoardRow()
        {
            string input = "000000110000000000000000000000000000000000000000000000000000000000000000000000000";
            Converter c = new Converter(input);
            var ex = Assert.ThrowsException<SudokuError>(() => new SudokuBoard(c.getSize(), c.getMat()));
            Assert.AreEqual(ex.Message, "Wrong board - there are duplicates in same row");
        }
        [TestMethod]
        public void TestWrongBoardCol()
        {
            string input = "000000010000000010000000000000000000000000000000000000000000000000000000000000000";
            Converter c = new Converter(input);
            var ex = Assert.ThrowsException<SudokuError>(() => new SudokuBoard(c.getSize(), c.getMat()));
            Assert.AreEqual(ex.Message, "Wrong board - there are duplicates in same column");
        }
        [TestMethod]
        public void TestWrongBoardBox()
        {
            string input = "000000010000000001000000000000000000000000000000000000000000000000000000000000000";
            Converter c = new Converter(input);
            var ex = Assert.ThrowsException<SudokuError>(() => new SudokuBoard(c.getSize(), c.getMat()));
            Assert.AreEqual(ex.Message, "Wrong board - there are duplicates in same box");
        }
        [TestMethod]
        public void TestWrongDimensions1()
        {
            string input = "0001200";
            var ex = Assert.ThrowsException<SudokuError>(() => new Converter(input));
            Assert.AreEqual(ex.Message,"Bad dimensions");

        }
        [TestMethod]
        public void TestWrongDimensions2()
        {
            string input = "000100030";
            Converter c = new Converter(input); 
            var ex = Assert.ThrowsException<SudokuError>(() => new SudokuBoard(c.getSize(), c.getMat()));
            Assert.AreEqual(ex.Message, "Bad dimensions");

        }
        [TestMethod]
        public void TestImpossibleBoard()
        {
            string input = "516849732307605000809700065135060907472591006968370050253186074684207500791050608";
            Converter c = new Converter(input);
            SudokuBoard sb = new SudokuBoard(c.getSize(), c.getMat());
            var ex = Assert.ThrowsException<SudokuError>(() => new Solution(sb.getMat(), sb.getBoxSize()));
            Assert.AreEqual(ex.Message, "Impossible to solve");
        }
        [TestMethod]
        public void TestUnexpectedCharacters()
        {
            string input = "0023001000000@00";
            var ex=Assert.ThrowsException<SudokuError>(()=>new Converter(input));
            Assert.IsTrue(ex.Message.Contains("Unexpected char"));
        }
        [TestMethod]
        public void TestNullInput()
        {
            string input = "";
            var ex = Assert.ThrowsException<IOError>(() => new IOHandler(input));
            Assert.AreEqual(ex.Message, "Input was null");
        }
        [TestMethod]
        public void TestFileNotFound()
        {
            string path = "C:\\Users\\Admin\\some_non_existing_file.txt";
            Assert.ThrowsException<FileNotFoundException>(() => new FileInput(path));
        }
        [TestMethod]
        public void TestWriteToReadFile()
        {
            string path = "C:\\Users\\Admin\\source\\repos\\Sudoku\\testfile.txt";
            FileInput fi = new FileInput("C:\\Users\\Admin\\source\\repos\\Sudoku\\testfile.txt");
            Assert.ThrowsException<UnauthorizedAccessException>(() => fi.testSetOutput(path));
        }
        [TestMethod]
        public void TestSpecialTabsInInput()
        {
            string input = "0\t10";
            var ex = Assert.ThrowsException<IOError>(() => new IOHandler(input));
            Assert.AreEqual(ex.Message, "Special tabs in input");
        }
    }
}
