using SignalRServer.Model;
using SignalRServer.Model.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SignalRServer.IHubs
{
    public interface IChatHub
    {
        //Tüm kullanıcılara mesaj gönderir
        Task ReceiveAllMessage(string message);
        
        //Connect olan client'ı diğer clientlara bildirir
        Task JoinClient(string nickname);

        //Tüm aktif kullanıcıların listesini döndürür
        Task ReceiveGetActiveUser(List<Client> allClients);

        //Mesajı gönderen kullanıcı hariç diğer tüm kullanıcılara mesaj gönderir
        Task ReceiveOtherMessage(string message);

        //Client bağlantı sağladığında kendisine döndürür
        Task OnConnectedAsync(string connectionId);

        //Client bağlantı sağladığında client'lara döndürür
        Task OnConnectedOtherAsync(string connectionId);

        //Client'ın bağlantısı kesildiğinde kendisine döndürür
        Task OnDisconnectedAsync(string connectionId);

        //Client'ın bağlantısı kesildiğinde diğer client'lara döndürür
        Task OnDisconnectedOtherAsync(string nickname);

        //Belirli hata mesajları geri döndürür.
        Task ErrorMessage(Messages messages);



    }
}
