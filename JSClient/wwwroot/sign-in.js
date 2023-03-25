var createState = function () {
    return "ShouldBeALongSha256String";
}

var createNonce = function () {
    return "JustANonceValue";
}
var signIn = function () {
    var redirectUri = "https://localhost:46025/home/signin";
    var responseType = "id_token token";
    var scope = "openid ApiOne";
    var authUrl = `/connect/authorize/callback?client_id=client_id_js&redirect_uri=${encodeURIComponent(redirectUri)}&response_type=${encodeURIComponent(responseType)}&scope=${encodeURIComponent(scope)}&nonce=${createNonce()}&state=${createState()}`;
    var returnUrl = encodeURIComponent(authUrl)
    console.log(authUrl);
    console.log(returnUrl);

    window.location.href = "https://localhost:5001/Auth/Login?ReturnUrl=" + returnUrl;
}
