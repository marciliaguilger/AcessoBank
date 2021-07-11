using FluentValidation.Results;
using MediatR;
using System;

namespace Bank.Transfer.Domain.Core.Messages
{
    public abstract class Command<R> : Message, IRequest<R>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }
        public Command()
        {
            Timestamp = DateTime.Now;
        }
        public virtual bool IsValid()
        {
            return true;
        }
    }
}
