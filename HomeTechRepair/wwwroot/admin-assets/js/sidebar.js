function ClearAll() {
    document.getElementById("li-dashboard").classList.remove('active');
    document.getElementById("li-appoinment").classList.remove('active');
    document.getElementById("li-patient").classList.remove('active');
    document.getElementById("li-transaction").classList.remove('active');
    document.getElementById("li-newEmployee").classList.remove('active');
    document.getElementById("li-doctor").classList.remove('active');
}

function Highlight(name) {
    var li = "li-".concat(name);
    document.getElementById(li).classList.add('active');
}