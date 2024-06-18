import gspread
from googleapiclient.discovery import build
from datetime import datetime

FOLDER_ID = "1c72y-TMG3PZL2qqB7q1OJSHVbOT9R-kl"

gc = gspread.oauth(credentials_filename="credentials.json")

sh = gc.create('')

created_file_id = sh.id

today = datetime.today().strftime('%d-%m-%Y')
new_title = f"backup_{today}"
destination_spreadsheet = gc.copy(created_file_id, folder_id=FOLDER_ID, title=new_title)

print(f"Spreadsheet copied with title: {destination_spreadsheet.title}")
gc.del_spreadsheet(created_file_id)