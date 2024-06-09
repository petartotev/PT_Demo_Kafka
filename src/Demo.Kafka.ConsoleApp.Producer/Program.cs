using Confluent.Kafka;

namespace Demo.Kafka.ConsoleApp.Producer;

public class Program
{
    public static async Task Main()
    {
        var random = new Random();
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            for (int i = 0; i < 100; i++)
            {
                try
                {
                    var dr = await producer.ProduceAsync("my-topic", new Message<Null, string> { Value = $"Hello Kafka {i}!" });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");

                    Thread.Sleep(random.Next(3, 6) * 1000);
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
