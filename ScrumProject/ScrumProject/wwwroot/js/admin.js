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

userForm.addEventListener("submit", function (event) {
    //var actionUrl = '@Url.Action("adder","Admin/Admin")';
    event.preventDefault(); // Prevent default form submission
    if (!userForm.checkValidity()) {
        // Form is invalid, do not proceed with submission
        return;
    }
    // Serialize form data
    var formData = $("#userForm").serialize();

    // Send form data via AJAX
    $.ajax({
        type: "POST",
        url: 'admin/adder',
        
        data: formData,
        
        success: function (recData) { alert('Success'); },
        error: function (status, error) { alert(status + error)}
    });
});
