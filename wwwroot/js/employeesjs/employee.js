

// Function to fetch employee by ID
const fetchEmployeeById = async (employeeId) => {

  

    const token = localStorage.getItem('token');
    if (!token) {

        alert("You must be logged in to perform this action.");
        document.getElementById('logoutButton').addEventListener('click', () => {
            logout();
        });
    }
    try {
        const response = await fetch(`/Employees/GetEmployeeById?id=${employeeId}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem('token')}` // Include the token if needed
            }
        });

        // Check for 404: Employee not found
        if (response.status === 404) {
            const errorData = await response.json(); // Ensure you parse the JSON response properly
            alert(`Employee with ID ${employeeId} does not exist.`); // Custom alert message
            return; // Stop further execution
        }

        // Check for 401 Unauthorized
        if (response.status === 401) {
            alert("You are not authorized. Redirecting to the login page.");
            window.location.href = "/Home/Index"; // Redirect to the login page
            return;
        }

        // Check for 403 Forbidden
        if (response.status === 403) {
            alert("You do not have permission to access this resource.");
            return; // Handle forbidden access accordingly
        }

        // Ensure response is ok for all other status codes
        if (!response.ok) {
            throw new Error("Network response was not ok");
        }

        // Parse the employee data
        const employeeData = await response.json();

        // Redirect to the Details page of the employee
        window.location.href = `/Employees/Details/${employeeData.id}`;

    } catch (error) {
        console.error('Error fetching employee:', error);
        alert("An error occurred while fetching the employee. Please try again later.");
    }
};


// Handle form submission
document.getElementById('employeeForm').addEventListener('submit', async function (e) {
    e.preventDefault(); // Prevent the default form submission


    const employeeId = document.getElementById('employeeId').value;

    if (!employeeId || employeeId <= 0) {
        alert("Please enter a valid employee ID.");
        return; // Stop if no employee ID is provided
    }

    // Fetch employee by ID
    await fetchEmployeeById(employeeId);
});
