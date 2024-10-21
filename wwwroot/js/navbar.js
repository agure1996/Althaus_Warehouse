"use strict"; // Enable strict mode for better error-checking and safer JavaScript

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

    // Always display "Home" and "Test" links, regardless of user role
    navItems.innerHTML += `
        <li class="nav-item"><a class="nav-link" href="/Home">Home</a></li>
        <li class="nav-item"><a class="nav-link" href="/Test">Test</a></li>
    `;

    // If the user is authenticated (i.e., userRole is not null), show role-specific items
    if (userRole) {
        // Show links for "Items" and "Employees" if the user has certain roles (Manager, HR, Sales)
        if (['Manager'].includes(userRole)) {
            navItems.innerHTML += `
                <li class="nav-item"><a class="nav-link" href="/Items/Index">Items</a></li>
                <li class="nav-item"><a class="nav-link" href="/Employees/Index">Employees</a></li>
            `;
        }
        // Add a "Logout" link for authenticated users
        navItems.innerHTML += `
            <li class="nav-item"><a class="nav-link" href="/Auth/Logout">Logout</a></li>
        `;
    } else {
        // Add a "Login" link for unauthenticated users
        navItems.innerHTML += `
            <li class="nav-item"><a class="nav-link" href="/Auth/login">Login</a></li>
        `;
    }
};

/**
 * parseJwt: Decodes a JWT (JSON Web Token) and extracts the 'Role' claim.
 * Used to determine the role of the authenticated user from the token.
 *
 * @param {string} token - The JWT token.
 * @returns {string} - The user role extracted from the token's 'Role' claim.
 */
const parseJwt = (token) => {
    // Split the token to get the payload (second part of JWT)
    const base64Url = token.split('.')[1];

    // Decode the base64-encoded payload
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');

    // Parse the JSON payload to extract claims
    const decodedToken = JSON.parse(window.atob(base64));

    // Return the 'Role' claim from the decoded token
    return decodedToken["Role"];
};

document.addEventListener('DOMContentLoaded', () => {
    // DOMContentLoaded ensures the script runs after the page content is fully loaded

    // Retrieve the token from localStorage
    const token = localStorage.getItem('token');
    let userRole = null; // Default role is null (unauthenticated)

    // If the token exists, parse it to extract the user role
    if (token) {
        userRole = parseJwt(token);
    }

    // Update the navigation bar based on the user's role (or lack of one)
    updateNavItems(userRole);

    // Event listener for all clicks within the navbar, using event delegation
    document.getElementById('nav-items').addEventListener('click', function (e) {
        const target = e.target.closest('.nav-link'); // Identify the closest clicked link element
        if (target) {
            // If the "Logout" link is clicked
            if (target.textContent === 'Logout') {
                e.preventDefault(); // Prevent default link behavior (page navigation)
                logout(); // Call the logout function
            }
            // If the "Login" link is clicked
            else if (target.textContent === 'Login') {
                e.preventDefault(); // Prevent default link behavior
                window.location.href = "/Auth/login"; // Redirect to the login page
            }
        }
    });

    /**
     * logout: Handles user logout by clearing the token, updating the navbar, 
     * and redirecting the user to the home page.
     */
    const logout = () => {
        localStorage.removeItem('token'); // Remove the authentication token from localStorage
        updateNavItems(null); // Update the navbar for an unauthenticated user
        window.location.href = "/Home"; // Redirect to the home page
        window.location.reload(); // Reload the page to reflect the logged-out state
    };
});
