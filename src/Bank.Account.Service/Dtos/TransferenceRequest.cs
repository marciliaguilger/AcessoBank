using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Account.Service.Dtos
{
    public class TransferenceRequest
    {
        public TransferenceRequest(string _accountNumber, decimal _value, string _type)
        {
            accountNumber = _accountNumber;
            value = _value;
            type = _type;
        }

        public string accountNumber { get; set; }
        public decimal value { get; set; }
        public string type { get; set; }

    }
}
