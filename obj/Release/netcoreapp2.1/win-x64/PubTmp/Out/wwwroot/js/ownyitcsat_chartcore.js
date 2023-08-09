var labels = "";
var receivepacket = "";
var data = "";
var data1 = "";
var bgcolor = "";
var bgcolor1 = "";
var ChartContent = "";
var labelvalue = "";
var titlevalue = "";
var charttype = "";
function piechart(id, label, data, bgcolor, bgcolor1) {
    //debugger;
    if (label === "") {
        label = "";
    }
    if (label === "undefined") {
        label = "";
    }
    if (id === "dashboard_policy_violation") {
        if (label === "[]") {
            //label = "[{\"Label\":\"RAM\",\"Value\":0,\"Bgcolor\":\"#a02c5a\",\"Bgcolor1\":\"rgba(160,44,90,0.5)\"},{\"Label\":\"CPU\",\"Value\":0,\"Bgcolor\":\"#009900\",\"Bgcolor1\":\"rgba(140,205,159,0.5)\"},{\"Label\":\"HDD\",\"Value\":0,\"Bgcolor\":\"#FFCC00\",\"Bgcolor1\":\"rgba(255,243,214,0.5)\"}]";
            label = "[{\"Label\":\"Login Password\",\"Value\":0,\"Bgcolor\":\"#FFC300\",\"Bgcolor1\":\"rgb(255,195,0)\"},{\"Label\":\"No Screen Saver Password\",\"Value\":0,\"Bgcolor\":\"#FF5733\",\"Bgcolor1\":\"rgb(255,87,51)\"},{\"Label\":\"No AV Installed\",\"Value\":0,\"Bgcolor\":\"#C70039\",\"Bgcolor1\":\"rgb(199,0,57)\"},{\"Label\":\"Pirated OS\",\"Value\":0,\"Bgcolor\":\"#33B5FF\",\"Bgcolor1\":\"rgb(51,181,255)\"},{\"Label\":\"USB Port Enabled\",\"Value\":0,\"Bgcolor\":\"#C28049\",\"Bgcolor1\":\"rgb(194,128,73)\"},{\"Label\":\"Firewall Disabled\",\"Value\":0,\"Bgcolor\":\"#4DC786\",\"Bgcolor1\":\"rgb(77,199,134)\"},{\"Label\":\"No Encryption Software\",\"Value\":0,\"Bgcolor\":\"#C74D81\",\"Bgcolor1\":\"rgb(199,77,129)\"},{\"Label\":\"Not In Domain\",\"Value\":0,\"Bgcolor\":\"#FF9933\",\"Bgcolor1\":\"rgb(255,153,51)\"},{\"Label\":\"Share Folder Exist\",\"Value\":0,\"Bgcolor\":\"#B266FF\",\"Bgcolor1\":\"rgb(178,102,255)\"}]";
        }
    }
    if (id === "dashboard_pc_connectivity") {
        if (label === "[]") {
            label = "[{\"Label\":\"Linked Equipment\",\"Value\":0,\"Bgcolor\":\"#009900\",\"Bgcolor1\":\"rgba(140,205,159,0.5)\"},{\"Label\":\"Details of system not linked\",\"Value\":0,\"Bgcolor\":\"#C00000\",\"Bgcolor1\":\"rgba(255,196,196,0.5)\"},{\"Label\":\"Not Monitoring Device\",\"Value\":0,\"Bgcolor\":\"#FFCC00\",\"Bgcolor1\":\"rgba(255,243,214,0.5)\"},{\"Label\":\"Today Connected Device\",\"Value\":0,\"Bgcolor\":\"#0000FF\",\"Bgcolor1\":\"rgba(0,0,255,0.5)\"}]";
        }
    }
    if (id === "dashboard_audit_trail") {
        if (label === "[]") {
            label = "[{\"Label\":\"Service\",\"Value\":0,\"Bgcolor\":\"#a02c5a\",\"Bgcolor1\":\"rgba(160,44,90,0.5)\"},{\"Label\":\"Process\",\"Value\":0,\"Bgcolor\":\"#003399\",\"Bgcolor1\":\"rgba(81,131,148,0.5)\"},{\"Label\":\"Hardware\",\"Value\":0,\"Bgcolor\":\"#FFCC00\",\"Bgcolor1\":\"rgba(255,243,214,0.5)\"},{\"Label\":\"Software\",\"Value\":0,\"Bgcolor\":\"#C00000\",\"Bgcolor1\":\"rgba(255,216,224,0.5)\"},{\"Label\":\"User\",\"Value\":0,\"Bgcolor\":\"#009900\",\"Bgcolor1\":\"rgba(140,205,159,0.5)\"},{\"Label\":\"Remote\",\"Value\":0,\"Bgcolor\":\"#A055BC\",\"Bgcolor1\":\"rgba(160,85,188,0.5)\"},{\"Label\":\"Share\",\"Value\":0,\"Bgcolor\":\"#2cc2c2\",\"Bgcolor1\":\"rgba(191,226,226,0.5)\"},{\"Label\":\"IP Change\",\"Value\":0,\"Bgcolor\":\"#002060\",\"Bgcolor1\":\"rgba(0,85,205,0.5)\"},{\"Label\":\"Performance Monitoring\",\"Value\":0,\"Bgcolor\":\"#de3e46\",\"Bgcolor1\":\"rgba(148,103,102,0.5)\"},{\"Label\":\"Storage\",\"Value\":0,\"Bgcolor\":\"#FF9900\",\"Bgcolor1\":\"rgba(255,233,211,0.5)\"}]";
        }
    }
    if (id === "dashboard_poll_time") {
        if (label === "[]") {
            label = "[{\"Label\":\"Current Day\",\"Value\":0,\"Bgcolor\":\"#009900\",\"Bgcolor1\":\"rgba(140,205,159,0.5)\"},{\"Label\":\"1 Days\",\"Value\":0,\"Bgcolor\":\"#C00000\",\"Bgcolor1\":\"rgba(255,196,196,0.5)\"},{\"Label\":\"2 Days\",\"Value\":0,\"Bgcolor\":\"#FFCC00\",\"Bgcolor1\":\"rgba(255,243,214,0.5)\"},{\"Label\":\"7 Days\",\"Value\":0,\"Bgcolor\":\"#002060\",\"Bgcolor1\":\"rgba(170,215,252,0.5)\"},{\"Label\":\"15 Days\",\"Value\":0,\"Bgcolor\":\"#C00000\",\"Bgcolor1\":\"rgba(255,216,224,0.5)\"}]";
        }
    }
    if (label !== "") {
        try {
            var ctx = document.getElementById(id);
            var selectedIndex = null;
            var label1 = JSON.parse(label);
            var label2 = label1.map(function (e) {
                return e.Label;
            });
            var data1 = label1.map(function (e) {
                return e.Value;
            });
            bgcolor = label1.map(function (e) {
                return e.Bgcolor;
            });
            bgcolor1 = label1.map(function (e) {
                return e.Bgcolor1;
            });
        }

        catch (err) {
            //  throw err;
        }
        var data5 = {
            labels: label2,
            datasets: [{

                data: data1,
                backgroundColor: bgcolor,
                borderColor: bgcolor1,
                //Borderopacity:0.5,
                borderWidth: 0,
                hoverBorderWidth: 4
            }
            ]
        };
        var options = {
            responsive: true,
            maintainAspectRatio: false,
            title: {
                display: true,
                position: "top",
                fontsize: 25,
                fontcolor: "#f2f2f2"
            },
            legend: {
                display: true,
                position: "bottom"
            },
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true,
                        callback: function (value, index, values) {
                            return '';
                        }
                    },
                    gridLines: {
                        display: false,
                        drawBorder: false
                    }
                }]
            }
        };
        //if (PieChart)
        //    PieChart.destroy();
        if (PieChart !== undefined)
            PieChart.destroy();
        var ChartContent = '';
        if (id === "dashboard_policy_violation") {
            ChartContent = document.getElementById('divpolicy');
            ChartContent.innerHTML = '&nbsp;';
            $('#divpolicy').append('<canvas id="dashboard_policy_violation" ></canvas>');
            //   ctx = $("#dashboard_policy_violation").get(0).getContext("2d");
            ctx = document.getElementById('dashboard_policy_violation');
        }
        if (id === "dashboard_audit_trail") {
            ChartContent = document.getElementById('divaudit');
            ChartContent.innerHTML = '&nbsp;';
            $('#divaudit').append('<canvas id="dashboard_audit_trail" ></canvas>');
            // ctx = $("#dashboard_audit_trail").get(0).getContext("2d");
            ctx = document.getElementById('dashboard_audit_trail');

        }
        if (id === "dashboard_poll_time") {
            ChartContent = document.getElementById('divlastpoll');
            ChartContent.innerHTML = '&nbsp;';
            $('#divlastpoll').append('<canvas id="dashboard_poll_time" ></canvas>');
            // ctx = $("#dashboard_poll_time").get(0).getContext("2d");
            ctx = document.getElementById('dashboard_poll_time');
        }
        if (id === "dashboard_pc_connectivity") {
            ChartContent = document.getElementById('divpcconnectivity');
            ChartContent.innerHTML = '&nbsp;';
            $('#divpcconnectivity').append('<canvas id="dashboard_pc_connectivity" ></canvas>');
            //  ctx = $("#dashboard_pc_connectivity").get(0).getContext("2d");
            ctx = document.getElementById('dashboard_pc_connectivity');
        }
        var PieChart = new Chart(ctx, {
            type: 'pie',
            data: data5,
            // backgroundColor: randomColorGenerator(),
            options: options
        });
        //Chart.plugins.register({
        //    beforeInit: function (chart) {
        //        if (id === "dashboard_audit_trail") {
        //            chart.data.labels = ['User', 'Hardware'];
        //            chart.data.datasets[0].backgroundColor = ['#1abc9c', '#34495e'];
        //        }
        //    }
        //});
        ctx.onclick = function (evt) {
            //debugger;
            var activePoints = PieChart.getElementsAtEvent(evt);

            if (activePoints[0]) {

                var chartData = activePoints[0]['_chart'].config.data;
                var idx = activePoints[0]['_index'];
                var label = chartData.labels[idx];
                var value = chartData.datasets[0].data[idx];

                if (label === 'No Login Password') {
                    labelvalue = 10001;
                    titlevalue = 'Login Password not set';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'No Screen Saver Password') {
                    labelvalue = 10002;
                    titlevalue = 'Screen Saver Password not set';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'No AV Installed') {
                    labelvalue = 10003;
                    titlevalue = 'Antivirus not Installed';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'Pirated OS') {
                    labelvalue = 10004;
                    titlevalue = 'Pirated OS Installed';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'USB Port Enabled') {
                    labelvalue = 10005;
                    titlevalue = 'USB Port Enabled';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'Firewall Disabled') {
                    labelvalue = 10006;
                    titlevalue = 'Firewall Disabled';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'No Encryption Software') {
                    labelvalue = 10007;
                    titlevalue = 'Encryption Software not installed';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'Not In Domain') {
                    labelvalue = 10008;
                    titlevalue = 'Not In Domain';
                    $('#policy_mgmt').modal('show');
                } if (label === 'Share Folder Exist') {
                    labelvalue = 10009;
                    titlevalue = 'Share Folder Exist';
                    $('#policy_mgmt').modal('show');
                }

                if (label === 'RAM') {
                    labelvalue = 112;
                    titlevalue = 'RAM Detail';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'CPU') {
                    labelvalue = 111;
                    titlevalue = 'CPU Detail';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'HDD') {
                    labelvalue = 122;
                    titlevalue = 'HDD Detail';
                    $('#policy_mgmt').modal('show');
                }
                if (label === 'Hardware') {
                    labelvalue = 11;
                    titlevalue = 'Hardware Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'Software') {
                    labelvalue = 12;
                    titlevalue = 'Software Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'Remote') {
                    labelvalue = 10;
                    titlevalue = 'Remote Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'User') {
                    labelvalue = 15;
                    titlevalue = 'User Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'Share') {
                    labelvalue = 20;
                    titlevalue = 'Share Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'IP Change') {
                    labelvalue = 1;
                    titlevalue = 'IP Change Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'Service') {
                    labelvalue = 19;
                    titlevalue = 'Service Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'Performance Monitoring') {
                    labelvalue = 13;
                    titlevalue = 'Performance Monitoring Detail';
                    $('#change_mgmt').modal('show');
                }
                if (label === 'Storage') {
                    labelvalue = 14;
                    titlevalue = 'Remote Detail';
                    $('#change_mgmt').modal('show');
                }
                // PC Connectivity
                if (label === 'Linked Equipment') {
                    document.getElementById('divdrpou4').style.display = 'block';
                    $('#presentlink_popup').modal('show');
                    $('#pctitle').html(label);
                }
                if (label === 'Details of system not linked') {
                    document.getElementById('divdrpou4').style.display = 'block';
                    $('#presentlink_popup').modal('show');
                    $('#pctitle').html(label);
                }
                if (label === 'Today Connected Device') {
                    document.getElementById('divdrpou4').style.display = 'block';
                    $('#presentlink_popup').modal('show');
                    $('#pctitle').html(label);
                }
                if (label === 'Not Monitoring Device') {
                    document.getElementById('divdrpou4').style.display = 'none';
                    bind_systemlist();
                    $('#presentlink_popup').modal('show');
                    $('#pctitle').html(label);
                }

                if (label === 'Current Day') {
                    $('#1day_time_popup').modal('show');
                    $('#systemheader').html('0');
                }

                if (label === '1 Days') {

                    $('#1day_time_popup').modal('show');
                    //$('#systemtitle').html(' System Detail [ Day 1 ]');

                    $('#systemheader').html('01');

                    // $('#pctitle').html(label);
                }

                if (label === '2 Days') {

                    $('#1day_time_popup').modal('show');
                    //$('#systemtitle').html(' System Detail [ Day 2 ]');
                    $('#systemheader').html('02');
                }

                if (label === '7 Days') {
                    $('#1day_time_popup').modal('show');
                    //$('#systemtitle').html(' System Detail [ Day 7 ]');
                    $('#systemheader').html('07');
                }

                if (label === '15 Days') {
                    $('#1day_time_popup').modal('show');
                    //$('#systemtitle').html(' System Detail [ Day 15 Before ]');
                    $('#systemheader').html('15');
                    // $('#pctitle').html(label);
                }

                //label = convertNameToType(label)

                //   var value = chartData.datasets[0].data[idx];
                // asset audit Trail popup 
                if (label === 'Hardware') {
                    //$('.modal-title').html('Hardware Detail [ Count: ' + value +' ]');
                    $('#asset_audit_hardware_popup').modal('show');
                }
            }
        };
    }
}
function stackedchart(id, data) {
    //debugger;
    if (data === "") {
        data = "";
    }
    if (data === "undefined") {
        data = "";
    }
    if (data !== "") {
        try {
            var ctx1 = document.getElementById(id);
            var data1 = JSON.parse(data);
            var labels = data1.map(function (e) {
                return e.OU_Name;
            });
            var v1 = data1.map(function (e) {
                return e.DataLeakage;
            });

            //var v2 = data1.map(function (e) {
            //    return e.Printer;
            //});
            var v3 = data1.map(function (e) {
                return e.Internet_Modem;
            });
            var v4 = data1.map(function (e) {
                return e.Removable_media;
            });
        }
        catch (err) {
            //throw err;
        }
        data = {
            datasets: [
                {
                    label: 'Data Leakage',
                    data: v1,
                    backgroundColor: '#C00000',
                    borderWidth: 0
                },
                //{
                //    label: 'Printer',
                //    data: v2,
                //    backgroundColor: '#FFCC00',
                //    borderWidth: 0
                //},
                {
                    label: 'Internet Modem Usage',
                    data: v3,
                    backgroundColor: '#009900',
                    borderWidth: 0
                },
                {
                    label: 'Removable Media',
                    data: v4,
                    backgroundColor: '#a02c5a',
                    borderWidth: 0
                }

            ],

            labels: labels

        };
        var options = {
            responsive: true,
            maintainAspectRatio: false,
            title: {
                display: true,
                position: "top",
                //text: "Most Effected PC/VLAN",
                fontsize: 15,
                fontcolor: "#f2f2f2"
            },
            legend: {
                display: true,
                position: "bottom"
            },
            scales: {
                xAxes: [{
                    stacked: true,
                    categoryPercentage: 0.4,
                    // Change here
                    //  barPercentage: 0.7,

                    ticks: {
                        autoSkip: false
                        //fontSize: 10,

                    }
                }],
                yAxes: [{
                    stacked: true,
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            // when the floored value is the same as the value we have a whole number
                            if (Math.floor(label) === label) {
                                return label;
                            }
                        }
                    }
                }]
            }
        };

        var ChartContent = document.getElementById('diveffectedou');
        ChartContent.innerHTML = '&nbsp;';
        if (id === "dashboard_effected_pc_vlan") {
            $('#diveffectedou').append('<canvas id="dashboard_effected_pc_vlan"><canvas>');
            // ctx1 = $("#dashboard_effected_pc_vlan").get(0).getContext("2d");
            ctx1 = document.getElementById('dashboard_effected_pc_vlan');
        }
        var StackedChart = new Chart(ctx1, {
            type: 'bar',
            data: data,
            options: options
        });

        ctx1.onclick = function (evt) {
            //debugger;
            var activePoint = StackedChart.getElementAtEvent(evt);
            if (activePoint[0]) {
                //    var data = activePoint._chart.data;
                var datasetIndex = activePoint[0]._datasetIndex;
                //var datasetIndex = activePoint._datasetIndex;
                var labels = data.labels[activePoint[0]._index];
                var value = data.datasets[datasetIndex].data[activePoint[0]._index];
                if (datasetIndex === 0) {
                    // debugger;
                    //  $('#dlheader').html('Data Leakage Detail [Count: ' + value + ' ]');
                    showprint
                    $('#effected_vlan_popup').modal('show');
                    $('#ouheader').html(labels);
                    charttype = datasetIndex;
                    showdataleakagedata(charttype, labels);
                }
                if (datasetIndex === 1) {
                    //$('#prheader').html('Printer Detail [Count: ' + value + ' ]');
                    $('#showprint').show();
                    $('#effected_vlan_popup').modal('show');
                    $('#ouheader').html(labels);
                    charttype = datasetIndex;
                    showdataleakagedata(charttype, labels);
                }
                if (datasetIndex === 2) {
                    $('#effected_vlan_popup').modal('show');
                    $('#ouheader').html(labels);
                    charttype = datasetIndex;
                    showdataleakagedata(charttype, labels);
                }
                if (datasetIndex === 3) {
                    $('#effected_vlan_popup').modal('show');
                    $('#ouheader').html(labels);
                    charttype = datasetIndex;
                    showdataleakagedata(charttype, labels);
                }
                else {
                    datasetIndex = null;
                }
            }
            // alert(label, value);
        };
    }
}
function New_stackedchart(id, data) {
    //debugger;

    if (data === "") {
        data = "";
    }
    if (data === "undefined") {
        data = "";
    }
    if (data !== "") {
        try {
            var ctx2 = document.getElementById(id);

            var data1 = JSON.parse(data);

            var labels = data1.map(function (e) {
                return e.Alert_type;
            });
            var v1 = data1.map(function (e) {
                return e.Authorise;
            });

            var v2 = data1.map(function (e) {
                return e.Unauthorise;
            });

            var v3 = data1.map(function (e) {
                return e.Remaining;
            });
        }
        catch (err) {
            //throw (err);
        }
        data = {
            datasets: [
                {
                    label: "Authorized",
                    data: v1,
                    backgroundColor: '#009900',
                    borderWidth: 0
                },
                {
                    label: "Unauthorized",
                    data: v2,
                    backgroundColor: '#C00000',
                    borderWidth: 0
                },
                {
                    label: "Remaining",
                    data: v3,
                    backgroundColor: '#FFCC00',
                    borderWidth: 0
                    //hoverBorderWidth: 10,
                }
            ],
            labels: labels
        };
        var options = {
            responsive: true,
            maintainAspectRatio: false,
            title: {
                display: true,
                position: "top",
                //text: "Most Effected PC/VLAN",
                fontsize: 15,
                fontcolor: "#f2f2f2"
            },
            legend: {
                display: true,
                position: "bottom"
            },
            scales: {
                xAxes: [{
                    stacked: true,
                    categoryPercentage: 0.5
                }],

                yAxes: [{
                    stacked: true,
                    ticks: {
                        beginAtZero: true,
                        userCallback: function (label, index, labels) {
                            // when the floored value is the same as the value we have a whole number
                            if (Math.floor(label) === label) {
                                return label;
                            }
                        }
                    }
                }]
            }
        };

        var ChartContent = document.getElementById('divcsataudit');
        ChartContent.innerHTML = '&nbsp;';
        if (id === "dashboard_csat_audit") {
            $('#divcsataudit').append('<canvas id="dashboard_csat_audit"><canvas>');
            // ctx = $("#dashboard_csat_audit").get(0).getContext("2d");
            ctx2 = document.getElementById('dashboard_csat_audit');
        }
        var StackedChart1 = new Chart(ctx2, {
            type: 'bar',
            data: data,
            options: options
        });

        ctx2.onclick = function (evt) {

            var activePoint = StackedChart1.getElementAtEvent(evt);
            if (activePoint[0]) {
                var labels = data.labels[activePoint[0]._index];
                var datasetIndex = activePoint[0]._datasetIndex;
                if (labels === "Port") {
                    // $('#prheader').html('Printer Detail [Count: ' + value + ' ]');
                    $('#port_authorise_popup').modal('show');
                    // $('#ouheader1').html(labels);
                }
                if (labels === "Process") {
                    $('#process_authorise_popup').modal('show');

                    if (datasetIndex === 0)
                        $('#processtitle').html('1');
                    else if (datasetIndex === 1)
                        $('#processtitle').html('0');
                    else if (datasetIndex === 2)
                        $('#processtitle').html('2');
                    ShowCsatAuditData(labels);
                }
                if (labels === "Software") {

                    $('#process_authorise_popup').modal('show');
                    if (datasetIndex === 0)
                        $('#processtitle').html('1');
                    else if (datasetIndex === 1)
                        $('#processtitle').html('0');
                    else if (datasetIndex === 2)
                        $('#processtitle').html('2');
                    ShowCsatAuditData(labels);
                }
                if (labels === "User") {

                    $('#process_authorise_popup').modal('show');
                    if (datasetIndex === 0)
                        $('#processtitle').html('1');
                    else if (datasetIndex === 1)
                        $('#processtitle').html('0');
                    else if (datasetIndex === 2)
                        $('#processtitle').html('2');
                    ShowCsatAuditData(labels);
                }
                else {
                    datasetIndex = null;
                }
            }
            // alert(label, value);
        };
    }
}

//function New_stackedchart_dataleakage(id, data) {
//    //debugger;
//    alert(11);
//    alert(id);
//    alert(data);
//    if (data === "") {
//        data = "";
//    }
//    if (data === "undefined") {
//        data = "";
//    }
//    if (data !== "") {
//        try {
//            var ctx2 = document.getElementById(id);

//            var data1 = JSON.parse(data);

//            var labels = data1.map(function (e) {
//                return e.Label;
//            });
//            var v1 = data1.map(function (e) {
//                return e.Value;
//            });

            
//        }
//        catch (err) {
//            //throw (err);
//        }
//        data = {
//            datasets: [
//                {
//                    label: "Count",
//                    data: v1,
//                    backgroundColor: '#009900',
//                    borderWidth: 0
//                }
//            ],
//            labels: labels
//        };
//        var options = {
//            responsive: true,
//            maintainAspectRatio: false,
//            title: {
//                display: true,
//                position: "top",
//                //text: "Most Effected PC/VLAN",
//                fontsize: 15,
//                fontcolor: "#f2f2f2"
//            },
//            legend: {
//                display: true,
//                position: "bottom"
//            },
//            scales: {
//                xAxes: [{
//                    stacked: true,
//                    categoryPercentage: 0.5
//                }],

//                yAxes: [{
//                    stacked: true,
//                    ticks: {
//                        beginAtZero: true,
//                        userCallback: function (label, index, labels) {
//                            // when the floored value is the same as the value we have a whole number
//                            if (Math.floor(label) === label) {
//                                return label;
//                            }
//                        }
//                    }
//                }]
//            }
//        };

//        var ChartContent = document.getElementById(id);
//        ChartContent.innerHTML = '&nbsp;';
//        if (id === "media_data_leakage") {
//            $('#divcsatDLMedia').append('<canvas id="media_data_leakage"><canvas>');
//            // ctx = $("#dashboard_csat_audit").get(0).getContext("2d");
//            ctx2 = document.getElementById('media_data_leakage');
//        }
//        var StackedChart1 = new Chart(ctx2, {
//            type: 'bar',
//            data: data,
//            options: options
//        });

//        ctx2.onclick = function (evt) {

//            var activePoint = StackedChart1.getElementAtEvent(evt);
//            if (activePoint[0]) {
//                var labels = data.labels[activePoint[0]._index];
//                var datasetIndex = activePoint[0]._datasetIndex;
//                $('#dataLeakageDetailsPopup').modal('show');
//                alert(labels);
//                //ShowCsatAuditData(labels);
//                datasetIndex = null;
//            }
//        };
//    }
//}
//function single_barchart_new_clickable(id, data, color) {
function single_barchart_new_clickable(id, data,chartType) {
    try {
        //alert(id);
        var strtxt = "";
        var ctx = document.getElementById(id);
        var data1 = JSON.parse(data);
        
        var labels = data1.map(function (e) {
            return e.Label;
        });
        V1 = data1.map(function (e) {
            return e.Value;
        });
    }
    catch (err) {
    }
    data = {
        labels: labels,
        datasets: [{
            label: 'Media',
            data: V1,
            backgroundColor: [
                '#003399',
                '#C00000',
                '#FFCC00',
                '#009900',
                '#FF9900',
                '#A055BC',
                '#a02c5a',
                '#2cc2c2'
            ],
            borderColor: [
                '#003399',
                '#C00000',
                '#FFCC00',
                '#009900',
                '#FF9900',
                '#A055BC',
                '#a02c5a',
                '#2cc2c2'

            ],
            borderWidth: 1
        }
        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: true,
            position: "top",
            text: strtxt,
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: false,
            position: "bottom"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.3,
                ticks: {
                    autoSkip: false
                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }

    };
    var media_data_leakage = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: options
    });
    //alert(ctx);
    ctx.onclick = function (evt) {
        //alert(11);
        var activePoint = media_data_leakage.getElementAtEvent(evt);
        if (activePoint[0]) {
            var labels = data.labels[activePoint[0]._index];
            var datasetIndex = activePoint[0]._datasetIndex;
            ShowDataLeakageDetails(chartType, labels);
            datasetIndex = null;
        }
    };
}
function single_barchart11(id, label2, data2) {

    var data1 = data2;
    var label1 = label2;
    var ctx = document.getElementById(id);
    var data = {
        // labels: [label],
        labels: label1,
        datasets: [{
            label: 'Media',
            //data: [data],
            data: data1,
            data: data1,
            backgroundColor: [
                '#003399',
                '#C00000',
                '#FFCC00',
                '#009900',
                '#FF9900',
                '#A055BC',
                '#a02c5a',
                '#2cc2c2'
                //'#2685CB',
                //'#4AD95A',
                //'#F49369',
                //'#FEC81B',
                //'#FD8D14',
                //'#7DB8FF',
                //'#6ADC88',
                //'#FEE45F'


            ],
            borderColor: [
                '#003399',
                '#C00000',
                '#FFCC00',
                '#009900',
                '#FF9900',
                '#A055BC',
                '#a02c5a',
                '#2cc2c2'
                //'#2685CB',
                //'#4AD95A',
                //'#F49369',
                //'#FEC81B',
                //'#FD8D14',
                //'#7DB8FF',
                //'#6ADC88',
                //'#FEE45F'
            ],
            borderWidth: 1
        }
        ]
    };


    var options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: true,
            position: "top",
            text: 'Media Wise Data Leakage',
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: false,
            position: "bottom"
        }

    };

    var media_data_leakage = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: options
    });
}
function single_barchart_new(id, data, color) {
    //debugger;
    try {
        var strtxt = "";
        //if (id == "media_data_leakage")
        //    strtxt = 'Media Wise Data Leakage';
        var ctx = document.getElementById(id);
        var data1 = JSON.parse(data);
        var labels = data1.map(function (e) {
            return e.Label;
        });
        data = data1.map(function (e) {
            return e.Value;
        });
    }
    catch (err) {
        //throw (err);
    }
    data1 = {
        labels: labels,
        datasets: [{
            label: 'Media',
            data: data,
            backgroundColor: color,
            borderColor: color,
            borderWidth: 1
        }
        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: true,
            position: "top",
            // text: 'Media Wise Data Leakage',
            text: strtxt,
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: false,
            position: "bottom"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.3,
                // Change here
                //  barPercentage: 0.7,

                ticks: {
                    autoSkip: false
                    //fontSize: 10,

                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }

    };
    var media_data_leakage = new Chart(ctx, {
        type: 'bar',
        data: data1,
        options: options
    });
}
function single_barchart(id, data) {

    var strtxt = "";
    var ctx = document.getElementById(id);
    var data1 = JSON.parse(data);
    var labels = data1.map(function (e) {
        return e.Label;
    });
    data = data1.map(function (e) {
        return e.Value;
    });

    data1 = {
        labels: labels,
        datasets: [{
            label: 'Media',
            data: data,
            backgroundColor: [
                '#003399',
                '#C00000',
                '#FFCC00',
                '#009900',
                '#FF9900',
                '#A055BC',
                '#a02c5a',
                '#2cc2c2'
            ],
            borderColor: [
                '#003399',
                '#C00000',
                '#FFCC00',
                '#009900',
                '#FF9900',
                '#A055BC',
                '#a02c5a',
                '#2cc2c2'

            ],
            borderWidth: 1
        }
        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: true,
            position: "top",
            // text: 'Media Wise Data Leakage',
            text: strtxt,
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: false,
            position: "bottom"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.4,
                // Change here
                //  barPercentage: 0.7,

                ticks: {
                    autoSkip: false
                    //fontSize: 10,

                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }

    };
    var media_data_leakage = new Chart(ctx, {
        type: 'bar',
        data: data1,
        options: options
    });
}
function Line_bar_chart(id, data) {
    //debugger;
    try {
        var ctx = document.getElementById(id);

        var data1 = JSON.parse(data.systemutilizationdata);
        var labels = data1.map(function (e) {
            return e.hours;
        });
        var cpu = data1.map(function (e) {
            return e.cpu;
        });

        var ram = data1.map(function (e) {
            return e.ram;
        });
        var hdddisk = data1.map(function (e) {
            return e.hdddisk;
        });
        var alert = data1.map(function (e) {
            return e.alert;
        });
    }
    catch (err) {
        //throw (err);
    }
    data = {
        labels: labels,
        datasets: [
            {
                type: 'line',
                label: "CPU Utilization",
                data: cpu,
                fill: false,
                borderColor: '#FFC000',
                backgroundColor: '#FFC000',
                pointBorderColor: '#FFC000',
                pointBackgroundColor: '#FFC000',
                pointHoverBackgroundColor: '#FFC000',
                pointHoverBorderColor: '#FFC000'

            },
            {
                type: 'line',
                label: "RAM  Utilization",
                data: ram,
                fill: false,
                borderColor: '#4472C4',
                backgroundColor: '#4472C4',
                pointBorderColor: '#4472C4',
                pointBackgroundColor: '#4472C4',
                pointHoverBackgroundColor: '#4472C4',
                pointHoverBorderColor: '#4472C4'

            },
            {
                type: 'line',
                label: "DISK  Utilization",
                data: hdddisk,
                fill: false,
                borderColor: '#50c878',
                backgroundColor: '#50c878',
                pointBorderColor: '#50c878',
                pointBackgroundColor: '#50c878',
                pointHoverBackgroundColor: '#50c878',
                pointHoverBorderColor: '#50c878'

            }
            //{
            //    type: 'line',
            //    label: "Disk Utilization",
            //    data: [445, 375, 355, 285, 245, 176, 200],
            //    fill: false,
            //    borderColor: '#C00000',
            //    backgroundColor: '#C00000',
            //    pointBorderColor: '#C00000',
            //    pointBackgroundColor: '#C00000',
            //    pointHoverBackgroundColor: '#C00000',
            //    pointHoverBorderColor: '#C00000'

            //},
            //{
            //    type: 'bar',
            //    label: 'Alert',
            //    data: alert,
            //    fill: false,
            //    backgroundColor: '#ce3f3f',
            //    borderColor: '#ce3f3f',
            //    hoverBackgroundColor: '#ce3f3f',
            //    hoverBorderColor: '#ce3f3f'

            //}
        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: false,
            text: 'System Device Utilization',
            position: "top",
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: true,
            position: "bottom"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.2,

                ticks: {
                    autoSkip: false

                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        },
    };

    var system_device_utilization = new Chart(ctx, {
        type: 'line',
        data: data,
        options: options
    });
}
function Line_chart(id, data) {
    //debugger;
    try {
        var ctx = document.getElementById(id);

        var data1 = JSON.parse(data.systemutilizationdata_alert);
        var labels = data1.map(function (e) {
            return e.hours;
        });
        var cpu = data1.map(function (e) {
            return e.cpu;
        });

        var ram = data1.map(function (e) {
            return e.ram;
        });
        var hdddisk = data1.map(function (e) {
            return e.hdddisk;
        });
    }
    catch (err) {
        //throw (err);
    }
    data = {
        labels: labels,
        datasets: [
            {
                type: 'line',
                label: "CPU Alert",
                data: cpu,
                fill: false,
                borderColor: '#FFC000',
                backgroundColor: '#FFC000',
                pointBorderColor: '#FFC000',
                pointBackgroundColor: '#FFC000',
                pointHoverBackgroundColor: '#FFC000',
                pointHoverBorderColor: '#FFC000'

            },
            {
                type: 'line',
                label: "RAM  Alert",
                data: ram,
                fill: false,
                borderColor: '#4472C4',
                backgroundColor: '#4472C4',
                pointBorderColor: '#4472C4',
                pointBackgroundColor: '#4472C4',
                pointHoverBackgroundColor: '#4472C4',
                pointHoverBorderColor: '#4472C4'

            },
            {
                type: 'line',
                label: "DISK Alert",
                data: hdddisk,
                fill: false,
                borderColor: '#50c878',
                backgroundColor: '#50c878',
                pointBorderColor: '#50c878',
                pointBackgroundColor: '#50c878',
                pointHoverBackgroundColor: '#50c878',
                pointHoverBorderColor: '#50c878'

            },

        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: false,
            text: 'System Device Alert',
            position: "top",
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: true,
            position: "bottom"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.2,
                ticks: {
                    autoSkip: false

                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }
    };

    var systemutilizationdata_alert = new Chart(ctx, {
        type: 'line',
        data: data,
        options: options
    });
}
function group_barchart(id, data, defcolor, usecolor, lable1, label2) {
    //debugger;
    try {
        var ctx = document.getElementById(id);
        var data1 = JSON.parse(data);
        var labels = data1.map(function (e) {
            return e.policy_type;
        });
        var defpol = data1.map(function (e) {
            return e.defpolicy;
        });

        var usepol = data1.map(function (e) {
            return e.usrpolicy;
        });
    }
    catch (err) {
        //throw (err);
    }
    data = {
        labels: labels,
        datasets: [{
            label: lable1,
            data: defpol,
            //  backgroundColor: '#31698a',
            //borderColor: '#31698a',
            backgroundColor: defcolor,
            borderColor: defcolor,
            borderWidth: 1
        },
        {
            label: label2,
            data: usepol,
            //backgroundColor: '#6897BB',
            //borderColor: '#6897BB',
            backgroundColor: usecolor,
            borderColor: usecolor,
            borderWidth: 1
        }]
    };
    var options = {
        //maintainAspectRatio: false,
        responsive: true,
        title: {
            display: true,
            position: "top",
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: true,
            position: "top",
            labels: {
                padding: 25
            }
        },
        barValueSpacing: 2,
        scales: {
            xAxes: [{
                barPercentage: 0.9,
                // Change here
                //  barPercentage: 0.7,

                ticks: {
                    autoSkip: false
                    //fontSize: 10,

                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }

    };
    var csat_policy_deployed = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: options
    });
}
function NetworkLinechart(id, datas, lbl, lbl1) {
    // debugger;
    try {
        var ctx = document.getElementById(id);

        var data1 = JSON.parse(datas);
        var labels = data1.map(function (e) {
            return e.Label;
        });
        var values = data1.map(function (e) {
            return e.Value;
        });
    }
    catch (err) {
        //throw (err);
    }
    var data = {
        labels: labels,
        datasets: [
            {
                type: 'line',
                label: lbl,
                data: values,
                fill: false,
                borderColor: '#FFC000',
                backgroundColor: '#FFC000',
                pointBorderColor: '#FFC000',
                pointBackgroundColor: '#FFC000',
                pointHoverBackgroundColor: '#FFC000',
                pointHoverBorderColor: '#FFC000'

            }

        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        layout: {
            padding: {
                left: 0,
                right: 0,
                top: 10,
                bottom: 10
            }
        },
        title: {
            display: true,
            text: lbl1,
            position: "left",
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: true,
            position: "top"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.2,
                ticks: {
                    autoSkip: false
                    //fontSize: 12,

                }
            }],
            yAxes: [{
                ticks: {
                    scaleBeginAtZero: true,
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]



        }
    };
    var ChartContent = document.getElementById('main_packets_charts_div');
    ChartContent.innerHTML = '&nbsp;';
    if (id === "main_packets_charts") {
        $('#main_packets_charts_div').append('<canvas id="main_packets_charts" ><canvas>');
        //ctx = $("#system_network_traffic").get(0).getContext("2d");
        ctx = document.getElementById('main_packets_charts');
    }
    var chartype = '';
    if (data.datasets[0].data.length === '1') {
        chartype = 'bar';
    }
    else {
        chartype = 'line';
    }
    var send_packets = new Chart(ctx, {
        type: chartype,
        data: data,
        options: options

    });

}
function Line_Network_Traffic_chart(id, traffictype, data, sendpacketcolor, receivepacketcolor, labeltext, labelshow, labelsend, labelreceive) {
    try {
        var data1 = JSON.parse(data);
        if (labelshow === "hours") {
            var labels = data1.map(function (e) {
                return e.label;
            });
        }
        else {
            labels = data1.map(function (e) {
                return e.label;
            });

        }
        var sendpacket = '';
        var receivepacket = '';
        if (traffictype === "packet") {
            sendpacket = data1.map(function (e) {
                return e.sendpackets;
            });

            receivepacket = data1.map(function (e) {
                return e.receivedpackets;
            });
        }
        else {
            sendpacket = data1.map(function (e) {
                return e.sendpackets;
            });

            receivepacket = data1.map(function (e) {
                return e.receivedpackets;
            });


        }
    }
    catch (err) {
        //throw (err);
    }
    data = {
        labels: labels,
        datasets: [
            {
                type: 'line',
                label: labelsend,
                data: sendpacket,
                fill: false,
                borderColor: sendpacketcolor,
                backgroundColor: sendpacketcolor,
                pointBorderColor: sendpacketcolor,
                pointBackgroundColor: sendpacketcolor,
                pointHoverBackgroundColor: sendpacketcolor,
                pointHoverBorderColor: sendpacketcolor

            },
            {
                type: 'line',
                label: labelreceive,
                data: receivepacket,
                fill: false,
                borderColor: receivepacketcolor,
                backgroundColor: receivepacketcolor,
                pointBorderColor: receivepacketcolor,
                pointBackgroundColor: receivepacketcolor,
                pointHoverBackgroundColor: receivepacketcolor,
                pointHoverBorderColor: receivepacketcolor

            }
        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        layout: {
            padding: {
                top: 20,
                left: 0,
                bottom: 0,
                right: 0
            }
        },
        title: {
            display: true,
            text: labeltext,
            position: "left",
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: true,
            position: "top"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.2,
                ticks: {
                    autoSkip: false
                    //fontSize: 12,

                }
            }],
            yAxes: [{
                ticks: {
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }
    };
    var system_network_traffic;

    var ChartContent = document.getElementById('system_network_trafficcontent');
    ChartContent.innerHTML = '&nbsp;';
    if (id === "system_network_traffic") {
        $('#system_network_trafficcontent').append('<canvas id="system_network_traffic" ><canvas>');
        //ctx = $("#system_network_traffic").get(0).getContext("2d");
        ctx = document.getElementById('system_network_traffic');
    }
    else {
        $('#system_network_trafficcontent').append('<canvas id="system_network_traffic_size" ><canvas>');
        //ctx = $("#system_network_traffic_size").get(0).getContext("2d");
        ctx = document.getElementById('system_network_traffic_size');
    }
    //if (system_network_traffic !== null)
    //    system_network_traffic.destroy();

    var chartype = '';
    if (data.datasets[0].data.length === '1') {
        chartype = 'bar';
    }
    else {
        chartype = 'line';
    }

    system_network_traffic = new Chart(ctx, {
        type: chartype,
        data: data,
        options: options
    });



}
function Line_Network_Traffic_Statitic_chart(id, traffictype, data, sendpacketcolor, receivepacketcolor, labeltext) {
    try {
        var data1 = JSON.parse(data);
        var labels = data1.map(function (e) {
            return e.Date;
        });
        var sendpacket = '';
        var receivepacket = '';
        if (traffictype === "packet") {
            sendpacket = data1.map(function (e) {
                return e.send_packets;
            });

            receivepacket = data1.map(function (e) {
                return e.received_packets;
            });
        }
        else {
            sendpacket = data1.map(function (e) {
                return e.send_size;
            });

            receivepacket = data1.map(function (e) {
                return e.received_size;
            });


        }
    }
    catch (err) {
        //throw (err);
    }
    data = {
        labels: labels,
        datasets: [
            {
                type: 'line',
                label: "Send Packets",
                data: sendpacket,
                fill: false,
                borderColor: sendpacketcolor,
                backgroundColor: sendpacketcolor,
                pointBorderColor: sendpacketcolor,
                pointBackgroundColor: sendpacketcolor,
                pointHoverBackgroundColor: sendpacketcolor,
                pointHoverBorderColor: sendpacketcolor

            },
            {
                type: 'line',
                label: "Receive Packets",
                data: receivepacket,
                fill: false,
                borderColor: receivepacketcolor,
                backgroundColor: receivepacketcolor,
                pointBorderColor: receivepacketcolor,
                pointBackgroundColor: receivepacketcolor,
                pointHoverBackgroundColor: receivepacketcolor,
                pointHoverBorderColor: receivepacketcolor

            }
        ]
    };
    var options = {
        responsive: false,
        maintainAspectRatio: false,
        title: {
            display: true,
            text: labeltext,
            position: "left",
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: true,
            position: "top"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.2,
                ticks: {
                    autoSkip: false
                    //fontSize: 12,
                }
            }],
            yAxes: [{
                ticks: {
                    scaleBeginAtZero: true,
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }
    };
    //var ctx = document.getElementById('number_of_statistic_chart');
    if (traffictype === "packet") {
        ChartContent = document.getElementById('divpacketschart');
        if (ChartContent !== null) {
            ChartContent.innerHTML = '&nbsp;';
            $('#divpacketschart').append('<canvas id="number_of_statistic_chart" class="network_statisticschart1"><canvas>');
            //ctx = $("#number_of_statistic_chart").get(0).getContext("2d");
            ctx = document.getElementById('number_of_statistic_chart');
        }
    }
    else {

        ChartContent = document.getElementById('divpacketssizechart');
        if (ChartContent !== null) {
            ChartContent.innerHTML = '&nbsp;';
            $('#divpacketssizechart').append('<canvas id="size_of_statistic_chart" class="network_statisticschart1"><canvas>');
            //ctx = $("#size_of_statistic_chart").get(0).getContext("2d");
            ctx = document.getElementById('size_of_statistic_chart');
        }
    }

    var number_of_statistic_chart = new Chart(ctx, {
        type: 'bar',
        data: data,
        options: options
    });

}

function Line_Network_Traffic_Statitic_chart_2(id, packettype, data, sendpacketcolor, receivepacketcolor, labeltext) {
    try {
        var data1 = JSON.parse(data);
        var labels = data1.map(function (e) {
            return e.label;
        });
        var sendpacket = '';
        var receivepacket = '';
        if (packettype === "1") {
            sendpacket = data1.map(function (e) {
                return e.sendpackets;
            });

            receivepacket = data1.map(function (e) {
                return e.receivedpackets;
            });
        }
        else {
            sendpacket = data1.map(function (e) {
                return e.sendpackets;
            });

            receivepacket = data1.map(function (e) {
                return e.receivedpackets;
            });


        }
    }
    catch (err) {
        //throw (err);
    }
    data = {
        labels: labels,
        datasets: [
            {
                type: 'line',
                label: "Send Packets",
                data: sendpacket,
                fill: false,
                borderColor: sendpacketcolor,
                backgroundColor: sendpacketcolor,
                pointBorderColor: sendpacketcolor,
                pointBackgroundColor: sendpacketcolor,
                pointHoverBackgroundColor: sendpacketcolor,
                pointHoverBorderColor: sendpacketcolor

            },
            {
                type: 'line',
                label: "Receive Packets",
                data: receivepacket,
                fill: false,
                borderColor: receivepacketcolor,
                backgroundColor: receivepacketcolor,
                pointBorderColor: receivepacketcolor,
                pointBackgroundColor: receivepacketcolor,
                pointHoverBackgroundColor: receivepacketcolor,
                pointHoverBorderColor: receivepacketcolor

            }
        ]
    };
    var options = {
        responsive: true,
        maintainAspectRatio: false,
        title: {
            display: true,
            text: labeltext,
            position: "left",
            fontsize: 25,
            fontcolor: "#f2f2f2"
        },
        legend: {
            display: true,
            position: "top"
        },
        scales: {
            xAxes: [{
                categoryPercentage: 0.2,
                ticks: {
                    autoSkip: false

                    //fontSize: 12,
                }
            }],
            yAxes: [{
                ticks: {
                    scaleBeginAtZero: true,
                    beginAtZero: true,
                    userCallback: function (label, index, labels) {
                        // when the floored value is the same as the value we have a whole number
                        if (Math.floor(label) === label) {
                            return label;
                        }
                    }
                }
            }]
        }
    };
    //var ctx = document.getElementById('number_of_statistic_chart');
    if (packettype === "1") {
        ChartContent = document.getElementById('divpacketschart');
        if (ChartContent !== null) {
            ChartContent.innerHTML = '&nbsp;';
            $('#divpacketschart').append('<canvas id="number_of_statistic_chart" class="network_statisticschart1"><canvas>');
            //ctx = $("#number_of_statistic_chart").get(0).getContext("2d");
            ctx = document.getElementById('number_of_statistic_chart');
        }
    }
    else {

        ChartContent = document.getElementById('divpacketssizechart');
        if (ChartContent !== null) {
            ChartContent.innerHTML = '&nbsp;';
            $('#divpacketssizechart').append('<canvas id="size_of_statistic_chart" class="network_statisticschart1"><canvas>');
            //ctx = $("#size_of_statistic_chart").get(0).getContext("2d");
            ctx = document.getElementById('size_of_statistic_chart');
        }
    }
    var chartype = '';
    if (data.datasets[0].data.length === '1') {
        chartype = 'bar';
    }
    else {
        chartype = 'line';
    }

    var Line_Network_Traffic_Statitic_chart_2 = new Chart(ctx, {
        type: chartype,
        data: data,
        options: options
    });

}