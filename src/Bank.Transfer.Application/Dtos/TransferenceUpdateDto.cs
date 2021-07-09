using Bank.Transfer.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bank.Transfer.Application.Dtos
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
