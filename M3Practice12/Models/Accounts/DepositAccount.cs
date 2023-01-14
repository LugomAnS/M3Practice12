﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3Practice12.Models.Accounts
{
    internal class DepositAccount : AccountBase
    {
        

        public DateTime ExpirationTime { get; set; }

        public DepositAccount()
        {
            CreationDate = DateTime.Now;
        }
    }
}
