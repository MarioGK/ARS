// See https://aka.ms/new-console-template for more information

using MessageBus;

var messageBus = new MessageBus.MessageBus();

messageBus.Listen<Packet1>().Subscribe(packet1 => { Console.WriteLine($"Recieved packet1: {packet1.Data}"); });

messageBus.Listen<Packet2>().Subscribe(packet2 =>
{
    Console.WriteLine($"Recieved packet2: {packet2.Data} {packet2.Data2}");
});

while (true)
{
    var random = Random.Shared.Next(100);

    if (random > 70)
    {
        messageBus.Publish(new Packet1("Hello World1"));
    }
    else
    {
        messageBus.Publish(new Packet2("Hello World2", "Hello World3"));
    }
}

namespace MessageBus
{
    internal record Packet1(string Data);

    internal record Packet2(string Data, string Data2);
}