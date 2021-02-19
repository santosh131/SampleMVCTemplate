$(document).ready(function () {
    $.ajaxSetup({
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            window.location = baseUrl + '/Error';
        }
    });
});

function GetData(url, params, header, dataType) {
    var outputResponse;
    var headerTemp;
    var dataTypes = "json";
    if (header != null && header != undefined) {
        headerTemp = header;
    }
    if (dataType != null && dataType != undefined) {
        dataTypes = dataType;
    }
    var ajaxTemp = $.ajax({
        url: baseUrl + "/" + url,
        type: 'GET',
        data: params,
        headers: headerTemp,
        dataType: dataTypes,
        async: false,
    });
    return ajaxTemp;
}


function PostData(url, params, header, dataType) {
    var outputResponse;
    var headerTemp;
    var dataTypes = "json";
    if (header != null && header != undefined) {
        headerTemp = header;
    }
    if (dataType != null && dataType != undefined) {
        dataTypes = dataType;
    }
    params = addRequestVerificationToken(params);

    var ajaxTemp = $.ajax({
        url: baseUrl + "/" + url,
        type: 'POST',
        data: params,
        contentType: 'application/x-www-form-urlencoded; charset=utf-8',
        headers: headerTemp,
        dataType: dataTypes,
        async: false,
    });
    return ajaxTemp;
}

function addRequestVerificationToken(data) {
    data.__RequestVerificationToken = $('input[name=__RequestVerificationToken]').val();
    return data;
};

$.fn.serializeObject = function () {
    var o = {};
    var disabled = this.find(':input:disabled').removeAttr('disabled');
    var a = this.serializeArray();
    disabled.attr('disabled', 'disabled');
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            //if (!o[this.name].push) {
            //    o[this.name] = [o[this.name]];
            //}
            //o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;

    //var obj = {};
    //$('input', this).each(function () {
    //    obj[this.name] = $(this).val();
    //});
    //return $.param(obj);
}


$.extend({
    toDictionary: function (query) {
        var parms = {};
        var items = query.split("&"); // split
        for (var i = 0; i < items.length; i++) {
            var values = items[i].split("=");
            var key = decodeURIComponent(values.shift());

            if (key.indexOf(']') > -1 && key.indexOf('.') > -1) {
                key = key.substring(key.indexOf('.') + 1, key.length);
            }

            if (parms[key] !== undefined) {

            }
            else {
                var value = values.join("=");
                parms[key] = decodeURIComponent(value);
            }
        }
        return (parms);
    }
});

$.fn.serializeFormJSON = function (tableId, obj, propertyName) {
    var o = [];
    $(this).find('table[id$=' + tableId + ']').find('tr').each(function () {
        var elements = $(this).find('input, textarea, select')
        if (elements.size() > 0) {
            var serialized = elements.serialize();
            var item = $.toDictionary(serialized);
            o.push(item);
        }
    });
    obj[propertyName] = o;
    return obj;
};