using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7202/logHub")
            .Build();

        await connection.StartAsync();

        while (true)
        {
            var runningProcesses = Process.GetProcesses()
           .GroupBy(process => process.ProcessName)
           .Select(group => group.First());

            foreach (var process in runningProcesses)
            {
                var processInfo = $"{process.ProcessName} - {process.Id}";
                await connection.InvokeAsync("SendProcessInfo", processInfo);
            }

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
