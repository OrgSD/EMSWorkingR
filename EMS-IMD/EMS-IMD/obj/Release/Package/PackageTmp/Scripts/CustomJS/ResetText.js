
// Reset Data
function ResetData() {
 
    $('input[type="hidden"]').val("");
    $('input[type="text"]').val("");   
    $("textarea").val("");
    $("select")[0].selectedIndex = 1;
    $('input[type="checkbox"]:checked').prop('checked', false);

}

function RemoveTxtChk() {
    $('.chk').remove();
    $('.txt').remove();

}
//For removing operational & required message after triggering other event
function ClearBorderRequiredMsg() {
    $(".RequiredField").css("border", "1px Solid black"); //Clear Red Color     
    $("#MessageText").html(""); //Clear operation Message  
}


