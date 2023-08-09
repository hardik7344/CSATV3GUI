var tableToExcel = (function() {
  var uri = 'data:application/vnd.ms-excel;base64,'
    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
    , base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) }
  return function(table, name) {
    if (!table.nodeType) table = document.getElementById(table)
    var ctx = {worksheet: name || 'Worksheet', table: table.innerHTML}
    window.location.href = uri + base64(format(template, ctx))
  }
})()

//var tableToExcel = (function () {
//    var uri = 'data:application/vnd.ms-excel;base64,'
//        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
//        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
//        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
//    return function (table, name, filename) {
//        if (!table.nodeType) table = document.getElementById(table)
//        var $temp = $("<table></table>");
//        $temp.append($table.floatThead("getRowGroups"));
//        var ctx = { worksheet: name || 'Worksheet', table: $temp.html() }

//        document.getElementById("parusexcel").href = uri + base64(format(template, ctx));
//        document.getElementById("parusexcel").download = filename;
//        document.getElementById("parusexcel").click();

//    }
//})()

//var tableToExcel = (function () {
//    var uri = 'data:application/vnd.ms-excel;base64,'
//    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
//    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
//    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
//    return function (table, name, filename) {
//        if (!table.nodeType) table = document.getElementById(table)
//        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }

//       // document.getElementById("parusexcel").href = uri + base64(format(template, ctx));
//        document.getElementById("parusexcel").href = "data:application/vnd.ms-excel;base64,PGh0bWwgeG1sbnM6bz0idXJuOnNjaGVtYXMtbWljcm9zb2Z0LWNvbTpvZmZpY2U6b2ZmaWNlIiB4bWxuczp4PSJ1cm46c2NoZW1hcy1taWNyb3NvZnQtY29tOm9mZmljZTpleGNlbCIgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnL1RSL1JFQy1odG1sNDAiPjxoZWFkPjwhLS1baWYgZ3RlIG1zbyA5XT48eG1sPjx4OkV4Y2VsV29ya2Jvb2s+PHg6RXhjZWxXb3Jrc2hlZXRzPjx4OkV4Y2VsV29ya3NoZWV0Pjx4Ok5hbWU+V29ya3NoZWV0PC94Ok5hbWU+PHg6V29ya3NoZWV0T3B0aW9ucz48eDpEaXNwbGF5R3JpZGxpbmVzLz48L3g6V29ya3NoZWV0T3B0aW9ucz48L3g6RXhjZWxXb3Jrc2hlZXQ+PC94OkV4Y2VsV29ya3NoZWV0cz48L3g6RXhjZWxXb3JrYm9vaz48L3htbD48IVtlbmRpZl0tLT48L2hlYWQ+PGJvZHk+PHRhYmxlPgoJPC90YWJsZT48L2JvZHk+PC9odG1sPg==";
//        document.getElementById("parusexcel").download = filename;
//        document.getElementById("parusexcel").click();

//    }
//})()
//$(document).ready(function () {
//    $('table.parus-table').floatThead({
//        useAbsolutePositioning: false
//    });
//});
