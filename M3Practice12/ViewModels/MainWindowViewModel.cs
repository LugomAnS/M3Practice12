using M3Practice12.Models;
using M3Practice12.Services;
using M3Practice12.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion
        public MainWindowViewModel()
        {
            Clients = DataService.GetData();
        }

        #region Команды

        #endregion V
    }
}
