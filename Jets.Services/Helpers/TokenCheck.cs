using Jets.Services.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jets.Services.Helpers
{
    public static class TokenCheck
    {
        public static bool IsTokenValid(Guid accessToken, JetsDatabaseContext jetsDatabaseContext)
        {
            if (accessToken == Guid.Empty)
                return false;

            Database.Client client = jetsDatabaseContext.Clients.SingleOrDefault(x => x.AccessToken == accessToken);

            return client != null && client.TokenExpire > DateTime.UtcNow;
        }
    }
}
