using Question1;
using System;

namespace Question1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoanChangedCallback callback = (memberName, currentLoans) =>
            {
                Console.WriteLine($"[LOAN]: {memberName} now has {currentLoans}");
            };

            var manager = new LibraryManager(callback);

            var m1 = new StudentMember("M001", "Nguyen Van A", 8, 3, "SE");
            var m2 = new StudentMember("M002", "Tran Thi B", 6, 2, "AI");
            var m3 = new StudentMember("M003", "Le Van C", 10, 5, "GD");

            manager.AddMember(m1);
            manager.AddMember(m2);
            manager.AddMember(m3);

            manager.BorrowForMember("M001", 2);
            manager.BorrowForMember("M002", 1);

            try
            {
                manager.BorrowForMember("M001", 0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            try
            {
                manager.BorrowForMember("M999", 2);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("=== Members With At Least 5 Available Quota ===");

            var availableMembers = manager.GetMembersWithAvailableQuota(5);
            int index = 1;
            foreach (var m in availableMembers)
            {
                int availableQuota = m.BorrowLimit - m.CurrentLoans;
                Console.WriteLine($"{index}. {m.FullName} | Code: {m.MemberCode} | Major: {m.Major} | Available: {availableQuota} | Type: {m.GetMemberType()}");
                index++;
            }
        }
    }
}