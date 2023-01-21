using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    /// <summary>
    /// Базовая информация о счете
    /// </summary>
    public abstract class AccountBase : INotifyPropertyChanged
    {
        /// <summary>Владелец счет</summary>
        public int ClientID { get; set; }

        /// <summary>Номер счета</summary>
        public string Number { get; set; }

        private double balance;

        /// <summary>Баланс</summary>
        public double Balance 
        {
            get => balance; 
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
                OnPropertyChanged("DisplayBalance");
            }
        }

        /// <summary>Форматированный вывод баланс</summary>
        public string DisplayBalance
        {
            get => Balance.ToString("C2");
        }

        /// <summary>Дата открытия счета</summary>
        public DateTime CreationDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string property = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
