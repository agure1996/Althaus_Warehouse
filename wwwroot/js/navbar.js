// Make the updateNavItems function globally accessible
window.updateNavItems = function (userRole) {
    const navItems = document.getElementById('nav-items');
    navItems.innerHTML = ''; // Clear existing items

    // Always show Home and Test links
    navItems.innerHTML += `
        <li class="nav-item"><a class="nav-link" href="/Home">Home</a></li>
        <li class="nav-item"><a class="nav-link" href="/Test">Test</a></li>
    `;

    if (userRole) {
        // Show additional items based on role
        if (['Manager', 'HR', 'Sales'].includes(userRole)) {
            navItems.innerHTML += `
                <li class="nav-item"><a class="nav-link" href="/Items/Index">Items</a></li>
                <li class="nav-item"><a class="nav-link" href="/Employees/Index">Employees</a></li>
            `;
        }
        // Add Logout button
        navItems.innerHTML += `
            <li class="nav-item"><a class="nav-link" href="/Auth/Logout">Logout</a></li>
        `;
    } else {
        // Add Login button for unauthenticated users
        navItems.innerHTML += `
            <li class="nav-item"><a class="nav-link" href="/Auth/login">Login</a></li>
        `;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    // Function to parse JWT and extract user role
    function parseJwt(token) {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        return JSON.parse(window.atob(base64));
    }

    // Get the token and user role from localStorage on load
    const token = localStorage.getItem('token');
    const userRole = token ? parseJwt(token).role : null;
    updateNavItems(userRole); // Update the navbar on load

    // Event delegation for click events on the navbar
    document.getElementById('nav-items').addEventListener('click', function (e) {
        const target = e.target.closest('.nav-link');
        if (target) {
            if (target.textContent === 'Logout') {
                e.preventDefault(); // Prevent default anchor behavior
                logout(); // Call logout function
            } else if (target.textContent === 'Login') {
                e.preventDefault(); // Prevent default anchor behavior
                window.location.href = "/Auth/login"; // Redirect to login page
            }
        }
    });

    // Logout function
    function logout() {
        localStorage.removeItem('token'); // Clear token
        updateNavItems(null); // Update navbar for logged out user
        window.location.href = "/Home"; // Redirect to home after logout
    }
});
