var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/glossary/GetAllGlossaryItems",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "term", "width": "50%" },
            { "data": "definition", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/glossary/UpdateCreate/${data}" class='btn btn-success text-white'
                                    style='cursor:pointer;'> Create<i class='far fa-edit'></i></a>
                                    &nbsp;
                                <a onclick=Delete("/glossary/Delete/${data}") class='btn btn-danger text-white'
                                    style='cursor:pointer;'> Del<i class='far fa-trash-alt'></i></a>
                                </div>
                            `;
                }, "width": "30%"
            }
        ]
    });
}

function Delete(url) {
    $.ajax({
        type: 'DELETE',
        url: url,
        success: function (data) {
            if (data.success) {
                alert('success');
                dataTable.ajax.reload();
            }
            else {
                alert('error');
            }
        }
    });
}