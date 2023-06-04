

using RabbitMQ.Client;
using System.Text;


//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://sywjinjf:5vIsZWAHKg8_oUWMdINOVukpQwzTjcfi@moose.rmq.cloudamqp.com/sywjinjf");

//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//byte[] message=Encoding.UTF8.GetBytes("Hello");
//channel.BasicPublish(exchange: "", routingKey: "example-queue",body:message);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Hello" + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
}

Console.ReadLine();