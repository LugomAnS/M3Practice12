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

        private List<AccountBase> _clientAccount;

        public List<AccountBase> ClientAccounts 
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
        }

        private void VisibilityReset()
        {
            ExchangeSelfAccount = Visibility.Collapsed;
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


        #region Перевод между своими счетами

        public ICommand SelfAccountExhangeCommand { get; }

        private void OnSelfAccountExhangeCommandExecute(object p)
        {
            ExchangeSelfAccount = Visibility.Visible;
            ClientAccounts = new List<AccountBase>
            {
                SelectedClientInfo.SavingAccount,
                SelectedClientInfo.DepositAccount
            };
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
            ClientAccounts = new List<AccountBase>
            {
                SelectedClientInfo.SavingAccount,
                SelectedClientInfo.DepositAccount
            };
        }

        private bool CanExchangeBalanceCommandExecute(object p)
        {
            if (SelectedAccount != null 
                && double.TryParse(AmmountToWithdraw, out double test)
                && SelectedAccount.Balance >= test)
            {
                return true;
            }

            return false;
        }
           

        #endregion

        #endregion
    }
}
