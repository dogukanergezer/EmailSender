using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using publisher.Models;
using RabbitMQ.Client;

namespace publisher.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class MessageController : ControllerBase
    {

        [HttpPost()]
        public IActionResult Post([FromForm]User model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new System.Uri("lcoalhost");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare("messagequeue", false, false, false);
            string serializeData =JsonSerializer.Serialize(model);

            byte[] data = Encoding.UTF8.GetBytes(serializeData);
            channel.BasicPublish("", "messagequeue", body: data);

            return Ok();
        }

    }


}