
////Get Product Details Grid
//var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

// this function is used to change event
function aitrate_change_event() {

}

// this function is used to spin event
function aitrate_spin_event() {

}

// this function is used to add master data
function vatrate_change_event() {

}

// this function is used to add master data
function vatrate_spin_event() {

}

// this function is used to remove details item to list grid
function ProductRemoveRow(id) {

    //Get Product Grid
    var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');
    //Get Product Details Grid by model id
    var dataItem = productDetailsGrid.dataSource.get(id);
    //Remove Product Item from Product Details Grid
    productDetailsGrid.dataSource.remove(dataItem);
}

// this function is used to add master data
function AddMaster(model) {

    //Get Model Data
    var masterModel = model;
}

// this function is used to reset details item to list grid
function ResetMaster() {
    $("#CategoryId").val("0");
}

// this function is used to add details item to list grid
function AddDetails(model) {

    console.log("Add Details");

    var subTotal = $('#GrossTotal').text().trim();
    var subTotalFloat = parseFloat(subTotal).toFixed(2);

    var priceFloat = parseFloat(model.Price).toFixed(2);

    console.log(subTotal);
    console.log(subTotalFloat);

    var subTotalFinal = priceFloat + subTotalFloat;

    console.log(subTotalFinal);

    $('#GrossTotal').text('');
    $('#GrossTotal').text(subTotalFinal);

    //Get Product Details Grid
    var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

    // Add Product Object to productDetailsGrid
    productDetailsGrid.dataSource.add(model);
}

// this function is used to reset details item to list grid
function ResetDetails() {

    $('#Name').val('');
    $('#Price').val('');

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

// this function is used to save master details
function MasterDetailsFormSave(postUrl, model, modelList) {

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

// this function is used to print master details
function MasterDetailsFormPrint() {

}

// this function is used to reset master details
function MasterDetailsFormReset() {

    // Reset master data
    ResetMaster();
    // Reset details data
    ResetDetails();

}

// this function is used to cancel this form
function MasterDetailsFormCancel() {

}

// this function is used to get details grid last added item
function GetDetailsGridLastAddedItemId() {

    //Get Product Details Grid
    var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

    //Get All Data from Product Details Grid
    var allData = productDetailsGrid.dataSource.data();
    var totalNumber = allData.length;

    var tempId = totalNumber + 1;

    return tempId;
}

function DetailsGridRefresh(parameters) {
    // this function is used to cancel this form
}

$(function () {

    //add Product
    $('#proAdd').click(function () {

        var tempId = GetDetailsGridLastAddedItemId();
        var name = $('#Name').val();
        var price = $('#Price').val();

        var actionLink = "<button id='productItem_" + tempId + "' onclick='ProductRemoveRow(" + tempId + ")' class='button'>Remove</button>";

        var productViewModel = {};
        productViewModel.TempId = tempId;
        productViewModel.Name = name;
        productViewModel.Price = price;

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
                    AddDetails(productViewModel);
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

        ResetDetails();
        return false;

    });

    //Form Save
    $('#proFormSave').click(function () {

        var postUrl = '/Product/MasterDetailsSave';

        //Master Model


        var category = {};


        //Details Model List
        var productList = [];

        //Get Product Grid
        var productDetailsGrid = $('#productDetailsGrid').data('kendoGrid');

        //Get All Data from Product Grid
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

        MasterDetailsFormPrint();
        return false;

    });

    //Form Reset
    $('#proFormReset').click(function () {

        MasterDetailsFormReset();
        return false;

    });

    //Form Cancel
    $('#proFormCancel').click(function () {

        MasterDetailsFormCancel();
        return false;

    });
});