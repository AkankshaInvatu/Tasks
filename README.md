

Right click on solution "LoggingWebsiteProcess"
Go to properties , select multiple projects ,
Both(ConsoleAppProcess , LoggingWebsiteProcess) project ,In action dropdown choose start in both project,
Inside ConsoleAppProcess project , open program.cs file ,  
Inside program.cs file will change -,
var connection = new HubConnectionBuilder()
.WithUrl("https://your-new-url/logHub") // Update the URL here
.Build(); ,
Now you can run project(click on start button).
