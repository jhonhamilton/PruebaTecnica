var dataTable;
$(document).ready(function () {
    cargarDataTableUsuarios();

    $(document).on('click', '.btnSave', (e) => {
        const _model = {
            UsuarioId: parseInt($('#hiddenUser').val()),
            Nombre: $('#txtNombre').val(),
            Fecha: $('#txtFechaNacimiento').val(),
            Sexo: $('#sSexo').val()
        }
        if (_model.Nombre === '') {
            toastr.error('Digita un nombre');
            $('#txtNombre').focus();
            return;
        }
        if (_model.Fecha === '') {
            toastr.error('Digita una fecha');
            $('#txtFechaNacimiento').focus();
            return;
        }
        if (_model.Sexo === '') {
            toastr.error('Escoge un sexo');
            $('#sSexo').focus();
            return;
        }
        swal({
            title: '¿Esta seguro de guardar este usuario?',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Si, guardar!',
            closeOnconfirm: true
        }, function () {
            $('#createUsuario_modal').modal('toggle');
            $.ajax({
                type: 'post',
                url: '/Admin/Usuario/Create',
                data: _model,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: (e) => {
                    toastr.error(e.statusText);
                }
            })
        });
    });
    $(document).on('click', '.btn-create', (e) => {
        $('#txtNombre').val('');
        $('#txtFechaNacimiento').val('');
        $('#sSexo').val('');
    });
    $(document).on('click', '.btn-edit', (e) => {
        $('#hiddenUser').val(e.target.attributes['ds-user'].value);
        var _filas = [...$('#tblUsuarios')[0].rows];
        const _model = {
            UsuarioId: e.target.attributes['ds-user'].value,
            Nombre: '',
            Fecha: '',
            Sexo: ''
        }
        _filas.map((data, index) => {
            if (index > 0) {
                if (data.cells[data.cells.length - 1].children[0].attributes['ds-user'].value === e.target.attributes['ds-user'].value) {
                    _model.Nombre = data.cells[0].innerHTML;
                    _model.Fecha = data.cells[1].innerHTML;
                    _model.Sexo = (data.cells[2].innerHTML === 'Hombre' ? 'H' : (data.cells[2].innerHTML === 'Mujer' ? 'M' : ''));
                }
            }
        });
        const _fecha = _model.Fecha.split('/');
        const fecha = _fecha[2] + '-' + _fecha[1] + '-' + _fecha[0];
        var _fe = new Date(fecha).toISOString().split('T')[0];
        $('#txtNombre').val(_model.Nombre);
        $('#txtFechaNacimiento').val(_fe);
        $('#sSexo').val(_model.Sexo);
    });

    $(document).on('click', '.btn-delete', (e) => {
        swal({
            title: '¿Esta seguro de eliminar este registro?',
            text: 'Una vez eliminado no podrá recuperarlo!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Si, eliminar!',
            closeOnconfirm: true
        }, function () {
            $.ajax({
                type: 'put',
                url: '/Admin/Usuario/Delete',
                data: { usuarioId: parseInt(e.target.attributes['ds-user'].value) },
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                    dataTable.ajax.reload();
                },
                error: (e) => {
                    toastr.error(e.statusText);
                }
            })
        });
    });
});
const getBytysFile = (fileContents) => {
    const binaryString = window.atob(fileContents);
    const binaryLen = binaryString.length;
    var bytes = new Uint8Array(binaryLen);
    for (var i = 0; i < binaryLen; i++) {
        bytes[i] = binaryString.charCodeAt(i);
    }
    return bytes;
}
const saveByteArray = (reportName, byte, typeApplication) => {
    let link = document.querySelector('.excel');
    $(link).removeClass('d-none');
    $(link).addClass('d-block');
    link.href = window.URL.createObjectURL(
        new Blob([byte], { type: typeApplication })
    );
    link.download = reportName;
};

const cargarDataTableUsuarios = () => {
    dataTable = $("#tblUsuarios").DataTable({
        retrieve: true,
        paging: false,
        processing: true,
        "ajax": {
            "url": "/Admin/Usuario/GetUsuarios",
            "type": "GET",
            "datatype": "json",
            dataSrc: (response) => {
                saveByteArray(
                    response.nameFile,
                    getBytysFile(response.fileContent.result.fileContents),
                    'application/pdf'
                );
                return response.data;
            }
        },
        language: {
            search: "Buscar",
            emptyTable: "No hay registros",
            infoEmpty: "Mostrando 0 a 0 de 0 registros",
            infoFiltered: "(Filtrado de _MAX_ total entradas)",
            zeroRecords: "Sin resultados",
            processing: "Procesando...",
            loadingRecords: "Cargando...",
            paginate: {
                previous: "<i class='mdi mdi-chevron-left'>",
                next: "<i class='mdi mdi-chevron-right'>"
            },
            info: "Mostrando usuarios _START_ a _END_ de _TOTAL_",
            lengthMenu: 'Mostrar <select class=\'form-select form-select-sm ms-1 me-1\'><option value="5">5</option><option value="10">10</option><option value="20">20</option><option value="-1">All</option></select> usuarios'
        },
        pageLength: 5,
        columns: [
            { "data": "nombre" },
            { "data": "fecha" },
            { "data": "sexo" },
            {
                "data": "usuarioId",
                render: function (e, l, a, o) {
                    return "display" === l &&
                        (e = "<a class='btn-edit' data-bs-toggle='modal' data-bs-target='#createUsuario_modal' ds-user='" + a.usuarioId + "' style='cursor: pointer;'><i class='fa-solid fa-pen-to-square mx-1'></i>Modificar</a>"
                            +
                            "<a class='btn-delete ms-3' ds-user='" + a.usuarioId + "' style='cursor: pointer;'><i class='fa-solid fa-trash-can mx-1'></i>Eliminar</a>"
                        )
                }
            }
        ],
        select:
        {
            style: "multi"
        },
        drawCallback: function () {
            //$(".dataTables_paginate > .pagination").addClass("pagination-rounded"),
            //    $("#products-datatable_length label").addClass("form-label"),
            //    $('div > table.dataTable > tbody > tr > td:nth-child(2)').addClass("cortarTexto")
        }
    });
}

