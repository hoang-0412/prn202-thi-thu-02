using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal class StudentMember : MemberRecord, IBorrowable
    {
        public string Major { get; set; }

        public StudentMember(string memberCode, string fullName, int borrowLimit, int currentLoans, string major)
            : base(memberCode, fullName, borrowLimit, currentLoans)
        {
            Major = major;
        }

        public override string GetMemberType()
        {
            return "Student";
        }

        public bool BorrowBooks(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Borrow quantity must be positive");
            }

            if (CurrentLoans + quantity > BorrowLimit)
            {
                return false;
            }

            CurrentLoans += quantity;
            return true;
        }
    }
}
