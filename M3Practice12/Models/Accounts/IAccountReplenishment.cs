using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    public interface IAccountReplenishment<out T>
        where T: AccountBase
    {
        public void ReplenishmentAccount(double ammount);
    }
}
