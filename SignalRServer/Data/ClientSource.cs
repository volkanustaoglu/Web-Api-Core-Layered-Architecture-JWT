using SignalRServer.Model;
using System.Collections.Generic;

namespace SignalRServer.Data
{
    public class ClientSource
    {
        public static List<Client> Clients { get; } = new List<Client>();
    }
}
