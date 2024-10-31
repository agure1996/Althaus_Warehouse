
// Function to fetch item by ID
const fetchItemById = async (itemId) => {
    const token = localStorage.getItem('token');
    if (!token) {
        alert("You must be logged in to perform this action.");
        document.getElementById('logoutButton').addEventListener('click', () => {
            logout();
        });
    }

    try {
        const response = await fetch(`/Items/GetItemById?id=${itemId}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${token}`, // Use token directly
                "Content-Type": "application/json"
            }
        });

        // Handle 404: Item not found
        if (response.status === 404) {
            alert(`Item with ID ${itemId} does not exist.`);
            return;
        }

        // Handle 401 Unauthorized
        if (response.status === 401) {
            alert("You are not authorized. Redirecting to the login page.");
            window.location.href = "/Home/Index"; // Redirect to the login page
            return;
        }

        // Handle 403 Forbidden
        if (response.status === 403) {
            alert("You do not have permission to access this item.");
            return; // Handle forbidden access accordingly
        }

        // Ensure response is ok for all other status codes
        if (!response.ok) {
            const errorDetails = await response.text(); // Get error details
            throw new Error(`Error ${response.status}: ${errorDetails}`);
        }

        // Parse the item data
        const itemData = await response.json();
        window.location.href = `/Items/Details/${itemData.id}`; // Redirect to item's details

    } catch (error) {
        console.error('Error fetching item:', error);
        alert("An error occurred while fetching the item. Please try again later.");
    }
};


// Handle form submission
document.getElementById('itemForm').addEventListener('submit', async function (e) {
    e.preventDefault(); // Prevent the default form submission

    const itemId = document.getElementById('itemId').value;

    if (!itemId || itemId <= 0) {
        alert("Please enter a valid item ID.");
        return; // Stop if no item ID is provided
    }

    // Fetch item by ID
    await fetchItemById(itemId);
});
