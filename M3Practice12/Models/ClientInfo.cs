using M3Practice12.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models
{
    internal class ClientInfo
    {
        public Client Client { get; set; }

        public SavingAccount? SavingAccount { get; set; }

        public DepositAccount? DepositAccount { get; set; }

        public ClientInfo() { }

        public void Delete<T>(T accountToDelete)
        {
            if (accountToDelete is SavingAccount)
            {
                SavingAccount = null;
            }
            if (accountToDelete is DepositAccount)
            {
                DepositAccount = null;
            }
        }

    }
}
