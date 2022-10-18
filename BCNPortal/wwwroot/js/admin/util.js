var connection;
var noMessages = $(".noMessages");
var currentOrder = $("#currentOrder");
var currentUserName = null;
var currentUserFullName = null;
var currentUserImage = null;
var currentRestaurant = null;
var ordersInProcess = null;
var restaurantOwnName = null;
var restaurantOwnId = null;
var hubUrl = null;
var sdn = new Audio("/sounds/notification.ogg");

function initializeScroll(panelHeader, buttonFirst, buttonSecond) {
    $(document).ready(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() === 0) {
                changeToNormal(panelHeader, buttonFirst, buttonSecond);
            }
            else {
                changePosition(panelHeader, buttonFirst, buttonSecond);
            }
        });
    });
}

function initializeChecks() {
    $(document).ready(function() {
        $("#datatables-adv input").iCheck({
            checkboxClass: "icheckbox_flat-blue",
            radioClass: "iradio_flat-blue"
        });
    });
}

function initChecks() {
    $(document).ready(function () {
        $("input[type=checkbox].icheckbox").iCheck({
            checkboxClass: "icheckbox_flat-blue",
            radioClass: "iradio_flat-blue"
        });
        selectAll();
    });
}

function changePosition(container, buttonFirst, buttonSecond) {
    $(`#${container}`).addClass("sticky");
    $(`#${buttonFirst}`).css("background-color", "RGBA(255, 171, 64, 0.51)");
    if (buttonSecond != null) {
        $(`#${buttonSecond}`).css("background-color", "RGBA(232, 232, 232, 0.51)");
    }

    var actualWith = $(`#${container}`);
    if ($("#main-wrapper").hasClass("sidebar-mini")) {
        $(`#${container}`).css("width", "97%");
    } else {
        $(`#${container}`).css("width", "83%");
    }
    $("#main-content").addClass("top-60");
}

function changeToNormal(container, buttonFirst, buttonSecond) {
    $(`#${container}`).removeClass("sticky");
    $(`#${container}`).css("width", "100%");
    $(`#${buttonFirst}`).css("background-color", "rgb(255, 171, 64)");
    if (buttonSecond != null) {
        $(`#${buttonSecond}`).css("background-color", "rgb(255, 171, 64)");
    }
    $("#main-content").removeClass("top-60");
}

function InitializeScroll(panelHeader, buttonFirst, buttonSecond, buttonThird) {
    $(document).ready(function () {
        $(window).scroll(function () {
            if ($(this).scrollTop() === 0) {
                ChangeToNormal(panelHeader, buttonFirst, buttonSecond, buttonThird);
            }
            else {
                ChangePosition(panelHeader, buttonFirst, buttonSecond, buttonThird);
            }
        });
    });
}

function InitializeDateTimePicker() {
    $(document).ready(function () {
        $.datetimepicker.setLocale("es");
        $(".dateTimePicker").datetimepicker({
            format: "d/m/Y H:i",
            showSecond:false
        });
    });
}

function ChangePosition(container, buttonFirst, buttonSecond, buttonThird) {
    $(`#${container}`).addClass("sticky");
    $(`#${buttonFirst}`).css("background-color", "#ffab40");
    if (buttonSecond != null) {
        $(`#${buttonSecond}`).css("background-color", "#ffab40");
    }
    if (buttonThird != null) {
        $(`#${buttonThird}`).css("background-color", "#e84e40");
    }
    $(".header-panel-text").css("color", "#ffffff");
    const subText = $(".header-panel-subtext");
    if (subText != null)
        subText.css("color", "#ffffff");
    $("#principal-icon").css("color", "#ffffff");
    if ($("#main-wrapper").hasClass("sidebar-mini") === true) {
        $(`#${container}`).css("width", "96%");
    }
    else {
        $(`#${container}`).css("width", "82.5%");
    }
}

function ChangeToNormal(container, buttonFirst, buttonSecond, buttonThird) {
    $(`#${container}`).removeClass("sticky");
    $(`#${container}`).css("width", "100%");
    $(`#${buttonFirst}`).css("background-color", "#ffab40");
    if (buttonSecond !== null) {
        $(`#${buttonSecond}`).css("background-color", "#ffab40");
    }
    if (buttonThird !== null) {
        $(`#${buttonThird}`).css("background-color", "#e84e40");
    }
    $(".header-panel-text").css("color", "#1d212a");
    const subText = $(".header-panel-subtext");
    if (subText !== null)
        subText.css("color", "#1d212a");
    $("#principal-icon").css("color", "#1d212a");
}

function confirmAndDelete(atag, href, message) {
    Lobibox.confirm({
        msg: `Sure to delete ${message}?`,
        title: "Caution",
        showButtons: true,
        callback: function ($this, type) {
            if (type === "yes") {
                window.location = href;
                return true;
            }
            return false;
        }
    });
}

function confirmAndDeleteRedirect(functionName, message) {
    Lobibox.confirm({
        msg: `Sure to delete ${message}?`,
        title: "Caution",
        showButtons: true,
        callback: function ($this, type) {
            if (type === "yes") {
                window[functionName]();
                return true;
            }
            return false;
        }
    });
}

function selectAll() {
    $("#all").on("ifChanged", function (event) {
        const action = event.currentTarget.checked ? "check" : "uncheck";
        $("input[type=checkbox].icheckbox").iCheck(action);
    });
}

function getImageType(arrayBuffer) {
    var type = "";
    const dv = new DataView(arrayBuffer, 0, 5);
    const nume1 = dv.getUint8(0, true);
    const nume2 = dv.getUint8(1, true);
    const hex = nume1.toString(16) + nume2.toString(16);

    switch (hex) {
    case "ffd8":
        type = "image/jpeg";
        break;
    default:
        type = null;
        break;
    }
    return type;
}

function ValidateSizeType(event, sizeLimit) {
    var temppath = URL.createObjectURL(event.files[0]);
    var xhr = new XMLHttpRequest();
    xhr.path = temppath;
    xhr.open("GET", temppath, true);
    xhr.responseType = "arraybuffer";
    xhr.onload = function (e) {
        if (this.status === 200) {
            const imageType = getImageType(this.response);
            if (imageType === null) {
                Lobibox.notify("info", {
                    showClass: "fadeInDown",
                    hideClass: "fadeUpDown",
                    delay: 10000,
                    sound: false,
                    title: "Información",
                    msg: "Sólo se permiten imágenes de tipo jpg o jpeg."
                });
                event.value = "";
                $("#output")[0].placeholder = "Dirección imagen";
            }
            else if (this.response.byteLength >= sizeLimit) {
                Lobibox.notify("info", {
                    showClass: "fadeInDown",
                    hideClass: "fadeUpDown",
                    delay: 10000,
                    sound: false,
                    title: "Información",
                    msg: `Sólo se permiten imágenes de tamaño inferior a ${sizeLimit / 1000} KB.`
                });
                event.value = "";
                $("#output")[0].placeholder = "Dirección imagen";
            }
        } else {
            console.log(`Problem retrieving image ${JSON.stringify(e)}`);
        }
    }
    xhr.send();
}

function CountCharacters(event, count, show) {
    const datalenght = event.textLength;
    $(`#${show}`)[0].textContent = (datalenght) + " de " + count + " caracteres";
}

//funcion para la mascara del file upload
$(".fileupload-v1-btn").on("click", function () {
    var wrapper = $(this).parent("span").parent("div");
    var path = wrapper.find($(".fileupload-v1-path"));
    $(".fileupload-v1-file").click();
    $(".fileupload-v1-file").on("change", function () {
        path.attr("placeholder", $(this).val());
        console.log(wrapper);
        console.log(path);
    });
});
function expandCollapsed() {
    $(".panel-body").each(function (i, e) {
        if ($(e).hasClass("colap")) {
            $(e).removeClass("colap");
            $(e).css("display", "inline-block");
        } else {
            $(e).addClass("colap");
            $(e).css("display", "none");
        }
    });
}

function initializeSelect2() {
    $(".js-example-basic-single").select2({
        language: {
            noResults: function (params) {
                return "<span class='noresult-text'>No results found.</span>";
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    });
}

function intializeMultiSelect2() {
    $(".multi").select2({
        allowClear: true,
        placeholder: "",
        language: {
            noResults: function (params) {
                return "<span class='noresult-text'>No results found.</span>";
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    });

    $(".multiTags").select2({
        tags: true,
        tokenSeparators: [","],
        allowClear: true,
        placeholder: "",
        language: {
            noResults: function (params) {
                return "<span class='noresult-text'>No se encontraron resultados.</span>";
            }
        },
        escapeMarkup: function (markup) {
            return markup;
        }
    });
}

function initializeSlidePanel(element, content, isClass = false) {
    $(document).ready(function () {
        let selector = "#";
        if (isClass)
            selector = ".";
        $(selector + element).on("click", function () {
            $.slidePanel.show({
                content: content
            },
            {
                direction: "right",
                closeSelector: ".close-side"
            });
        });
    });
}

function ValidateNumber(event) {
    var character;
    switch (event.which) {
    case 96:
        character = 0;
        break;
    case 97:
        character = 1;
        break;
    case 98:
        character = 2;
        break;
    case 99:
        character = 3;
        break;
    case 100:
        character = 4;
        break;
    case 101:
        character = 5;
        break;
    case 102:
        character = 6;
        break;
    case 103:
        character = 7;
        break;
    case 104:
        character = 8;
        break;
    case 105:
        character = 9;
        break;
    default:
        character = String.fromCharCode(event.which);
        break;
    }
    var formatto = /[0-9]+$/;
    if (!formatto.test(character) && (event.which !== 46 && event.which !== 8 && event.which !== 9 && event.which !== 144 && event.which !== 39 && event.which !== 37
        && (95 < event.which < 106))) {
        event.preventDefault();
    }
}


function toggleSimplePanel(option, panel, width) {
    var layoutPanel = $(`#${panel}`);
    layoutPanel.css("width", width);
    switch (option) {
    case "open":
        layoutPanel.addClass("sidebar-right-open");
        break;

    case "close":
        layoutPanel.removeClass("sidebar-right-open");
        break;
    default:
    }
};

function resetForm(idForm, selectArray) {
    $(`#${idForm}`).find("input:text, input:password, input:file, select, textarea").each(function () {
        $(this).val("");
    });
    $(`#${idForm}`).find("input:radio, input:checkbox").each(function () {
        $(this).removeAttr("checked").removeAttr("selected");
    });
    if (selectArray.length > 0) {
        for (let i = 0; i < selectArray.length; i++) {
            $(`#${selectArray[i]}`).val(null).trigger("change");
        }
    }
}

function formatState(state) {
    if (!state.id && state.disable === true) { return state.text; }
    var $state = $('<span style ="font-size: 14pt; font-family: Lucida Sans Unicode Regular; color: #000000">' + state.text + "</span>");
    return $state;
}

function setTabOrder(order) {
    $("#optionTab")[0].value = order;
    InitTab(order);
}

function validateNumber(event) {
    var character;
    switch (event.which) {
    case 96:
        character = 0;
        break;
    case 97:
        character = 1;
        break;
    case 98:
        character = 2;
        break;
    case 99:
        character = 3;
        break;
    case 100:
        character = 4;
        break;
    case 101:
        character = 5;
        break;
    case 102:
        character = 6;
        break;
    case 103:
        character = 7;
        break;
    case 104:
        character = 8;
        break;
    case 105:
        character = 9;
        break;
    case 188:
        character = 0;
        break;
    default:
        character = String.fromCharCode(event.which);
        break;
    }
    var formatto = /^[-+]?(?:[0-9]+,)*[0-9]+(?:\.[0-9]+)?$/;
    if (!formatto.test(character) && (event.which !== 46 && event.which !== 8 && event.which !== 9 && event.which !== 144 && event.which !== 39 && event.which !== 37
        && event.which !== 190 && event.which !== 110 && (95 < event.which < 106))) {
        event.preventDefault();
    }
}

function InitTab(optionValue) {
    switch (optionValue) {
        case 1:
            {
                $("#home3").addClass("active");
                $("#home").parent().addClass("active");
                $("#home").prop("aria-expanded", true);
                $("#profile3").removeClass("active");
                $("#tab2").parent().removeClass("active");
                $("#tab2").prop("aria-expanded", false);
                break;
            }
        case 2:
            {
                $("#home3").removeClass("active");
                $("#home").parent().removeClass("active");
                $("#home").prop("aria-expanded", false);
                $("#profile3").addClass("active");
                $("#tab2").parent().addClass("active");
                $("#tab2").prop("aria-expanded", true);
                break;
            }
        //case "3":
        //    {
        //        $("#home3").removeClass("active");
        //        $("#home").parent().removeClass("active");
        //        $("#home").prop("aria-expanded", false);
        //        $("#profile4").addClass("active");
        //        $("#tab3").parent().addClass("active");
        //        $("#tab3").prop("aria-expanded", true);
        //        break;
        //    }
        //case "4":
        //    {
        //        $("#home3").removeClass("active");
        //        $("#home").parent().removeClass("active");
        //        $("#home").prop("aria-expanded", false);
        //        $("#profile5").addClass("active");
        //        $("#tab4").parent().addClass("active");
        //        $("#tab4").prop("aria-expanded", true);
        //        break;
        //    }
        //case "5":
        //    {
        //        $("#home3").removeClass("active");
        //        $("#home").parent().removeClass("active");
        //        $("#home").prop("aria-expanded", false);
        //        $("#profile6").addClass("active");
        //        $("#tab5").parent().addClass("active");
        //        $("#tab5").prop("aria-expanded", true);
        //        break;
        //    }
        //case "6":
        //    {
        //        $("#home3").removeClass("active");
        //        $("#home").parent().removeClass("active");
        //        $("#home").prop("aria-expanded", false);
        //        $("#profile7").addClass("active");
        //        $("#tab6").parent().addClass("active");
        //        $("#tab6").prop("aria-expanded", true);
        //        break;
        //    }
        //case "7":
        //    {
        //        $("#home3").removeClass("active");
        //        $("#home").parent().removeClass("active");
        //        $("#home").prop("aria-expanded", false);
        //        $("#profile8").addClass("active");
        //        $("#tab7").parent().addClass("active");
        //        $("#tab7").prop("aria-expanded", true);
        //        break;
        //    }
        default:
        {
            break;
        }
    }
}

function getSelectedValue(select2Element) {
    return $(`#${select2Element}`).find(":selected")[0].value;
}

function getMultiSelectedValues(select2Element) {
    return $(`#${select2Element}`).select2("data");
}

function setSelectedValue(select2Element, value) {
    $(`#${select2Element}`).val(value);
    $(`#${select2Element}`).trigger("change");
}

function cleanAllFields(container) {
    //Clean all inputs
    container.find(":input").each(function() {
        switch (this.type) {
            case "password":
            case "text":
            case "textarea":
            case "file":
            case "date":
            case "number":
            case "tel":
            case "email":
                $(this).val("");
                break;
        }
    });
    //Clean all selects and multi selects
    container.find(".js-example-basic-single").each(function() {
        $(this).val(null).trigger("change");
    });
    container.find(".multi").each(function () {
        $(this).val(null).trigger("change");
    });
}

function cleanValidationMessages(container) {
    container.find(".text-danger").each(function() {
        if (!$(this).hasClass("hidden"))
            $(this).addClass("hidden");
    });
}

function toogleFilter(filterBody) {
    const filter = $(`#${filterBody}`);
    if (filter.hasClass("open")) {
        filter.removeClass("open");
        filter.css("display", "none");
    } else {
        filter.addClass("open");
        filter.css("display", "block");
    }
}

function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    const ampm = hours >= 12 ? "PM" : "AM";
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? `0${minutes}` : minutes;
    const strTime = hours + ":" + minutes + " " + ampm;
    return strTime;
}

function formatDate(date) {
    var day = date.getDate();
    var month = date.getMonth() + 1;
    const year = date.getFullYear();
    day = day < 10 ? `0${day}` : day;
    month = month < 10 ? `0${month}` : month;
    const formattedDate = day + "/" + month + "/" + year;
    return formattedDate;
}

function createStars(ratingArray) {
    return `<i class="${ratingArray[0] === 1 ? "fa fa-star" : ratingArray[0] === -1 ? "fa fa-star-half-empty" : "fa fa-star-o"}" style="color:${ratingArray[0] === 1 || ratingArray[0] === -1 ? "goldenrod" : "gray"}"></i>
                            <i class="${ratingArray[1] === 1 ? "fa fa-star" : ratingArray[1] === -1 ? "fa fa-star-half-empty" : "fa fa-star-o"}" style="color:${ratingArray[1] === 1 || ratingArray[1] === -1 ? "goldenrod" : "gray"}"></i>
                            <i class="${ratingArray[2] === 1 ? "fa fa-star" : ratingArray[2] === -1 ? "fa fa-star-half-empty" : "fa fa-star-o"}" style="color:${ratingArray[2] === 1 || ratingArray[2] === -1 ? "goldenrod" : "gray"}"></i>
                            <i class="${ratingArray[3] === 1 ? "fa fa-star" : ratingArray[3] === -1 ? "fa fa-star-half-empty" : "fa fa-star-o"}" style="color:${ratingArray[3] === 1 || ratingArray[3] === -1 ? "goldenrod" : "gray"}"></i>
                            <i class="${ratingArray[4] === 1 ? "fa fa-star" : ratingArray[4] === -1 ? "fa fa-star-half-empty" : "fa fa-star-o"}" style="color:${ratingArray[4] === 1 || ratingArray[4] === -1 ? "goldenrod" : "gray"}"></i>`;
}

function showNotify(type, delay, title, msg) {
    Lobibox.notify(type,
        {
            showClass: "fadeInDown",
            hideClass: "fadeUpDown",
            delay: delay === undefined ? 10000 : delay,
            sound: false,
            title: title,
            msg: msg
        });
}

$("#allowSound").click(function () {
    playSound();
});

function playSound() {
    sdn.preload = "none";
    var playPromise = sdn.play();

    if (playPromise !== undefined) {
        playPromise.then(_ => {
                sdn.play();
            })
            .catch(error => {
                // Auto-play was prevented
                // Show paused UI.
            });
    }
}

function setGlobalVariables(userName, userFullName, userImage,restaurantId, restaurantName, hubServerUrl) {
    currentUserName = userName;
    currentUserFullName = userFullName;
    currentUserImage = userImage;
    restaurantOwnId = restaurantId;
    restaurantOwnName = restaurantName;
    hubUrl = hubServerUrl;
}

function initSignalr(ordersArray, restaurantId) {
    ordersInProcess = ordersArray;
    displayInProcessCount(ordersArray.length);
    currentRestaurant = restaurantId;
    const ordersId = ordersArray.map(function (v) { return v.id; });
    connection = new signalR.HubConnectionBuilder()
        .withUrl(`${hubUrl}/chatHub?ordersId=${JSON.stringify(ordersId)}&restaurantId=${restaurantId}`)
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.serverTimeoutInMilliseconds = 6000;
    connection.keepAliveIntervalInMilliseconds = 3000;

    connection.on("ReceiveMessage", (chatContactDto, message) => {
        const isSender = currentUserName === message.fromEmail;
        if (chatContactDto !== null && chatContactDto.orderId !== null && currentUserName !== null)
            updateUnreadMessagesCount(chatContactDto, isSender, true);
    });

    connection.on("ReceiveContact", (chatContactDto, inProcessCount) => {
        if (chatContactDto !== null && currentUserName !== null) {
            displayInProcessCount(inProcessCount);
            $("#allowSound").click();
        }
    });

    connection.start().catch(err => console.error(err.toString()));
}

function displayInProcessCount(inProcessCount) {
    $("#inProcessLink").html(`<span>${inProcessCount !== null ? inProcessCount : 0}</span><i class="fa fa-bell"></i>`);
}

function displayUnreadMessagesCount(unreadMessagesCount) {
    $("#messagesLink").html(`<span>${unreadMessagesCount !== null ? unreadMessagesCount : 0}</span><i class="fa fa-envelope"></i>`);
}

function updateUnreadMessagesCount(chatContactDto, isSender, increase) {
    const orderInput = $(`#${chatContactDto.orderId}`);
    let wrap = {};
    if (orderInput !== {})
        wrap = orderInput.prev();
    if (!isSender && (orderInput === {} || (increase && !wrap.parent().hasClass("active") ||
        (!increase && wrap.parent().hasClass("active"))))) {
        const messagesContainer = $("#messagesLink");
        const spanChild = messagesContainer.children().first();
        const text = spanChild.text();
        const unreadNumber = parseInt(text);
        if (increase) {
            spanChild.text(unreadNumber + 1);
            $("#allowSound").click();
        }
        else
        if (unreadNumber > 0)
            spanChild.text(unreadNumber - chatContactDto.unreadCant);
    }
}
