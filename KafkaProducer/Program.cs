using Confluent.Kafka;
using Confluent.Kafka.Admin;
using System;
using System.Threading.Tasks;

namespace KafkaProducer
{
    class Program
    {
        static string topicName = "TopicA";
        static string bootstrapServers = "localhost:9092,localhost:9093, localhost:9094";

        static void Main(string[] args)
        {
            CreateTopics().Wait();
            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = "Test",

            };
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                for(int idx = 0;idx < 10;++idx)
                    producer.Produce(topicName, new Message<Null, string> { Value = $"Hello world times {idx}" });
                producer.Flush();
            }
        }

        private static async Task CreateTopics()
        {
            using (var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = bootstrapServers }).Build())
            {
                try
                {
                    await adminClient.CreateTopicsAsync(new TopicSpecification[] {
                        new TopicSpecification { Name = topicName, ReplicationFactor = 3, NumPartitions = 3 } 
                    });
                }
                catch (CreateTopicsException e)
                {
                    Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
                }
            }
        }
    }
}
