# ETSU-Schoolwork
For Server Side Web Programming
WEB API that displays plants from list of plants from Cincinnati Zoo

## Problem 1
  Used converter to convert json data from API into their attributes so that they could be easier to read and put into C# code.
  The json serilization method that came with the template of the page to help display the data onto a webpage was conflicting with the json serializer I was using and making     ambiguous statements.

  **Fixed by deleting the unnecessary using statements**
  
## Problem 2
  Was getting Compiler Error CS0029 when trying to store json data into list.
  Code from json attribute converter had data stored in arrays and was throwing errors when trying to store data from http response into list to be used to display data.

  **Fixed by changing the mentioned arrays into lists in Plant.cs**
  
## Problem 3 
  When trying to load webpage to display data was getting error.
  The API I was using had hundreds of entries and I only grabbed a set amount out of the total data to save build time and part of the data is the address of the different         plants. Where I was just using part of the data available, I was only using pulling one of the addresses available out of the two that was in the data in the json            reader and the reader was looking for the other address.

  **Fixed by returning the one address from my snippet of data in the reader**
  
