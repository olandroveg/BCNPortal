var locationTable;

function initDataTable() {
   
    locationTable = $("#location-table").DataTable({
        responsive: true,
        select: true,
        fixedHeader: true,
        pagingType: "full_numbers",
        displayLength: 20,
        "dom": '<"top col-md-12 without-padding"f><"top col-md-5 without-padding"i><"top col-md-7 without-padding"p>rt<"bottom col-md-5 without-padding"i><"bottom col-md-7 without-padding"p><"clear">',
        columnDefs: [
            {
                "className": "text-center custom-middle-align subCateg-name pointer-cursor",
                "targets": [0,1,2,3]
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
            "url": "/Admin/Location/LoadLocation",
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
                    return `<a href='/Admin/Location/Edit?locationId=${full.Id}'>${data}</a>`;
                }
            },
            { "data": "State", "orderable": false, "width": "15%", "className": "custom-middle-align read-data" },
            { "data": "Country", "orderable": false, "width": "15%", "className": "custom-middle-align read-data" }
           
        ],
        drawCallback: function (settings, json) {
            initChecks();
        }
    });
    $("#location-table").dataTable().fnFilterOnReturn();
    
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
function deleteSelection() {
    var selection = [];
    $("#tablebody input.icheckbox:checked").each(function (i, e) {
        selection.push($(this).attr("id"));
    });
    if (selection.length > 0)
        $.ajax({
            type: "POST",
            url: "/Admin/Location/DeleteRange",
            data: { locations: selection }
        }).done(function (result) {
            locationTable.draw(true);
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

function LoadNotifications(succRemovelocation, succUpdatelocation, succAddlocation) {
    if (succRemovelocation === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "Selected locations has been deleted."
        });
    }
    else if (succUpdatelocation === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "The location has been updated."
        });
    }
    else if (succAddlocation === "True") {
        Lobibox.notify("info", {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: 10000,
            sound: false,
            title: "Info",
            msg: "A new location has been created."
        });
    }
}