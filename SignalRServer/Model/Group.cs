using System.Collections.Generic;

namespace SignalRServer.Model
{
    public class Group
    {
        public string GroupName { get; set; }
        public List<Client> Clients { get; set; }
    }
}
