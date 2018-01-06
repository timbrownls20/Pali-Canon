# Pali-Canon
API for Pali Canon texts and sample clients. Original texts from www.accesstoinsight.org

## API calls

### Get random quote

*http://localhost:5000/api/quote/{bookCode}*

Example: 

*http://localhost:5000/api/quote/dhp*

Random quote from the Dhammapada

### First quote

*http://localhost:5000/api/quote/first/{bookCode}*

Example:

*http://localhost:5000/api/quote/first/dhp*

Gets first verse from Dhammapada


### Last quote

*http://localhost:5000/api/quote/last/{bookCode}*

Example:

*http://localhost:5000/api/quote/last/dhp*

Gets last verse from Dhammapada


### Next quote

*http://localhost:5000/api/quote/next/{bookCode}/{chapter}/{verse}*

Examples:

*http://localhost:5000/api/quote/next/dhp/1/2*

Gets next verse from Dhammapada for chapter 1 verse 2 - so chapter 1 verse 3


*http://localhost:5000/api/quote/next/dhp/26/424*

Gets next verse from Dhammapada for chapter 26 verse 424 - this is out of range so will return last chapter i.e. chpater 26 verse 423


### Get specific quote
*http://localhost:5000/api/sutta/{bookCode}/{chapterNumber?}/{verseNumber?}*

Examples:

*http://localhost:5000/api/sutta/dhp/4*

Get chapter 4 of the Dhammapada  
returns all text from Flowers chapter

*http://localhost:5000/api/sutta/dhp/4/48*

Get chapter 4, verse 49 of the Dhammapada   
returns "As a bee gathers honey from the flower without injuring its 
color or fragrance, even so the sage goes on his alms-round in the village."
plus metadata

*http://localhost:5000/api/sutta/dhp*

Gets all text for Dhammapada



## Supported books and text

dhp - Dhammapada
