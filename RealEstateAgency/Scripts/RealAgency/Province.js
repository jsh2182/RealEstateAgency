function RemoveProvince(id) {
    $.ajax({
        url: "/Province/Remove",
        type: "Delete",
        dataType: "JSON",
        data: { id: id },
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

function EditProvince(id) {
    if (id == 0) {
        alert("موردی برای ویرایش انتخاب نشده است");
    }
    else {
        var data = $('#gridSearch').data('kendoGrid').dataSource.data();
        var grp = data.find(d => d.ProvinceID == id);
        $('#ProvinceID').val(grp.ProvinceID);
        $('#ProvinceName').val(grp.ProvinceName);
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
    $('#ProvinceID').val('');
    $('#ProvinceName').val('');
}

function Submit() {
    if ($('#ProvinceName').val() == '') {
        alert('نام استان مشخص نشده است');
        return;
    }
    //$('.loader').show();
    var formData = $('#frmProvince').serialize();
    $.ajax({
        url: '/Province/Submit',
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

