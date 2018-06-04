function increaseItem() {


    var x = document.getElementById('<%=txtQuantity.ClientID>%').value;
    alert(""+x);
    
}
function decreaseItem() {
    document.getElementById("txtQuantity").innerText = "New";
    return false;
}