using Confluent.Kafka;
using Domain;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services
{
    public class KafkaProductor
    {
        public const String HOST = "127.0.0.1:9092";
        public const String KAFKA_TOPIC_NAME = "test-topic";
        public const int TIME_DELAY = 1000;
        public static async Task SendMessage()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.Console()
                            .WriteTo.File(".\\logProductorKafka-.txt", rollingInterval: RollingInterval.Day).CreateLogger();

            var Conf = new ProducerConfig { BootstrapServers = HOST };
           

            using var Producer = new ProducerBuilder<Null, string>(Conf).Build();
            {
                try
                {
                    while (true)
                    {
                        Message Msg = new Message();

                        var Message = await Producer.ProduceAsync(KAFKA_TOPIC_NAME,
                            new Message<Null, string> { Value = Msg.GetText() });

                        Log.Logger.Information($"Submited '{Message.Value}' to '{Message.TopicPartitionOffset}'");
                        Thread.Sleep(TIME_DELAY);
                    }
                }
                catch (ProduceException<Null, string> e)
                {
                    Log.Logger.Error($"Error occured: {e.Error.Reason}");
                }
            }

        }
    }
}
