using Jets.ClientParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jets.Interfaces
{
    public delegate void OnCloseRoom(IRoom room);

    public interface IRoom
    {
        event OnCloseRoom OnCloseRoom;

        void Close();
        Task Cycle();

        void AddClient(Client client);
        void RemoveClient(Client client);
    }
}
