using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bank.Transfer.Application.Dtos
{
    public class TransferenceDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Origin Account is Required")]
        [MinLength(2)]
        [MaxLength(20)]
        [DisplayName(" Account Origin")]
        public string AccountOrigin { get; set; }

        [Required(ErrorMessage = "Destination Account is Required")]
        [MinLength(2)]
        [MaxLength(20)]
        [DisplayName(" Account Destination")]
        public string AccountDestination { get; set; }


        [Required(ErrorMessage = "The amount must be greater than 1 cent")]
        [Range(0.01, 999999.0)]
        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        
    }
}
