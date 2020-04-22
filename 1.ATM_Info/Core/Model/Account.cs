using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.ATM_Info.Core.Model
{
    class Account
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public decimal Amount(List<CashFlow> cashFlow)
        {
            if (cashFlow is null)
            {
                throw new ArgumentNullException(nameof(cashFlow));
            }

            return cashFlow.Where(x => x.AccountId == Id && x.Type == CashFlowType.Deposit).Sum(x => x.Amount) - 
                cashFlow.Where(x => x.AccountId == Id && x.Type == CashFlowType.Withdrawals).Sum(x => x.Amount);
        }

        public static Account FromData(int id, DateTime date, int userId)
        {
            return new Account()
            {
                Id = id,
                Date = date,
                UserId = userId
            };
        }
        public static List<Account> GetDefaultList()
        {
            return new List<Account>()
            {
                Account.FromData(1, DateTime.Now, 2),
                Account.FromData(2, DateTime.Parse("06.08.2004"), 1),
                Account.FromData(3, DateTime.Parse("11.12.1996"), 3),
                Account.FromData(4, DateTime.Parse("06.06.2000"), 4),
                Account.FromData(5, DateTime.Parse("11.12.2019"), 3),
            };
        }
    }
}
