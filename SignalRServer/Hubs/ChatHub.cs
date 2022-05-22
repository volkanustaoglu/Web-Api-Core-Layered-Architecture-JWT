using Microsoft.AspNetCore.SignalR;
using SignalRServer.Data;
using SignalRServer.IHubs;
using SignalRServer.Model;
using SignalRServer.Model.Enum;
using System;
using System.Threading.Tasks;

namespace SignalRServer.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        //static List<string> AllClients = new List<string>();

        //Metot client tarafından 'SendAllMessageAsync' tetiklenir.
        public async Task SendAllMessageAsync(string message)
        {
            await Clients.All.ReceiveAllMessage(message);

            //'receiveAllMessage' isimli isteği dinleyen tüm client'lara fonksiyona gelen mesajı döndürür.
            //await Clients.All.SendAsync("receiveAllMessage", message);
        }

        //Aktif userların 'ConnectionId' 'lerini döndürür.
        public async Task GetActiveUserAsync()
        {
            await Clients.All.ReceiveGetActiveUser(ClientSource.Clients);
        }

        public async Task SendOtherMessageAsync(string message)
        {
            await Clients.Others.ReceiveOtherMessage(message);

            //'receiveOtherMessage' isimli isteği dinleyen isteği gönderen client hariç tüm client'lara fonksiyona gelen mesajı döndürür.
            //await Clients.Others.SendAsync("ReceiveOtherMessage", message);
        }

        //Bağlantı gerçekleştiğinde
        public async override Task OnConnectedAsync()
        {
            await Clients.Caller.OnConnectedAsync(Context.ConnectionId);
        }

        //Bağlantı koptuğunda
        async public override Task OnDisconnectedAsync(Exception exception)
        {
            //Bağlanan ve bağlanıtısı kesilen clientlar bu şekilde tutularak aktif clientlara erişilebilir.
            //AllClients.Remove(Context.ConnectionId);
            Client client = new Client();
            client = ClientSource.Clients.Find(x => x.ConnectionId == Context.ConnectionId);
            ClientSource.Clients.Remove(client);

            //await Clients.All.SendAsync("OnDisconnected", $"{Context.ConnectionId}");

            //Tüm client'lara aktif clientların listesini döndürür.
            await Clients.All.ReceiveGetActiveUser(ClientSource.Clients);

            //Çıkış yapan client'ı döndürür
            await Clients.Others.OnDisconnectedOtherAsync(client.NickName);
        }

        public async Task AnnounceMe(string nickname)
        {
            //var checkNickName = ClientSource.Clients.Find(x => x.NickName == nickname);
            //if (checkNickName != null)
            //{
            //    await Clients.Caller.ErrorMessage(Messages.SameName);
            //    return;
            //}

            //Client aktif clientlar arasına eklenir.
            Client client = new Client
            {
                ConnectionId = Context.ConnectionId,
                NickName = nickname
            };
            ClientSource.Clients.Add(client);

            //Diğer clientlara yeni client bildirilir.
            await Clients.Others.JoinClient(nickname);

            //Tüm Clientlara güncel aktif client listesi döndürülür.
            await Clients.All.ReceiveGetActiveUser(ClientSource.Clients);
        }

    }
}
