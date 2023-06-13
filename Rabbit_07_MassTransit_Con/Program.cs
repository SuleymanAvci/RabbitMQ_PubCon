using MassTransit;
using Rabbit_07_MassTransit_Con.Consumers;

string rabbitMQUri = "amqps://sywjinjf:5vIsZWAHKg8_oUWMdINOVukpQwzTjcfi@moose.rmq.cloudamqp.com/sywjinjf";


string queueName = "example-pueue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});
    
await bus.StartAsync();

Console.Read();
    