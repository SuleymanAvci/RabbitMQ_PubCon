﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://sywjinjf:5vIsZWAHKg8_oUWMdINOVukpQwzTjcfi@moose.rmq.cloudamqp.com/sywjinjf");

//Bağlantıyı Aktifleştirme ve Kanal Açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-queue-example");


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};



Console.ReadLine();
