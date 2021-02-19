var timeOutInterval;
var showDialogBefore = 1;
var timeoutCounter;
var timeI;
var timeIDisplayCounter;
function CreateTimeOutDialog()
{
    var timeInter = (showDialogBefore) * 60;
    $("#divdialog").dialog({
        autoOpen: false,         
        resizable: false,
        modal: true,
        title: "Session will Timeout in " + timeInter + " Seconds",
        width: 400,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close").hide(); 
        },
        buttons: {
            Yes: function () {
                $.ajax({
                    url: '/Session/Index',
                    dataType: "json",
                    type: "GET",
                    error: function () {                        
                        HideTimeOut();
                    },
                    success: function (data) {
                        HideTimeOut();
                    }
                });
                CreateTimeOut();
                clearTimeout(timeoutCounter);
                clearTimeout(timeIDisplayCounter);
            },
            Logout: function () {
                location.href = '/Login/Logout';
                clearTimeout(timeoutCounter);
                clearTimeout(timeIDisplayCounter);
            }
        }
    });
     
}

function DisplayChangedTimer()
{    
    timeI = timeI - 1;
    if (timeI > 0) {
        $("#divdialog").dialog('option', 'title', "Session will Timeout in " + timeI + " Seconds");
    }
    else
        clearTimeout(timeIDisplayCounter);
}

function ShowTimeOut() {   
    $("#divdialog").dialog('open');
    timeoutCounter = setTimeout(function () {
        location.href = '/Login/Logout';
    }, (showDialogBefore) * 60000);

    timeI = (showDialogBefore) * 60;
    timeIDisplayCounter = setInterval(function () { DisplayChangedTimer() }, 1000)
}


function HideTimeOut()
{
    $("#divdialog").dialog('close');
}