import gspread

gc = gspread.oauth(credentials_filename="credentials.json")

sh = gc.create('A new spreadsheet')