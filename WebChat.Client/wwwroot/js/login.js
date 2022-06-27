function Login() {
    const login = $("#login").val();
    $.post("Login/login", { login }).then(() => {
        window.location.href = "/";
    });
}