import fandom


fandom.set_wiki("valorant")
test = fandom.search("Astra", results = 1)
print(test)
page = fandom.page(title = "Astra")
section = page.section('Profile')
if section:
	print(section)
else:
	print("Section not found.")