# import PyPDF2
# import re
#
# # create product object
# class Product:
#     def __init__(self, name, original_price, quantity, weight):
#         self.name = name
#         self.original_price = original_price
#         self.quantity = quantity
#         self.weight = weight
#
#     def calculate_discounted_price(self, percentage):
#         return round(self.original_price * (1 + percentage / 100), 2)
#
#     def __str__(self):
#         return f'Product: {self.name}, Original Price: {self.original_price}, Quantity: {self.quantity}, Weight: {self.weight}'
#
# def extract_text(pdf_path: str) -> [str]:
#     with open(pdf_path, 'rb') as pdf:
#         reader = PyPDF2.PdfReader(pdf, strict=False)
#         pdf_text = []
#
#         for page_num in range(len(reader.pages)):
#             page = reader.pages[page_num]
#             content = page.extract_text()
#             pdf_text.append(content)
#
#         return pdf_text
#
# def extract_product_data(pdf_text: [str]) -> [(str, str, str, str)]:
#     pattern_name = r'IMPACT-\d+\s+(.*?)\s+000-\d+\s+'
#     pattern_original_price = r'\d{3}-\d{5}-\d{5}\s+(\d+\.\d{2})'
#     pattern_quantity_weight = r'\s+(\d+)\s+(\d+)\s+G'
#
#     product_data = []
#
#     for text in pdf_text:
#         names = re.findall(pattern_name, text, re.IGNORECASE)
#         original_prices = re.findall(pattern_original_price, text, re.IGNORECASE)
#         quantity_weights = re.findall(pattern_quantity_weight, text)
#
#         for name, original_price, (quantity, weight) in zip(names, original_prices, quantity_weights):
#             product_data.append((name.strip(), original_price, quantity, weight))
#
#     return product_data
#
# if __name__ == '__main__':
#     pdf_path = 'circulaire-25-janvier-au-8-fev.pdf'
#     extracted_text = extract_text(pdf_path)
#     product_data = extract_product_data(extracted_text)
#
#     percentage = 14.95
#
#     for name, original_price, quantity, weight in product_data:
#         product = Product(name=name, original_price=float(original_price), quantity=quantity, weight=weight)
#         discounted_price = product.calculate_discounted_price(percentage)
#         print(f'{product}\nDiscounted Price: ${discounted_price}\n')
#
#

import PyPDF2
import re

class Product:
    def __init__(self, name, original_price, quantity, weight):
        self.name = name
        self.original_price = original_price
        self.quantity = quantity
        self.weight = weight

    def calculate_discounted_price(self, percentage):
        return round(self.original_price * (1 + percentage / 100), 2)

    def __str__(self):
        return f'Product: {self.name}, Original Price: {self.original_price}, Quantity: {self.quantity}, Weight: {self.weight}'

def extract_text(pdf_path: str) -> [str]:
    with open(pdf_path, 'rb') as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        pdf_text = []

        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()
            pdf_text.append(content)

        return pdf_text

def extract_product_data(pdf_text: [str]) -> [(str, str, str, str)]:
    pattern_name = r'IMPACT-\d+\s+([^\d]+)\s+000-'
    pattern_original_price = r'\d{3}-\d{5}-\d{5}\s+(\d+\.\d{2})\s+'
    pattern_quantity_weight = r'\s+(\d+)\s+(\d+)\s+G'

    product_data = []

    # Set up flags for header and footer lines
    header_found = False
    footer_found = False

    for text in pdf_text:
        # Check for the end of the table (footer) and stop processing
        if '----------------------------------------------------------------------------------------------------------------------------------------------------------' in text:
            footer_found = True
            continue

        # Check for the beginning of the table (header) to start processing
        if 'DESCRIPTION DU PRODUIT' in text:
            header_found = True
            continue

        # Continue processing only if the header has been found and footer has not been found
        if header_found and not footer_found:
            # Check if the line contains product information
            if re.search(pattern_name, text, re.IGNORECASE):
                names = re.findall(pattern_name, text, re.IGNORECASE)
                original_prices = re.findall(pattern_original_price, text, re.IGNORECASE)
                quantity_weights = re.findall(pattern_quantity_weight, text)

                for name, original_price, (quantity, weight) in zip(names, original_prices, quantity_weights):
                    product_data.append((name, original_price, quantity, weight))

    return product_data

if __name__ == '__main__':
    pdf_path = 'circulaire-25-janvier-au-8-fev.pdf'
    extracted_text = extract_text(pdf_path)
    product_data = extract_product_data(extracted_text)

    percentage = 14.95

    for name, original_price, quantity, weight in product_data:
        product = Product(name=name, original_price=float(original_price), quantity=quantity, weight=weight)
        discounted_price = product.calculate_discounted_price(percentage)
        print(f'{product}\nDiscounted Price: ${discounted_price}\n')
import PyPDF2
import re

class Product:
    def __init__(self, name, size, weight, price):
        self.name = name
        self.size = size
        self.weight = weight
        self.price = price

    def __str__(self):
        return f'Product: {self.name}, Size: {self.size}, Weight: {self.weight}, Price: {self.price}'

def extract_text(pdf_path: str) -> [str]:
    with open(pdf_path, 'rb') as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        pdf_text = []

        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()
            pdf_text.append(content)

        return pdf_text

def extract_product_data(pdf_text: [str]) -> [Product]:
    pattern_product_line = r'IMPACT-\d+\s+([^\d]+)\s+(\d+)\s+(\d+)G\s+(\d+\.\d{2})'

    product_data = []

    for text in pdf_text:
        # Check if the line contains product information
        matches = re.findall(pattern_product_line, text, re.IGNORECASE)
        for match in matches:
            name, size, weight, price = match
            product = Product(name=name, size=size, weight=weight, price=price)
            product_data.append(product)

    return product_data

if __name__ == '__main__':
    pdf_path = 'circulaire-25-janvier-au-8-fev.pdf'
    extracted_text = extract_text(pdf_path)
    product_data = extract_product_data(extracted_text)

    for product in product_data:
        print(product)

