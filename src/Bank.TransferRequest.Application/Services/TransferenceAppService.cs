using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bank.TransferRequest.Application.Dtos;
using Bank.Transfer.Domain.Entities;
using Bank.Transfer.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using Bank.TransferRequest.Application.Interfaces;

namespace Bank.TransferRequest.Application.Services
{
    public class TransferenceAppService : ITransferenceAppService
    {
        private readonly IMapper _mapper;
        private readonly ITransferenceService _transferenceService;
        public TransferenceAppService(IMapper mapper,
                                    ITransferenceService transferenceService)
        {
            _mapper = mapper;
            _transferenceService = transferenceService;
        }

        public void Add(TransferenceDto transferenceDto)
        {
            var transference = _mapper.Map<Transference>(transferenceDto);

            _transferenceService.Add(transference);

        }

        public IEnumerable<TransferenceDto> GetAll()
        {
            return _transferenceService.GetAll().ProjectTo<TransferenceDto>(_mapper.ConfigurationProvider);
        }

        public RequestStatusDto GetRequestStatusById(Guid id)
        {
            return _mapper.Map<RequestStatusDto>(_transferenceService.GetById(id));
        }

        public TransferenceDto GetById(Guid id)
        {
            return _mapper.Map<TransferenceDto>(_transferenceService.GetById(id));
        }

        public void Remove(Guid id)
        {
            _transferenceService.Remove(id);
        }

        public void Update(TransferenceDto transferenceDto)
        {
            var transference = _mapper.Map<Transference>(transferenceDto);
            _transferenceService.Update(transference);
        }
    }
}
