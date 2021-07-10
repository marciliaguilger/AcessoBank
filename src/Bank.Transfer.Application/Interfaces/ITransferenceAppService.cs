using Bank.Transfer.Application.Dtos;
using Bank.TransferRequest.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Transfer.Application.Interfaces
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
