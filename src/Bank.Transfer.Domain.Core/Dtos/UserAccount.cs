using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Transfer.Domain.Core.Dtos
{
    public class UserAccount
    {
        public int id { get; set; }
        public string accountNumber { get; set; }
        public decimal balance { get; set; }
    }
}
