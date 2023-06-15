using MassTransit;
using Rabbit_09_MassTransit.RequestResponsePattern_Con.Consumers;

Console.WriteLine("Consumer");

string rabbitMQUri = "amqps://sywjinjf:5vIsZWAHKg8_oUWMdINOVukpQwzTjcfi@moose.rmq.cloudamqp.com/sywjinjf";

string requestQueue = "request-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(requestQueue, endpoint =>
    {
        endpoint.Consumer<ReqeustMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();