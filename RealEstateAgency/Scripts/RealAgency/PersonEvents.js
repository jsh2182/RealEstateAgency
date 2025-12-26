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

    var dialog = $('#dialog');
    dialog.kendoDialog({
        width: "450px",
        title: "پیام سیستم",
        visible: false,
        closable: true,
        modal: true,
        content: ""
    });
    $('#PeopleWindow').kendoWindow({
        width: "65%",
        title: "انتخاب مالک",
        visible: false,
        actions: [
            "Pin",
            "Close"
        ]
    });
    $("#FollowUpWindow").kendoWindow({
        width: "40%",
        title: "ثبت پیگیری",
        visible: false,
        actions: [
            "Pin",
            "Close"
        ]
    });
    var date;
    date = $('#PEventDateSearch').persianDatepicker({

        initialValue: false,
        inline: false,
        altField: '#CreationDate',
        altFormat: 'gregorian',
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
        maxDate: new persianDate(),
        timePicker: {
            enabled: false,
            meridiem: {
                enabled: true
            }
        },
        observer: true,
        autoClose: true,
        altFieldFormatter: function (unixDate) {

            var gregorianDate = new persianDate(unixDate)
                .toCalendar('gregorian').toLocale('en').format('YYYY-MM-DD');
            return (gregorianDate);
        }

    }); 
    date = $('#PEventDateTo').persianDatepicker({

        initialValue: false,
        inline: false,
        altField: '#CreationDateTo',
        altFormat: 'gregorian',
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
        maxDate: new persianDate(),
        timePicker: {
            enabled: false,
            meridiem: {
                enabled: true
            }
        },
        observer: true,
        autoClose: true,
        altFieldFormatter: function (unixDate) {

            var gregorianDate = new persianDate(unixDate)
                .toCalendar('gregorian').toLocale('en').format('YYYY-MM-DD');
            return (gregorianDate);
        }

    }); 

});
function CancelPerson() {
    $('#SelectedPersonID').val('');
    $('#SelectedPersonName').val('');
    $('#SelectedSubscriptionCode').val('');
    $('#PeopleWindow').data("kendoWindow").close();
}
function SelectPerson() {

    var personID = $('#SelectedPersonID').val();
    $('#PersonID').val(personID);
    var personName = $('#SelectedPersonName').val();
    $('#PersonName').val(personName);
    $('#PeopleWindow').data("kendoWindow").close();
}
function PersonNameOnInput() {
    $('#PeopleWindow').data('kendoWindow').center().open();
}
function SearchPersonPartial() {
    $('#gridSearchPerson').data('kendoGrid').dataSource.read();
}
function OnGridSearchPersonChanged(e) {
    var grid = $('#gridSearchPerson').data('kendoGrid');
    var data = this.dataItem(grid.select()[0]);
    $('#SelectedPersonID').val(data.PersonID);
    $('#SelectedPersonName').val(data.PersonName);
    $('#SelectedSubscriptionCode').val(data.SubscriptionCode);
}
function GetSearchPersonData() {
    return {
        PersonName: $('#PersonNameForSearch').val(),
        SubscriptionCode: $('#SubscriptionCode').val(),
        MobileNumber: $('#MobileNumber').val(),
        PhoneNumber: $('#PhoneNumber').val()

    }
}
function GetPersonID() {
    return { personID: $('#PersonID').val() }
}
function GetEventID() {
    return { eventID: $("#EventID").val() }
}
function ResetForm() {
    $("input:text").each(function () {
        $(this).val('');
    })
    $("input[type=number]").each(function () {
        $(this).val('0');
    })
    $("textarea").each(function () {
        $(this).val('');
    })
    $("input:checkbox").each(function () {
        $(this).prop('checked', false);
    })
    $(".k-combobox").each(function () {
        let cmb = $(this).data("kendoComboBox");
        if (cmb !== undefined)
            cmb.value(null);
    })
    $('#gridDetails').data('kendoGrid').dataSource.data([]);
}
function ResetFormDetail() {
    $("#DetailID").val("");
    $("#DetailName").data("kendoComboBox").value(null);
    $("#DetailValue").val("");
    $("#divNewDetail").hide(200);
    $("#btnNewDetail").show();
}
function EditDetail(detailID) {
    let data = $("#gridDetails").data("kendoGrid").dataSource.data();
    let detail = data.find(d => d.DetailID == detailID);
    $("#DetailName").data("kendoComboBox").value(detail.DetailName);
    $("#DetailValue").val(detail.DetailValue);
    $("#DetailID").val(detailID);
    ShowNewDetail();
}
function SubmitDetail() {
    if ($("#EventID").val() == "" || $("#EventID").val() == "0") {
        $('#dialog').data('kendoDialog').content('<p>ابتدا باید ثبت رویداد انجام شود</p>');
        $('#dialog').data('kendoDialog').open();
        return;
    }
    $.ajax({
        url: "/PersonEvents/SubmitEventDetail",
        type: "POST",
        //dataType: "JSON",
        data: {
            DetailID: $("#DetailID").val(),
            DetailValue: $("#DetailValue").val(),
            DetailName: $("#DetailName").data("kendoComboBox").value(),
            EventID: $("#EventID").val()
        },
        success: function (result) {
            if (result == 200) {
                $("#btnNewDetail").show();
                ResetFormDetail();
                $('#dialog').data('kendoDialog').content('<p>عملیات با موفقیت انجام شد</p>');
                $("#gridDetails").data("kendoGrid").dataSource.read();
                $('#dialog').data('kendoDialog').open();
            } else if (result == 400) {
                $('#dialog').data('kendoDialog').content('<p>اشکال در انجام عملیات</p>');
                $('#dialog').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }
        }
    })
}
function detailInit(e) {
    var grid = $("#gridEventDetails_" + e.data.EventID).data("kendoGrid");
    grid.dataSource.data(e.data.Details);

}
function SubmitEvent() {
    var formData = $('#frmEventInfo').serialize();
    $.ajax({
        url: '/PersonEvents/SubmitEvent',
        data: formData,
        //dataType: 'JSON',
        type: 'POST',
        success: (function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('<p>عملیات با موفقیت انجام شد</p>');
                ResetForm();
                $('#dialog').data('kendoDialog').open();
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
function EventDateChange() {
    if ($("#PEventDate").val() == "")
        $("#CreationDate").val("");
    if ($("#PEventDateTo").val() == "")
        $("#CreationDateTo").val("");
}
function GetSearchData() {
    var result = {};
    $.each($('#frmSearch').serializeArray(), function () {
        result[this.name] = this.value;
    });
    return result;
}
function Search() {
    $('#gridSearchEvents').data('kendoGrid').dataSource.read();
}

function ShowNewDetail() {
    $('#divNewDetail').show();
    $('#btnNewDetail').hide();
}
function OnGridSearchChanged() {
    $('#panelSearch').parents('.panel').find('.panel-body').slideUp();
    $('#SearchPanelClickable').addClass('panel-collapsed');
    $('#SearchPanelClickable').find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');
    var grid = $('#gridSearchEvents').data('kendoGrid');
    var data = this.dataItem(grid.select()[0]);
    for (let key in data) {
        let cmb;
        if ($("#" + key).data() != null)
            cmb = $("#" + key).data().kendoComboBox;
        if (cmb !== undefined)
            $("#" + key).data('kendoComboBox').value(data[key]);
        else {
            $("#" + key).val(data[key]);
            if ($("#" + key).is("input:checkbox"))
                $("#" + key).prop('checked', data[key]);
        }
    }
    $("#btnShowFollowUp").show();
    $("#divEventDetails").show();
    $("#gridDetails").data("kendoGrid").dataSource.read();

}
function GetEventType() {
    return { eventType: $("#EventType").data("kendoComboBox").value() }
}
function RemoveDetail(detailID) {
    $.ajax({
        url: "/PersonEvents/DeleteEventDetail",
        data: { detailID: detailID },
        type: "Delete",
        //dataType: "JSON",
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('<p>عملیات با موفقیت انجام شد</p>');
                $('#dialog').data('kendoDialog').open();
                $("#gridDetails").data("kendoGrid").dataSource.read();
            }
            else if (result == 400) {
                $('#dialog').data('kendoDialog').content('<p>اشکال در انجام عملیات</p>');
                $('#dialog').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }
            
        }
    })

}
function SetCreationDate() {
    if ($("#PEventDate").val() == "")
        $("CreationDate").val("");
}
function ShowFollowUpWindow() {
    let eventID = $("#EventID").val();
    if (eventID == "" || eventID == 0) {
        $("#dialog").data("kendoDialog").content("رویدادی برای پیگیری انتخاب نشده است");
        $("#dialog").data("kendoDialog").open();
        return;
    }
    $("#PersonEventID").val(eventID);
    $("#FollowUpWindow").data("kendoWindow").center().open();
}
