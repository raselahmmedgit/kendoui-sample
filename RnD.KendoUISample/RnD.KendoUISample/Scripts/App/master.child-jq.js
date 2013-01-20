
////Get Product Details Grid
//var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

// this function is used to remove item to list grid
function ProductRemoveRow(id) {

    //Get Product Grid
    var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');
    //Get Product Details Grid by model id
    var dataItem = productDetailsGrid.dataSource.get(id);
    //Remove Product Item from Product Details Grid
    productDetailsGrid.dataSource.remove(dataItem);
}

// this function is used to add item to list grid
function Add(model) {

    //Get Product Details Grid
    var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

    // Add Product Object to productDetailsGrid
    productDetailsGrid.dataSource.add(model);
}

// this function is used to reset item to list grid
function Reset() {
    $('#Name').val('');
    $('#Price').val('');
}

// this function is used to save this form
function FormSave(postUrl, model, modelList) {

    var paramValue = JSON.stringify({ model: model, modelList: modelList });

    $.ajax({
        url: postUrl,
        type: 'POST',
        dataType: 'json',
        data: paramValue,
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            //JQDialogAlert mass, status
            JQDialogAlert(data.msg, data.status);
        },
        error: function (objAjaxRequest, strError) {
            var respText = objAjaxRequest.responseText;
            //JQDialogAlert mass, status
            JQDialogAlert(respText, 'info');
        }

    });
}

// this function is used to print this form
function FormPrint() {

}

// this function is used to reset this form
function FormReset() {

    $("#CategoryId").val("0");
    Reset();

    //Get Product Grid
    var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

    //Get All Data from Category Grid
    var allData = productDetailsGrid.dataSource.data();
    var totalNumber = allData.length;

    for (var i = 1; i <= totalNumber; i++) {

        //Get Product Details Grid by model id
        var currentDataItem = productDetailsGrid.dataSource.get(i);

        //Remove Product Item from Product Details Grid
        productDetailsGrid.dataSource.remove(currentDataItem);

    }

}

// this function is used to cancel this form
function FormCancel() {

}

// this function is used to cancel this form
function GetGridLastAddedItemId() {
    //Get Product Details Grid
    var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

    //Get All Data from Product Details Grid
    var allData = productDetailsGrid.dataSource.data();
    var totalNumber = allData.length;

    var tempId = totalNumber + 1;

    return tempId;
}

function GridRefresh(parameters) {
    // this function is used to cancel this form
}

$(function () {

    //add Product
    $('#proAdd').click(function () {

        var tempId = GetGridLastAddedItemId();
        var name = $('#Name').val();
        var price = $('#Price').val();
        var categoryId = $("#CategoryId option:selected").val();
        var categoryName = $("#CategoryId option:selected").text();
        var actionLink = "<button id='productItem_" + tempId + "' onclick='ProductRemoveRow(" + tempId + ")' class='button'>Remove</button>";

        var productViewModel = {};
        productViewModel.TempId = tempId;
        productViewModel.Name = name;
        productViewModel.Price = price;
        productViewModel.CategoryId = categoryId;
        productViewModel.CategoryName = categoryName;
        productViewModel.ActionLink = actionLink;

        var paramValue = JSON.stringify({ productViewModel: productViewModel });

        $.ajax({
            url: '/Product/Add',
            type: 'POST',
            dataType: 'json',
            data: paramValue,
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                if (data == "True") {
                    //Add new product to grid
                    Add(productViewModel);
                } else {
                    //JQDialogAlert mass, status
                    JQDialogAlert(data.msg, data.status);
                }
            },
            error: function (objAjaxRequest, strError) {
                var respText = objAjaxRequest.responseText;
                //JQDialogAlert mass, status
                JQDialogAlert(respText, 'info');
            }

        });

        return false;

    });

    //reset Product
    $('#proReset').click(function () {

        Reset();
        return false;

    });

    //Form Save
    $('#proFormSave').click(function () {

        var postUrl = '/Product/MasterDetailsSave';

        //Master Model
        var categoryId = $("#CategoryId option:selected").val();
        var categoryName = $("#CategoryId option:selected").text();

        var category = {};
        category.CategoryId = categoryId;
        category.Name = categoryName;

        //Details Model List
        var productList = [];

        //Get Product Grid
        var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

        //Get All Data from Category Grid
        var allData = productDetailsGrid.dataSource.data();
        var totalNumber = allData.length;

        for (var i = 0; i < totalNumber; i++) {

            var currentDataItem = allData[i];

            var productTempId = currentDataItem.TempId;
            var productName = currentDataItem.Name;
            var productPrice = currentDataItem.Price;
            var productCategoryId = currentDataItem.CategoryId;

            var product = {};
            product.TempId = productTempId;
            product.Name = productName;
            product.Price = productPrice;
            product.CategoryId = productCategoryId;

            // Push Data to productList Array
            productList.push(product);
        }

        //FormSave(postUrl, category, productList);

        return false;

    });

    //Form Print
    $('#proFormPrint').click(function () {

        FormPrint();
        return false;

    });

    //Form Reset
    $('#proFormReset').click(function () {

        FormReset();
        return false;

    });

    //Form Cancel
    $('#proFormCancel').click(function () {

        FormCancel();
        return false;

    });
});