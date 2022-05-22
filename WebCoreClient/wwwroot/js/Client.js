
var apiAdress = "https://localhost:5001/chathub";
var nickname;
var connectId;
var errorCodes = {
    Error: 0,
    Success: 1,
    SameName: 2
}


//Bağlantı oluşturulur
const connection = new signalR.HubConnectionBuilder()
    .withUrl(apiAdress)
    .withAutomaticReconnect() //Bağlanılamadığı durumlarda 0-2-10-30 saniyelik aralıklarla 4 kere oto bağlantı dener.
    .build();

function Login() {
    nickname = $("#loginName").val();
    if (nickname !== "") {

        //Bağlantıyı başlatır.
        connection.start();

    } else {
        $("#loginDiv").css("background-color", "red");
        $("#loginLabel").html(`Nickname boş bırakılamaz.`);
    }
}

function Exit() {
    //Soket bağlantısı kesilir.
    connection.stop();

    connectId = "";
    $("#pageDiv").hide()
    $("#loginDiv").css("background-color", "pink");
    $("#loginDiv").show(2000)
}



$(document).ready(() => {

    //Soketdeki 'OnConnected' isimli fonksiyon tarafından tetiklenir. Bir parametre gönderir.
    connection.on("OnConnectedAsync", connectionId => {
        connectId = connectionId;

        $("#pageDiv").show()
        $("#loginDiv").css("background-color", "green");
        $("#loginDiv").hide(2000);
        $("#loginLabel").html(``);
        $("#nickname").html(nickname);

        //Tüm kullanıcıların listesini çağırır.
        connection.invoke("AnnounceMe", nickname);
    });


    connection.on("ReceiveGetActiveUser", clients => {

        $('.subtabs').remove();
        $.each(clients, function (index, value) {
            $("#list-tab").append('<a class="subtabs list-group-item list-group-item-action" id="list-settings-list" data-toggle="list" href="#list-settings" role="tab" aria-controls="settings">' + value.nickName + '</a>');
        });
        console.log("AllClients", message);

    });




    connection.on("JoinClient", nickname => {


        console.log("nickname", nickname);

    });




    connection.on("ErrorMessage", errorCode => {

        if (errorCode == errorCodes.SameName) {
            Exit();
            $("#loginLabel").html(`Farklı bir nickname seçiniz.`);
        }



    });









    connection.on("OnDisconnectedAsync", connectionId => {



    });


    $("button1").click(() => {

        let message = $("#txtMessage").val();


        //Server tarafındaki 'SendAllMessageAsync' isimli fonksiyonu tetikler. Bir parametre gönderir.
        connection.invoke("SendAllMessageAsync", message)
            .catch(error => console.log("Mesaj gönderilirken hata alınmıştır."));



        //connection.invoke("SendOtherMessageAsync", message)
        //    .catch(error => console.log("Mesaj gönderilirken hata alınmıştır."));



        //connection.invoke("GetActiveUserAsync")
        //    .catch(error => console.log("Mesaj gönderilirken hata alınmıştır."));



    });


    connection.on("ReceiveAllMessage", message => {


        //$("#messages").append(`${message}<br>`);
        alert(message);

    });


    connection.on("receiveOtherMessage", message => {


        //$("#messages").append(`${message}<br>`);
        //alert(message);

    });











    //connection.on("receiveMessageOther", (message, id, nickName) => {
    //    var d = new Date();
    //    var data = { "UserId": id, "MessageDetail": message, "Time": d.toUTCString(), "UserName": nickName };

    //    $scope.$apply(function () {
    //        $scope.Messages.push(data);
    //    });
    //});
});




