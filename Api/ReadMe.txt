#API calls

Get random quote
http://localhost:5000/api/quote/{bookCode}

Example: 
http://localhost:5000/api/quote/dhp
Random quote from the Dhammapada

Get specific quote
http://localhost:5000/api/sutta/{bookCode}/{chapterNumber?}/{verseNumber?}

Examples:
http://localhost:5000/api/sutta/dhp/4
Get chapter 4 of the Dhammapada
returns all text from Flowers chapter

http://localhost:5000/api/sutta/dhp/4/48
Get chapter 4, verse 49 of the Dhammapada 
returns "As a bee gathers honey from the flower without injuring its 
color or fragrance, even so the sage goes on his alms-round in the village."
plus metadata

http://localhost:5000/api/sutta/dhp
Gets all text for Dhammapada

#Supported books and text

dhp - Dhammapada