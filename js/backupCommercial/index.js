const axios = require("axios");
require("dotenv").config();

const api_key = process.env.WIX_API_KEY;

const fetchProducts = async () => {
  try {
    const response = await axios.post(
      "https://www.wixapis.com/stores/v1/products/query",
      {
        query: {
          sort: '[{"price": "desc"}]',
        },
      },
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: api_key,
        },
      }
    );
    return response.data;
  } catch (error) {
    console.error(
      "Error fetching products:",
      error.response ? error.response.data : error.message
    );
  }
};

const exportData = async () => {
  const products = await fetchProducts();
  if (products) {
    console.log("Products: ", JSON.stringify(products, null, 2));
  }
};

exportData();
