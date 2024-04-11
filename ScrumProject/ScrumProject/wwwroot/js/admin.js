function showEditOverlay() {
    var editOverlay = document.getElementById("editOverlay");
    editOverlay.style.display = "block";
}

// Method to show New User overlay
function showNewUserOverlay() {
    var newUserOverlay = document.getElementById("newUserOverlay");
    newUserOverlay.style.display = "block";
}

// Method to hide overlay
function hideOverlay() {
    var editOverlay = document.getElementById("editOverlay");
    var newUserOverlay = document.getElementById("newUserOverlay");

    editOverlay.style.display = "none";
    newUserOverlay.style.display = "none";
}
