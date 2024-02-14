using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SignalRClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7242/logHub")
                .Build();

            await connection.StartAsync();

          

            while (true)
            {
                Console.WriteLine("Enter data to send to the server:");
                string inputData = Console.ReadLine();
                var jsonObject = ParseInputData(inputData);

                if (jsonObject != null)
                {
                    // Convert the JSON object to JSON string
                    string jsondata = JsonConvert.SerializeObject(jsonObject);

                    // Send the JSON data to the server
                    await connection.InvokeAsync("SendMessage", jsondata);
                }
                else
                {
                    Console.WriteLine("Invalid input format. Please enter data in the format.");
                }

            }
        }
        static Dictionary<string, object> ParseInputData(string inputData)
        {
            var regex = new Regex(@"(?<key>[^,:]+):(?<value>[^,:]+)");

            var matches = regex.Matches(inputData);

            if (matches.Count == 0)
                return null;

            var jsonObject = matches.ToDictionary(
                match => match.Groups["key"].Value.Trim(),
                match => (object)match.Groups["value"].Value.Trim());

            return jsonObject;
        }
    }
}

