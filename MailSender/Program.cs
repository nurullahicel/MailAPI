// See https://aka.ms/new-console-template for more information
using MailSender;
using Microsoft.AspNetCore.SignalR.Client;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

Console.WriteLine("");
HubConnection connectionSignalR = new HubConnectionBuilder()
  .WithUrl("https://localhost:7105/messagehub")
  .Build();
await connectionSignalR.StartAsync();

ConnectionFactory factory = new ConnectionFactory();
factory.Uri = new Uri("amqps://wcketmgx:dIWDsdGT3MJKoV3B-DDvokdJOhlL2HcS@seal.lmq.cloudamqp.com/wcketmgx");
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare("messagequeue", false, false, false);

EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
channel.BasicConsume("messagequeue", true, consumer);

consumer.Received += async(s, e) =>
{
    string serializeData = Encoding.UTF8.GetString(e.Body.Span);
    User user=JsonSerializer.Deserialize<User>(serializeData);
    Emailsender.Send(user.Email,user.Message);

    Console.WriteLine($"Sent to {user.Email}");

    await connectionSignalR.InvokeAsync("SendMessageAsync", $"Sen to {user.Email} ");

};
Console.Read();