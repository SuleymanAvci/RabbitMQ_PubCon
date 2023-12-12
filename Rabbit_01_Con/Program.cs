
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


//Bağlantı Oluşturma
ConnectionFactory factory = new() { HostName="localhost", Port=5672 };



//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();



//Queue Oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);

//Queue'dan Mesaj Okuma
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
channel.BasicQos(0, 1, false);
consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yerdir.
    //e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
};

Console.ReadLine();