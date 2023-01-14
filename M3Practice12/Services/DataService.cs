using M3Practice12.Models;
using M3Practice12.Models.Accounts;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace M3Practice12.Services
{
    /// <summary>
    /// Сервис работы с данными 
    /// </summary>
    internal class DataService
    {
        private const string CLIENTINFO_PATH = "ClientInfo.json";


        /// <summary>
        /// Создание БД при ее отсутствии
        /// </summary>
        static DataService()
        {
            int client_index = 1;

            var test_clientinfo = Enumerable.Range(1, 10)
                     .Select(c => new ClientInfo
                     {
                         Client = new Client
                         {
                             Id = client_index,
                             Name = $"Имя {client_index}",
                             Surname = $"Фамилия {client_index}",
                             Patronymic = $"Отчетсво {client_index}"
                         },
                         SavingAccount = new SavingAccount
                         {
                             ClientID = client_index,
                             CreationDate = DateTime.Now,
                             Number = $"Номер счета {client_index}",
                             Balance = client_index * 10
                         },
                         DepositAccount = new DepositAccount
                         {
                             ClientID = client_index,
                             CreationDate = DateTime.Now,
                             Number = $"Номер счета {client_index + 10}",
                             Balance = (client_index++) * 10
                         }

                     });
            ObservableCollection<ClientInfo> clients = new ObservableCollection<ClientInfo>();

            foreach (ClientInfo item in test_clientinfo)
            {
                clients.Add(item);
            }

            WriteData(clients);
        }

        /// <summary>
        /// Получение данных с диска
        /// </summary>
        /// <returns>ObservableCollection<ClientInfo></returns>
        public static ObservableCollection<ClientInfo> GetData()
        {
            string json = File.ReadAllText(CLIENTINFO_PATH);

            return JsonConvert.DeserializeObject<ObservableCollection<ClientInfo>>(json);
        }

        /// <summary>
        /// Запись информации на диск
        /// </summary>
        /// <param name="clientDB">Информация для записи</param>
        public static void WriteData(ObservableCollection<ClientInfo> clientDB)
        {
            File.WriteAllText(CLIENTINFO_PATH, JsonConvert.SerializeObject(clientDB));
        }
    }
}
