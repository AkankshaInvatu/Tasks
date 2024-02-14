signalr2/LoggingProcess -- steps 

Right click on solution "LoggingProcess"


Go to properties , select multiple projects ,

Both(ConsoleClient , LoggingProcess) project action will be start ,


Inside Consoleclient project , open program.cs file ,  

Inside program.cs file will change -,

var connection = new HubConnectionBuilder()
.WithUrl("https://your-new-url/logHub") // Update the URL here
.Build(); ,


Now you can click on start project will be run, console will open you enter data, that data will display on website ,


example you enter data - name:xyz,age:12 this data will display on website in json format.
