const { CLIENT_ID, CLIENT_SECRET } = require("./secret")
const axios = require('axios');

async function getAccessToken() {
	const tokenUrl = 'https://www.wixapis.com/oauth/access';
	const data = {
		grant_type: 'client_credentials',
		client_id: CLIENT_ID,
		client_secret: CLIENT_SECRET
	};

	try {
		const response = await axios.post(tokenUrl, data);
		const accessToken = response.data.access_token;
		console.log("Wix Access Token:", accessToken); // Print the access token

		return accessToken; // Return the access token for further use
	} catch (error) {
		console.error("Error getting access token:", error);
		return null;
	}
}

// Call the function and handle the result
(async () => {
	const accessToken = await getAccessToken();
	if (accessToken) {
		console.log("Access token retrieved successfully.");
	} else {
		console.error("Failed to obtain access token.");
	}
})();
