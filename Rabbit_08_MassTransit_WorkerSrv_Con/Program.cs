using MassTransit;
using Rabbit_08_MassTransit_WorkerSrv_Con;
using Rabbit_08_MassTransit_WorkerSrv_Con.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurrator =>
        {
            configurrator.AddConsumer<ExampleMessageConsumer>();
            configurrator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps://sywjinjf:5vIsZWAHKg8_oUWMdINOVukpQwzTjcfi@moose.rmq.cloudamqp.com/sywjinjf");

                _configurator.ReceiveEndpoint("example-message-queue", e => e.ConfigureConsumer<ExampleMessageConsumer>(context));
            });
        });
    })
    .Build();

host.Run();
