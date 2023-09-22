import json
import re
import requests
import os

# Function to recursively search for links in JSON data
def extract_links(data):
    links = []
    if isinstance(data, dict):
        for key, value in data.items():
            links.extend(extract_links(value))
    elif isinstance(data, list):
        for item in data:
            links.extend(extract_links(item))
    elif isinstance(data, str):
        matches = re.findall(r'(https?://\S+)', data)
        links.extend(matches)
    return links

# Read the JSON file
with open('Agents.json', 'r') as f:
    data = json.load(f)

# Extract links from the JSON data
links = extract_links(data)

# Create a directory for downloaded images if it doesn't exist
os.makedirs('downloaded_images', exist_ok=True)

# Download the images
counter = 0
for link in links:
    response = requests.get(link)
    if response.status_code == 200:
        filename = f'downloaded_images/file{counter}_{link.split("/")[-1]}'
        with open(filename, 'wb') as f:
            f.write(response.content)
        counter += 1
        print(f"Downloaded: {link} => {filename}")
    else:
        print(f"Failed to download: {link} (Status code: {response.status_code})")

input("Press Enter to exit...")