import pyautogui
import time

print('Move cursor to desireds position within 5 seconds...')
time.sleep(5)

x,y = pyautogui.position()

print(f"Current mouse position: {x},{y}")
