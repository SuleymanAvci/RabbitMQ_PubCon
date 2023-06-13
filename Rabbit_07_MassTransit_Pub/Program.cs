using MassTransit;
using Rabbit_07_MassTransit_Shared.Messages;
using System.Threading.Channels;

string rabbitMQUri = "amqps://sywjinjf:5vIsZWAHKg8_oUWMdINOVukpQwzTjcfi@moose.rmq.cloudamqp.com/sywjinjf";

string queueName = "example-pueue";

IBusControl bus= Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));

Console.Write("Gönderilecek mesaj : ");
string message = Console.ReadLine();
sendEndpoint.Send<IMessage>(new ExampleMessage() { Text=message});

Console.Read();