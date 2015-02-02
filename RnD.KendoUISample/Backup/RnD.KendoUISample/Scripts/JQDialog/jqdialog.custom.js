
//-----------------------------------------------------
//start JQDialog Notification Funtion
function JQDialogNotify(message, durationTime, status) {

    // notify dialog
    $.jqDialog.notify(message, durationTime);

    // first remove class
    $("#jqDialog_box").removeClass();

    // notify dialog status class in jqDialog_box
    $("#jqDialog_box").addClass(status);

}
//end JQDialog Notification Funtion
//-----------------------------------------------------

//-----------------------------------------------------
//start JQDialog Alert Funtion
function JQDialogAlert(message, status, callBackOk) {

    // alert dialog
    $.jqDialog.alert(message, callBackOk);

    // first remove class
    $("#jqDialog_box").removeClass();

    // alert dialog status class in jqDialog_box
    $("#jqDialog_box").addClass(status);

}
//end JQDialog Alert Funtion
//-----------------------------------------------------

//-----------------------------------------------------
//start JQDialog Confirm Funtion
function JQDialogConfirm(message, status, callBackYes, callBackNo) {

    // confirm dialog
    $.jqDialog.confirm(message, callBackYes, callBackNo);

    // first remove class
    $("#jqDialog_box").removeClass();

    // confirm dialog status class in jqDialog_box
    $("#jqDialog_box").addClass(status);

}
//end JQDialog Confirm Funtion
//-----------------------------------------------------

//-----------------------------------------------------
//start JQDialog Prompt Funtion
function JQDialogPrompt(message, content, status, callBackOk, callBackCancel) {

    // prompt
    $.jqDialog.prompt(message, content, callBackOk, callBackCancel);

    // first remove class
    $("#jqDialog_box").removeClass();

    // alert dialog status class in jqDialog_box
    $("#jqDialog_box").addClass(status);

}
//end JQDialog Prompt Funtion
//-----------------------------------------------------

//-----------------------------------------------------
//start JQDialog CustomContent Funtion
function JQDialogCustomContent(content, status) {

    // custom content
    $.jqDialog.content(content);

    // first remove class
    $("#jqDialog_box").removeClass();

    // alert dialog status class in jqDialog_box
    $("#jqDialog_box").addClass(status);

}
//end JQDialog CustomContent Funtion
//-----------------------------------------------------
