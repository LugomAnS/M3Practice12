using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M3Practice12.Models;
using M3Practice12.Models.Accounts;
using Newtonsoft.Json;

namespace Serialization
{
    internal class DataService
    {
        const string CLIENT_PATH = "client.json";

        public ObservableCollection<ClientInfo> clients = new ObservableCollection<ClientInfo>();
        
        public DataService()
        {
            CreateDB();
            if (!File.Exists(CLIENT_PATH))
            {
                Serialize();
            }
            Deserialize();
        }

        private void CreateDB()
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

            foreach (ClientInfo item in test_clientinfo)
            {
                clients.Add(item);
            }

        }

        private void Serialize()
        {
            string json = JsonConvert.SerializeObject(clients);
            File.WriteAllText(CLIENT_PATH, json);
        }

        private void Deserialize()
        {
            string json = File.ReadAllText(CLIENT_PATH);

            clients = JsonConvert.DeserializeObject<ObservableCollection<ClientInfo>>(json);
        }
    }
}
