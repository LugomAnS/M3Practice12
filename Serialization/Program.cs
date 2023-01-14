using M3Practice12.Models;

namespace Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataService test = new DataService();

            foreach (ClientInfo item in test.clients)
            {
                Console.WriteLine($"{item.Client.Name} {item.SavingAccount.Number}");
            }

            Console.ReadKey(true);
        }
    }
}