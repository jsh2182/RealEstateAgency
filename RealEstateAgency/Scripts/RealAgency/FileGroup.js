$(document).on('click', '.pnl span.clickable', function (e) {
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
var dlg = $('#dlg');
$(function () {

    dlg.kendoDialog({
        width: "450px",
        title: "پیام سیستم",
        visible: false,
        closable: true,
        modal: true,
        content: ""
        
    });
});
function RemoveGroup(id) {
    $.ajax({
        url: "/FileGroup/RemoveGroup",
        type: "Delete",
       // dataType: "JSON",
        data: { groupID: id },
        success: function (result) {
            if (result == 200) {
                $('#dlg').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#dlg').data('kendoDialog').open();
                ResetForm();
                $('#gridSearch').data('kendoGrid').dataSource.read();
            }
            else if (result == 400) {
                $('#dlg').data('kendoDialog').content('اشکال در انجام عملیات');
                $('#dlg').data('kendoDialog').open();
            }
            else if (result.Message !== undefined) {
                $('#dlg').data('kendoDialog').content(result.Message);
                $('#dlg').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }
            

        }
    });
}

function EditGroup(id) {
    if (id == 0) {
        $('#dlg').data('kendoDialog').content('موردی برای ویرایش انتخاب نشده است');
        $('#dlg').data('kendoDialog').open();
    }
    else {
        var data = $('#gridSearch').data('kendoGrid').dataSource.data();
        var grp = data.find(d => d.GroupID == id);
        $('#GroupID').val(grp.GroupID);
        $('#GroupName').val(grp.GroupName);
        $('#GroupDesc').val(grp.GroupDesc);
        $('#fldInfo').show(500);

    }

}
function ShowPanelInfo() {
    ResetForm();
    $('#fldInfo').show(500);
}
function CancelInfo() {
    ResetForm();
    ('#fldInfo').hide(500);
}

function ResetForm() {
    $('#GroupID').val('');
    $('#GroupName').val('');
    $('#GroupDesc').val('');
}

function Submit() {
    if ($('#GroupName').val() == '') {
        $('#dlg').data('kendoDialog').content('نام گروه مشخص نشده است');
        $('#dlg').data('kendoDialog').open();
        return;
    }
    var formData = $('#frmFileGroup').serialize();
    $.ajax({
        url: '/FileGroup/Submit',
        type: 'POST',
        //dataType: 'json',
        data: formData,
        success: function (result) {
            if (result == 200) {
                $('#dlg').data('kendoDialog').content('<p>عملیات با موفقیت انجام شد</p>');
                $('#gridSearch').data('kendoGrid').dataSource.read();
                $('#dlg').data('kendoDialog').open();
                $('#fldInfo').hide(500);
                ResetForm();
            }
            else if (result == 400) {
                $('#dlg').data('kendoDialog').content('اشکال در انجام عملیات');
                $('#dlg').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }
            
        }

    });
}

function onFileGroupGridChanged(e) {
    var grid = $('#gridSearch').data('kendoGrid');
    var row = grid.select()[0];
    var dataItem = this.dataItem(row);
    $('#GroupID').val(dataItem.GroupID);
    $('#GroupName').val(dataItem.GroupName);
    $('#GroupDesc').val(dataItem.GroupDesc);
}

