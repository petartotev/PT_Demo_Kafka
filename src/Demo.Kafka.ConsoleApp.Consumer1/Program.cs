using Confluent.Kafka;

namespace Demo.Kafka.ConsoleApp.Consumer1;

public class Program
{
    public static void Main()
    {
        var config = new ConsumerConfig
        {
            GroupId = "group-consumer-1",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe("my-topic");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed message '{cr.Message.Value}' at: '{cr.TopicPartitionOffset}'.");
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
        }
    }
}
