using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    public class SavingAccount : AccountBase
    {
        public SavingAccount() 
        {
            CreationDate = DateTime.Now;
        }
        
    }
}
