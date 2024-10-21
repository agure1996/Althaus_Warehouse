"use strict"; // Enable strict mode

// Assign the async function to a constant
const handleLogin = async (formData) => {
    try {
        const response = await fetch("/api/v1/Auth/login", {
            method: "POST",
            body: JSON.stringify({
                Email: formData.get('Email'),
                Password: formData.get('Password')
            }),
            headers: {
                'Content-Type': 'application/json', // Set the content type to JSON
                'Accept': 'application/json'
            }
        });

        // Check if the response is okay
        if (!response.ok) {
            const errorResponse = await response.json();
            throw new Error(errorResponse.message || "Login failed."); // Handle non-200 responses
        }

        const data = await response.json();

        // Assuming the token is in the response data
        if (data.token) {
            localStorage.setItem('token', data.token); // Store the token
            // console.log('Token stored:', data.token); // Log the stored token
            const userRole = parseJwt(data.token).role; // Extract user role from token
            // console.log('User Role:', userRole); // Log the user role
            updateNavItems(userRole); // Update navbar after successful login
            window.location.href = "/Home"; // Redirect to home page
        } else {
            alert("Login failed. Please check your credentials."); // Alert on login failure
        }
    } catch (error) {
        console.error("Error during login:", error);
        alert("An error occurred during login. Please try again later."); // Generic error handling
    }
};

// Get the form element directly
const form = document.getElementById('loginForm');

// Event listener for form submission
if (form) { // Ensure the form exists before adding the event listener
    form.addEventListener("submit", async function (e) {
        e.preventDefault(); // Prevent default form submission

        // Create a FormData object
        const formData = new FormData(form);

        // Basic validation
        if (!formData.get('Email') || !formData.get('Password')) {
            alert("Email and password are required.");
            return;
        }

        // Call the handleLogin function with the formData
        await handleLogin(formData);
    });
}

// Function to parse JWT and extract user role
const parseJwt = (token) => {
    const base64Url = token.split('.')[1];
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    return JSON.parse(window.atob(base64));
};
