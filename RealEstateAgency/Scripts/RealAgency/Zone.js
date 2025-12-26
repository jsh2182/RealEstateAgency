function RemoveZone(id) {
    $.ajax({
        url: "/Zone/Remove",
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

function EditZone(id) {
    if (id == 0) {
        alert("موردی برای ویرایش انتخاب نشده است");
    }
    else {
        var data = $('#gridSearch').data('kendoGrid').dataSource.data();
        var grp = data.find(d => d.ZoneID == id);
        $('#ZoneID').val(grp.ZoneID);
        $('#ZoneName').val(grp.ZoneName);
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
    $('#ZoneID').val('');
    $('#ZoneName').val('');
}

function Submit() {
    if ($('#ZoneName').val() == '') {
        alert('نام استان مشخص نشده است');
        return;
    }
    //$('.loader').show();
    $.ajax({
        url: '/Zone/Submit',
        type: 'POST',
        dataType: 'json',
        data: {
            ZoneID : $('#ZoneID').val(),
            ZoneName: $('#ZoneName').val(),
            CityID: $('#CityID').val()
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
function GetCityID() {
    return { cityID: $('#CityID').val()}
}
function OnSelectCity(e) {
    var id = this.dataItem(e.item.index()).Value;
    $('#CityID').val(id);
    $('#gridSearch').data('kendoGrid').dataSource.read();
}

