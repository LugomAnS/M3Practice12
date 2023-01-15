using M3Practice12.Models;
using M3Practice12.Models.Accounts;
using System;


namespace M3Practice12.Services
{
    internal class InvariantOperations<T>
        where T : AccountBase
    {
        public static T? CreateAccount(AccountType accountType)
        {
            AccountBase newAccount = null;
            switch (accountType)
            {
                case AccountType.Deposit:
                    newAccount = new DepositAccount();
                    break;
                case AccountType.Saving:
                    newAccount = new SavingAccount();
                    break;
                default:
                    
                    break;
            }

            return newAccount as T;
        }

        public static void Exchange (T fromAccount, T whereAccount, double ammount)
        {
            fromAccount.Balance -= ammount;
            whereAccount.Balance += ammount;
        }
    }
}
