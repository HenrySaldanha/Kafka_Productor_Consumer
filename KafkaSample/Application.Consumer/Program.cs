using Application.Services;
using System;

namespace Application
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("*** Hi, my name is Henry Saldanha. Enjoy this program. :) ***\n\n");
            KafkaConsumer Consumer = new KafkaConsumer();

            Consumer.ReciveMessage();
        }
    }
}
