using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
  
        string serviceBusConnectionString = "Endpoint=sb://apcoaservicebusexternal.servicebus.windows.net/;SharedAccessKeyName=apimkey;SharedAccessKey=CUumR+UuRnMIhtK8i3oD8iAa2v8wPUDHvwkgPslSu7I=;";
        string queueName = "sessions";

        
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7202/parkingHub")
            .Build();

      
        await connection.StartAsync();

       
        var queueClient = new QueueClient(serviceBusConnectionString, queueName);


        queueClient.RegisterMessageHandler(
      async (message, token) => await ProcessMessagesAsync(message, token, connection, queueClient),
      new MessageHandlerOptions(ExceptionReceivedHandler)
      {
          MaxConcurrentCalls = 1,
          MaxAutoRenewDuration = TimeSpan.FromMinutes(5) 
      });

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();

      
        await queueClient.CloseAsync();
        await connection.StopAsync();
    }

    static async Task ProcessMessagesAsync(Message message, CancellationToken token, HubConnection connection, QueueClient queueClient)
    {
        try
        {
            string messageBody = Encoding.UTF8.GetString(message.Body);
            // User userDetails = JsonConvert.DeserializeObject<ParkingModel>(messageBody);
            if (connection.State == HubConnectionState.Disconnected)
            {
                // Start the SignalR connection
                await connection.StartAsync();
            }
            Console.WriteLine($"{messageBody}");
            await connection.InvokeAsync("SendUserData", messageBody);

        
           
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while processing message: {ex.Message}");
          
        }
    }

    static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
    {
        Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
        var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
        Console.WriteLine("Exception context for troubleshooting:");
        Console.WriteLine($"- Endpoint: {context.Endpoint}");
        Console.WriteLine($"- Entity Path: {context.EntityPath}");
        Console.WriteLine($"- Executing Action: {context.Action}");

        return Task.CompletedTask;
    }
}




