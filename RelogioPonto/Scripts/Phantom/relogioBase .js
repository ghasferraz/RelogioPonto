var args = require('system').args;
if (args.length != 4) {
    console.log("numero invalido de parametros");
    phantom.exit();

} else {
    var ip = args[1];
    var usuario = args[2];
    var senha = args[3];
  //  console.log(usuario);
   // console.log(senha);
    var page = require('webpage').create();
    page.onConsoleMessage = function (msg) {
        phantom.outputEncoding = "utf-8";
        console.log(msg);
    };

    page.onLoadStarted = function () {
        loadInProgress = true;
        console.log("load started");
    };

    page.onLoadFinished = function () {
        loadInProgress = false;
        console.log("load finished");
    };
    page.open(ip, function (status, usuario,senha) {
        console.log(usuario);
        console.log("Status: " + status);
        if (status === "success") {
            page.render('example.png');
            page.evaluate(function (usuario, senha) {

                console.log(usuario);
                console.log(senha);

                a = document.getElementById("username");
                a.value = usuario;

                a = document.getElementById("password");
                a.value = senha;

                a = document.getElementById("entrar");
                a.click();

            });
            page.render('example2.png');
            interval = setInterval(function () {
                console.log("Interval ");

                if (!loadInProgress) {
                    console.log("getInfo");

                    page.open(ip + "/info", function () {
                        console.log("Passei aqui");
                        var jsonSource = page.plainText;
                        console.log(jsonSource);
                        var resultObject = JSON.parse(jsonSource);
                        console.log(resultObject.info[0].statusImpressora);
                        phantom.exit();
                    });


                }
            }, 15750);
        }

    }, usuario,senha);
    
}
