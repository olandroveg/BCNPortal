function initTable() {
	var table = $("#usermanagertable").DataTable({
		responsive: true,
		select: false,
		fixedHeader: true,
		dom: '<"top col-md-4 toolbox-container"><"top col-md-4 without-padding"l><"top col-md-4 without-padding"f>rt<"bottom col-md-5 without-padding"i><"bottom col-md-7 without-padding"p><"clear">',
		columnDefs: [
			{
				"className": "text-center custom-middle-align subCateg-name pointer-cursor",
				"targets": [1]
			}
		],
		language:
		{
			"processing": "<div class='mycustom-loader-background'><div class=\"loader col-md-4\"></div> <div  class=\"col-md-8\"> <span style=\"margin-top:15px;font-style: italic;color:#3498db\" class=\"date-message\">Loading..</span> </div> </div>",
			"infoFiltered": "<span class='text-danger'>(de _MAX_ elementos en total)</span>"
		},
		processing: true,
		serverSide: true,
		ajax: {
			"url": "/UserManager/GetUserCollection",
			"type": "POST",
			"dataType": "JSON"
		},
		columns: [
			{
				"data": "Id",
				"width": "5%",
				"select": false,
				"searchable": false,
				"orderable": false,
				"render": function (data, type, full, meta) {
					return "<input type=\"checkbox\"  id=\"" + data + "\" value=\"\">";
				}
			},
			{ "data": "Name", "width": "20%", "className": "custom-middle-align subCateg-name read-data" },
			{ "data": "FullName", "width": "20%", "searchable": true, "className": "custom-middle-align subCateg-name", "style": "max-width:70%;overflow:hidden" },
			{ "data": "AsignedStorage", "width": "15%", "className": "custom-middle-align subCateg-name read-data" },
			{ "data": "RolString", "width": "15%", "className": "custom-middle-align subCateg-name read-data" },
			{
				"data": "Name",
				"width": "10%",
				"select": false,
				"searchable": false,
				"orderable": false,
				"render": function(data, type, full, meta) {
					return "<div><a href=\"/UserManager/Edit?userName=" +
						full.Name +
						"\"> <span style=\"color: green !important;font-style:italic\">Edit</span> </a> " +
						"| <a href=\"/UserManager/Delete?userName=" +
						full.Name +
						"\"> <span style=\"color: rgb(255, 171, 64) !important;font-style:italic\">Delete</span> </a> </div>";
				}
			}
		],
		createdRow: function (row, data, dataIndex) {
			if (data.Unread == true) {
				$(row).addClass("unread-email");
			}
		}
	});

	$("#datatables-details").dataTable().fnFilterOnReturn();
	initSelection(table);
}

function initSelection(table) {
	$("#tbodysample").on("click", "tr", function () {
		if ($(this).hasClass("selected")) {
			$(this).removeClass("selected");
		}
		else {
			table.$("tr.selected").removeClass("selected");
			$(this).addClass("selected");
		}
		//readEmail(table.row(".selected"));
	});

	$("#tbodysample").on("click", "td", function (data, type, full, meta) {
		if ($(this).hasClass("read-data") || $(this).hasClass("subCateg-name")) {
			$("#tablecontainer").addClass("hidden");
			$("#readercontainer").removeClass("hidden");
			$("#composeemail").addClass("hidden");
		}
	});

	$("#button").click(function () {
		table.row(".selected").remove().draw(false);
	});
}