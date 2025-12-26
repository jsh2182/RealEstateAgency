$(document).on('click', '.panel-heading span.clickable', function (e) {
    var $this = $(this);
    if (!$this.hasClass('panel-collapsed')) {
        $this.parents('.panel').find('.panel-body').slideUp();
        $this.addClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');

    } else {
        $this.parents('.panel').find('.panel-body').slideDown();
        $this.removeClass('panel-collapsed');
        $this.find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');

    }
})

$(function () {

    var myWindow = $("#GroupsWindow");
    var dialog = $('#dialog');

    $("#btnSelectGroup").removeAttr("href");
    dialog.kendoDialog({
        width: "450px",
        title: "پیام سیستم",
        visible: false,
        closable: true,
        modal: true,
        content: ""
    });

    myWindow.kendoWindow({
        width: "800px",
        title: "انتخاب گروه",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ]
    });
    $("#btnSelectGroup").click(function () {
        myWindow.data("kendoWindow").center().open();
    });
    var date;
    date = $('#PBirthDate').persianDatepicker({

        initialValue: false,
        inline: false,
        altField: '#inlineExampleAlt',
        altFormat: 'LL',
        format: 'L',
        calendar: {
            persian: {
                locale: 'fa'
            }
        },
        toolbox: {
            calendarSwitch: {
                enabled: false
            }
        },
        navigator: {
            scroll: {
                enabled: true
            }
        },
        minDate: new persianDate(),
        timePicker: {
            enabled: false,
            meridiem: {
                enabled: true
            }
        },
        onSelect: function (unix) {

            var state = date.getState();
            var gregorianDate = new persianDate([state.selected.year, state.selected.month, state.selected.date])
                .toCalendar('gregorian').toLocale('en').format('YYYY-MM-DD');
            $('#BirthDate').val(gregorianDate);
            $('.datepicker-container').addClass('pwt-hide');
        }

    });
    $('[data-toggle="tooltip"]').tooltip();
    $("#btnGetCode").on("click", function () {
        $.ajax({
            url: "/Person/GetNextCode",
            dataType: "JSON",
            type: "POST",
            success: function (result) {
                if (result != 400) {
                    $("#SubscriptionCode").val(result);
                }
            }
        });
    });

});
var GroupList = [];
function GetProvinceIDForSearch() {
    return { provinceID: $('#ProvinceIDSearch').data('kendoComboBox').value() }
}
function GetCityIDForSearch() {
    return { cityID: $('#CityIDSearch').data('kendoComboBox').value() }
}
function GetProvinceID() {
    return { provinceID: $('#ProvinceID').data('kendoComboBox').value() }
}
function GetCityID() {
    return { cityID: $('#CityID').data('kendoComboBox').value() }
}
function GetPersonID() {
    return { personID: $('#PersonID').val() }
}
function SelectGroup() {
    var gridData = $('#gridSelectPersonGroup').data('kendoGrid').dataSource.data();
    var data4Init = [];
    GroupList = [];
    $('.Groups').each(function () {
        if ($(this).is(":checked")) {
            var id = parseInt($(this).val());
            GroupList.push(id);
            var model = gridData.find(g => g.GroupID == id);
            data4Init.push({ GroupID: id, GroupName: model.GroupName, GroupDesc: model.GroupDesc });

        }
    });
    $('#gridPersonGroups').data('kendoGrid').dataSource.data(data4Init);
    $('#GroupsWindow').data('kendoWindow').close();
}
function ResetForm() {
    $("input:text").each(function () {
        $(this).val('');
    })
    $("input[type=number]").each(function () {
        $(this).val('0');
    })
    $("#PersonID").val(0);
    $("textarea").each(function () {
        $(this).val('');
    })
    $("input:checkbox").each(function () {
        $(this).prop('checked', false);
    })
    $(".k-combobox").each(function () {
        if ($(this)[0].childNodes[1].name.toLowerCase().indexOf("search") > 0) {
            return;
        }
        let cmb = $("#" + $(this)[0].childNodes[1].name).data("kendoComboBox");
        if (cmb !== undefined) {
            cmb.value(null);
            cmb.dataSource.read();
        }
    })
    $('#gridPersonGroups').data('kendoGrid').dataSource.data([]);
    $('.Groups').each(function () {
        $(this).prop("checked", false)
    });
    $.ajax({
        url: "/Person/GetNextCode",
        dataType: "JSON",
        type: "POST",
        success: function (result) {
            if (result != 400) {
                $("#SubscriptionCode").val(result);
            }
        }
    });
}
function Submit() {
    let requiredElements = $("[isRequired]");
    for (let i = 0; i < requiredElements.length; i++) {
        if (($(requiredElements[i]).closest("div").css("display") !== "none") && ($(requiredElements[i]).val() == "" /*|| $(requiredElements[i]).val() == "0")*/)) {
            $('#dialog').data('kendoDialog').content('<p>لطفا فیلدهای الزامی را پر کنید</p>');
            $('#dialog').data('kendoDialog').open();
            return;
        }
    }
    if (GroupList.length == 0 && parseInt($("#PersonID").val()) === 0) {
        $('#dialog').data('kendoDialog').content('<p>گروهبندی مشخص نشده است</p>');
        $('#dialog').data('kendoDialog').open();
        return;
    }

    var formData = $('#frmPersonInfo').serialize();
    $.ajax({
        url: '/Person/Submit',
        data: formData + '&GroupIDs=' + JSON.stringify(GroupList),
        //dataType: 'JSON',
        type: 'POST',
        success: (function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('<p>عملیات با موفقیت انجام شد</p>');
                $('#dialog').data('kendoDialog').open();
                ResetForm();
            }
            else if (result == 400) {
                $('#dialog').data('kendoDialog').content('<p>اشکال در انجام عملیات</p>');
                $('#dialog').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }

        })
    })
}
function DeletePerson(id) {
    $.ajax({
        url: "/Person/Delete/" + id,
        type: "Delete",
        dataType: "JSON",
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#gridSearch').data('kendoGrid').dataSource.read();

            }
            else if (result == 403) {
                $('#dialog').data('kendoDialog').content('حذف این شخص مجاز نیست.');
            }
            else {
                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
            }
            $('#dialog').data('kendoDialog').open();

        }
    })
}
function GetSearchData() {
    var result = {};
    $.each($('#frmSearch').serializeArray(), function () {
        result[this.name] = this.value;
    });
    return result;
}
function Search() {
    $('#gridSearch').data('kendoGrid').dataSource.read();
}
function SubmitInfo() {
    $.ajax({
        url: '/Person/SubmitOtherInfo',
        type: 'POST',
        dataType: 'JSON',
        data: {
            PersonID: $('#PersonID').val(),
            InfoDesc: $('#InfoDesc').val()
        },
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('<p>عملیات با موفقیت انجام شد</p>');

                $('#gridOtherInfo').data('kendoGrid').dataSource.read();
                $('#InfoID').val('');
                $('#InfoDesc').val('');
                $('#divNewInfo').hide();
            }
            else if (result == 204)
                $('#dialog').data('kendoDialog').content('<p>شخصی برای افزودن اطلاعات وجود ندارد</p>');
            else if (result == 401) {
                $('#dialog').data('kendoDialog').content('<p>شما اجازه ویرایش این اطلاعات را ندارید</p>')
            }
            else {
                $('#dialog').data('kendoDialog').content('<p>اشکال در انجام عملیات</p>');
            }
            $('#dialog').data('kendoDialog').open();
        }
    })
}
function ShowNewInfo() {
    $('#divNewInfo').show();
    $('#btnNewInfo').hide();
}
function OnGridSearchChanged(e) {
    $('#panelSearch').parents('.panel').find('.panel-body').slideUp();
    $('#SearchPanelClickable').addClass('panel-collapsed');
    $('#SearchPanelClickable').find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
    $("#PanelInfo").parents('.panel').find('.panel-body').slideDown();
    $("#PanelInfo").removeClass('panel-collapsed');
    $("#PanelInfo").find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
    var grid = $('#gridSearch').data('kendoGrid');
    var data = this.dataItem(grid.select()[0]);
    for (let key in data) {
        let cmb;
        if ($("#" + key).data() != null)
            cmb = $("#" + key).data().kendoComboBox;
        if (cmb !== undefined && "ProvinceID,CityID,ZoneID".indexOf(key) === -1) {
            $("#" + key).data('kendoComboBox').value(data[key]);
        }
        else if (cmb === undefined) {
            $("#" + key).val(data[key]);
            if ($("#" + key).is("input:checkbox"))
                $("#" + key).prop('checked', data[key]);
        }
    }
    $("#ProvinceID").data('kendoComboBox').value(data["ProvinceID"]);
    $("#CityID").data('kendoComboBox').dataSource.read().then(function () {
        $("#CityID").data('kendoComboBox').value(data["CityID"]);
    }).then(function () {
        $("#ZoneID").data('kendoComboBox').dataSource.read().then(function () {
            $("#ZoneID").data('kendoComboBox').value(data["ZoneID"]);
        });
    });
    $('#BirthDate').val(new persianDate(data.BirthDate)
        .toCalendar('gregorian').toLocale('en').format('YYYY-MM-DD'));
    $('.Groups').each(function () {
        $(this).prop("checked", false)
    });
    $('#gridPersonGroups').data('kendoGrid').dataSource.read().then(
        function () {
            var data = $('#gridPersonGroups').data('kendoGrid').dataSource.data();
            for (i = 0; i < data.length; i++) {
                var chk = '#Group' + data[i].GroupID;
                $(chk).prop('checked', true);
            }
        }
    )

}
