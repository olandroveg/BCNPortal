var bcNodeTable;

function initDataTable() {
   
    bcNodeTable = $("#bcNode-table").DataTable({
        responsive: true,
        select: true,
        fixedHeader: true,
        pagingType: "full_numbers",
        displayLength: 20,
        "dom": '<"top col-md-12 without-padding"f><"top col-md-5 without-padding"i><"top col-md-7 without-padding"p>rt<"bottom col-md-5 without-padding"i><"bottom col-md-7 without-padding"p><"clear">',
        columnDefs: [
            {
                "className": "text-center custom-middle-align subCateg-name pointer-cursor",
                "targets": [0,1,2]
            }
        ],
        language:
        {
            "processing": "<div class='overlay custom-loader-background'><i class='fa fa-spinner fa-spin custom-loader-color'></i></div>",
            "search": "<span>Find</span>",
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
            "url": "/Admin/BcNode/LoadBcNodes",
            "type": "POST",
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
            
            
            {
                "data": "Name",
                "orderable": true,
                "width": "22%",
                "className": "custom-middle-align read-data",
                "render": function (data, type, full, meta) {
                    return `<a href='/Admin/Bcnode/Edit?bcnodeId=${full.Id}'>${data}</a>`;
                }
            },
            { "data": "Description", "orderable": false, "width": "15%", "className": "custom-middle-align read-data" },
           
        ],
        drawCallback: function (settings, json) {
            initChecks();
        }
    });
    $("#bcNode-table").dataTable().fnFilterOnReturn();
    
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
            bcNodeTable.draw(true);
            LoadNotifications("True", false, false);
            $("#all").iCheck("uncheck");
        });
    else
        showNotify("warning", undefined, "Info", "Select at least one item.");
}

function deleteSelection() {
    var selection = [];
    $("#tablebody input.icheckbox:checked").each(function (i, e) {
        selection.push($(this).attr("id"));
    });
    if (selection.length > 0)
        $.ajax({
            type: "POST",
            url: "/Admin/Bcnode/DeleteRange",
            data: { bcnodes: selection }
        }).done(function (result) {
            bcNodeTable.draw(true);
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

function LoadNotifications(succRemovebcnode, succUpdatebcnode, succAddbcnode) {
    if (succRemovebcnode === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "Selected bcNode has been deleted."
        });
    }
    else if (succUpdatebcnode === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "The bcNode has been updated."
        });
    }
    else if (succAddbcnode === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "A new bcNode has been created."
        });
    }
}


