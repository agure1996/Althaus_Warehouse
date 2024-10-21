//logout
    localStorage.removeItem('token'); // Clear token from local storage
    window.location.href = '/Home'; // Redirect to login
