import sys
import subprocess
import importlib
import os
import time

required = ['pyautogui', 'pillow', 'pynput']

def prompt_install(package, import_name=None):
    try:
        importlib.import_module(import_name or package)
    except ImportError:
        answer = input(f"Package '{package}' is not installed. Do you want to install it? (y/n): ").strip().lower()
        if answer == 'y':
            python_exec = sys.executable
            if python_exec.endswith("pythonw.exe"):
                python_exec = python_exec.replace("pythonw.exe", "python.exe")
            try:
                subprocess.check_call([python_exec, '-m', 'pip', 'install', package])
            except subprocess.CalledProcessError as e:
                print(f"Failed to install '{package}'. Error: {e}")
                sys.exit(1)
        else:
            print(f"Cannot continue without '{package}'. Exiting.")
            sys.exit(1)

# Install required packages if missing
prompt_install('pyautogui')
prompt_install('pillow', 'PIL')
prompt_install('pynput')

# Re-import in case they were just installed
import pyautogui
from pynput import keyboard
from PIL import ImageGrab

# --- Color picker logic ---
running = True
last_color = None

def get_color(x, y):
    img = ImageGrab.grab(bbox=(x, y, x + 1, y + 1))
    rgb = img.getpixel((0, 0))
    return '#{:02x}{:02x}{:02x}'.format(*rgb)

def on_press(key):
    global running
    if key == keyboard.Key.esc:
        running = False
        return False  # Stop listener

listener = keyboard.Listener(on_press=on_press)
listener.start()

print("Move your mouse around. Press ESC to exit.\n")

try:
    while running:
        x, y = pyautogui.position()
        color = get_color(x, y)
        if color != last_color:
            print(f"Color at ({x}, {y}): {color}")
            last_color = color
        time.sleep(0.1)
except KeyboardInterrupt:
    pass

listener.join()
print("Exited.")

