using M3Practice12.Commands;
using M3Practice12.Models;
using M3Practice12.Models.Accounts;
using M3Practice12.Services;
using M3Practice12.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace M3Practice12.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Поля и свойства

        #region Информация о всех клиентах

        private ObservableCollection<ClientInfo> _clients = new ObservableCollection<ClientInfo>();
        public ObservableCollection<ClientInfo> Clients
        {
            get => _clients;
            set => Set(ref _clients, value);
        }

        #endregion

        #region Выбранный клиент

        private ClientInfo _selectedClientInfo;

        public ClientInfo SelectedClientInfo
        {
            get => _selectedClientInfo;
            set
            {
                Set(ref _selectedClientInfo, value);
                VisibilityReset();
            }
        }

        #endregion

        #region Видимость - Работа со счетами
        private Visibility _workingWithAccounts = Visibility.Visible;

        public Visibility WorkingWithAccount
        {
            get => _workingWithAccounts;
            set => Set(ref _workingWithAccounts, value);
        }
        #endregion

        #region Видимость - Добавление клиента
        private Visibility _addingClient = Visibility.Collapsed;

        public Visibility AddingClient
        {
            get => _addingClient;
            set => Set(ref _addingClient, value);
        }

        #endregion

        #region Новый клиент
        private Client _newClient = new Client();

        public Client NewClient
        {
            get => _newClient;
            set => Set(ref _newClient, value);
        }

        #endregion

        #region Счета клиента

        private ObservableCollection<AccountBase> _clientAccount;

        public ObservableCollection<AccountBase> ClientAccounts 
        {
            get => _clientAccount;
            set => Set(ref _clientAccount, value);
        }

        #endregion

        #region Видимость - Перевод между своими счетами 
        private Visibility _exchandeSelfAccounts = Visibility.Collapsed;

        public Visibility ExchangeSelfAccount
        {
            get => _exchandeSelfAccounts;
            set => Set(ref _exchandeSelfAccounts, value);
        }
        #endregion

        #region Выбранный счет
        private AccountBase _selectedAccount;

        public AccountBase SelectedAccount
        {
            get => _selectedAccount;
            set => Set(ref _selectedAccount, value);
        }

        #endregion

        #region Сумма перевода
        private string _ammountToWithdraw;

        public string AmmountToWithdraw
        {
            get => _ammountToWithdraw;
            set => Set(ref _ammountToWithdraw, value);
        }

        #endregion

        #region Выбранный счет

        private AccountType _accountType;

        public AccountType AccountType
        {
            get => _accountType;
            set => Set(ref _accountType, value);
        }

        #endregion

        #region Свои счета для пополнения

        private List<IAccountReplenishment<AccountBase>> _accountsToRefill = new List<IAccountReplenishment<AccountBase>>();

        public List<IAccountReplenishment<AccountBase>> AccountsToRefill
        {
            get => _accountsToRefill;
            set => Set(ref _accountsToRefill, value);
        }

        #endregion

        #region Сумма для пополнения
        private string _replenishmentAmmount;

        public string ReplenishmentAmmount
        {
            get => _replenishmentAmmount;
            set => Set(ref _replenishmentAmmount, value);
        }

        #endregion

        #region Видимость пополнение своего счета
        private Visibility _replehishmentVisibility = Visibility.Collapsed;

        public Visibility ReplenishmentVisibility
        {
            get => _replehishmentVisibility;
            set => Set(ref _replehishmentVisibility, value);
        }
        #endregion

        #region Счет для пополнения

        private IAccountReplenishment<AccountBase> _accountToReplenishment;

        public IAccountReplenishment<AccountBase> AccountToReplenishment
        {
            get => _accountToReplenishment;
            set => Set(ref _accountToReplenishment, value);
        }

        #endregion


        #region Хранилище всех счетов
        private AccountsStorage<AccountBase> _accountsStorage;

        public AccountsStorage<AccountBase> AccountsStorage
        {
            get => _accountsStorage;
            set => Set(ref _accountsStorage, value);
        }

        #endregion

        #region Видимость перевода между счетами клиентов
        private Visibility _storageVisibility = Visibility.Collapsed;
        
        public Visibility StorageVisibility
        {
            get => _storageVisibility;
            set => Set(ref _storageVisibility, value);
        }

        #endregion

        #region Сумма перевода другому клиенту

        private string _transactionAmmount;

        public string TransactionAmmount
        {
            get => _transactionAmmount;
            set => Set(ref _transactionAmmount, value);
        }

        #endregion

        #region Счет другого клиента для перевода
        private AccountBase _accountTransaction;

        public AccountBase AccountTransaction
        {
            get => _accountTransaction;
            set => Set(ref _accountTransaction, value);
        }
        #endregion

        #endregion
        public MainWindowViewModel()
        {
            Clients = DataService.GetData();

            SaveNewClientCommand = new LambdaCommand(OnSaveNewClientExecute, 
                                                     CanSaveNewClientExecute);
            CancelSavingClientCommand = new LambdaCommand(OnCancelSavingClientExecute, 
                                                          CanCancelSavingClientExecute);
            AddNewClientCommand = new LambdaCommand(OnAddNewClientCommandExecute, 
                                                    CanAddNewClientCommandExecute);
            DeleteClientCommand = new LambdaCommand(OnDeleteClientCommandExecute,
                                                    CanDeleteClientCommandExecute);

            SelfAccountExhangeCommand = new LambdaCommand(OnSelfAccountExhangeCommandExecute,
                                                          CanSelfAccountExhangeCommandExecute);
            ExchangeBalanceCommand = new LambdaCommand(OnExchangeBalanceCommandExecute,
                                                       CanExchangeBalanceCommandExecute);

            ClosingAccountCommand = new LambdaCommand(OnClosingAccountCommandExecute,
                                                      CanClosingAccountCommandExecute);
            OpenNewAccountCommand = new LambdaCommand(OnOpenNewAccountExecute,
                                                      CanOpenNewAccountExecute);

            ReplenishmentVisibilityCommand = new LambdaCommand(OnReplenishmentVisibilityCommandExecute,
                                                               CanReplenishmentVisibilityCommandExecute);
            ReplenishmentCommand = new LambdaCommand(OnReplehishmentCommandExecute,
                                                     CanReplehishmentCommandExecute);

            TransactionVisibilityCommand = new LambdaCommand(OnTransactionVisibilityCommandExecute,
                                                             CanTransactionVisibilityCommandExecute);
            TransactionCommand = new LambdaCommand(OnTransactionCommandExecute,
                                                   CanTransactionCommandExecute);
        }


        /// <summary>
        /// Сброс видимости режимов работы
        /// </summary>
        private void VisibilityReset()
        {
            ExchangeSelfAccount = Visibility.Collapsed;
            ReplenishmentVisibility = Visibility.Collapsed;
            StorageVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Наполнение хранилища всеми счетами других клиентов
        /// </summary>
        private void StorageFill()
        {
            AccountsStorage = new AccountsStorage<AccountBase>();
            foreach (var item in Clients)
            {
                if (item.DepositAccount != null
                    && SelectedClientInfo != item)
                {
                    AccountsStorage.AddValue = item.DepositAccount;
                }
                if (item.SavingAccount !=null
                    && SelectedClientInfo != item)
                {
                    AccountsStorage.AddValue = item.SavingAccount;
                }
                
            }
        }

        /// <summary>
        /// обновление списка счетов клиента
        /// </summary>
        private void ClientAccountsRefresh()
        {
            ClientAccounts = new ObservableCollection<AccountBase>();

           // if (SelectedClientInfo.SavingAccount != null)
            {
                ClientAccounts.Add(SelectedClientInfo.SavingAccount);
            }

           // if (SelectedClientInfo.DepositAccount != null)
            {
                ClientAccounts.Add(SelectedClientInfo.DepositAccount);
            }

            OnPropertyChanged("ClientAccounts");
        }

        #region Команды

        #region Добавить нового клиента
        public ICommand AddNewClientCommand { get; }

        private void OnAddNewClientCommandExecute(object p)
        {
            NewClient = new Client();
            WorkingWithAccount = Visibility.Collapsed;
            AddingClient = Visibility.Visible;
        }

        private bool CanAddNewClientCommandExecute(object p) => true;

        #endregion

        #region Сохранение нового клиента
        public ICommand SaveNewClientCommand { get; }

        private void OnSaveNewClientExecute(object p) 
        {
            NewClient.Id = Client.GetNextID();
            ClientInfo newClientInfo = new ClientInfo
            {
                Client = NewClient
            };
            Clients.Add(newClientInfo);
            DataService.WriteData(Clients);

            AddingClient = Visibility.Collapsed;
            WorkingWithAccount = Visibility.Visible;
            SelectedClientInfo = newClientInfo;
        }

        private bool CanSaveNewClientExecute(object p)
            => NewClient.Name != null && NewClient.Surname != null && NewClient.Patronymic != null;

        #endregion

        #region Отмена сохранения клиента
        public ICommand CancelSavingClientCommand { get; }

        private void OnCancelSavingClientExecute(object p)
        {
            AddingClient = Visibility.Collapsed;
            WorkingWithAccount = Visibility.Visible;
        }
        private bool CanCancelSavingClientExecute(object p) => true;

        #endregion

        #region Удаление клиента
        public ICommand DeleteClientCommand { get; }

        private void OnDeleteClientCommandExecute(object p)
        {
            Clients.Remove(SelectedClientInfo);
            DataService.WriteData(Clients);
        }

        private bool CanDeleteClientCommandExecute(object p) => p != null;

        #endregion


        #region Закрыть счет
        public ICommand ClosingAccountCommand { get; }

        private void OnClosingAccountCommandExecute(object p)
        {
            SelectedClientInfo.Delete<AccountBase>(p as AccountBase);

            OnPropertyChanged(nameof(SelectedClientInfo));
            DataService.WriteData(Clients);
      
        }

        private bool CanClosingAccountCommandExecute(object p) => p is AccountBase;

        #endregion

        #region Открыть счет
        public ICommand OpenNewAccountCommand { get; }

        private void OnOpenNewAccountExecute(object p)
        {
            if ((AccountType)p == AccountType.Saving)
            {
                SelectedClientInfo.OpenAccount( new SavingAccount() );
            }

            if ((AccountType)p == AccountType.Deposit)
            {
                SelectedClientInfo.OpenAccount(new DepositAccount());
            }

            OnPropertyChanged(nameof(SelectedClientInfo));
            DataService.WriteData(Clients);
        }

        private bool CanOpenNewAccountExecute(object p)
        {
            if (p == null || SelectedClientInfo == null) return false;

            if ((AccountType)p == AccountType.Deposit && SelectedClientInfo.DepositAccount == null)
            {
                return true;
            }

            if ((AccountType)p == AccountType.Saving && SelectedClientInfo.SavingAccount == null)
            {
                return true;
            }

            return false;
        }    

        #endregion


        #region Вкладка перевод между своими счетами

        public ICommand SelfAccountExhangeCommand { get; }

        private void OnSelfAccountExhangeCommandExecute(object p)
        {
            VisibilityReset();

            ExchangeSelfAccount = Visibility.Visible;

            ClientAccountsRefresh();
        }

        private bool CanSelfAccountExhangeCommandExecute(object p) => p != null;

        #endregion

        #region Выполнение перевода
        public ICommand ExchangeBalanceCommand { get; }

        private void OnExchangeBalanceCommandExecute(object p)
        {
            SelectedClientInfo.Exchange(SelectedAccount, double.Parse(AmmountToWithdraw));

            DataService.WriteData(Clients);

            // Не получается обновить информацию 
            OnPropertyChanged(nameof(SelectedClientInfo.SavingAccount.DisplayBalance));
            OnPropertyChanged(nameof(SelectedClientInfo.DepositAccount.DisplayBalance));

            MessageBox.Show("Перевод завершен");

            ClientAccountsRefresh();
        }

        private bool CanExchangeBalanceCommandExecute(object p)
        {
            if (SelectedAccount != null 
                && double.TryParse(AmmountToWithdraw, out double test)
                && SelectedAccount.Balance >= test
                && test > 0)
            {
                return true;
            }

            return false;
        }


        #endregion


        #region Вкладка пополнение счета
        public ICommand ReplenishmentVisibilityCommand { get; }

        private void OnReplenishmentVisibilityCommandExecute(object p)
        {
            VisibilityReset();
            ReplenishmentVisibility = Visibility.Visible;
            AccountsToRefill = new List<IAccountReplenishment<AccountBase>>
            {
                ((ClientInfo)p).SavingAccount,
                ((ClientInfo)p).DepositAccount
            };
        }

        private bool CanReplenishmentVisibilityCommandExecute(object p) => p != null;

        #endregion

        #region Выполнение пополнения
        public ICommand ReplenishmentCommand { get; }

        private void OnReplehishmentCommandExecute(object p)
        {
            AccountToReplenishment.ReplenishmentAccount(double.Parse(ReplenishmentAmmount));

            DataService.WriteData(Clients);

            MessageBox.Show("Пополнение выполнено");

            AccountsToRefill = new List<IAccountReplenishment<AccountBase>>
            {
                SelectedClientInfo.SavingAccount,
                SelectedClientInfo.DepositAccount
            };
        }

        private bool CanReplehishmentCommandExecute(object p)
        {
            if (AccountToReplenishment != null
                && double.TryParse(ReplenishmentAmmount, out double test)
                && test > 0)
            {
                return true;
            }

            return false;
        }

        #endregion


        #region Видимость вкладки перевода другому клиенту
        public ICommand TransactionVisibilityCommand { get; }

        private void OnTransactionVisibilityCommandExecute(object p)
        {
            VisibilityReset();
            StorageFill();
            StorageVisibility = Visibility.Visible;

            ClientAccountsRefresh();
        }

        

        private bool CanTransactionVisibilityCommandExecute(object p) => p != null;

        #endregion

        #region Выполнение перевода
        public ICommand TransactionCommand { get; }

        private void OnTransactionCommandExecute(object p)
        {
            AccountsStorage.Transaction(SelectedAccount, AccountTransaction, double.Parse(TransactionAmmount));

            DataService.WriteData(Clients);

            StorageFill();

            MessageBox.Show("Перевод осуществлен");
        }

        private bool CanTransactionCommandExecute(object p)
        {
            if (SelectedAccount != null
                && double.TryParse(TransactionAmmount, out double test)
                && SelectedAccount.Balance >= test
                && test > 0)
            {
                return true;
            }

            return false;
        }

        #endregion

        #endregion
    }
}
