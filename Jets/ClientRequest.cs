﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jets
{
    public class ClientRequest<T>
    {
        public IEnumerable<Guid> Clients { get; set; }

        public T Data { get; set; }
    }
}
