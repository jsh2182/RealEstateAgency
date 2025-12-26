function RemoveCity(id) {
    $.ajax({
        url: "/City/Remove",
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

function EditCity(id) {
    if (id == 0) {
        alert("موردی برای ویرایش انتخاب نشده است");
    }
    else {
        var data = $('#gridSearch').data('kendoGrid').dataSource.data();
        var grp = data.find(d => d.CityID == id);
        $('#CityID').val(grp.CityID);
        $('#CityName').val(grp.CityName);
        $('#divNew').show(100);

    }

}
function ShowDivNew() {
    ResetForm();
    $('#divNew').show(100);
}
function CancelInfo() {
    ResetForm();
    $('#divNew').hide(100);
}

function ResetForm() {
    $('#CityID').val('');
    $('#CityName').val('');
}

function Submit() {
    if ($('#CityName').val() == '') {
        alert('نام استان مشخص نشده است');
        return;
    }
    //$('.loader').show();
    $.ajax({
        url: '/City/Submit',
        type: 'POST',
        dataType: 'json',
        data: {
            CityID : $('#CityID').val(),
            CityName: $('#CityName').val(),
            ProvinceID: $('#ProvinceID').val()
        },
        success: function (result) {
            if (result == 200) {
                alert("عملیات با موفقیت انجام شد");
                //$('.loader').hide();
                $('#gridSearch').data('kendoGrid').dataSource.read();
                $('#divNew').hide(100);
                ResetForm();
            }
            else {
                alert("اشکال در انجام عملیات");
            }
        }

    });
}
function GetProvinceID() {
    return { provinceID: $('#ProvinceID').val()}
}
function OnSelectProvince(e) {
    var id = this.dataItem(e.item.index()).Value;
    $('#ProvinceID').val(id);
    $('#gridSearch').data('kendoGrid').dataSource.read();
}

