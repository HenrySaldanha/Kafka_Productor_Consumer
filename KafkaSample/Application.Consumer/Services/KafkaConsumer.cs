using Confluent.Kafka;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Application.Services
{
    public class KafkaConsumer
    {
        public const String HOST = "127.0.0.1:9092";
        public const String KAFKA_TOPIC_NAME = "test-topic";
        public const String CONSUMER_GROUP_ID = "test-consumer-group";

        public void ReciveMessage()
        {
            Console.WriteLine("Waiting...");

            Log.Logger = new LoggerConfiguration().WriteTo.Console()
                .WriteTo.File(".\\logConsumerKafka-.txt", rollingInterval: RollingInterval.Day).CreateLogger();


            ConsumerConfig conf = new ConsumerConfig
            {
                GroupId = CONSUMER_GROUP_ID,
                BootstrapServers = HOST,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var Consumer = new ConsumerBuilder<Ignore, string>(conf).Build();
            {

                Consumer.Subscribe(KAFKA_TOPIC_NAME);

                CancellationTokenSource Cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true;
                    Cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var Message = Consumer.Consume(Cts.Token);
                            Log.Logger.Information($"Received message '{Message.Value}' from: '{Message.TopicPartitionOffset}'");
                        }
                        catch (ConsumeException e)
                        {
                            Log.Logger.Error($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Consumer.Close();
                }
            }
        }
    }
}
