var contentTable;
function initDataTableContents(bcNodeId) {
    contentTable = $("#contents-table").DataTable({
        responsive: true,
        select: true,
        fixedHeader: true,
        pagingType: "full_numbers",
        displayLength: 20,
        "dom": '<"top col-md-12 without-padding"f><"top col-md-5 without-padding"i><"top col-md-7 without-padding"p>rt<"bottom col-md-5 without-padding"i><"bottom col-md-7 without-padding"p><"clear">',
        columnDefs: [
            {
                "className": "text-center custom-middle-align subCateg-name pointer-cursor",
                "targets": [0, 1, 2, 3, 4, 5]
            }
        ],
        language:
        {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-spinner fa-spin custom-loader-color'></i></div>",
            "search": "",
            "emptyTable": "<span class='text-danger1'>No data</span>",
            "info": "<span class='text-danger1'>Showing _START_ to _END_ of _TOTAL_ items</span>",
            "infoEmpty": "<span class='text-danger1'>Showing 0 items</span>",
            "lengthMenu": "<span class='text-danger1'>Show</span> _MENU_ <span class='text-danger1'>items</span>",
            "infoFiltered": "<span class='text-danger1'>(of _MAX_ totally)</span>",
            "loadingRecords": "Loading...",
            "zeroRecords": "<span class='text-danger1'>No result found</span>",
            "paginate": {
                "first": "First",
                "last": "Last",
                "next": "Next",
                "previous": "Previous"
            },
            "aria": {
                "sortAscending": ": Sort ascendently",
                "sortDescending": ": Sort descendently"
            }
        },
        processing: true,
        serverSide: true,
        order: [[1, "asc"]],
        ajax: {
            "url": "/Admin/BcNode/LoadBcNodeContents",
            "type": "POST",
            "data": { 'bcNodeId': bcNodeId },
            "dataType": "JSON"
        },
        columns: [
            {
                "data": "Id",
                "width": "3%",
                "searchable": false,
                "orderable": false,
                "render": function (data) {
                    return "<div class='radio'>" +
                        "<input type='checkbox' class='icheckbox body-table-check' id='" + data + "'>" +
                        "</div>";
                }
            },

            { "data": "Service", "orderable": false, "width": "15%", "className": "custom-middle-align read-data" },
            { "data": "SourceLocation", "orderable": false, "width": "15%", "className": "custom-middle-align read-data" },
            { "data": "Bitrate", "orderable": false, "width": "15%", "className": "custom-middle-align read-data" },
            { "data": "Size", "orderable": false, "width": "15%", "className": "custom-middle-align read-data" },
            {
                "data": "Id",
                "orderable": false,
                "width": "22%",
                "className": "custom-middle-align read-data",
                "render": function (data, type, full, meta) {
                    return `<a href='/Admin/Bcnode/EditBcNodeContent?bcnodeContentId=${full.Id}'>Edit</a>`;
                }
            },
            //{
            //    "data": "Id",
            //    "orderable": false,
            //    "width": "22%",
            //    "className": "custom-middle-align read-data",
            //    "render": function (data, type, full, meta) {
            //        return `<a href='/Admin/Bcnode/SendConfig?bcnodeContentId=${full.Id}'>Send Config</a>`;
            //    }
            //},

        ],
        drawCallback: function (settings, json) {
            initChecks();
        }
    });
    //$('#contents-table').dataTable().fnFilterOnReturn();
    

}


function getFilters() {
    //Get parameters
    const category = "category";
    const destiny = "plaza";
    const price = "5";
    return {
        Category: category,
        DeliveryZone: destiny,
        AveragePrice: price
    };
}
function deleteSelectionBcNodeContent() {
    var selection = [];
    $("#tablebody input.icheckbox:checked").each(function (i, e) {
        selection.push($(this).attr("id"));
    });
    if (selection.length > 0)
        $.ajax({
            type: "POST",
            url: "/Admin/Bcnode/DeleteRangeBcNodeContents",
            data: { bcnodeContentsIds: selection }
        }).done(function (result) {
            contentTable.draw(true);
            LoadNotifications("True", false, false);
            $("#all").iCheck("uncheck");
        });
    else
        showNotify("warning", undefined, "Info", "Select at least one item.");
}

//function getFilters() {
//    //Get parameters
//    const category = getSelectedValue("restaurant-categories");
//    const destiny = getSelectedValue("restaurant-deliveries");
//    const price = getSelectedValue("restaurant-price");
//    return {
//        Category: category,
//        DeliveryZone: destiny,
//        AveragePrice: price
//    };
//}
//function applyFilter() {
//    $("#expand-collapse").click();
//    restaurantTable.draw(true);
//}

function LoadNotificationsContents(succRemoveContent, succUpdateContent, succAddContent) {
    if (succRemoveContent === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "Selected content has been unplugged from this bcNode."
        });
    }
    else if (succUpdateContent === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "Selected content has been updated in this bcNode."
        });
    }
    else if (succAddContent === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "New content is plugged to this bcNode."
        });
    }
}
function LoadNotificationsApiResponse(response) {
    if (response == "Success") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "BcNode accepted successfully the configuration"
        });
    }
    else {

    }
    
    
}

function InitModeRadioInput() {
    const radioButtonList = document.querySelectorAll('input[name = "modes"]');
    for (const radiobtn of radioButtonList) {
        if (radiobtn.id == "mode:0")
            radiobtn.checked = "true";
    }
    
}
function CheckRadioBtn() {
    const radioButtonList = document.querySelectorAll('input[name = "modes"]');
    let id;
    for (const radiobtn of radioButtonList) {
        if (radiobtn.checked == true)
            id = radiobtn.id
        
    }
    return id;
}
function SendBcNodeConfig() {
    var id = CheckRadioBtn();
    var bcNodeId = $("#bcNodeIdId").val();
    let a = "";
    $.ajax({
        type: "POST",
        url: "/Admin/Bcnode/SendConfig",
        data: {
            bcNodeId: bcNodeId,
            mode: id
        }
    }).done(function (result) {
        LoadNotificationsApiResponse(result);
    });
    
}
function confirmSendConfig() {
    Lobibox.confirm({
        msg: `Send bcNode config?`,
        title: "Confirm",
        showButtons: true,
        callback: function ($this, type) {
            if (type === "yes") {
                SendBcNodeConfig();
                return true;
            }
            return false;
        }
    });
}