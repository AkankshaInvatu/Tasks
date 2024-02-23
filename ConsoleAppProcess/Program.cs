using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel;

class Program
{
    static async Task Main(string[] args)
    {
        var connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7202/logHub")
            .Build();

        await connection.StartAsync();

        Dictionary<string, int> processCounts = new Dictionary<string, int>();

        while (true)
        {
            try
            {
                var runningProcesses = Process.GetProcesses()
                    .Where(process =>
                        !string.IsNullOrEmpty(process.MainWindowTitle) &&
                        !string.IsNullOrWhiteSpace(process.MainWindowTitle) &&
                        process.MainModule != null);


                foreach (var process in runningProcesses)
                {
                    try
                    {
                        string filePath = null;
                        try
                        {
                            filePath = process.MainModule?.FileName;
                        }
                        catch (Win32Exception ex)
                        {
                            Console.WriteLine($"Error accessing main module of the process '{process.ProcessName}': {ex.Message}");
                        }

                        string processName = process.ProcessName;
                        if (!processCounts.ContainsKey(processName))
                        {
                            processCounts[processName] = 1;
                        }
                        else
                        {
                            processCounts[processName]++;
                        }

                        var processInfo = $" File Path: {filePath} - Process Name: {processName} - Total Copies: {processCounts[processName]}";

                        await connection.InvokeAsync("SendProcessInfo", processInfo);
                    }
                    catch (Win32Exception ex)
                    {
                        Console.WriteLine($"Error accessing process '{process.ProcessName}': {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(5));
            }

            catch (Win32Exception ex)
            {
                Console.WriteLine("errorrs");
            }
        } } 
    

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No IP Address Found";
        }
    
}