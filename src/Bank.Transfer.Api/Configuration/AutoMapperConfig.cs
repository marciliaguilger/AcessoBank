using AutoMapper;
using Bank.Transfer.Application.Dtos;
using Bank.Transfer.Domain.Entities;

namespace Bank.Transfer.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<Transference, TransferenceDto>().ReverseMap();
        }
    }
}
