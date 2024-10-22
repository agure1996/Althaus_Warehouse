
    document.addEventListener("DOMContentLoaded", function () {
        // Check for the token in local storage or any other method you use for authentication
        const token = localStorage.getItem('token'); // Replace with your actual token storage method
    const manageEmployeesBtn = document.getElementById("manageEmployeesBtn");

    // If token exists, enable the button and add an event listener
    if (token) {
        manageEmployeesBtn.disabled = false; // Enable the button
    manageEmployeesBtn.style.backgroundColor = ""; // Reset to original color
    manageEmployeesBtn.style.cursor = ""; // Reset cursor
    manageEmployeesBtn.addEventListener("click", function () {
        window.location.href = "/Employees/Index"; // Redirect to Employees page
            });
        } else {
        manageEmployeesBtn.disabled = true; // Keep the button disabled
    manageEmployeesBtn.style.backgroundColor = "gray"; // Change button color to gray
    manageEmployeesBtn.style.cursor = "not-allowed"; // Change cursor to not-allowed
        }
    });
