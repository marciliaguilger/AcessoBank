using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Transaction.Application.Dtos
{
    public class AccountDto
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
