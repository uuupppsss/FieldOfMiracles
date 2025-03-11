using Microsoft.AspNetCore.SignalR;

namespace SignalRServer
{
    public class Player
    {
        public string ConnectionId { get; set; }
        public string Name { get; set; }
        public ISingleClientProxy Proxy { get; set; }
    }
}
