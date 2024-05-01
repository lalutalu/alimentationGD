import re
import os
import PyPDF2
from numpy.lib.shape_base import row_stack
from reportlab.pdfgen import canvas
from reportlab.graphics.barcode import code39
from reportlab.lib.colors import black
import pandas as pd

noCode = 0
PERCENTAGE = 14.95

class Product:
    def __init__(self, name, prixOg, prixNew, code39, weight, unit, handleid) -> None:
        self.name = name
        self.prixOg = prixOg
        self.prixNew = prixNew
        self.code39 = code39
        self.weight = weight
        self.unit = unit
        self.handleid = handleid


def extract_product_name(product_line: str) -> str:
    pattern = r'IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})'
    match = re.search(pattern, product_line)
    if match:
        captured_text = match.group(1)
        result = f"{captured_text.strip()}"
        return result
    else:
        return "No Name"


def extract_product_upc(product_line: str) -> str:
    pattern = r'IMPACT-\d+\s+(.*?)\s+(\d{3}-\d{5}-\d{5})'
    match = re.search(pattern, product_line)
    if match:
        captured_text = match.group(2)
        result = f"{captured_text.strip()}"
        return result
    else:
        return "No UPC"


def extract_product_code(product_line: str) -> str:
    match = re.search(r'\b(\d{7})[A-Za-z]?\b', product_line)
    if match:
        return match.group(1)
    else:
        print(f"No Code: {product_line}")
        return "No Code"


def extract_product_with_details(pdf_path: str) -> list:
    with open(pdf_path, 'rb') as pdf:
        reader = PyPDF2.PdfReader(pdf, strict=False)
        product_lines = []
        for page_num in range(len(reader.pages)):
            page = reader.pages[page_num]
            content = page.extract_text()

            lines = content.split('\n')
            product_line = ""

            for line in lines:
                if re.match(r'^IMPACT-\d+\s+', line):
                    product_line = line
                elif product_line:
                    product_line += f" {line}"
                    product_lines.append(product_line)
                    product_line = ""
        return product_lines


def extract_original_price(product_line: str) -> str:
    match = re.search(r'\b(\d+\.\d+)\s+', product_line)
    return match.group(1) if match else "No Original Price"


def extract_quantity(product_line: str) -> str:
    match = re.search(r'\d+\s+(\d+)', product_line)
    return match.group(1) if match else "No Quantity"


def extract_weight(product_line: str) -> tuple:
    match = re.search(r'\d+\s+(\d+)\s+([A-Za-z]+)', product_line)

    if match:
        return match.groups()
    else:
        weight_match = re.search(r'(\d+)\D*$', product_line)
        weight = weight_match.group(1) if weight_match else "No Weight"

        unit_match = re.search(r'([^\d]+)$', product_line)
        unit = unit_match.group(1).strip() if unit_match else "No Unit"

        if weight == "No Weight" and unit == "No Unit":
            print(f"No Weight or Unit: {product_line}")

        return (weight, unit)


def calculate_new_price(ogPrice: float) -> float:
    new_price = ogPrice * (1 + PERCENTAGE / 100)
    return round(new_price, 2)

def create_product_from_row(row):
  name = row.get('name', None)
  prixOg = row.get('prixOriginal', None)
  prixNew = row.get('price', None)
  code39 = row.get('sku', None)
  weight = row.get('description', None)
  unit = row.get('unit', "")

  product = Product(name, prixOg, prixNew, code39, weight, unit, row.get('handleId', None))
  return product

def search_csv(filepath, create_products=False):
    data = pd.read_csv(filepath)
    products = []
    for index, row in data.iterrows():
        product = create_product_from_row(row)
        products.append(product)
    return products

def create_csv(filepath, products):
   headings = [ "handleId", "fieldType", "name", "description", "productImageUrl", "collection", "sku", "ribbon", "price", "surcharge", "visible",
            "discountMode", "discountValue", "inventory", "weight", "cost", "productOptionName1", "productOptionType1", "productOptionDescription1", "productOptionName2",
            "productOptionType2", "productOptionDescription2", "productOptionName3", "productOptionType3", "productOptionDescription3", "productOptionName4",
            "productOptionType4", "productOptionDescription4", "productOptionName5", "productOptionType5", "productOptionDescription5", "productOptionName6",
            "productOptionType6", "productOptionDescription6", "additionalInfoTitle1", "additionalInfoDescription1", "additionalInfoTitle2", "additionalInfoDescription2",
            "additionalInfoTitle3", "additionalInfoDescription3", "additionalInfoTitle4", "additionalInfoDescription4", "additionalInfoTitle5", "additionalInfoDescription5",
            "additionalInfoTitle6", "additionalInfoDescription6", "customTextField1", "customTextCharLimit1", "customTextMandatory1", "customTextField2", "customTextCharLimit2",
            "customTextMandatory2", "brand" ];
            


pdf_products = []
csv_products = []
pdf_path = "../pdfs/04-03-2024.pdf"
new_csv_path = "../../ProduitsAvecRabais.csv"
csv_path = "../../../Produits.csv"
combined_lines = extract_product_with_details(pdf_path)

for line in combined_lines:
    weight, unit = extract_weight(line)

    product = Product(
        handleid="",
        name=extract_product_name(line),
        prixOg=extract_original_price(line),
        prixNew=calculate_new_price(float(extract_original_price(line))),
        code39=extract_product_code(line),
        weight=weight,
        unit=unit
    )

    pdf_products.append(product)
    csv_products = search_csv(csv_path)
    # for product in csv_products:
    #     print(product.name)
    for csv_product in csv_products:
        for pdf_product in pdf_products:
            if csv_product.code39 == pdf_product.code39:
                # print("Product with code {} found in both CSV and PDF:".format(csv_product.code39))
                # print("CSV Product: ", csv_product.__dict__)
                # print("PDF Product: ", pdf_product.__dict__)
                # print()
                csv_product.name = pdf_product.name
                csv_product.prixOg = pdf_product.prixOg
                csv_product.prixNew = pdf_product.prixNew
                csv_product.code39 = pdf_product.code39
                csv_product.weight = pdf_product.weight
                csv_product.unit = pdf_product.unit
                csv_product.handleid = pdf_product.handleid
                # print("Product updated!")
                break
      