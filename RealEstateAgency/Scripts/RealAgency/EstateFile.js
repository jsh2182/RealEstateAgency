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

    $("#UnitPrice").on("change", function () {
        let unitPrice = 0;
        if ($("#UnitPrice").val() !== "")
            unitPrice = parseFloat($("#UnitPrice").val());
        let area = 0;
        if ($("#Area").val() !== "")
            area = parseFloat($("#Area").val());
        $("#TotalPrice").val(area * unitPrice);
    });
    $("#Area").on("change", function () {
        let unitPrice = 0;
        if ($("#UnitPrice").val() !== "")
            unitPrice = parseFloat($("#UnitPrice").val());
        let area = 0;
        if ($("#Area").val() !== "")
            area = parseFloat($("#Area").val());
        $("#TotalPrice").val(area * unitPrice);
    })
    var myWindow = $("#GroupsWindow");
    var dialog = $('#dialog');
    $('input:checkbox').on('change', function () {
        $(this).val($(this).prop('checked'));
    })

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
    $('#FileReferWindow').kendoWindow({
        title: 'ارجاع ها',
        width: '60%',
        visible: false,
        actions: ["Close"]
    });
    $('#PeopleWindow').kendoWindow({
        width: "65%",
        title: "انتخاب مالک",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ]
    });
    $('#AttachmentWindow').kendoWindow({
        width: "65%",
        title: "ضمیمه ها",
        visible: false,
        actions: ["Close"]
    })
    $('#FilesOtherInfoWindow').kendoWindow({
        width: "40%",
        title: "سایر اطلاعات",
        visible: false,
        actions: ["Close"]
    });
    $("#btnSelectGroup").click(function () {
        myWindow.data("kendoWindow").center().open();
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
function GetFileID() {
    return { fileID: $('#FileID').val() }
}
function SelectGroup() {
    var gridData = $('#gridSelectFileGroup').data('kendoGrid').dataSource.data();
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
    $('#gridFileGroups').data('kendoGrid').dataSource.data(data4Init);
    $('#GroupsWindow').data('kendoWindow').close();
}

function ResetForm() {
    $("input:text").each(function () {
        if (this.id.indexOf("Search") == -1)
            $(this).val('');
    })
    $("input[type=number]").each(function () {
        if (this.id.indexOf("Search") == -1)
            $(this).val('0');
    })
    $("textarea").each(function () {
        if (this.id.indexOf("Search") == -1)
            $(this).val('');
    })
    $("input:checkbox").each(function () {
        if (this.id.indexOf("Search") == -1)
            $(this).prop('checked', false);
    })
    $(".k-combobox").each(function () {
        let cmb = $("#" + $(this)[0].childNodes[1].name).data("kendoComboBox");
        if (cmb !== undefined) {
            cmb.value(null);
            cmb.dataSource.read();
        }
    })
    $('#gridFileGroups').data('kendoGrid').dataSource.data([]);
}
function Submit() {
    var formData = $('#frmFileInfo').serialize();
    var otherInfo = $('.otherInfo').serialize();
    if (otherInfo.length > 0)
        formData += '&' + $('.otherInfo').serialize();
    GroupList = [];
    $('.Groups').each(function () {
        if ($(this).is(':checked')) {
            var id = parseInt($(this).val());
            GroupList.push(id);
        }
    })
    let requiredElements = $("[isRequired]");
    for (let i = 0; i < requiredElements.length; i++) {
        if (($(requiredElements[i]).closest("div").css("display") !== "none") && ($(requiredElements[i]).val() == "" /*|| $(requiredElements[i]).val() == "0")*/)) {
            $('#dialog').data('kendoDialog').content('<p>لطفا فیلدهای الزامی را پر کنید</p>');
            $('#dialog').data('kendoDialog').open();
            return;
        }
    }
    if (GroupList.length == 0) {
        $('#dialog').data('kendoDialog').content('<p>گروهبندی مشخص نشده است</p>');
        $('#dialog').data('kendoDialog').open();
        return;
    }
    $.ajax({
        url: '/EstateFile/Submit',
        data: formData + '&GroupIDs=' + JSON.stringify(GroupList),
        //dataType: 'JSON',
        type: 'POST',
        success: (function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('<p>عملیات با موفقیت انجام شد</p>');
                $('#dialog').data('kendoDialog').open();
                if ($('FileID').val() == '')
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
function DeleteFile(id) {
    $.ajax({
        url: "/EstateFile/Delete/" + id,
        type: "DELETE",
        dataType: "JSON",
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#gridSearch').data('kendoGrid').dataSource.read();
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
    let grid = $('#gridSearch').data('kendoGrid');
    grid.dataSource.read();
    if (grid.dataSource.data.length > 0)
        grid.one("dataBound", function () {
            this.dataSource.page(1);
        })
}

function OnGridSearchChanged(e) {
    $('#panelSearch').parents('.panel').find('.panel-body').slideUp();
    $('#SearchPanelClickable').addClass('panel-collapsed');
    $('#SearchPanelClickable').find('i').removeClass('glyphicon-chevron-down').addClass('glyphicon-chevron-up');

    $('#panelInfo').parents('.panel').find('.panel-body').slideDown();
    $('#panelInfo').removeClass('panel-collapsed');
    $('#panelInfo').find('i').removeClass('glyphicon-chevron-up').addClass('glyphicon-chevron-down');
    let grid = $('#gridSearch').data('kendoGrid');
    let data = this.dataItem(grid.select()[0]);
    $.ajax({
        url: "/EstateFile/GetFile",
        data: { id: data.FileID },
        type: "POST",
        dataType: "JSON",
        success: function (result) {
            if (result != 400) {
                for (let key in result) {
                    let cmb;
                    if ($("#" + key).data() != null)
                        cmb = $("#" + key).data().kendoComboBox;
                    if (cmb !== undefined && "ProvinceID,CityID,ZoneID".indexOf(key) === -1)
                        $("#" + key).data('kendoComboBox').value(result[key]);
                    else {
                        $("#" + key).val(result[key]);
                        if ($("#" + key).is("input:checkbox"))
                            $("#" + key).prop('checked', result[key]);
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
                let type = $("#EstateType").data("kendoComboBox").value();
                let className = "." + type.replace("آپارتمان", "appartment")
                    .replace("ویلایی", "villa")
                    .replace("زمین و کلنگی", "villa")
                    .replace("تجاری", "commercial")
                    .replace("مستغلات", "realEstate")
                    .replace("دفترکار(اداری)‏", "office");
                $('div.appartment, div.villa, div.commercial, div.realEstate, div.office').each(function () { $(this).hide(); });
                $(className).each(function () { $(this).show(); });
                $('#gridFileGroups').data('kendoGrid').dataSource.read().then(
                    function () {
                        var data = $('#gridFileGroups').data('kendoGrid').dataSource.data();
                        if (data != null)
                            for (i = 0; i < data.length; i++) {
                                var chk = '#Group' + data[i].GroupID;
                                $(chk).prop('checked', true);
                            }
                    });
            }
        }
    });



}
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
function SearchPersonPartial() {
    $('#gridSearchPerson').data('kendoGrid').dataSource.read();
}
function OpenAttachWindow() {
    $('#AttachmentWindow').load('/Attachment/AttachmentPartial?ParentIDAndAttachType=' + $('#FileID').val() + '_EstateFile')
    $('#AttachmentWindow').data('kendoWindow').center().open();
}
function AttachAdditionalData(attachType) {
    return {
        ParentID: $('#FileID').val(),
        AttachType: attachType
    }
}
function DeleteAttachFile(fileName) {
    $.ajax({
        url: '/Attachment/Delete',
        type: 'Delete',
        //dataType: 'JSON',
        data: { fileName: fileName },
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#dialog').data('kendoDialog').open();
                $('#AttachList').data('kendoGrid').dataSource.read();
            }
            else if (result == 400) {
                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
                $('#dialog').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }

        }
    })
}
function ResetfrmFileRefer() {
    $('#FileReferID').val('');
    $('#FileID').val('');
    $('#ReferedTo').data('kendoComboBox').value(null);
    $('#ReferDesc').val('');
    $('#fldFileReferInfo').hide(300);
}
function SubmitFileRefer() {
    var formData = $('#frmFileReferInfo').serialize() + '&FileID=' + $('#FileID').val()
    $.ajax({
        url: '/FileRefer/Submit',
        type: 'POST',
        //dataType: 'JSON',
        data: formData,
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#dialog').data('kendoDialog').open();
                $('#gridFileReferList').data('kendoGrid').dataSource.read();
                ResetfrmFileRefer();
            }
            else if (result == 400) {
                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
                $('#dialog').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }


        }
    })
}
function DeleteFileRefer(referID) {
    $.ajax({
        url: '/FileRefer/Delete',
        data: { referID: referID },
        type: 'Delete',
        //dataType: 'JSON',
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#dialog').data('kendoDialog').open();
                ResetfrmFileRefer();
            }
            else if (result == 400) {

                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
                $('#dialog').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }

        }
    })
}
function ShowFileAttachWindow(fileID) {
    $('#AttachmentWindow').load('/Attachment/AttachmentPartial?ParentIDAndAttachType=' + fileID + '_FileRefer');
    $('#AttachmentWindow').data('kendoWindow').center().open();
}

function ShowFileReferWindow() {
    $('#FileReferWindow').load('/FileReferAndRequest/FileReferPartial');
    $('#FileReferWindow').data('kendoWindow').center().open();
}
function OpenFileOtherInfoWindow() {
    $('#FilesOtherInfoWindow').data('kendoWindow').center().open();
}
function OnEstateTypeChanged(e) {
    let type = e.dataItem.Value;
    let className = "." + type.replace("آپارتمان", "appartment")
        .replace("ویلایی", "villa")
        .replace("زمین و کلنگی", "villa")
        .replace("تجاری", "commercial")
        .replace("مستغلات", "realEstate")
        .replace("دفترکار(اداری)‏", "office");
    $('div.appartment, div.villa, div.commercial, div.realEstate, div.office').each(function () { $(this).hide(); });
    $(className).each(function () { $(this).show(); });
}
function OnFileTypeChange(e) {
    $.ajax({
        url: "/EstateFile/GetNextCode",
        dataType: "JSON",
        type: "POST",
        success: function (result) {
            if (result != 400)
                $('#FileCode').val(result);
        }
    })
}