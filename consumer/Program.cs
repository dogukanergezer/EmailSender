using System;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            HubConnection connSignalR = new HubConnectionBuilder().Build();
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new System.Uri("localhost");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messagequeue", false, false, false);
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume("messagequeue", true, consumer);

            consumer.Received += async (s, e) =>
             {
                 //Email operation proccessing 
                 //e.Body.Span
                 string serializeData = Encoding.UTF8.GetString(e.Body.Span);
                 User user = JsonSerializer.Deserialize<User>(serializeData);

                 EmailSender.Send(user.Email, user.Message);
                 Console.WriteLine($"{user.Email} mail has been sended");

                 await connSignalR.InvokeAsync("SendMessageAsync", $"{user.Email} mail has been sended");
             };
            Console.Read();


        }
    }
}
