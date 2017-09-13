function ValidationMsg() {
    $("#MessageText").show(500).css("margin", "0 1px 20px 0").html("Enter your value according to the field!").delay(800).fadeOut(10000);
    $("#MessageText").css("color", "Red");
}
function AcknowledgeMsg() {
    $("#MessageText").show(500).css("margin", "0 1px 20px 0").html("Data not found!").delay(800).fadeOut(10000);
    $("#MessageText").css("color", "Red");
}
function OperationMsg(Mode) {
    
    if (Mode == "I")
    {
        $("#MessageText").show(500).css("margin", "0 1px 20px 0", "color", "grren").html("Saved Successfully!").delay(800).fadeOut(10000);
        $("#MessageText").css("color", "green");
    }
   else if (Mode == "U")
   {       
        $("#MessageText").show(500).css("margin", "0 1px 20px 0", "color", "grren").html("Updated Successfully!").delay(800).fadeOut(10000);
        $("#MessageText").css("color", "green");
    }
  else if (Mode == "No")
   {     
        $("#MessageText").show(500).css("margin", "0 1px 20px 0", "color", "grren").html("Not Saved!").delay(800).fadeOut(10000);
        $("#MessageText").css("color", "green");

    }
  else if (Mode == "D") {
      $("#MessageText").show(500).css("margin", "0 1px 20px 0", "color", "red").html("Deleted Successfully!").delay(800).fadeOut(10000);
      $("#MessageText").css("color", "green");
  }
  else if (Mode == "NoDel") {
      $("#MessageText").show(500).css("margin", "0 1px 20px 0", "color", "red").html("Not Deleted!").delay(800).fadeOut(10000);
      $("#MessageText").css("color", "green");
  }
}
function WarningMsg() {
    $("#MessageText").show(500).css("margin", "0 1px 20px 0").html("Duplicate Data!").delay(800).fadeOut(10000);
    $("#MessageText").css("color", "Red");
}
function ErrorFrmServerMsg(msgValue) {   
    $("#MessageText").show(500).css("margin", "0 1px 20px 0").html(msgValue).delay(800).fadeOut(10000);
    $("#MessageText").css("color", "Red");
}
function ErrorFrmClientMsg() {
    $("#MessageText").show(500).css("margin", "0 1px 20px 0").html("Error occured!").delay(800).fadeOut(10000);
    $("#MessageText").css("color", "Red");
}
function CompletedMsg() {
    $("#MessageText").show(500).css("margin", "0 1px 20px 0").html("Process Completed!").delay(800).fadeOut(10000);
    $("#MessageText").css("color", "Red");
}