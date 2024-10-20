document.addEventListener('DOMContentLoaded', () => {
    const form = document.querySelector("form");

    form.addEventListener("submit", async function (e) {
        e.preventDefault(); // Prevent default form submission

        const formData = new FormData(form);

        try {
            const response = await fetch("/api/v1/Auth/login", { // Update the URL to match your API
                method: "POST",
                body: formData,
                headers: {
                    'Accept': 'application/json'
                }
            });

            // Check if the response is okay (status in the range 200-299)
            if (response.ok) {
                const data = await response.json();

                if (data.token) {
                    // Store the token in local storage
                    localStorage.setItem('token', data.token);
                    // Redirect to home or dashboard
                    window.location.href = "/Home";
                } else {
                    // Handle invalid login
                    alert("Invalid username or password.");
                }
            } else {
                // Handle server errors (e.g., 500, 404, etc.)
                const errorData = await response.json();
                alert(errorData.message || "An error occurred while logging in.");
            }
        } catch (error) {
            // Handle network errors
            console.error("Error:", error);
            alert("An error occurred. Please try again later.");
        }
    });
});
