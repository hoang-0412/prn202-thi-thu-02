using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal interface IBorrowable
    {
        int CurrentLoans { get; }
        bool BorrowBooks(int quantity);
    }
}
