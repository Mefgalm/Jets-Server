using Jets.ClientParts;

namespace Jets.Interfaces
{
    public interface IEventHandle
    {
        void Subscribe(Client client);
        void Unsubscribe(Client client);
    }
}
