"use strict"; // Enable strict mode to catch common errors

// Function to sanitize employee data
const sanitizeEmployeeData = (data) => {
    return {
        FirstName: data.FirstName.trim(),
        LastName: data.LastName.trim(),
        Email: data.Email.trim().toLowerCase(),  // Enforce lowercase emails
        Password: data.Password.trim(), // Add password sanitization
        EmployeeType: data.EmployeeType.trim(),
    };
};

// Function to fetch employee by ID
const fetchEmployeeById = async (employeeId) => {
    const token = localStorage.getItem('token');
    if (!token) {
        alert("You must be logged in to perform this action.");
        return;
    }

    try {
        const response = await fetch(`/Employees/GetEmployeeById?id=${employeeId}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            const errorDetails = await response.text();
            throw new Error(`Failed to fetch employee: ${response.statusText} - ${errorDetails}`);
        }

        const employeeData = await response.json();
        console.log("Fetched employee data:", employeeData);

        // Redirect to the employee details page
        window.location.href = `/Employees/Details/${employeeData.id}`;
    } catch (error) {
        console.error('Error fetching employee:', error);
        alert("An error occurred while fetching the employee. Please try again later.");
    }
};


// Handle form submission for searching an employee by ID
document.addEventListener("DOMContentLoaded", () => {
    const searchForm = document.getElementById('searchEmployeeForm');
    if (searchForm) {
        searchForm.addEventListener('submit', async function (e) {
            e.preventDefault(); // Prevent the default form submission

            const employeeId = document.getElementById('employeeId').value; // Get the employee ID from the search form

            if (!employeeId || employeeId <= 0) {
                alert("Please enter a valid employee ID.");
                return; // Stop if no valid employee ID is provided
            }

            // Fetch employee by ID
            await fetchEmployeeById(employeeId);
        });
    }
});


