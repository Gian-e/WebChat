const loginCookieName = "login";
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        await registerConnection();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function registerConnection() {
    const login = $.cookie(loginCookieName);
    await connection.invoke("RegisterConnection", login);
}

function newMessageToast(message, sender) {
    let toast = $("#snackbar");
    toast.html(`<b>${sender}:</b> ${message}`);
    // Add the "show" class to DIV
    toast.addClass('show');

    // After 3 seconds, remove the show class from DIV
    setTimeout(function () { toast.removeClass('show') }, 3000);
}
connection.onclose(async () => {
    await start();
});

// Start the connection.
start();