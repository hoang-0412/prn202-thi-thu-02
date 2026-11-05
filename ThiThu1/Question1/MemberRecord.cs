using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal abstract class MemberRecord
    {
        public string MemberCode { get; set; }
        public string FullName { get; set; }
        public int BorrowLimit { get; set; }
        public int CurrentLoans { get; set; }

        public MemberRecord(string memberCode, string fullName, int borrowLimit, int currentLoans)
        {
            if (borrowLimit <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(borrowLimit), "BorrowLimit must be greater than 0");
            }

            if (currentLoans < 0 || currentLoans > borrowLimit)
            {
                throw new ArgumentOutOfRangeException(nameof(currentLoans), "CurrentLoans must be between 0 and BorrowLimit");
            }

            MemberCode = memberCode;
            FullName = fullName;
            BorrowLimit = borrowLimit;
            CurrentLoans = currentLoans;
        }

        public abstract string GetMemberType();
    }
}
