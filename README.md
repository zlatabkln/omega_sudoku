This is Sudoku solver. It is based on algorithm X and represents Sudoku as exact cover problem by using dictionaries and creating a list from parts that fill the missing values without duplicates.
The program runs from main - in main function, the run function is called until user won't coose to exit - so the input can be unlimited.
Input may be given from console or from file, and the output will be written to file and/or shown on console.
The IOHandler creates a matching IO object - for fileIO or consoleIO - and manages them.
Then the converter gets input from a modul (file or console IO object) though handler and converts it to matrix.
The SudokuBoard validates the matrix
Then Solution modul gets the board, solvs it (using functions from AlgorithmXFunctions file) and return a solution (or throws an exception of impossible board)
The solution is converted to string by Converter and this output is handled by IOHandler using matching IO object
