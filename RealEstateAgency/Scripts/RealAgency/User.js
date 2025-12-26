function GetSearchData() {
    return {UserID:0}
}
$(function () {
    var dialog = $('#dialog');
    var myWindow = $("#window");

    $("#btnPermission").removeAttr("href");
    $("#btnAddNew").removeAttr("href");
    dialog.kendoDialog({
        width: "450px",
        title: "پیام سیستم",
        visible: false,
        closable: true,
        modal: true,
        content: "<p>لطفاً یک مورد را از جدول زیر انتخاب نمایید<p>"
    });

    myWindow.kendoWindow({
        width: "800px",
        title: "مجوز دسترسی ها",
        visible: false,
        actions: [
            "Pin",
            "Minimize",
            "Maximize",
            "Close"
        ]
    });
    $('#IsActive').on('change', function () {
        $('#IsActive').val($('#IsActive').prop('checked'));
    })
    $('#IsAdmin').on('change', function () {
        $('#IsAdmin').val($('#IsAdmin').prop('checked'));
    })
    $("#btnPermission").click(function () {

        var grid = $("#UsersGrid").data("kendoGrid");
        var selected = grid.dataItem(grid.select());
        if (selected == null) {
            dialog.data("kendoDialog").open();
            return false;
        }

        PersonID = selected.ID;
        myWindow.data("kendoWindow").center().open();
        $("#TreeViewPageListPartial").empty();
        $("#TreeViewPageListPartial").load("/PageList/TreeViewPageListPartial");
    });
    $('#btnAddNew').click(function () {
        $('#fldUserInfo').show(200);
    })

});
function UsersGridOnChange(e) {
    var selectedRow = $('#UsersGrid').data('kendoGrid').select()[0];
    dataItem = this.dataItem(selectedRow);
    $('#SelectedUserID').val(dataItem.UserID);
}
function ResetForm() {
    $('#UserID').val('');
    $('#Name').val('');
    $('#UserName').val('');
    $('#Password').val('');
    $('#IsActive').prop('checked', false);
    $('#IsAdmin').prop('checked', false);
    $('#fldUserInfo').hide(200);
}
function SubmitUser() {
    $.ajax({
        url: '/User/SubmitUser',
        type:'POST',
        data: $('#frmUserInfo').serialize(),
        //dataType: 'JSON',
        success: function (result) {
            if (result == 200) {
                $('#dialog').data('kendoDialog').content('عملیات با موفقیت انجام شد');
                $('#dialog').data('kendoDialog').open();
                ResetForm();
                $('#UsersGrid').data('kendoGrid').dataSource.read();
            }
            else if (result == 400) {
                $('#dialog').data('kendoDialog').content('اشکال در انجام عملیات');
                $('#dialog').data('kendoDialog').open();
            }
            else {
                $("html").html(result);
            }
            
        }
    });
}
function EditUser(userID) {
    if (userID == 0) {
        $('#dialog').data('kendoDialog').content('موردی برای ویرایش انتخاب نشده است');
        $('#dialog').data('kendoDialog').open();
    }
    else {
        var data = $('#UsersGrid').data('kendoGrid').dataSource.data();
        var user = data.find(d => d.UserID == userID);
        $('#UserID').val(user.UserID);
        $('#UserName').val(user.UserName);
        $('#Name').val(user.Name);
        $('#Password').val(user.Password);
        $('#IsActive').prop('checked', user.IsActive);
        $('#IsActive').val(user.IsActive);
        $('#IsAdmin').prop('checked', user.IsAdmin);
        $('#IsAdmin').val(user.IsAdmin);
        $('#fldUserInfo').show(200);
    }
}