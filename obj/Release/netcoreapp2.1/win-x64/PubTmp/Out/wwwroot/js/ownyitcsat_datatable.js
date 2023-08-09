var reportname;
var reportdate;

//no any proprties apply
function datatable_simple() {
    $(document).ready(function () {
        $('.tblsimple').DataTable({
            retrieve: true,
            paging: false,
            order: [],
            colReorder: true,
            searching: false,
            info: false,
            responsive: false,
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
    });
}
//only paging apply 
function datatable_paging() {
    $(document).ready(function () {
        $('.tblpaging').DataTable({
            retrieve: true,
            paging: true,   
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            colReorder: true,
            searching: false,
            info: true,
            responsive: false,
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
    });
}
//only searching apply
function datatable_searching() {
    $.fn.dataTable.ext.errMode = 'none';
    $(document).ready(function () {
        var tblsearching = $('.tblsearching').DataTable({
            paging: false,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            searching: true,
            info: false,
            responsive: false,
            "dom": '<"top"f<"clear">>'
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
        $('.reset_btn_id').on('click', function () {
            tblsearching.search('').columns().search('').draw();
        });
        tblsearching.on('error.dt', function (e, settings, techNote, message) {
            console.log('An error has been reported by DataTables: ', message);
        });
    });
}
//only paging and searching apply
function datatable_paging_searching() {
    $.fn.dataTable.ext.errMode = 'none';
    $(document).ready(function () {
        var tbl_paging_searching = $('.tbl_paging_searching').DataTable({
            paging: true,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            searching: true,
            info: false,
            responsive: true,
            //"dom": '<"top"f<"clear">>'
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
        $('.reset_btn_id').on('click', function () {
            tbl_paging_searching.search('').columns().search('').draw();
        });
        tbl_paging_searching.on('error.dt', function (e, settings, techNote, message) {
            console.log('An error has been reported by DataTables: ', message);
        });
    });
}
//scrollY apply as fixed
function datatable_scrolly() {
    $.fn.dataTable.ext.errMode = 'none';
    $(document).ready(function () {
        $('.tblscrolly').DataTable({
           // retrieve: true,
            paging: false,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            scrollY: 132,
            scrollCollapse: true,
            searching: false,
            info: false,
            responsive: false,
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
        $($.fn.dataTable.tables(true)).DataTable().columns.adjust().draw();
    });
}
//scrollx apply as fixed
function datatable_scrollx() {
    // $.fn.dataTable.ext.errMode = 'none';
    //$(document).on('shown.bs.modal', '#manage_agent_registry', function () {
    $(document).ready(function () {
        var tblscrollx = $('.tblscrollx').DataTable({
            paging: false,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            searching: false,
            info: false,
            responsive: false,
            scrollX: true,
            retrieve: true
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
        tblscrollx.columns.adjust().draw();
    });
    //});
}
//pdf and excel simple export 
function datatable_pdfexport(reportname) {
    // debugger;
    //$.fn.dataTable.ext.errMode = 'none';
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "-" + (month) + "-" + now.getFullYear();
    $(document).ready(function () {
        $('.tblpdfexport').DataTable({
            paging: true,
            scrollX: false,
            retrieve: true,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            colReorder: true,
            searching: false,
            info: false,
            responsive: false,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    extend: 'colvis',
                },
                {
                    extend: 'pdf',
                    title: reportname + ' Date :-  ' + today,
                    exportOptions: {
                        columns: "thead th:not(.noExport)",
                        body: function (data, row, column, node) {
                            // Strip &gt; from branch/unit column to make it '>'
                            return column === 0 ?
                                data.replace(/&gt;/g, '>') :
                                data;
                        }
                    },
                    pageSize: 'A4',
                    customize: function (doc) {
                        doc.content.splice(0, 1);
                        var now = new Date();
                        var jsDate = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();
                        doc.pageMargins = [20, 60, 20, 30];
                        doc.defaultStyle.fontSize = 7;
                        doc.styles.tableHeader.fontSize = 7;
                        //  doc.styles.table.widths = '100 %';
                        // doc.defaultStyle.alignment = 'center';
                        doc.content.forEach(function (item) {
                            if (item.table) {
                                // item.table.widths = ['*', '*', '*', '*', '*', '*', '*', '*', '*', '*']
                            }
                        })

                        doc['header'] = (function () {
                            return {
                                columns: [
                                    {
                                        margin: [0, -5, 30, 15],
                                        alignment: 'left',
                                        width: 100,
                                        //image: logo
                                        image: 'data:image/png;base64,' + pdflogo + ''
                                    },
                                    {
                                        margin: [-10, 10, 10, 0],
                                        alignment: 'center',
                                        width: 410,
                                        //  italics: true,
                                        text: reportname,
                                        fontSize: 12,
                                    },
                                    {
                                        margin: [0, 20, 0, 0],
                                        alignment: 'right',
                                        italics: true,
                                        text: '',
                                        fontSize: 8,
                                    },

                                ],
                                margin: 20
                            }
                        });

                        doc['footer'] = (function (page, pages) {
                            return {
                                columns: [
                                    {
                                        alignment: 'left',
                                        text: ['Created on: ', { text: jsDate.toString() }]
                                    },
                                    {
                                        alignment: 'right',
                                        text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                    }
                                ],
                                margin: 20
                            }
                        });

                        var objLayout = {};
                        objLayout['hLineWidth'] = function (i) { return .5; };
                        objLayout['vLineWidth'] = function (i) { return .5; };
                        objLayout['hLineColor'] = function (i) { return '#ddd'; };
                        objLayout['vLineColor'] = function (i) { return '#ddd'; };
                        objLayout['paddingLeft'] = function (i) { return 5; };
                        objLayout['paddingRight'] = function (i) { return 5; };
                        doc.content[0].layout = objLayout;
                    },
                },
                {
                    extend: 'excel',
                    title: reportname + '- Date :- ' + today,
                    customize: function (xlsx) {
                        var sheet = xlsx.xl.worksheets['sheet1.xml'];
                        var numrows = 2;
                        var clR = $('row', sheet);
                        //var abc = 'Report Date :  ' + document.getElementById('date').value;
                        //update Row
                        clR.each(function () {
                            var attr = $(this).attr('r');
                            var ind = parseInt(attr);
                            ind = ind + numrows;
                            $(this).attr("r", ind);
                        });

                        // Create row before data
                        $('row c ', sheet).each(function () {
                            var attr = $(this).attr('r');
                            var pre = attr.substring(0, 1);
                            var ind = parseInt(attr.substring(1, attr.length));
                            ind = ind + numrows;
                            $(this).attr("r", pre + ind);
                        });

                        function Addrow(index, data) {
                            msg = '<row r="' + index + '">'
                            for (i = 0; i < data.length; i++) {
                                var key = data[i].key;
                                var value = data[i].value;
                                msg += '<c t="inlineStr" r="' + key + index + '">';
                                msg += '<is>';
                                msg += '<t>' + value + '</t>';
                                msg += '</is>';
                                msg += '</c>';
                            }
                            msg += '</row>';
                            return msg;
                        }

                        var r1 = Addrow(1, [{ key: 'B', value: reportname + ' Date : ' + today }]);
                        var r2 = Addrow(2, [{ key: 'A', value: '' }]);

                        sheet.childNodes[0].childNodes[1].innerHTML = r1 + r2 + sheet.childNodes[0].childNodes[1].innerHTML;
                        $('row c[r^="B1"]', sheet).attr('s', '2');
                    }
                }
            ],
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
    });
}
//pdf and excel for counting wise export  
function datatable_pdfexport_counting(reportname) {
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "-" + (month) + "-" + now.getFullYear();
    $(document).ready(function () {
        $('.tbl_pdfexport_count').DataTable({
            paging: true,
            retrieve: true,
            order: [],
            columnDefs: [{
                orderable: false,
                targets: -1,
                visible: false,
            }],
            colReorder: true,
            searching: false,
            info: false,
            responsive: false,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    extend: 'colvis',
                },
                {
                    extend: 'pdf',
                    title: reportname + ' Date :-  ' + today,
                    exportOptions: {
                        columns: "thead th:not(.noExport)",
                        body: function (data, row, column, node) {
                            // Strip &gt; from branch/unit column to make it '>'
                            return column === 0 ?
                                data.replace(/&gt;/g, '>') :
                                data;
                        }
                    },
                    pageSize: 'A4',
                    customize: function (doc) {
                        doc.content.splice(0, 1);
                        var now = new Date();
                        var jsDate = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();
                        doc.pageMargins = [20, 60, 20, 30];
                        doc.defaultStyle.fontSize = 7;
                        doc.styles.tableHeader.fontSize = 7;
                        //  doc.styles.table.widths = '100 %';
                        // doc.defaultStyle.alignment = 'center';
                        doc.content.forEach(function (item) {
                            if (item.table) {
                                // item.table.widths = ['*', '*', '*', '*', '*', '*', '*', '*', '*', '*']
                            }
                        })

                        doc['header'] = (function () {
                            return {
                                columns: [
                                    {
                                        margin: [0, -5, 30, 15],
                                        alignment: 'left',
                                        width: 100,
                                        //image: logo
                                        image: 'data:image/png;base64,' + pdflogo + ''
                                    },
                                    {
                                        margin: [-10, 10, 10, 0],
                                        alignment: 'center',
                                        width: 410,
                                        //  italics: true,
                                        text: reportname,
                                        fontSize: 12,
                                    },
                                    {
                                        margin: [0, 20, 0, 0],
                                        alignment: 'right',
                                        italics: true,
                                        text: '',
                                        fontSize: 8,
                                    },

                                ],
                                margin: 20
                            }
                        });

                        doc['footer'] = (function (page, pages) {
                            return {
                                columns: [
                                    {
                                        alignment: 'left',
                                        text: ['Created on: ', { text: jsDate.toString() }]
                                    },
                                    {
                                        alignment: 'right',
                                        text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                    }
                                ],
                                margin: 20
                            }
                        });

                        var objLayout = {};
                        objLayout['hLineWidth'] = function (i) { return .5; };
                        objLayout['vLineWidth'] = function (i) { return .5; };
                        objLayout['hLineColor'] = function (i) { return '#ddd'; };
                        objLayout['vLineColor'] = function (i) { return '#ddd'; };
                        objLayout['paddingLeft'] = function (i) { return 5; };
                        objLayout['paddingRight'] = function (i) { return 5; };
                        doc.content[0].layout = objLayout;
                    },
                },
                {
                    extend: 'excel',
                    title: reportname + '- Date :- ' + today,
                    customize: function (xlsx) {
                        var sheet = xlsx.xl.worksheets['sheet1.xml'];
                        var numrows = 2;
                        var clR = $('row', sheet);
                        //var abc = 'Report Date :  ' + document.getElementById('date').value;
                        //update Row
                        clR.each(function () {
                            var attr = $(this).attr('r');
                            var ind = parseInt(attr);
                            ind = ind + numrows;
                            $(this).attr("r", ind);
                        });

                        // Create row before data
                        $('row c ', sheet).each(function () {
                            var attr = $(this).attr('r');
                            var pre = attr.substring(0, 1);
                            var ind = parseInt(attr.substring(1, attr.length));
                            ind = ind + numrows;
                            $(this).attr("r", pre + ind);
                        });

                        function Addrow(index, data) {
                            msg = '<row r="' + index + '">'
                            for (i = 0; i < data.length; i++) {
                                var key = data[i].key;
                                var value = data[i].value;
                                msg += '<c t="inlineStr" r="' + key + index + '">';
                                msg += '<is>';
                                msg += '<t>' + value + '</t>';
                                msg += '</is>';
                                msg += '</c>';
                            }
                            msg += '</row>';
                            return msg;
                        }

                        var r1 = Addrow(1, [{ key: 'B', value: reportname + ' Date : ' + today }]);
                        var r2 = Addrow(2, [{ key: 'A', value: '' }]);

                        sheet.childNodes[0].childNodes[1].innerHTML = r1 + r2 + sheet.childNodes[0].childNodes[1].innerHTML;
                        $('row c[r^="B1"]', sheet).attr('s', '2');
                    }
                }
            ],
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
    });
}
//pdf and excel for scroll wise export  
function datatable_pdfexport_scroll(reportname, reportdate) {
    // debugger;
    //$.fn.dataTable.ext.errMode = 'none';
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "-" + (month) + "-" + now.getFullYear();
    $(document).ready(function () {
        var tbl_pdfexport_scroll = $('.tbl_pdfexport_scroll').DataTable({
            paging: true,
            scrollX: true,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            colReorder: true,
            searching: false,
            info: false,
            responsive: false,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    extend: 'colvis',
                },
                {
                    extend: 'pdf',
                    title: reportname + ' Date :-  ' + today,
                    exportOptions: {
                        columns: "thead th:not(.noExport)",
                        body: function (data, row, column, node) {
                            // Strip &gt; from branch/unit column to make it '>'
                            return column === 0 ?
                                data.replace(/&gt;/g, '>') :
                                data;
                        }
                    },
                    pageSize: 'A4',
                    customize: function (doc) {
                        doc.content.splice(0, 1);
                        var now = new Date();
                        var jsDate = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();
                        doc.pageMargins = [20, 60, 20, 30];
                        doc.defaultStyle.fontSize = 7;
                        doc.styles.tableHeader.fontSize = 7;
                        doc.content.forEach(function (item) {
                            if (item.table) {
                                // item.table.widths = ['*', '*', '*', '*', '*', '*', '*', '*', '*', '*']
                            }
                        })
                        // var date = document.getElementById('date').value;
                        doc['header'] = (function () {
                            return {
                                columns: [
                                    {
                                        margin: [0, -5, 30, 15],
                                        alignment: 'left',
                                        width: 100,
                                        //image: logo
                                        image: 'data:image/png;base64,' + pdflogo + ''
                                    },
                                    {
                                        margin: [-10, 10, 10, 0],
                                        alignment: 'center',
                                        width: 410,
                                        //  italics: true,
                                        text: reportname,
                                        fontSize: 12,
                                    },
                                    {
                                        margin: [-82, 30, 5, 0],
                                        alignment: 'right',
                                        italics: true,
                                        text: 'Report On:' + reportdate,
                                        fontSize: 8,
                                    },

                                ],
                                margin: 20
                            }
                        });

                        doc['footer'] = (function (page, pages) {
                            return {
                                columns: [
                                    {
                                        alignment: 'left',
                                        text: ['Created on: ', { text: jsDate.toString() }]
                                    },
                                    {
                                        alignment: 'right',
                                        text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                    }
                                ],
                                margin: 20
                            }
                        });

                        var objLayout = {};
                        objLayout['hLineWidth'] = function (i) { return .5; };
                        objLayout['vLineWidth'] = function (i) { return .5; };
                        objLayout['hLineColor'] = function (i) { return '#ddd'; };
                        objLayout['vLineColor'] = function (i) { return '#ddd'; };
                        objLayout['marginTop'] = function (i) { return 10; };
                        objLayout['paddingLeft'] = function (i) { return 5; };
                        objLayout['paddingRight'] = function (i) { return 5; };
                        doc.content[0].layout = objLayout;
                    },
                },
                {
                    extend: 'excel',
                    title: reportname + '- Date :- ' + today,
                    customize: function (xlsx) {
                        var sheet = xlsx.xl.worksheets['sheet1.xml'];
                        var numrows = 4;
                        var clR = $('row', sheet);
                        //var abc = 'Report Date :  ' + document.getElementById('date').value;
                        //update Row
                        clR.each(function () {
                            var attr = $(this).attr('r');
                            var ind = parseInt(attr);
                            ind = ind + numrows;
                            $(this).attr("r", ind);
                        });

                        // Create row before data
                        $('row c ', sheet).each(function () {
                            var attr = $(this).attr('r');
                            var pre = attr.substring(0, 1);
                            var ind = parseInt(attr.substring(1, attr.length));
                            ind = ind + numrows;
                            $(this).attr("r", pre + ind);
                        });

                        function Addrow(index, data) {
                            msg = '<row r="' + index + '">'
                            for (i = 0; i < data.length; i++) {
                                var key = data[i].key;
                                var value = data[i].value;
                                msg += '<c t="inlineStr" r="' + key + index + '">';
                                msg += '<is>';
                                msg += '<t>' + value + '</t>';
                                msg += '</is>';
                                msg += '</c>';
                            }
                            msg += '</row>';
                            return msg;
                        }

                        var r1 = Addrow(1, [{ key: 'B', value: reportname + ' Date : ' + today }]);
                        var r2 = Addrow(2, [{ key: 'A', value: '' }]);
                        var r3 = Addrow(3, [{ key: 'A', value: 'Report On: ' + reportdate }]);
                        var r4 = Addrow(4, [{ key: 'A', value: '' }]);

                        sheet.childNodes[0].childNodes[1].innerHTML = r1 + r2 + r3 + r4 + sheet.childNodes[0].childNodes[1].innerHTML;
                        $('row c[r^="B1"]', sheet).attr('s', '2');
                        $('row c[r^="A3"]', sheet).attr('s', '2');

                    }
                }
            ],
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
        tbl_pdfexport_scroll.columns.adjust().draw();
    });
}
//pdf and excel only date wise export 
function datatable_pdfexport_date(reportname, reportdate) {
    //$.fn.dataTable.ext.errMode = 'none';
    // debugger;
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "-" + (month) + "-" + now.getFullYear();

    $(document).ready(function () {
        $('.tbl_pdfexport_date').DataTable({
            paging: true,
            retrieve: true,
            scrollX: false,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            colReorder: true,
            searching: false,
            info: false,
            responsive: false,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    extend: 'colvis',
                },
                {
                    extend: 'pdf',
                    title: reportname + ' Date :-  ' + today,
                    exportOptions: {
                        columns: "thead th:not(.noExport)",
                        body: function (data, row, column, node) {
                            // Strip &gt; from branch/unit column to make it '>'
                            return column === 0 ?
                                data.replace(/&gt;/g, '>') :
                                data;
                        }
                    },
                    pageSize: 'A4',
                    customize: function (doc) {
                        doc.content.splice(0, 1);
                        var now = new Date();
                        var jsDate = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();
                        doc.pageMargins = [20, 60, 20, 30];
                        doc.defaultStyle.fontSize = 7;
                        doc.styles.tableHeader.fontSize = 7;
                        doc.content.forEach(function (item) {
                            if (item.table) {
                                // item.table.widths = ['*', '*', '*', '*', '*', '*', '*', '*', '*', '*']
                            }
                        })
                        // var date = document.getElementById('date').value;
                        doc['header'] = (function () {
                            return {
                                columns: [
                                    {
                                        margin: [0, -5, 30, 15],
                                        alignment: 'left',
                                        width: 100,
                                        //image: logo
                                        image: 'data:image/png;base64,' + pdflogo + ''
                                    },
                                    {
                                        margin: [-10, 10, 10, 0],
                                        alignment: 'center',
                                        width: 410,
                                        //  italics: true,
                                        text: reportname,
                                        fontSize: 12,
                                    },
                                    {
                                        margin: [-82, 30, 5, 0],
                                        alignment: 'right',
                                        italics: true,
                                        text: 'Report On:' + reportdate,
                                        fontSize: 8,
                                    },

                                ],
                                margin: 20
                            }
                        });

                        doc['footer'] = (function (page, pages) {
                            return {
                                columns: [
                                    {
                                        alignment: 'left',
                                        text: ['Created on: ', { text: jsDate.toString() }]
                                    },
                                    {
                                        alignment: 'right',
                                        text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                    }
                                ],
                                margin: 20
                            }
                        });

                        var objLayout = {};
                        objLayout['hLineWidth'] = function (i) { return .5; };
                        objLayout['vLineWidth'] = function (i) { return .5; };
                        objLayout['hLineColor'] = function (i) { return '#ddd'; };
                        objLayout['vLineColor'] = function (i) { return '#ddd'; };
                        objLayout['marginTop'] = function (i) { return 10; };
                        objLayout['paddingLeft'] = function (i) { return 5; };
                        objLayout['paddingRight'] = function (i) { return 5; };
                        doc.content[0].layout = objLayout;
                    },
                },
                {
                    extend: 'excel',
                    title: reportname + '- Date :- ' + today,
                    customize: function (xlsx) {
                        var sheet = xlsx.xl.worksheets['sheet1.xml'];
                        var numrows = 4;
                        var clR = $('row', sheet);
                        //var abc = 'Report Date :  ' + document.getElementById('date').value;
                        //update Row
                        clR.each(function () {
                            var attr = $(this).attr('r');
                            var ind = parseInt(attr);
                            ind = ind + numrows;
                            $(this).attr("r", ind);
                        });

                        // Create row before data
                        $('row c ', sheet).each(function () {
                            var attr = $(this).attr('r');
                            var pre = attr.substring(0, 1);
                            var ind = parseInt(attr.substring(1, attr.length));
                            ind = ind + numrows;
                            $(this).attr("r", pre + ind);
                        });

                        function Addrow(index, data) {
                            msg = '<row r="' + index + '">'
                            for (i = 0; i < data.length; i++) {
                                var key = data[i].key;
                                var value = data[i].value;
                                msg += '<c t="inlineStr" r="' + key + index + '">';
                                msg += '<is>';
                                msg += '<t>' + value + '</t>';
                                msg += '</is>';
                                msg += '</c>';
                            }
                            msg += '</row>';
                            return msg;
                        }

                        var r1 = Addrow(1, [{ key: 'B', value: reportname + ' Date : ' + today }]);
                        var r2 = Addrow(2, [{ key: 'A', value: '' }]);
                        var r3 = Addrow(3, [{ key: 'A', value: 'Report On: ' + reportdate }]);
                        var r4 = Addrow(4, [{ key: 'A', value: '' }]);

                        sheet.childNodes[0].childNodes[1].innerHTML = r1 + r2 + r3 + r4 + sheet.childNodes[0].childNodes[1].innerHTML;
                        $('row c[r^="B1"]', sheet).attr('s', '2');
                        $('row c[r^="A3"]', sheet).attr('s', '2');

                    }
                }
            ],
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
    });
}
//pdf and excel only date and time wise export
function datatable_pdfexport_datetime(reportname, reportdate) {
    // debugger;
    //$.fn.dataTable.ext.errMode = 'none';
    var now = new Date();
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = (day) + "-" + (month) + "-" + now.getFullYear();

    $(document).ready(function () {
        $('.tbl_pdfexport_datetime').DataTable({
            paging: true,
            scrollX: false,
            order: [],
            columnDefs: [{ orderable: false, targets: [] }],
            colReorder: true,
            searching: false,
            info: false,
            responsive: false,
            dom: '<"html5buttons"B>lTfgitp',
            buttons: [
                {
                    extend: 'colvis',
                },
                {
                    extend: 'pdf',
                    title: reportname + ' Date :-  ' + today,
                    exportOptions: {
                        columns: "thead th:not(.noExport)",
                        body: function (data, row, column, node) {
                            // Strip &gt; from branch/unit column to make it '>'
                            return column === 0 ?
                                data.replace(/&gt;/g, '>') :
                                data;
                        }
                    },
                    pageSize: 'A4',
                    customize: function (doc) {
                        doc.content.splice(0, 1);
                        var now = new Date();
                        var jsDate = now.getDate() + '-' + (now.getMonth() + 1) + '-' + now.getFullYear();
                        doc.pageMargins = [20, 60, 20, 30];
                        doc.defaultStyle.fontSize = 7;
                        doc.styles.tableHeader.fontSize = 7;
                        doc.content.forEach(function (item) {
                            if (item.table) {
                                // item.table.widths = ['*', '*', '*', '*', '*', '*', '*', '*', '*', '*']
                            }
                        })
                        // var date = document.getElementById('date').value;
                        doc['header'] = (function () {
                            return {
                                columns: [
                                    {
                                        margin: [0, -5, 30, 15],
                                        alignment: 'left',
                                        width: 100,
                                        //image: logo
                                        image: 'data:image/png;base64,' + pdflogo + ''
                                    },
                                    {
                                        margin: [-10, 10, 10, 0],
                                        alignment: 'center',
                                        width: 410,
                                        //  italics: true,
                                        text: reportname,
                                        fontSize: 12,
                                    },
                                    {
                                        margin: [-160, 30, 5, 0],
                                        alignment: 'right',
                                        italics: true,
                                        text: 'Report On:' + reportdate,
                                        fontSize: 8,
                                    },

                                ],
                                margin: 20
                            }
                        });

                        doc['footer'] = (function (page, pages) {
                            return {
                                columns: [
                                    {
                                        alignment: 'left',
                                        text: ['Created on: ', { text: jsDate.toString() }]
                                    },
                                    {
                                        alignment: 'right',
                                        text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                    }
                                ],
                                margin: 20
                            }
                        });

                        var objLayout = {};
                        objLayout['hLineWidth'] = function (i) { return .5; };
                        objLayout['vLineWidth'] = function (i) { return .5; };
                        objLayout['hLineColor'] = function (i) { return '#ddd'; };
                        objLayout['vLineColor'] = function (i) { return '#ddd'; };
                        objLayout['marginTop'] = function (i) { return 10; };
                        objLayout['paddingLeft'] = function (i) { return 5; };
                        objLayout['paddingRight'] = function (i) { return 5; };
                        doc.content[0].layout = objLayout;
                    },
                },
                {
                    extend: 'excel',
                    title: reportname + '- Date :- ' + today,
                    customize: function (xlsx) {
                        var sheet = xlsx.xl.worksheets['sheet1.xml'];
                        var numrows = 4;
                        var clR = $('row', sheet);
                        //var abc = 'Report Date :  ' + document.getElementById('date').value;
                        //update Row
                        clR.each(function () {
                            var attr = $(this).attr('r');
                            var ind = parseInt(attr);
                            ind = ind + numrows;
                            $(this).attr("r", ind);
                        });

                        // Create row before data
                        $('row c ', sheet).each(function () {
                            var attr = $(this).attr('r');
                            var pre = attr.substring(0, 1);
                            var ind = parseInt(attr.substring(1, attr.length));
                            ind = ind + numrows;
                            $(this).attr("r", pre + ind);
                        });

                        function Addrow(index, data) {
                            msg = '<row r="' + index + '">'
                            for (i = 0; i < data.length; i++) {
                                var key = data[i].key;
                                var value = data[i].value;
                                msg += '<c t="inlineStr" r="' + key + index + '">';
                                msg += '<is>';
                                msg += '<t>' + value + '</t>';
                                msg += '</is>';
                                msg += '</c>';
                            }
                            msg += '</row>';
                            return msg;
                        }

                        var r1 = Addrow(1, [{ key: 'B', value: reportname + ' Date : ' + today }]);
                        var r2 = Addrow(2, [{ key: 'A', value: '' }]);
                        var r3 = Addrow(3, [{ key: 'A', value: 'Report On: ' + reportdate }]);
                        var r4 = Addrow(4, [{ key: 'A', value: '' }]);

                        sheet.childNodes[0].childNodes[1].innerHTML = r1 + r2 + r3 + r4 + sheet.childNodes[0].childNodes[1].innerHTML;
                        $('row c[r^="B1"]', sheet).attr('s', '2');
                        $('row c[r^="A3"]', sheet).attr('s', '2');

                    }
                }
            ],
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });
    });
}
//asset system list only
function systemlisttable() {
    $(document).ready(function () {
        // debugger;
        $('#asset').DataTable({
            paging: true,
            order: [],
            columnDefs: [{
                orderable: false,
                columnDefs: {
                    visible: false,
                    targets: 8,
                },
            }],
            // columnDefs: [{ orderable: false, targets: [] }],
            colReorder: true,
            searching: false,
            info: true,
            responsive: false
        });
        $(".table").css({ "width": "100%" });
        $(".dataTables_scrollHeadInner ").css({ "width": "100%" });

    });
}
