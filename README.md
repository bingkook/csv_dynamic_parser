# csv_dynamic_parser
sample task for csv parser

Implemented features:
1.Management for the configuration data,such as name, required,data type etc.
2.Select CSV file and uploaded it to server.
3.Implement the configuration check for the data item.such as data type checking, required checking, max length checking etc. 
4.Display the checking result as pop-up message dialog if find issues.
5.Mapping the configuration data with the CSV columns.
6.Show the converted data table.
7.UX improvement.such as displaying error message.
8.Add some unit test for the methods.but It is not enough and test cases were not enough to cover all methods.
9.Use log4net to log the errors.

if the csv file dosen't has header row, for the mapping page, the column name will be : column1, column2, column3 ... etc.

![step 1. create configuration data](https://github.com/bingkook/csv_dynamic_parser/blob/master/docs/1.configuration%20data.jpg)

![step 2. upload csv file and mapping](https://github.com/bingkook/csv_dynamic_parser/blob/master/docs/2.upload%20CSV%20file%20and%20mapping.jpg)

![step 3. display convert result data list](https://github.com/bingkook/csv_dynamic_parser/blob/master/docs/3.convert%20result.jpg)

![show validate error message ](https://github.com/bingkook/csv_dynamic_parser/blob/master/docs/parse%20error%20message.jpg)
