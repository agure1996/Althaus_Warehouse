// auth.js
$(document).ready(function () {
    $('#loginForm').on('submit', function (event) {
        event.preventDefault(); // Prevent the default form submission

        // Gather form data
        const email = $('#email').val();
        const password = $('#password').val();

        // Make the API request to login
        $.ajax({
            type: 'POST',
            url: 'http://localhost:5168/api/v1/Auth/login', // Ensure this URL matches your API route
            contentType: 'application/json',
            data: JSON.stringify({ email: email, password: password }),
            success: function (response) {
                // Handle successful login
                console.log('Login successful!', response);
                // Store token and redirect as needed
            },
            error: function (xhr, status, error) {
                // Handle error
                console.error('Login failed:', xhr.responseText);
                $('#error-message').text('Login failed: ' + xhr.responseText).show();
            }
        });
    });
});
