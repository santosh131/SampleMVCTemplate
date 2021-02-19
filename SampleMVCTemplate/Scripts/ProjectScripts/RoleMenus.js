var ddlMenuId = "MenuId";
var menuActionSave = "btnMenuActionSave";
var tbRoleMenuActionId = "tbRoleMenuAction";
var tbRoleMenuActionResults;

$(document).ready(function () {
    LoadRoleMenuActions();
    RegisterEvents();
});

function RegisterMenuChange() {
    $("#" + ddlMenuId).change(function () {
        LoadRoleMenuActions();
    });
}

function RegisterSave() {
    $("#" + menuActionSave).click(function () {
        HideSuccessErrorMessage();
        var frm = $(document.getElementById("frmMenuAction"));
        var str = frm.serializeObject();
        var params = null;
        var headers = null;
        var dataType = "json";

        str = frm.serializeFormJSON('tbRoleMenuAction', str, 'RoleMenuAction'); 
         
        var params = {
            rolesmVM: str,
            __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
        }; 

        headers = AppendMenuActionCodeHeader(null, "A00025");

        var ajaxTemp = PostData("Roles/MenuActionsSave", params, headers, dataType);
        ajaxTemp.then(function (response) {
            if (ShowMessage(response.MessageCode, response.MessageText)) {
                LoadRoleMenuActions();
            }
        },
            function (xhr, status, error) {
                console.log(error);
            });
    });
}

function RegisterEvents() {
    RegisterMenuChange();
    RegisterSave();
}

function LoadRoleMenuActions() {
    var params = null;
    var headers = null;
    var dataType = "html";

    var params = {
        RoleId: $("#RoleId").val(),
        MenuId: $("#MenuId").val()
    };

    headers = AppendMenuActionCodeHeader(null, "A00025");

    var ajaxTemp = GetData("Roles/MenuActions", params, headers, dataType);

    ajaxTemp.then(function (response) {
        $("#divRoleMenuAction").empty();
        $("#divRoleMenuAction").html(response);
        tbRoleMenuActionResults = $("#" + tbRoleMenuActionId).DataTable({
            searching: false,
            lengthChange: false,
            paging: false,
            bInfo: false,
            scrollY: "280px",
            scrollX: true,
            ordering: true
        });
        RegisterSave();
    },
        function (xhr, status, error) {
            console.log(error);
        });
}