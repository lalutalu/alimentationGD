import re
import os
import csv
import PyPDF2
from numpy.lib.shape_base import row_stack
from reportlab.pdfgen import canvas
from reportlab.graphics.barcode import code39
from reportlab.lib.colors import black
import pandas as pd

class Product:
    def __init__(self, name, prixOg, prixNew, code39, weight, unit, handleid) -> None:
        self.name = name
        self.prixOg = prixOg
        self.prixNew = prixNew
        self.code39 = code39
        self.weight = weight
        self.unit = unit
        self.handleid = handleid
        self.fieldType = "Product"
        self.Visible = "true"
        self.DiscountMode = "PERCENT"
        self.Inventory = "InStock"

def create_product_from_row(row):
    handleid = row.get("handleId", None)
    name = row.get("name", None)
    prixOg = row.get("prixOriginal", None)
    prixNew = row.get("price", None)
    code39 = row.get("sku", None)
    weight = row.get("description", None)
    unit = row.get("unit", "")

    product = Product(
        name, prixOg, prixNew, code39, weight, unit, handleid 
    )
    return product

def create_product_from_row_new(row):
    name = row.get("nom", None)
    prixNew = row.get("Prix vente entreprise (Caisse)", None)
    code39 = row.get("Code 39", None)
    weight = row.get("Format", None)

    product = Product(name, "", prixNew, code39, weight, "", "" )
    return product

def search_new_csv(filepath, create_products=False):
    data = pd.read_csv(filepath, encoding='utf-8')
    products = []
    for index, row in data.iterrows():
        product = create_product_from_row_new(row)
        products.append(product)
    return products

def search_old_csv(filepath, create_products=False):
    data = pd.read_csv(filepath, encoding = 'utf-8')
    products = []
    for index, row in data.iterrows():
        product = create_product_from_row(row)
        products.append(product)
    return products

def create_csv(filepath, products):
    headings = [
        "handleId",
        "fieldType",
        "name",
        "description",
        "productImageUrl",
        "collection",
        "sku",
        "ribbon",
        "price",
        "surcharge",
        "visible",
        "discountMode",
        "discountValue",
        "inventory",
        "weight",
        "cost",
        "productOptionName1",
        "productOptionType1",
        "productOptionDescription1",
        "productOptionName2",
        "productOptionType2",
        "productOptionDescription2",
        "productOptionName3",
        "productOptionType3",
        "productOptionDescription3",
        "productOptionName4",
        "productOptionType4",
        "productOptionDescription4",
        "productOptionName5",
        "productOptionType5",
        "productOptionDescription5",
        "productOptionName6",
        "productOptionType6",
        "productOptionDescription6",
        "additionalInfoTitle1",
        "additionalInfoDescription1",
        "additionalInfoTitle2",
        "additionalInfoDescription2",
        "additionalInfoTitle3",
        "additionalInfoDescription3",
        "additionalInfoTitle4",
        "additionalInfoDescription4",
        "additionalInfoTitle5",
        "additionalInfoDescription5",
        "additionalInfoTitle6",
        "additionalInfoDescription6",
        "customTextField1",
        "customTextCharLimit1",
        "customTextMandatory1",
        "customTextField2",
        "customTextCharLimit2",
        "customTextMandatory2",
        "brand",
    ]


    with open(filepath, "w", newline="") as csvfile:
        writer = csv.DictWriter(csvfile, fieldnames=headings)
        writer.writeheader()

        for product in products:
            writer.writerow(
                {
                    "handleId": product.handleid,
                    "fieldType": product.fieldType,
                    "name": product.name,
                    "description": f"{product.weight}x{product.unit}",
                    "productImageUrl": "",
                    "collection": "metro",
                    "sku": product.code39,
                    "ribbon": "",
                    "price": product.prixNew,
                    "surcharge": "",
                    "visible": product.Visible,
                    "discountMode": product.DiscountMode,
                    "discountValue": "0",
                    "inventory": product.Inventory,
                    "weight": "",
                    "cost": "",
                    "productOptionName1": "",
                    "productOptionType1": "",
                    "productOptionDescription1": "",
                    "productOptionName2": "",
                    "productOptionType2": "",
                    "productOptionDescription2": "",
                    "productOptionName3": "",
                    "productOptionType3": "",
                    "productOptionDescription3": "",
                    "productOptionName4": "",
                    "productOptionType4": "",
                    "productOptionDescription4": "",
                    "productOptionName5": "",
                    "productOptionType5": "",
                    "productOptionDescription5": "",
                    "productOptionName6": "",
                    "productOptionType6": "",
                    "productOptionDescription6": "",
                    "additionalInfoTitle1": "",
                    "additionalInfoDescription1": "",
                    "additionalInfoTitle2": "",
                    "additionalInfoDescription2": "",
                    "additionalInfoTitle3": "",
                    "additionalInfoDescription3": "",
                    "additionalInfoTitle4": "",
                    "additionalInfoDescription4": "",
                    "additionalInfoTitle5": "",
                    "additionalInfoDescription5": "",
                    "additionalInfoTitle6": "",
                    "additionalInfoDescription6": "",
                    "customTextField1": "",
                    "customTextCharLimit1": "",
                    "customTextMandatory1": "",
                    "customTextField2": "",
                    "customTextCharLimit2": "",
                    "customTextMandatory2": "",
                    "brand": "",
                }
            )

old_products = []
new_products = []
read_csv = "../../../ProduitsAvecRabais.csv"
read_coke_csv = "../pdfs/coca-cola.csv"
new_csv_path = "../../../ProduitsAvecCoke.csv"
old_products = search_old_csv(read_csv)
new_products = search_new_csv(read_coke_csv)
for new_product in new_products:
    for old_product in old_products:
        if new_product.code39 == old_product.code39:
            new_product.name = old_product.name
            new_product.prixNew = old_product.prixNew
            new_product.handleId = new_product.handleId
            break
create_csv(new_csv_path, old_products)
print(f"CSV file '{new_csv_path}' created successfully.")
