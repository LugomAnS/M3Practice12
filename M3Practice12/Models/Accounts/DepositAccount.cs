using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    internal class DepositAccount : AccountBase , IAccountReplenishment<DepositAccount>
    {
        private double interestRate = 5.00;

        public double InterestRate
        {
            get => interestRate;
            set => interestRate = value;
        }

        public DateTime ExpirationTime { get; set; }

        public DepositAccount()
        {
            CreationDate = DateTime.Now;
            ExpirationTime = new DateTime(
                DateTime.Now.Year,
                DateTime.Now.Month + 3,
                DateTime.Now.Day
                );
        }

        public void ReplenishmentAccount(double ammount)
        {
            Balance += ammount;
        }
    }
}
