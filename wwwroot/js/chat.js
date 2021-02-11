"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();


//Disable send button until connection is established
//document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var d = new Date();
    var recieveUser = document.getElementById("userInput").value;
    var time = d.getHours() + ":" + d.getMinutes();
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = msg + " ";
    var messageBubble = document.createElement("div");
    var timeStamp = document.createElement("span");
    timeStamp.textContent = time;
    messageBubble.classList.add("message-bubble");
    if (user == recieveUser) {
        messageBubble.classList.add("self-message");
        messageBubble.textContent = encodedMsg;
        timeStamp.classList.add("self-time-stamp");

    } else {
        timeStamp.classList.add("time-stamp");
        messageBubble.textContent = user + " : " + encodedMsg;

    }
    messageBubble.appendChild(timeStamp);
    setTimeout(function () {
        var element = document.getElementById("reportHub");
        element.scrollTop = element.scrollHeight;
        document.getElementById("messageInput").value = " ";
        console.log("uslo");
    }, 300);
    document.getElementById("messagesList").appendChild(messageBubble);
});

connection.start().then(function () {
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("messageInput").addEventListener("keypress", function (event) {
    if (event.key === 'Enter') {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        if (message.length > 0) {
            $.post("/Home/Add?user=" + user + "&message=" + message, function (data) {
                console.log("success");
            });
            connection.invoke("SendMessage", user, message).catch(function (err) {
                return console.error(err.toString());
            });

            setTimeout(function () {
                var element = document.getElementById("reportHub");
                element.scrollTop = element.scrollHeight;
                document.getElementById("messageInput").value = " ";
                console.log("uslo");
            }, 300);

            event.preventDefault();
        }
    }
});