using AutoMapper;
using Bank.Transfer.Application.Dtos;
using Bank.Transfer.Domain.Entities;
using Bank.TransferRequest.Application.Dtos;

namespace Bank.TransferRequest.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<Transference, TransferenceDto>().ReverseMap();
            CreateMap<Transference, RequestStatusDto>()
                .ForMember(d => d.Message, o => o.MapFrom(s => s.TransferStatusDetail))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.TransferStatus.ToString()));
        }
    }
}
