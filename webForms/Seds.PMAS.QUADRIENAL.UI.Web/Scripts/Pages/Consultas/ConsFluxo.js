$(document).ready(function() {
    var grid = $("#results"), firstButtonColumnIndex, buttonNames = {};
    grid.jqGrid({       
        url: '<%= ResolveClientUrl("~/Consultas/ConsFluxo.aspx/GetDados") %>',        
        // this is what jqGrid is looking for in json callback
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            cell: "cell",
            id: "0", //index of the column with the PK in it 
            userdata: "userdata",
            repeatitems: false
        },

        height: 500,
        colNames: ['ID', 'Município', 'Situação'],
        colModel: [
                { name: 'PRE_ID', hidden: true, width: 60, sortable: false },
                { name: 'MUN_DESC', width: 200, sortable: false },
                { name: 'SIT_DESC', width: 200, sortable: false }
            ],
        rowNum: 50,
        rowList: [20, 30, 50],
        viewrecords: true,        
        rownumbers: true,
        rownumWidth: 40,
        gridview: true,
        imgpath: '<%= ResolveClientUrl("~/Styles/custom-theme/images") %>',
        caption: "People",
        prmNames: {
            search: "isSearch",
            nd: null,
            rows: "numRows",
            page: "page",
            sort: "sortField",
            order: "sortOrder"
        }
    });
    $("#list").jqGrid('navGrid', '#pager', { edit: false, add: false, del: false });
});
function getData(pData) {
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: '<%= ResolveClientUrl("~/Consultas/ConsFluxo.aspx/GetDados") %>',
        data: JSON.stringify(pData),
        dataType: "json",
        success: function(data, textStatus) {
            if (textStatus == "success")
                ReceivedClientData(JSON.parse(getMain(data)).rows);
        },
        error: function(data, textStatus) {
            alert('An error has occured retrieving data!');
        }
    });
}
function ReceivedClientData(data) {
    var thegrid = $("#list");
    thegrid.clearGridData();
    for (var i = 0; i < data.length; i++)
        thegrid.addRowData(i + 1, data[i]);
}
function getMain(dObj) {
    if (dObj.hasOwnProperty('d'))
        return dObj.d;
    else
        return dObj;
}