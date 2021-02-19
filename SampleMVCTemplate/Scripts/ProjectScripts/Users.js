var tbUserSearchResults;
var tbUserSearchId = "tbUserSearch";
var btnUserSearchId = "btnSearch";
var btnUserResetId = "btnReset";
var userSearchParams;
var hdTotalPageCount = "hdTotalPageCount";

var userCreateSave = "btnUserCreateSave";
var userEditSave = "btnUserEditSave";
var userEditActivateInactivate = "btnUserEditActivateInactivate";
var frmUserCUDOperation = "frmUserCUDOperation";

$(document).ready(function () {  
    if (pageName == null || pageName == undefined || pageName == "") {
        InitialUserLoad();
        RegisterUserClickEvents();
    }
    else if (pageName == "CreateUpdateDeleteSaveIndex") {
        LoadCreateUpdateDeleteSection("1");
        RegisterEvents();
    }
});

function InitialUserLoad() {
    ShowHideExpandCollapseIcons('btnExpcollapseUserSearch');
    LoadUsers("1", "btnSearch", "", sortOrderAsc, defPageIndex);   
}

function RegisterUserClickEvents() {
    $("#" + btnUserSearchId).click(function () {
       LoadUsers("1", this.id, "", sortOrderAsc, defPageIndex );
    });
    $("#" + btnUserResetId).click(function () {
        LoadUsers("0", this.id, "", sortOrderAsc, defPageIndex);
    });
}
function LoadUserPager(isDestroyed) {
    var totPageCnt = $("#" + hdTotalPageCount).val();
    if (totPageCnt == "" || totPageCnt == undefined || totPageCnt != null) {        
        totPageCnt = 1;
    }
    if (isDestroyed=="1") {
        $('#UserPager').twbsPagination('destroy');
    }
    $('#UserPager').twbsPagination({
        totalPages: totPageCnt,
        visiblePages: 10,
        next: 'Next',
        prev: 'Prev',
        initiateStartPageClick: false,
        onPageClick: function (event, page) {      
            if (userSearchParams != null && userSearchParams != undefined) {
                LoadUsers("3", btnUserSearchId, userSearchParams.SortColumnName, userSearchParams.SortAscDesc, page);
            }
            else {
                LoadUsers("3", btnUserSearchId, "", sortOrderAsc, page);
            }
        }
    });
}

function ClearSearchSection(isSearch) {
    if (isSearch == "0") {
        $("#UserName").val("");
        $("#LastName").val("");
        $("#FirstName").val("");
        $("#optCols").val("-1");
        $("#RoleId").val("-2");
        $("#StatusCode").val("1");
    }
}
function GetSearchParameters(isSearch, sortCol, sortAscDesc, pageIndex) {
    ClearSearchSection(isSearch);
    var params = {
        UserName: $("#UserName").val(),
        LastName: $("#LastName").val(),
        FirstName: $("#FirstName").val(),
        RoleId: $("#RoleId").val(),
        StatusCode: $("#StatusCode").val(),
        IsSearch: isSearch,
        PageIndex: defPageIndex,
        SortColumnName: "",
        SortAscDesc: sortOrderAsc
    };

    if (isSearch == "0" || isSearch == "1") {
        userSearchParams = params;
    }
    else if (isSearch == "2" || isSearch == "3") { //sorting-2,paging -3
        if (userSearchParams == null || userSearchParams == undefined) {
            userSearchParams = params;
        }
        userSearchParams.PageIndex = pageIndex;
        userSearchParams.SortColumnName = sortCol;
        userSearchParams.SortAscDesc = sortAscDesc;
        params = userSearchParams;
    }
    return params;
}

function LoadUsers(isSearch, eleId, sortCol, sortAscDesc, pageIndex) {
    var outputResponse;
    var params = null;
    var headers = null;
    var dataType = "html";

    params = GetSearchParameters(isSearch, sortCol, sortAscDesc, pageIndex);

    headers = AppendMenuActionCodeHeaderByElementId(null, eleId);
    
    var ajaxTemp = GetData("Users/Search", params, headers, dataType);
    
    ajaxTemp.then(function (response) {
        response = JSON.parse(response);
        ShowMessage(response.MessageCode, response.MessageText);
        $("#" + hdTotalPageCount).val(response.TotalPageCount);

        $("#divUserSearch").empty();
        $("#divUserSearch").html(response.HtmlContent);
        SetUserSortingPaging(tbUserSearchId)
        tbUserSearchResults = $("#" + tbUserSearchId).DataTable({
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
function SetUserSortingPaging(tableId) {
    SetUserSortingForColumn(tableId);
    SetUserPagingForColumn();
}

function SetUserSortingForColumn(tableId) {
    var aLinks = $("#" + tableId).find("thead").find("a");

    $.each(aLinks, function (i, val) {
        if ($(this).attr("data-sortable") == "true") {
            $(this).click(function () {
                LoadUserPager("1");  
                if (userSearchParams != null && userSearchParams != undefined) {
                    if (userSearchParams.SortColumnName == "") {
                        LoadUsers("2", btnUserSearchId, $(this).attr("data-sort-col-name"), sortOrderDesc, defPageIndex);
                    }
                    else if (userSearchParams.SortColumnName == $(this).attr("data-sort-col-name") && userSearchParams.SortAscDesc != sortOrderDesc) {
                        LoadUsers("2", btnUserSearchId, $(this).attr("data-sort-col-name"), sortOrderDesc, defPageIndex );
                    }
                    else {
                        LoadUsers("2", btnUserSearchId, $(this).attr("data-sort-col-name"), sortOrderAsc, defPageIndex );
                    }
                }
            });
        }
    });
}

function SetUserPagingForColumn() {
    LoadUserPager("0");  
}


//Create or Edit Role

function RegisterSave() {
    $("#" + userCreateSave).click(function () {
        CUDOperation("A00012", "A");
        return false;
    });

    $("#" + userEditSave).click(function () {
        CUDOperation("A00013", "E");
        return false;
    });
}

function RegisterDelete() {
    $("#" + userEditActivateInactivate).click(function () {
        if (confirm(activaetInactivateMessage)) {
            CUDOperation("A00014", "AI");
        }
        return false;
    });
}

function CUDOperation(menuActionCode, operationType) {
    HideSuccessErrorMessage();
    var formValidresult = $(document.getElementById(frmUserCUDOperation)).valid();  
    if (formValidresult == "0") {
        return false;
    }
    var str = $(document.getElementById(frmUserCUDOperation)).serializeObject();
    var params = null;
    var headers = null;
    var dataType = "json";

    str.CUDOperationType = operationType;

    var params = {
        usersVM: str,
        __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val()
    };

    headers = AppendMenuActionCodeHeader(null, menuActionCode);

    var ajaxTemp = PostData("Users/CreateUpdateDeleteSaveOperation", params, headers, dataType);

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
        UserId: $("#UserId").val(),
        CUDOperationType: $("#CUDOperationType").val()
    };

    headers = AppendMenuActionCodeHeader(null, "A00011");

    var ajaxTemp = GetData("Users/CreateUpdateDeleteSaveView", params, headers, dataType);

    ajaxTemp.then(function (response) {
        response = JSON.parse(response);
        if (showMessage=="1") {
            ShowMessage(response.MessageCode, response.MessageText);
        }
        $("#divUserCreateUpdateDeleteSaveView").empty();
        $("#divUserCreateUpdateDeleteSaveView").html(response.HtmlContent);
        RegisterEvents();
        $.validator.unobtrusive.parse('#' + frmUserCUDOperation);
    },
        function (xhr, status, error) {
            console.log(error);
        });
}