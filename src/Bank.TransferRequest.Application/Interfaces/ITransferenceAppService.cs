using Bank.TransferRequest.Application.Dtos;
using System;
using System.Collections.Generic;

namespace Bank.TransferRequest.Application.Interfaces
{
    public interface ITransferenceAppService
    {
        void Add(TransferenceDto transferenceDto);
        void Update(TransferenceDto transferenceDto);
        void Remove(Guid id);
        IEnumerable<TransferenceDto> GetAll();
        TransferenceDto GetById(Guid id);
        RequestStatusDto GetRequestStatusById(Guid id);

    }
}
