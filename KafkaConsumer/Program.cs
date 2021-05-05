using Confluent.Kafka;
using System;
using System.Collections.Generic;

namespace KafkaConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092,localhost:9093, localhost:9094",
                GroupId = "foo",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            var topics = new List<String>() {"TopicA"};
            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                Console.WriteLine($"Consuming topics: {topics.ToArray()}");
                consumer.Subscribe(topics);
                while (true)
                {
                    var consumeResult = consumer.Consume(5000);
                    if (consumeResult == null)
                        break;
                    Console.WriteLine($"Topic: {consumeResult.Topic}-{consumeResult.Message.Value}");
                }
            }
            Console.WriteLine("Done");
        }
    }
}
