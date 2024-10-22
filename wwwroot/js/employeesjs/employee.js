// Function to fetch employee by ID
const fetchEmployeeById = async (employeeId) => {
    try {
        const response = await fetch(`/Employees/GetEmployeeById?id=${employeeId}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem('token')}` // Include the token if needed
            }
        });

        // Handle 404: Employee not found
        if (response.status === 404) {
            const errorData = await response.json(); // Ensure you parse the JSON response properly
            alert(`Employee with ID ${employeeId} does not exist.`); // Custom alert message
            return; // Stop further execution
        }

        if (!response.ok) {
            throw new Error("Network response was not ok");
        }

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
