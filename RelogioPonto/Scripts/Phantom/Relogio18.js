
  

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
    page.open("http://"+192.168.11.44, function (status) {
    
        console.log("Status: " + status);
        if (status === "success") {
            page.render('example.png');
            page.evaluate(function () {

        

                a = document.getElementById("username");
                a.value = 15;

                a = document.getElementById("password");
                a.value = 15;

                a = document.getElementById("entrar");
                a.click();

            });
            page.render('example2.png');
            interval = setInterval(function () {
                console.log("Interval ");

                if (!loadInProgress) {
                    console.log("getInfo");

                    page.open("http://"+192.168.11.44 + "/info", function () {
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

    });
    

