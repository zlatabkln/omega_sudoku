using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    //io exceptions - null input or input with "special" tabs (newline, tab and others)
    public class IOError:Exception
    {
        public IOError(string mes)
            :base(mes)
        {
        }
    }
}
