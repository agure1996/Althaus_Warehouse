// Function to fetch item by ID
const fetchItemById = async (itemId) => {
    try {
        const response = await fetch(`/Items/GetItemById?id=${itemId}`, {
            method: "GET",
            headers: {
                "Authorization": `Bearer ${localStorage.getItem('token')}`
            }
        });

        if (response.status === 404) {
            alert(`Item with ID ${itemId} does not exist.`);
            return;
        }

        if (!response.ok) {
            const errorDetails = await response.text(); // Get error details
            throw new Error(`Error ${response.status}: ${errorDetails}`);
        }

        const itemData = await response.json();
        window.location.href = `/Items/Details/${itemData.id}`;

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
