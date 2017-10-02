var page = require('webpage').create();
page.onConsoleMessage = function (msg) {
    phantom.outputEncoding = "utf-8";
    console.log(msg);
};

page.onLoadStarted = function () {
    loadInProgress = true;
    //console.log("load started");
};

page.onLoadFinished = function () {
    loadInProgress = false;
    //console.log("load finished");
};
page.open('http://192.168.22.208', function(status) {
  //console.log("Status: " + status);
  if(status === "success") {
    page.render('example.png');
    page.evaluate(function () {
        
                    //a = document.getElementsByClassName("t5col");
                    //a[0].value = "_cep"
                    //a = document.getElementsByClassName("btn1 f2col float-right");
                    //a[0].click();
        
                    a = document.getElementById("username");
                    a.value = "ADMIN"
        
                    a = document.getElementById("password");
                    a.value = "180516"

                    a = document.getElementById("entrar");
                    a.click();
                   /* a = document.getElementsByTagName("input");
                    a[3].click();*/
                   
                });  
                interval = setInterval(function () {
                  // console.log("Interval ");

                    if(!loadInProgress){
                      //  console.log("getInfo");

                        page.open("http://192.168.22.208/info",function(){
                            var jsonSource = page.plainText;
                           // console.log(jsonSource);
                            var resultObject = JSON.parse(jsonSource);
                            console.log(resultObject.info[0].statusImpressora);
                           phantom.exit();
                        });
                       

                    }
                }, 15750);
}

});
