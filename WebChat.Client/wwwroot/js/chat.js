$(function () {

    $('#chatForm').submit(function () {
        const user = $("#sender").text();
        sendMessage(user);
        return false;
    });

    connection.on("ReceiveMessage", (message, sender) => {

        if ($("#sender").text().toUpperCase() == sender.toUpperCase()) {
            const messageDiv = `<div style="width:100%; padding:5px">
         <div style="text-align:left; background-color:#bbb" class="message">
        ${message}
         </div>
         </div>`;
            $("#messages").append(messageDiv);
        } else {
            newMessageToast(message, sender);
        }

    });

});

async function sendMessage(user) {
    const message = $("#message").val();
    const sender = $.cookie(loginCookieName);
    $("#message").val('');
    await connection.invoke("SendMessage", user, sender, message);
    const messageDiv = `<div style="width:100%; padding:5px">
    <div style="text-align:right; background-color:#00ff90" class="message">
        ${message}
    </div>
    </div>`;
    $("#messages").append(messageDiv);
    return false;
}
