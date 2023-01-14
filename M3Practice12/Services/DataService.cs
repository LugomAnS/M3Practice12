using M3Practice12.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Services
{
    /// <summary>
    /// Сервис работы с данными 
    /// </summary>
    internal class DataService
    {
        private const string CLIENTINFO_PATH = "ClientInfo.json";

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
            File.WriteAllText(CLIENTINFO_PATH,
                              JsonConvert.SerializeObject(clientDB));
        }
    }
}
