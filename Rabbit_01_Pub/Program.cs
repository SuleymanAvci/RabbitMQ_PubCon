using RabbitMQ.Client;
using System.Text;


//Bağlantı Oluşturma
ConnectionFactory factory = new() { HostName = "localhost", Port = 5672 };


//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//byte[] message=Encoding.UTF8.GetBytes("Hello");
//channel.BasicPublish(exchange: "", routingKey: "example-queue",body:message);
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;
for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Hello" + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: properties);
}

Console.ReadLine();