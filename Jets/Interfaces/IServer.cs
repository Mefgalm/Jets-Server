using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jets.Interfaces
{
    public interface IServer
    {
        Task Start();

        void Stop();
    }
}
