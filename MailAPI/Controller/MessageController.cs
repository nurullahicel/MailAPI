using MailAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace MailAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult Post([FromForm] User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri("amqps://wcketmgx:dIWDsdGT3MJKoV3B-DDvokdJOhlL2HcS@seal.lmq.cloudamqp.com/wcketmgx");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messagequeue", false, false, false);
            string serializeData = JsonSerializer.Serialize(model);
            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            channel.BasicPublish("", "messagequeue", body: data);

            return Ok();
        }
    }
}
