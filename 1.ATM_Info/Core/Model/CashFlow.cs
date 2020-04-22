using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.ATM_Info.Core.Model
{
    class CashFlow
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public CashFlowType Type { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }
        public static CashFlow FromData(int id, DateTime date, CashFlowType type, decimal amount, int accountId)
        {
            return new CashFlow()
            {
                Id = id,
                Date = date,
                Type = type,
                Amount = amount,
                AccountId = accountId
            };
        }
        public static List<CashFlow> GetDefaultList()
        {
            return new List<CashFlow>()
            {
                CashFlow.FromData(1, DateTime.Parse("06.08.2004"), CashFlowType.Deposit, 100, 2),
                CashFlow.FromData(2, DateTime.Parse("06.12.2004"), CashFlowType.Deposit, 2_365, 2),
                CashFlow.FromData(3, DateTime.Parse("08.12.2004"), CashFlowType.Withdrawals, 956.88m, 2),
                CashFlow.FromData(4, DateTime.Parse("12.12.2004"), CashFlowType.Deposit, 678, 2),

                CashFlow.FromData(5, DateTime.Parse("06.08.1997"), CashFlowType.Deposit, 1_000_000, 3),
                CashFlow.FromData(6, DateTime.Parse("22.06.2000"), CashFlowType.Deposit, 95_000, 3),
                CashFlow.FromData(7, DateTime.Parse("08.08.2005"), CashFlowType.Deposit, 55_000_000, 3),
                CashFlow.FromData(8, DateTime.Parse("12.12.2020"), CashFlowType.Withdrawals, 533_689, 3),

                CashFlow.FromData(9, DateTime.Parse("06.08.2001"), CashFlowType.Deposit, 1.56m, 4),
                CashFlow.FromData(10, DateTime.Parse("14.03.2002"), CashFlowType.Deposit, 12_365, 4),
                CashFlow.FromData(11, DateTime.Parse("08.08.2005"), CashFlowType.Withdrawals, 12_165, 4),
                CashFlow.FromData(12, DateTime.Parse("12.12.2009"), CashFlowType.Withdrawals, 50.60m, 4),

                CashFlow.FromData(13, DateTime.Parse("06.08.2001"), CashFlowType.Deposit, 1.56m, 5),
                CashFlow.FromData(14, DateTime.Parse("14.03.2002"), CashFlowType.Deposit, 12_365, 5),
                CashFlow.FromData(15, DateTime.Parse("08.08.2005"), CashFlowType.Withdrawals, 12_165, 5),
                CashFlow.FromData(16, DateTime.Parse("12.12.2009"), CashFlowType.Withdrawals, 50.60m, 5),
            };
        }
    }
}
