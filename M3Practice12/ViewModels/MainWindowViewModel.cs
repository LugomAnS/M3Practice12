using M3Practice12.Commands;
using M3Practice12.Models;
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
            set => Set(ref _selectedClientInfo, value);
        }

        #endregion

        #region Работа со счетами
        private Visibility _workingWithAccounts = Visibility.Visible;

        public Visibility WorkingWithAccount
        {
            get => _workingWithAccounts;
            set => Set(ref _workingWithAccounts, value);
        }
        #endregion

        #region Добавление клиента
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

        #endregion
    }
}
