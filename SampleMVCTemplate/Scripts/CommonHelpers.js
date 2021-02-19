var sortOrderDesc = "desc";
var sortOrderAsc = "";
var defPageIndex = 1;
var activaetInactivateMessage = "Any changes made to this record will not be saved, record will either be Activated or Inactivated. Are you sure you want to continue?"
var deleteMessage = "Any changes made to this record will not be saved, record will be Deleted. Are you sure you want to continue?"
var pageName = "";

$(document).ready(function () {
    $(".make-pass").attr("type", "password");
}); 

function GetMenuActionCode(obj)
{
    if(obj!=null && obj!=undefined)
    {
        return $(obj).attr('data-menu-action-code');
    }
    return "";
}

function AppendMenuActionCodeHeader(header, menuActionCode)
{
    var headerTemp = [];
    if (header != null && header != undefined) {        
        headerTemp =header;
    }
    headerTemp.MenuActionCode = menuActionCode;   
    return headerTemp;
}

function AppendMenuActionCodeHeaderByElementId(header, eleId) {
    var menuActionCode = "";
    if (eleId != undefined && eleId != null && eleId != "") {
        menuActionCode = GetMenuActionCode($("#" + eleId));
    }
    return AppendMenuActionCodeHeader(header, menuActionCode);
}

function ShowHideExpandCollapseIcons(id)
{
    $('#' + id).click(function () {
        $(this)
          .find('[data-fa-i2svg]')
          .toggleClass('fa-angle-double-down')
          .toggleClass('fa-angle-double-up');
    });
} 

//Hides the columns in the DataTable based on the Options selected in multiselect
function HideColumnsBasedOnConfiguration(tableObj, colConfigs) {
    if (colConfigs != null && colConfigs != undefined) {
        for (var key in colConfigs) {
            ShowHideUserSearchColumns(tableObj,colConfigs[key])
        }
    }
}

//Helper function to hides the column in the DataTable based on the Options selected in multiselect
function ShowHideUserSearchColumns(tableObj, obj) {
    if (tableObj != null && tableObj != undefined) {
        tableObj.column(obj.ColumnNumber).visible(obj.IsChecked);
    }
}

function ShowMessage(messageCode, msgText) {
    if (messageCode != null && messageCode != undefined && messageCode != "") {
        if (messageCode.toUpperCase() == "SUCCESS_LIST") {
            HideSuccessErrorMessage();
            return true;
        }
        else if (messageCode.toUpperCase() == "SUCCESS_GET") {
            HideSuccessErrorMessage();
            return true;
        }
        else if (messageCode.toUpperCase() == "SUCCESS") {
            ShowSuccessMessage(msgText);
            return true;
        }
        else {
            ShowErrorMessage(msgText);
            return false;
        }
    }
    return false;
}

function ShowSuccessMessage(msgText) {
    HideSuccessErrorMessage();

    $("#divSuccessMessage").show();
    $("#divSuccessMessageText").html(msgText);
}

function ShowErrorMessage(msgText) {
    HideSuccessErrorMessage();

    $("#divErrorMessage").show();
    $("#divErrorMessageText").html(msgText);
}

function HideSuccessErrorMessage() {
    $("#divSuccessMessage").hide();
    $("#divSuccessMessageText").empty();
    $("#divErrorMessage").hide();
    $("#divErrorMessageText").empty(); 
}