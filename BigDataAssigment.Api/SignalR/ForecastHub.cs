using System;
using System.Threading.Tasks;
using BigDataAssigment.Api.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;

namespace BigDataAssigment.Api.SignalR
{
    public class ForecastHub:Hub
    {
        [HubMethodName("SendMessage")]
        public async Task SendMessage(Forecast forecast)
            {
            HubConnection connection = new HubConnectionBuilder()
                  .WithUrl(new Uri("https://localhost:5002/forecasthub"))
                  .WithAutomaticReconnect()
                  .Build();

            await connection.StartAsync().ContinueWith(task => {


                connection.InvokeAsync("SendMessage", forecast);


            });
        }

        [HubMethodName("Connected")]
        public async Task Connect(string message)
        {
            await Clients.All.SendAsync("Connected", message);
        }


    }
}
