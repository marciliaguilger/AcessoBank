namespace Bank.Transfer.Domain.Core.Messages
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }
        public Message()
        {
            MessageType = GetType().Name;
        }
    }
}
