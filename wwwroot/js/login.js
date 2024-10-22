"use strict"; // Enable strict mode to catch common errors

/**
 * handleLogin: Handles the login process, sends the form data to the server, 
 * and processes the response (token storage, role extraction, navbar update, and redirects).
 *
 * @param {FormData} formData - The form data containing user login details (Email and Password).
 * @returns {Promise<void>} - A promise that resolves when the login process completes.
 */
const handleLogin = async (formData) => {
    try {
        // Make an asynchronous POST request to the login API endpoint
        const response = await fetch("/api/v1/Auth/login", {
            method: "POST",
            body: JSON.stringify({
                Email: formData.get('Email'), // Retrieve email from formData
                Password: formData.get('Password') // Retrieve password from formData
            }),
            headers: {
                'Content-Type': 'application/json', // Specify that the body is in JSON format
                'Accept': 'application/json' // Expect a JSON response
            }
        });

        // If the response status is not OK (not 200), throw an error with the message from the server
        if (!response.ok) {
            const errorResponse = await response.json();
            throw new Error(errorResponse.message || "Login failed."); // Default message if server message is unavailable
        }

        // Extract the JSON response
        const data = await response.json();

        // Check if the server returned a token
        if (data.token) {
            localStorage.setItem('token', data.token); // Store the token in localStorage

            // Parse JWT token to extract the user's role
            const userRole = parseJwt(data.token).role;

            // Update the navigation items based on the user's role
            updateNavItems(userRole);

            // Reload the current page to reflect the logged-in state
            window.location.reload();

            // Redirect to the home page
            window.location.href = "/Home";
        } else {
            // Show an alert if no token was returned, indicating login failure
            alert("Login failed. Please check your credentials.");
        }
    } catch (error) {
        // Log any errors that occur during the login process to the console for debugging
        console.error("Error during login:", error);

        // Show a generic error message to the user
        alert(`Error during login: ${error.message}`);
    }
};

/**
 * Grabs the login form from the DOM and adds a 'submit' event listener to handle login logic.
 * Ensures the form exists and performs basic validation before submitting.
 */
const form = document.getElementById('loginForm'); // Select the login form by its ID

// Check if the form exists on the page
if (form) {
    form.addEventListener("submit", async function (e) {
        e.preventDefault(); // Prevent the default form submission behavior

        // Create a FormData object from the form element
        const formData = new FormData(form);

        // Basic validation: Check if both email and password fields are filled
        if (!formData.get('Email') || !formData.get('Password')) {
            alert("Email and password are required."); // Show an alert if validation fails
            return; // Exit the function if validation fails
        }

        // If validation passes, proceed with the login request
        await handleLogin(formData);
    });
}

/**
 * parseJwt function was removed from this file because it is now handled in the navbar.js
 * to avoid duplication of logic across multiple scripts.
 */
