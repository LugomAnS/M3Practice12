using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models
{

    /// <summary>
    /// Клиент банка
    /// </summary>
    internal class Client
    {
        public static int ClientDBID { get; set; } = 0;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }

        public Client() { }

        public static int GetNextID() => ++ClientDBID;

    }
}
