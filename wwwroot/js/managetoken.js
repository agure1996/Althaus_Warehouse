
document.addEventListener("DOMContentLoaded", function () {
    // Check for the token in local storage or any other method you use for authentication
    const token = localStorage.getItem('token'); // Replace with your actual token storage method
    const manageEmployeesBtn = document.getElementById("manageEmployeesBtn");
    const manageItemsBtn = document.getElementById("manageItemsBtn"); 

    // If token exists, enable the button and add an event listener
    if (token) {
        manageEmployeesBtn.disabled = false; // Enable the button
        manageEmployeesBtn.style.backgroundColor = ""; // Reset to original color
        manageEmployeesBtn.style.cursor = ""; // Reset cursor

        /* same for manageitemsbtn */

        manageItemsBtn.disabled = false; // Enable the button
        manageItemsBtn.style.backgroundColor = ""; // Reset to original color
        manageItemsBtn.style.cursor = ""; // Reset cursor

        manageItemsBtn.addEventListener("click", function () {
            window.location.href = "/Items/Index"; // Redirect to Employees page
        });
        manageEmployeesBtn.addEventListener("click", function () {
            window.location.href = "/Employees/Index"; // Redirect to Employees page
        });
    } else {
        manageEmployeesBtn.disabled = true; // Keep the button disabled
        manageEmployeesBtn.style.backgroundColor = "gray"; // Change button color to gray
        manageEmployeesBtn.style.cursor = "not-allowed"; // Change cursor to not-allowed

        /* same for manageitemsbtn */
        manageItemsBtn.disabled = true; // Keep the button disabled
        manageItemsBtn.style.backgroundColor = "gray"; // Change button color to gray
        manageItemsBtn.style.cursor = "not-allowed"; // Change cursor to not-allowed
    }
});

