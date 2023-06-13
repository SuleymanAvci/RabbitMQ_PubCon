
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Security.AccessControl;
using System.Text;


//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://sywjinjf:5vIsZWAHKg8_oUWMdINOVukpQwzTjcfi@moose.rmq.cloudamqp.com/sywjinjf");

//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P(Point-to-Point) Tasarım
//string queueName = "example-p2p-queue";

//channel.QueueDeclare(
//    queue:queueName,
//    durable:false,
//    exclusive:false,
//    autoDelete:false);

//byte[] message = Encoding.UTF8.GetBytes("Merhaba...");
//channel.BasicPublish(
//    exchange: string.Empty,
//    routingKey: queueName,
//    body: message);

//Console.ReadLine();

#endregion


#region Publish/Subscribe (Pub/Sub) Tasarımı

//string exchangeName = "example-pub-sub-exchange";

//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//for (int i = 0; i < 100; i++)
//{   
//    await Task.Delay(200);
//    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);

//    channel.BasicPublish(

//        exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message);
//}
//Console.ReadLine();

#endregion


#region Vork Queue(İş Kuyruğu) Tasarımı

//string queueName = "example-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);
//    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);

//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message);
//}
//Console.ReadLine();
#endregion


#region Request/Response Tasarımı
string ruquestQueueName = "ecample-request-response-queue";
channel.QueueDeclare(
    queue:ruquestQueueName,
    durable:false,
    exclusive:false,
    autoDelete:false);

string replyQueueName = channel.QueueDeclare().QueueName;

string correlationId=Guid.NewGuid().ToString();

#region Request Mesajını Oluşturma ve Gönderme

IBasicProperties properties=channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
properties.ReplyTo = replyQueueName;

for (int i = 0; i < 10; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("merhaba " + i);
    channel.BasicPublish(
        exchange:string.Empty,
        routingKey:replyQueueName,
        body:message,
        basicProperties:properties);
}
#endregion
#region Response Kuyruğu Dinleme

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: replyQueueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        //...
        Console.WriteLine($"Response : {Encoding.UTF8.GetString(e.Body.Span)}");
    }
};

#endregion

#endregion

Console.ReadLine();