//-----------------------------------------------------
//start Add, Edit, Delete - Success Funtion
// Add Category Success Function
function AddCategorySuccess() {
    alert("AddCategorySuccess");
    if ($("#addCategoryMess").html() == "True") {

        //now we can close the dialog
        $('#addCategoryDialog').dialog('close');
        //twitter type notification
        $('#commonMessage').html("Category Added.");
        $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);
        //Refresh Kendo Grid
        KendoGridRefresh();
    }
    else {
        //show message in popup
        $("#addCategoryMess").show();
    }
}

// Edit Category Success Function
function EditCategorySuccess() {
    if ($("#editCategoryMess").html() == "True") {

        //now we can close the dialog
        $('#editCategoryDialog').dialog('close');
        //twitter type notification
        $('#commonMessage').html("Category Updated.");
        $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);
        //Refresh Kendo Grid
        KendoGridRefresh();
    }
    else {
        //show message in popup
        $("#editCategoryMess").show();
    }
}

// Delete Category Success Function
function DeleteCategorySuccess() {
    if ($("#deleteCategoryMess").html() == "True") {

        //now we can close the dialog
        $('#deleteCategoryDialog').dialog('close');
        //twitter type notification
        $('#commonMessage').html("Task deleted");
        $('#commonMessage').delay(400).slideDown(400).delay(3000).slideUp(400);
        //Refresh Kendo Grid
        KendoGridRefresh();
    }
    else {
        //show message in popup
        $("#deleteCategoryMess").show();
    }
}
//end Add, Edit, Delete - Success Funtion
//-----------------------------------------------------

//-----------------------------------------------------
//start Add, Edit, Delete - Success Common Funtion
function AjaxSuccess(updateTargetId, dailogId, commonMessageId, commonMessage) {

    var _updateTargetId = "#" + updateTargetId;
    var _dailogID = "#" + dailogId;
    var _commonMessageId = "#" + commonMessageId;
    var _commonMessage = commonMessage;

    if ($(_updateTargetId).html() == "True") {

        //now we can close the dialog
        $(_dailogID).dialog('close');
        //twitter type notification
        $(_commonMessageId).html(_commonMessage);
        $(_commonMessageId).delay(400).slideDown(400).delay(3000).slideUp(400);
        //Refresh Kendo Grid
        KendoGridRefresh();
    }
    else {
        //show message in popup
        $(_updateTargetId).show();
    }
}
//end Add, Edit, Delete - Success Common Funtion
//-----------------------------------------------------

//-----------------------------------------------------
//start Refresh Kendo Grid Funtion
function KendoGridRefresh() {
    //Get Category Grid
    var catGrid = $('#categoryGrid').data('kendoGrid');
    catGrid.dataSource.read();
}
//end Refresh Kendo Grid Funtion
//-----------------------------------------------------

$(document).ready(function () {

    //-------------------------------------------------------
    //start Add, Edit, Delete - Dialog, Click Event

    $("#addCategoryDialog").dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        modal: true,
        buttons: {
            "Add": function () {
                //make sure there is nothing on the message before we continue 
                $("#addCategoryMess").html('');
                $("#addCategoryForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    //add Category
    $('.lnkAddCategory').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#addCategoryDialog');
        var viewUrl = linkObj.attr('href');

        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#addCategoryForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //edit Category
    $("#editCategoryDialog").dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        closeOnEscape: false,
        modal: true,
        close: function (event, ui) {
            $(".popover").hide();
        },
        buttons: {
            "Edit": function () {
                //make sure there is nothing on the message before we continue   
                $("#editCategoryMess").html('');
                $("#editCategoryForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }

    });

    $('#categoryGrid tbody tr td a.lnkEditCategory').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#editCategoryDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#editCategoryForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //delete Category
    $("#deleteCategoryDialog").dialog({
        autoOpen: false,
        width: 600,
        resizable: false,
        modal: true,
        buttons: {
            "Yes": function () {
                //make sure there is nothing on the message before we continue                         
                $("#deleteCategoryMess").html('');
                $("#deleteCategoryForm").submit();
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#categoryGrid tbody tr td a.lnkDeleteCategory').live('click', function () {

        //change the title of the dialog
        var linkObj = $(this);
        var dialogDiv = $('#deleteCategoryDialog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            //validation
            var $form = $("#deleteCategoryForm");
            // Unbind existing validation
            $form.unbind();
            $form.data("validator", null);
            // Check document for changes
            $.validator.unobtrusive.parse(document);
            // Re add validation with changes
            $form.validate($form.data("unobtrusiveValidation").options);
            //open dialog
            dialogDiv.dialog('open');
        });
        return false;

    });

    //For details Category
    $("#detailsCategoryDailog").dialog({
        autoOpen: false,
        width: 500,
        resizable: false,
        modal: true,
        buttons: {
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });

    $('#categoryGrid tbody tr td a.lnkDetailCategory').live('click', function () {

        var linkObj = $(this);
        var dialogDiv = $('#detailsCategoryDailog');
        var viewUrl = linkObj.attr('href');
        $.get(viewUrl, function (data) {
            dialogDiv.html(data);
            dialogDiv.dialog('open');
        });
        return false;

    });

    //end Add, Edit, Delete - Dialog, Click Event
    //-------------------------------------------------------

});