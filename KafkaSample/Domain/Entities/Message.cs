using System;

namespace Domain
{
    public class Message
    {
        private String Content { get; set; }
        private long Timestamp { get; set; }
        private int RequisitionId { get; set; }
        private static Guid MicroserviceId { get; set; }

        public Message()
        {
            Random Rand = new Random();
            Content = "Hello World";
            Timestamp = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            RequisitionId = Rand.Next();
            if(MicroserviceId == Guid.Empty)
            {
                MicroserviceId = Guid.NewGuid();
            }
        }
        public String GetText()
        {
            return $"{Content} [Timestamp:{Timestamp}][RequisitionId:{RequisitionId}][MicroserviceId:{MicroserviceId}]";
        }
    }
}
