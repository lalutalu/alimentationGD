import os
import requests
from requests.auth import HTTPBasicAuth
import dotenv

dotenv.load_dotenv()

CLIENT_ID = os.environ.get("CLIENT_ID")
CLIENT_SECRET = os.environ.get("CLIENT_SECRET")

def get_access_token(client_id, client_secret):
    token_url = 'https://www.wix.com/oauth/access'

    headers = {
        'Content-Type': 'application/x-www-form-urlencoded'
    }

    data = {
        'grant_type': 'client_credentials',
        'client_id': client_id,
        'client_secret': client_secret
    }

    encoded_data = '&'.join([f"{key}={value}" for key, value in data.items()])

    response = requests.post(token_url, headers=headers, data=encoded_data)

    if response.status_code == 200:
        return response.json()['access_token']
    else:
        print(f"Error getting access token: {response.status_code}, {response.text}")
        return None

def fetch_products(access_token):
    endpoint = 'https://www.wixapis.com/stores/v1/products/query'

    headers = {
        'Authorization': f'Bearer {access_token}',
        'Content-Type': 'application/json'
    }

    payload = {
        "query": {
            "paging": {
                "limit": 50,
                "offset": 0
            }
        }
    }

    response = requests.post(endpoint, headers=headers, json=payload)

    if response.status_code == 200:
        return response.json().get('products', [])
    else:
        print(f"Error fetching products: {response.status_code}, {response.text}")
        return None

def main():
    access_token = get_access_token(CLIENT_ID, CLIENT_SECRET)

    if access_token:
        products = fetch_products(access_token)

        if products:
            for product in products:
                name = product.get('name', 'N/A')
                price_data = product.get('priceData', {})
                price = price_data.get('price', 'N/A')
                print(f"Name: {name}, Price: {price}")
        else:
            print("No products found.")
    else:
        print("Failed to obtain access token.")

if __name__ == "__main__":
    main()

