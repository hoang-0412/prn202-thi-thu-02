using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal interface IBorrowable
    {
        public int CurrentLoans { get; }
        public bool BorrowBooks(int quantity);
    }
}
