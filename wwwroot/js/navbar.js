document.addEventListener('DOMContentLoaded', () => {
    function parseJwt(token) {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        return JSON.parse(window.atob(base64));
    }

    const token = localStorage.getItem('token');
    const navItems = document.getElementById('nav-items');

    // Check if token exists and determine user role
    if (token) {
        const decodedToken = parseJwt(token);
        const userRole = decodedToken.role;

        // Render navigation items based on role
        if (userRole === 'Manager' || userRole === 'HR' || userRole === 'Sales') {
            navItems.innerHTML += `
                <li class="nav-item">
                    <a class="nav-link" href="/Items/Index">Items</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="/Employees/Index">Employees</a>
                </li>
            `;
        }

        // Show a Logout button if the user is logged in
        navItems.innerHTML += `
            <li class="nav-item">
                <a class="nav-link" href="#" onclick="logout();">Logout</a>
            </li>
        `;
    } else {
        // Show a Login link if the user is not logged in
        navItems.innerHTML += `
            <li class="nav-item">
                <a class="nav-link" href="/Login/Index">Login</a>
            </li>
        `;

    }

    function logout() {
        localStorage.removeItem('token'); // Clear token from local storage
        window.location.href = '/Auth/Login'; // Redirect to login
    }
});
