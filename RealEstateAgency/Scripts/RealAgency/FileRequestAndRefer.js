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
});
$(function () {
    $("#dialog").kendoDialog({
        width: "450px",
        title: "پیام سیستم",
        visible: false,
        closable: true,
        modal: true,
        content: ""
    });
    $('#FilesWindow').kendoWindow({
        title: 'انتخاب فایل',
        width: '65%',
        visible: false,
        actions: ["Close"]
    })
    $(".selectFile").on("input", function (e) {
        $("#CallerElement").val(e.target.id);
        $('#FilesWindow').data("kendoWindow").center().open();
    });
});
function ResetfrmFileRefer() {
    $('#FileReferID').val('');
    $('#ReferFileID').val('');
    $('#ReferedTo').val('');//.data('kendoComboBox').value(null);
    $('#ReferDesc').val('');
    $('#divNewRefer').hide(200);
}
function ResetfrmFileRequest() {
    $('#FileRequestID').val('');
    $('#RequestedFileID').val('');
    $('#RequestFileCode').val('');
    $('#RequestDesc').val('');
    $('#divNewRequest').hide(100);
}
function SubmitFileRefer() {
    var formData = $('#frmFileReferInfo').serialize() + '&FileID=' + $('#FileID').val()
    $.ajax({
        url: '/FileReferAndRequest/SubmitRefer',
        type: 'POST',
        dataType: 'JSON',
        data: formData,
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#gridFileReferList').data('kendoGrid').dataSource.read();
                ResetfrmFileRefer();
            }
            else
                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
            $('#dialog').data('kendoDialog').open();

        }
    })
}
function DeleteFileRefer(referID) {
    $.ajax({
        url: '/FileReferAndRequest/DeleteRefer',
        data: { referID: referID },
        type: 'Delete',
        dataType: 'JSON',
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                ResetfrmFileRefer();
            }
            else {

                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
            }
            $('#dialog').data('kendoDialog').open();
        }
    })
}

function ShowDivNewRequest() {
    $("#divNewRequest").show(100);
}
function SelectFile() {
    let selectedFileId = $("#SelectedFileID").val();
    if (selectedFileId == "" || selectedFileId == 0)
        return;
    let selectedFileCode = $("#SelectedFileCode").val();
    if ($("#CallerElement").val() == "RequestFileCode") {
        $("#RequestFileCode").val(selectedFileCode);
        $("#RequestedFileID").val(selectedFileId);
    }
    $('#FilesWindow').data("kendoWindow").close();
}
function SubmitRequest() {
    if ($("#RequestedFileID").val() == "" || $("#RequestedFileID").val() == "0" || $("#RequestDesc").val() == "") {
        $('#dialog').data('kendoDialog').content('اطلاعات را تکمیل کنید');
        $('#dialog').data('kendoDialog').open();
        return;
    }
    var formData = $('#frmFileRequest').serialize()
    $.ajax({
        url: '/FileReferAndRequest/SubmitRequest',
        type: 'POST',
        dataType: 'JSON',
        data: formData,
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#gridSearchRequest').data('kendoGrid').dataSource.read();
                ResetfrmFileRequest();
            }
            else
                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
            $('#dialog').data('kendoDialog').open();

        }
    })

}
function EditRequest(requestID) {
    if (requestID == "" || requestID == 0)
        return;
    let data = $('#gridSearchRequest').data('kendoGrid').dataSource.data();
    let req = data.find(d => d.FileRequestID == requestID);
    $("#FileRequestID").val(req.FileRequestID);
    $("#RequestedFileID").val(req.FileID);
    $("#RequestFileCode").val(req.FileCode);
    $("#RequestDesc").val(req.RequestDesc);
    ResetfrmFileRefer();
    ShowDivNewRequest();

}
function DeleteRequest(requestID) {
    if (requestID == "" || requestID == 0)
        return;
    $.ajax({
        url: "/FileReferAndRequest/DeleteRequest",
        type: "Delete",
        data: { RequestID: requestID },
        dataType: "JSON",
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#gridSearchRequest').data('kendoGrid').dataSource.read();
                ResetfrmFileRequest();

            }
            else {
                let content = result == 403 ? "به دلیل وجود ارجاع برای این درخواست، امکان حذف آن وجود ندارد" : "اشکال در انجام عملیات";
                $('#dialog').data('kendoDialog').content(content);
            }
            $('#dialog').data('kendoDialog').open();
        }

    })
}
function ShowDivNewRefer(fileID) {
    let data = $("#gridSearchRequest").data("kendoGrid").dataSource.data();
    let req = data.find(d => d.FileID == fileID);
    if (req.RequestType == "درخواست من") {
        $('#dialog').data('kendoDialog').content('امکان انجام این عملیات وجود ندارد.');
        $('#dialog').data('kendoDialog').open();
        return;
    }
    $("#ReferFileID").val(req.FileID);
    $("#ReferedTo").val(req.RequestBy);
    $("#RequestID").val(req.FileRequestID);
    ResetfrmFileRequest();
        $("#divNewRefer").show(200);
    
}
function ReferFile() {

        $.ajax({
            url: "/FileReferAndRequest/SubmitRefer",
            data: $("#frmFileRefer").serialize(),
            type: "POST",
            dataType: "JSON",
            success: function (result) {
                if (result == 200) {
                    $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                    $("#gridSearchRequest").data("kendoGrid").dataSource.read();
                }
                else
                    $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
                $('#dialog').data('kendoDialog').open();
            }
        })

}