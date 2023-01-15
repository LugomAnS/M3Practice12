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


        /// <summary>
        /// Удаление счета
        /// </summary>
        /// <typeparam name="T">AccountBase</typeparam>
        /// <param name="accountToDelete">Счет для закрытия</param>
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

        public void Exchange<T>(T accountFrom, double ammountToWithDraw)
        {
            if (accountFrom is SavingAccount)
            {
                SavingAccount.Balance -= ammountToWithDraw;
                DepositAccount.Balance += ammountToWithDraw;
            }
            if (accountFrom is DepositAccount)
            {
                DepositAccount.Balance -= ammountToWithDraw;
                SavingAccount.Balance += ammountToWithDraw;
            }
        }

    }
}
