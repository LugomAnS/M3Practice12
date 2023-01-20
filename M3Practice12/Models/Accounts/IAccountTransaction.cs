using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    public interface IAccountTransaction <in T>
        where T : AccountBase
    {

        T AddValue { set; }

        void Transaction(T accountToWithdraw, T accountToFill, double ammount);
    }
}
