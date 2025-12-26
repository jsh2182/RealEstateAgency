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
function RemoveGroup(id) {
    $.ajax({
        url: "/PersonGroup/RemoveGroup",
        type: "Delete",
        dataType: "JSON",
        data: { groupID: id },
        success: function (result) {
            if (result == 200) {
                alert("عملیات با موفقیت انجام شد");
                ResetForm();
                $('#gridSearch').data('kendoGrid').dataSource.read();
            }
            else if (result == 400) {
                alert("اشکال در انجام عملیات");
            }
            else {
                alert(result.Message);
            }

        }
    });
}

function EditGroup(id) {
    if (id == 0) {
        alert("موردی برای ویرایش انتخاب نشده است");
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
    $('#fldInfo').hide(500);
}

function ResetForm() {
    $('#GroupID').val('');
    $('#GroupName').val('');
    $('#GroupDesc').val('');
}

function Submit() {
    if ($('#GroupName').val() == '') {
        alert('نام گروه مشخص نشده است');
        return;
    }
    //$('.loader').show();
    var formData = $('#frmPersonGroup').serialize();
    $.ajax({
        url: '/PersonGroup/SubmitPersonGroup',
        type: 'POST',
        dataType: 'json',
        data: formData,
        success: function (result) {
            if (result == 200) {
                alert("عملیات با موفقیت انجام شد");
                //$('.loader').hide();
                $('#gridSearch').data('kendoGrid').dataSource.read();
                $('#fldInfo').hide(500);
                ResetForm();
            }
            else {
                alert("اشکال در انجام عملیات");
            }
        }

    });
}

function onPersonGroupGridChanged(e) {
    var grid = $('#gridSearch').data('kendoGrid');
    var row = grid.select()[0];
    var dataItem = this.dataItem(row);
    $('#GroupID').val(dataItem.GroupID);
    $('#GroupName').val(dataItem.GroupName);
    $('#GroupDesc').val(dataItem.GroupDesc);
}

