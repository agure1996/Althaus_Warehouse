"use strict"; // Enable strict mode to catch common errors

/**
 * updateNavItems: Updates the navigation bar dynamically based on the user's role.
 * Displays different links depending on whether the user is authenticated and their role.
 *
 * @param {string|null} userRole - The role of the user (e.g., 'Manager', 'HR', 'Sales') or null if unauthenticated.
 */
const updateNavItems = (userRole) => {
    const navItems = document.getElementById('nav-items'); // Get the navbar items container

    // Always start by clearing the current content
    navItems.innerHTML = ''; // Clear existing items

    if (userRole) {
        // If the user has certain roles, show additional dropdowns
        if (['Manager', 'HR', 'Sales'].includes(userRole)) {
            // Employees Dropdown
            navItems.innerHTML += `
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="employeesDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Employees
                </a>
                <ul class="dropdown-menu" aria-labelledby="employeesDropdown">
                    <li><a class="dropdown-item" href="/Employees/Index">Get All Employees</a></li>
                    <li><a class="dropdown-item" href="/Employees/SearchEmployeeById">Get Employee by ID</a></li>
                    <li><a class="dropdown-item" href="/Employees/Create">Create Employee</a></li>
                </ul>
            </li>
            `;

            // Items Dropdown
            navItems.innerHTML += `
            <li class="nav-item dropdown">
                <a class="nav-link dropdown-toggle" href="#" id="itemsDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                    Items
                </a>
                <ul class="dropdown-menu" aria-labelledby="itemsDropdown">
                    <li><a class="dropdown-item" href="/Items/Index">Get All Items</a></li>
                    <li><a class="dropdown-item" href="/Items/SearchItemById">Get Item by ID</a></li>
                    <li><a class="dropdown-item" href="/Items/Create">Create Item</a></li>
                </ul>
            </li>
            `;
        }

        // Add a "Logout" link for authenticated users
        navItems.innerHTML += `
            <li class="nav-item"><a class="nav-link" href="#" id="logoutLink">Logout</a></li>
        `;
    } else {
        // Add a "Login" link for unauthenticated users
        navItems.innerHTML += `
            <li class="nav-item"><a class="nav-link" href="/Auth/Login">Login</a></li>
        `;
    }
};

// Function to parse JWT and extract user role
const parseJwt = (token) => {
    if (!token) {
        throw new Error("Token is required for parsing.");
    }
    const base64Url = token.split('.')[1]; // Get the payload from the token
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/'); // Replace URL-safe characters
    const decodedToken = JSON.parse(window.atob(base64)); // Decode and parse the payload
    return decodedToken; // Return the full decoded token
};

// Logout function
const logout = () => {
    localStorage.removeItem('token'); // Remove the token from local storage
    updateNavItems(null); // Update the navigation items to reflect the logged-out state
    alert("You have successfully logged out."); // Show a logout confirmation message
    window.location.href = '/'; // Redirect to the homepage after logging out
};

// Add event listeners to handle the click events
document.addEventListener('DOMContentLoaded', () => {
    const token = localStorage.getItem('token');
    let userRole = null;

    // If the token exists, extract the user role from the token
    if (token) {
        userRole = parseJwt(token)["Role"]; // Use parseJwt to get the role
    }

    updateNavItems(userRole); // Update the navbar with the correct role

    // Event delegation for click events on the navbar
    document.getElementById('nav-items').addEventListener('click', function (e) {
        const target = e.target.closest('.nav-link');
        if (target) {
            if (target.id === 'logoutLink') {
                e.preventDefault(); // Prevent default anchor behavior
                logout(); // Call logout function
            } else if (target.textContent === 'Login') {
                e.preventDefault(); // Prevent default anchor behavior
                window.location.href = "/Auth/Login"; // Redirect to login page
            }
        }
    });
});
