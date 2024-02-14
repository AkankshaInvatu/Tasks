LoggingProcess -- steps 

Right click on solution "LoggingWebsiteProcess"
Go to properties , select multiple projects ,
Both(ConsoleClient , LoggingWebsiteProcess) project action will be start ,
Inside Consoleclient project , open program.cs file ,  
Inside program.cs file will change -,
var connection = new HubConnectionBuilder()
.WithUrl("https://your-new-url/logHub") // Update the URL here
.Build(); ,
Now you can run project(click on start button).
