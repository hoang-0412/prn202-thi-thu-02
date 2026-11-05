using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question1
{
    internal class LibraryManager
    {
        private List<StudentMember> _members;
        private LoanChangedCallback _onLoanChanged;

        public LibraryManager(LoanChangedCallback callback)
        {
            _members = new List<StudentMember>();
            _onLoanChanged = callback;
        }

        public void AddMember(StudentMember item)
        {
            _members.Add(item);
        }

        public bool BorrowForMember(string memberCode, int quantity)
        {
            var item = _members.FirstOrDefault(m => m.MemberCode == memberCode);
            if (item == null)
            {
                throw new InvalidOperationException($"Member not found: {memberCode}");
            }

            bool result = item.BorrowBooks(quantity);
            if (result)
            {
                _onLoanChanged?.Invoke(item.FullName, item.CurrentLoans);
            }

            return result;
        }

        public List<StudentMember> GetMembersWithAvailableQuota(int minQuota)
        {
            return _members
                .Where(m => (m.BorrowLimit - m.CurrentLoans) >= minQuota)
                .OrderByDescending(m => m.BorrowLimit - m.CurrentLoans)
                .ToList();
        }
    }
}
