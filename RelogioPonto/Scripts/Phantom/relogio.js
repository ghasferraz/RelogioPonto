
  

    var page = require('webpage').create();
    page.onConsoleMessage = function (msg) {
        phantom.outputEncoding = "utf-8";
        console.log(msg);
    };

    page.onLoadStarted = function () {
        loadInProgress = true;
       // console.log("load started");
    };

    page.onLoadFinished = function () {
        loadInProgress = false;
        //console.log("load finished");
    };
    page.open("http://#ip#", function (status) {

        //  console.log("Status: " + status);
        var check1 = page.evaluate(function () {
            return document.title
        });
        if (status === "success" && check1 =="Login | Inner Rep Plus") {
          //  page.render('example.png');
            page.evaluate(function () {

        

                a = document.getElementById("username");
                a.value = "#usuario#";

                a = document.getElementById("password");
                a.value = "#senha#";

                a = document.getElementById("entrar");
                a.click();

            });
           // page.render('example2.png');
            interval = setInterval(function () {
           //     console.log("Interval ");

                if (!loadInProgress) {
                  //  console.log("getInfo");

                    page.open("http://#ip#/info", function () {
                      //  console.log("Passei aqui");
                        var jsonSource = page.plainText;
                        //console.log(jsonSource);
                        var resultObject = JSON.parse(jsonSource);
                        console.log(resultObject.info[0].statusImpressora);
                        phantom.exit();
                    });


                }
            }, 750);
        }
        else {
            console.log("IP invalido");
            phantom.exit();
        }
    }); 
    

