var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_Load').DataTable({
        "ajax": {
            "url": "/admin/getall/",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "fullName", "width": "20%" },
            { "data": "email", "width": "20%" },
            { "data": "password", "width": "20%" },
            { "data": "idRole", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center"> 
                        <a href="/admin/Upsert?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                        Edit 
                        </a>
                        &nbsp;
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete('/admin/Delete?id='+${data})>
                            Delete 
                        </a>
                    </div> `;
                }, "width": "45%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        showCancelButton: true
    }).then((result) => {
        if (result.value) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}