using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    internal class SavingAccount : AccountBase, IAccountReplenishment<SavingAccount>
    {
        public SavingAccount() 
        {
            CreationDate = DateTime.Now;
        }


        public void ReplenishmentAccount(double ammount)
        {
            Balance += ammount;
        }

    }
}
