$(function () {
    var authProxy = $.connection.AuthHub;  //get proxy
    authProxy.client.notify = function (message) {  //set on functions
        alert(message);
    };
    
    window.connectionHub.done(function () {
        $("#loginBtn").click(function () {
            authProxy.server.login($("#userNameText").val(), $("#passwordText").val()).done(function (res) { //invoke server side function
                alert(res);
            }).fail(function () { alert('fail invoke'); });
        });
    }).fail(function (error) {

    });
});