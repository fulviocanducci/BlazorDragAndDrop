

function dragstart1(e, id, dotnetHelper) {

    e.dataTransfer.setData('text', id.toString());
    dotnetHelper.invokeMethodAsync('InvokeDragStartFromJS', id);

}


function drop1(e, el, id, dotnetHelper) {

    if (e.preventDefault) { e.preventDefault(); }
    if (e.stopPropagation) { e.stopPropagation(); }


    dotnetHelper.invokeMethodAsync('InvokeDropFromJS', id, e.dataTransfer.getData("text"));

    document.getElementById(el).removeEventListener('dragstart', null);

    return false;
}


window.JsFunctions = {
    Alert: function (msg) {
        alert(msg);
        return true;
    },
    handleDragStart: function (el, id, dotnetHelper) {
        if (document.getElementById(el) !== null) {

            document.getElementById(el).addEventListener('dragstart', function (e) { dragstart1(e, id, dotnetHelper); }, true);
            return true;
        }
        else {
            console.log("can't find element");
        }

        return false;
    },
    handleDrop: function (el, id, dotnetHelper) {
        if (document.getElementById(el) !== null) {
            document.getElementById(el).addEventListener('drop', function (e) { drop1(e, el, id, dotnetHelper); }, true);
            return true;
        }
        else {
            console.log("can't find element");
        }

        return false;
    }
};