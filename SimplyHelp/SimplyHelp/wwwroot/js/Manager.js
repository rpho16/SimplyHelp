function FillAlert() {
    var disasterId = $('#Disaster').val();
    $.ajax({
        url: '/Manager/FillAlertType',
        type: "GET",
        dataType: "JSON",
        data: { Disaster: disasterId },
        success: function (alertype) {
            $("#AlertType").html(""); // clear before appending new list 
            $("#AlertType").append(
                $('<option></option>').html("Select Alert Type"));
            $.each(alertype, function (i, typealert) {
                $("#AlertType").append(                    
                    $('<option></option>').val(typealert.id).html(typealert.alertTypeName));
            });
        }
    });
}
function FillMessage() {
    var alertId = $('#AlertType').val();
    $.ajax({
        url: '/Manager/FillAlertMessage',
        type: "GET",
        dataType: "JSON",
        data: { AlertType: alertId },
        success: function (alertmessage) {
            $("#AlertMessage").html(""); // clear before appending new list 
            $.each(alertmessage, function (i, messagealert) {
                $("#AlertMessage").html(messagealert.alertMessageName);
                //$("#AlertMessage").append(
                //    $('<option></option>').val(messagealert.id).html(messagealert.alertMessageName));
            });
        }
    });
}