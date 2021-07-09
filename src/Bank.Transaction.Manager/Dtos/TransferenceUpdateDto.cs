using Bank.Transfer.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Transaction.Update.Api.Dtos
{
    public class TransferenceUpdateDto
    {
        [Key]
        [Required(ErrorMessage = "Id is Required")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Status is Required")]
        public TransferenceStatus Status { get; set; }

        public string StatusDetail { get; set; }
        

       
    }
}
