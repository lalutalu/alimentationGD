import os
import csv
import pandas as pd
import glob
import pyautogui
import time

# GLOBALS
latest_file = None

# Function Top Bar

# Function Left Bar

# Function Export to csv

# Function to find the latest downloaded csv file
def get_latest_file():
    downloads_path = os.path.join(os.path.expanduser('~'), 'Downloads')
    list_of_files = glob.glob(os.path.join(downloads_path, '*.xls'))
    if not list_of_files:
        raise ValueError(f"Pas de fichiers CSV dans {downloads_path}")
    latest_file =max(list_of_files, key=os.path.getctime) 
    return latest_file


# Function Read CSV File and parse data
def read_csv(filePath: str):
    with open(filePath, mode='r') as file:
        csvFile = csv.reader(file)
        for lines in csvFile:
            print(lines)

# Function export products to google cloud


# MAIN
if __name__ == "__main__":
    print(get_latest_file())
