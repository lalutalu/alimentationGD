import os
from typing import List
from plum import convert
import xlrd
import pandas as pd
import glob
import pyautogui
import time
from product import Product

# GLOBALS
latest_file = None
products: List[Product] = []

# Function Top Bar

# Function Left Bar

# Function Export to csv

# Function to find the latest downloaded csv file
def get_latest_file():
    downloads_path = os.path.join(os.path.expanduser('~'), 'Downloads')
    list_of_files = glob.glob(os.path.join(downloads_path, '*.xls'))
    if not list_of_files:
        raise ValueError(f"Pas de fichiers XLS dans {downloads_path}")
    latest_file =max(list_of_files, key=os.path.getctime) 
    return latest_file


# def convert_file(file_path:str):
#     column_width = 57.89
#     new_file_name = 
#     workbook = Workbook()
#     workbook.LoadFromFile(file_path)
#     worksheet = workbook.Worksheets[0]
#     worksheet.SetColumnWidth(1, column_width)
#     workbook.SaveToFile(, ExcelVersion.Version2016)
#     workbook.Dispose()


def read_file(file_path):
    try:
        df = pd.read_excel(file_path)
        df = df.dropna(how='all')  # Optional: Remove rows with all empty values
        lines = df.values.tolist()
        return lines
    except pd.errors.ParserError:
        # Handle potential errors during reading (optional)
        return None


# Function Parse Code39
# Function Parse Name
# Function Parse Prix
# Function Parse Quantity
# Function Parse Weight
# Function Parse Unit

# Function export products to google cloud


# MAIN
if __name__ == "__main__":
    latest_file = get_latest_file()
    # convert_file(latest_file)
    print(read_file(latest_file))
