using System;

namespace ActiveMQ.Connect
{
    public class EIPBusConfig
    {
        public string QueueName { get; set; }
        public string BusEndpoint { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PassiveBusEndpoint { get; set; }
    }
}
