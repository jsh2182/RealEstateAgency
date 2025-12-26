
$(function () {
    $("#dialog").kendoDialog({
        width: "450px",
        title: "پیام سیستم",
        visible: false,
        closable: true,
        modal: true,
        content: ""
    });
    $('#PersonEventWindow').kendoWindow({
        width: "60%",
        title: "رویداد مربوطه",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
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
    date = $('#PFollowDateSearch').persianDatepicker({

        initialValue: false,
        inline: false,
        altField: '#FollowDateSearch',
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

})
function OpenEventWindow(eventID) {
    $('#PersonEventWindow').load('/PersonEvents/PersonEventsPartial?eventID=' + eventID)
    $('#PersonEventWindow').data('kendoWindow').center().open();
}
function GetSearchData() {
    return {
        FollowDate: $("#FollowDateSearch").val(),
        FollowUpUserID: $("#Consultant").data("kendoComboBox").value(),
        IsDone: $("#IsDoneSearch").val()
    }
}
function SearchFollowUps() {
    $("#gridSearchFollowUps").data("kendoGrid").dataSource.read();
}

function ShowFollowUpDiv(eventID, followUpID, followUpCode) {
    $("#FollowUpCode").val(followUpCode);
    $("#SelectedFollowID").val(followUpID);
    $("#divFollowUp").show(200);
}
function HideFollowUpDiv() {
    $("#FollowUpID").val("");
    $("#FollowUpResult").val("");
    $("#divFollowUp").hide(200);
}
function FollowUpDone(eventID) {
    $.ajax({
        url: "/PersonEvents/DoneEvent",
        data: {
            followUpID: $("#SelectedFollowID").val(),
            followUpResult: $("#FollowUpResult").val(),
            followUpCode: $("#FollowUpCode").val()
        },
        dataType: "JSON",
        type: "POST",
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('<p>علیات با موفقیت انجام شد</p>');
                $("#btnFollow" + eventID).text("پیگیری شد");
                HideFollowUpDiv();
                $("#gridSearchFollowUps").data("kendoGrid").dataSource.read();
            }
            else
                $('#dialog').data('kendoDialog').content('<p>اشکال در انجام عملیات</p>');
            $('#dialog').data('kendoDialog').open();
        }
    })
}
function ShowFollowUpWindow(eventID) {
    $("#PersonEventID").val(eventID);
    $("#FollowUpWindow").data("kendoWindow").center().open();
}