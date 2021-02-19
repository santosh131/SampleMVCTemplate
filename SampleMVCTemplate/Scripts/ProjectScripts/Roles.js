var tbRoleSearchResults;
var tbRoleSearchId = "tbRoleSearch";
var btnRoleSearchId = "btnSearch";
var btnRoleResetId = "btnReset";
var RoleSearchParams;
var hdTotalPageCount = "hdTotalPageCount";

var roleCreateSave = "btnRoleCreateSave";
var roleEditSave = "btnRoleEditSave";
var roleEditActivateInactivate = "btnRoleEditActivateInactivate";
var frmRoleCUDOperation = "frmRoleCUDOperation";

$(document).ready(function () {
    if (pageName == null || pageName == undefined || pageName == "") {
        InitialRoleLoad();
        RegisterRoleClickEvents();
    }
    else if (pageName == "CreateUpdateDeleteSaveIndex") {
        LoadCreateUpdateDeleteSection("1");
        RegisterEvents();
    }
});

function InitialRoleLoad() {
    ShowHideExpandCollapseIcons('btnExpcollapseRoleSearch');
    LoadRoles("1", "btnSearch", "", sortOrderAsc, defPageIndex);
}

function RegisterRoleClickEvents() {
    $("#" + btnRoleSearchId).click(function () {
        LoadRoles("1", this.id, "", sortOrderAsc, defPageIndex);
    });
    $("#" + btnRoleResetId).click(function () {
        LoadRoles("0", this.id, "", sortOrderAsc, defPageIndex);
    });

}
function LoadRolePager(isDestroyed) {
    var totPageCnt = $("#" + hdTotalPageCount).val();
    if (totPageCnt == "" || totPageCnt == undefined || totPageCnt != null) {
        totPageCnt = 1;
    }
    if (isDestroyed == "1") {
        $('#RolePager').twbsPagination('destroy');
    }
    $('#RolePager').twbsPagination({
        totalPages: totPageCnt,
        visiblePages: 10,
        next: 'Next',
        prev: 'Prev',
        initiateStartPageClick: false,
        onPageClick: function (event, page) {
            if (RoleSearchParams != null && RoleSearchParams != undefined) {
                LoadRoles("3", btnRoleSearchId, RoleSearchParams.SortColumnName, RoleSearchParams.SortAscDesc, page);
            }
            else {
                LoadRoles("3", btnRoleSearchId, "", sortOrderAsc, page);
            }
        }
    });
}

function ClearSearchSection(isSearch) {
    if (isSearch == "0") {
        $("#RoleName").val("");
        $("#optCols").val("-1");
        $("#StatusCode").val("1");
    }
}
function GetSearchParameters(isSearch, sortCol, sortAscDesc, pageIndex) {
    ClearSearchSection(isSearch);
    var params = {
        RoleName: $("#RoleName").val(),
        StatusCode: $("#StatusCode").val(),
        IsSearch: isSearch,
        PageIndex: defPageIndex,
        SortColumnName: "",
        SortAscDesc: sortOrderAsc
    };

    if (isSearch == "0" || isSearch == "1") {
        RoleSearchParams = params;
    }
    else if (isSearch == "2" || isSearch == "3") { //sorting-2,paging -3
        if (RoleSearchParams == null || RoleSearchParams == undefined) {
            RoleSearchParams = params;
        }
        RoleSearchParams.PageIndex = pageIndex;
        RoleSearchParams.SortColumnName = sortCol;
        RoleSearchParams.SortAscDesc = sortAscDesc;
        params = RoleSearchParams;
    }
    return params;
}

function LoadRoles(isSearch, eleId, sortCol, sortAscDesc, pageIndex) {
    var outputResponse;
    var params = null;
    var headers = null;
    var dataType = "html";

    params = GetSearchParameters(isSearch, sortCol, sortAscDesc, pageIndex);

    headers = AppendMenuActionCodeHeaderByElementId(null, eleId);

    var ajaxTemp = GetData("Roles/Search", params, headers, dataType);

    ajaxTemp.then(function (response) {
        response = JSON.parse(response);
        ShowMessage(response.MessageCode, response.MessageText);
        $("#" + hdTotalPageCount).val(response.TotalPageCount);

        $("#divRoleSearch").empty();
        $("#divRoleSearch").html(response.HtmlContent);

        SetRoleSortingPaging(tbRoleSearchId)
        tbRoleSearchResults = $("#" + tbRoleSearchId).DataTable({
            searching: false,
            lengthChange: false,
            paging: false,
            bInfo: false,
            scrollY: "280px",
            scrollX: true,
            ordering: false
        });

    },
        function (xhr, status, error) {
            console.log(error);
        });
}
function SetRoleSortingPaging(tableId) {
    SetRoleSortingForColumn(tableId);
    SetRolePagingForColumn();
}

function SetRoleSortingForColumn(tableId) {
    var aLinks = $("#" + tableId).find("thead").find("a");

    $.each(aLinks, function (i, val) {
        if ($(this).attr("data-sortable") == "true") {
            $(this).click(function () {
                LoadRolePager("1");
                if (RoleSearchParams != null && RoleSearchParams != undefined) {
                    if (RoleSearchParams.SortColumnName == "") {
                        LoadRoles("2", btnRoleSearchId, $(this).attr("data-sort-col-name"), sortOrderDesc, defPageIndex);
                    }
                    else if (RoleSearchParams.SortColumnName == $(this).attr("data-sort-col-name") && RoleSearchParams.SortAscDesc != sortOrderDesc) {
                        LoadRoles("2", btnRoleSearchId, $(this).attr("data-sort-col-name"), sortOrderDesc, defPageIndex);
                    }
                    else {
                        LoadRoles("2", btnRoleSearchId, $(this).attr("data-sort-col-name"), sortOrderAsc, defPageIndex);
                    }
                }
            });
        }
    });
}

function SetRolePagingForColumn() {
    LoadRolePager("0");
}


//Create or Edit Role

function RegisterSave() {
    $("#" + roleCreateSave).click(function () {
        CUDOperation("A00022", "A");
        return false;
    });

    $("#" + roleEditSave).click(function () {
        CUDOperation("A00023", "E");
        return false;
    });
}

function RegisterDelete() {
    $("#" + roleEditActivateInactivate).click(function () {
        if (confirm(activaetInactivateMessage)) {
            CUDOperation("A00024", "AI");
        }
        return false;
    });
}

function CUDOperation(menuActionCode, operationType) {
    HideSuccessErrorMessage();
    var formValidresult = $(document.getElementById(frmRoleCUDOperation)).valid();
    if (formValidresult == "0") {
        return false;
    }
    var str = $(document.getElementById(frmRoleCUDOperation)).serializeObject();
    var params = null;
    var headers = null;
    var dataType = "json";

    str.CUDOperationType = operationType;

    var params = {
        roles: str,
        __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
    };

    headers = AppendMenuActionCodeHeader(null, menuActionCode);

    var ajaxTemp = PostData("Roles/CreateUpdateDeleteSaveOperation", params, headers, dataType);

    ajaxTemp.then(function (response) {
        if (ShowMessage(response.MessageCode, response.MessageText)) {
            LoadCreateUpdateDeleteSection("0");
        }
    },
        function (xhr, status, error) {
            console.log(error);
        });
}

function RegisterEvents() {
    RegisterSave();
    RegisterDelete();
}

function LoadCreateUpdateDeleteSection(showMessage) {
    var params = null;
    var headers = null;
    var dataType = "html";

    var params = {
        RoleId: $("#RoleId").val(),
        CUDOperationType: $("#CUDOperationType").val()
    };

    headers = AppendMenuActionCodeHeader(null, "A00021");

    var ajaxTemp = GetData("Roles/CreateUpdateDeleteSaveView", params, headers, dataType);

    ajaxTemp.then(function (response) {
        response = JSON.parse(response);
        if (showMessage=="1") {
            ShowMessage(response.MessageCode, response.MessageText);
        }
        $("#divRoleCreateUpdateDeleteSaveView").empty();
        $("#divRoleCreateUpdateDeleteSaveView").html(response.HtmlContent);
        RegisterEvents();
        $.validator.unobtrusive.parse('#' + frmRoleCUDOperation);
    },
        function (xhr, status, error) {
            console.log(error);
        });
}