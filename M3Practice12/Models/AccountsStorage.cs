using M3Practice12.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace M3Practice12.Models
{
    public class AccountsStorage<T> : IAccountTransaction<T>
        where T: AccountBase
    {
        private List<AccountBase> allAccounts;

        public List<AccountBase> AllAccounts
        {
            get => allAccounts;
            set => allAccounts = value;
        }
        public T AddValue { set => AllAccounts.Add(value); }

        public AccountsStorage()
        {
            AllAccounts = new List<AccountBase>();
        }

        public void Transaction(T accountToWithdraw, T accountToFill, double ammount)
        {
            accountToWithdraw.Balance -= ammount;
            accountToFill.Balance += ammount;
        }
    }
}
