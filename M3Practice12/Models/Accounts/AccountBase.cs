using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    /// <summary>
    /// Базовая информация о счете
    /// </summary>
    public abstract class AccountBase
    {
        /// <summary>Владелец счет</summary>
        public int ClientID { get; set; }

        /// <summary>Номер счета</summary>
        public string Number { get; set; }

        /// <summary>Баланс</summary>
        public int Balance { get; set; }

        /// <summary>Форматированный вывод баланс</summary>
        public string DisplayBalance
        {
            get => Balance.ToString("C2");
        }

        /// <summary>Дата открытия счета</summary>
        public DateTime CreationDate { get; set; }

        public DateTime? ClosedDate { get; set; }
    }
}
