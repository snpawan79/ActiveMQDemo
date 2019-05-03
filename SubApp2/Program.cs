using System;
using ActiveMQ.Connection;
using Apache.NMS;

namespace ListenerConsole
{
    class Program
    {
        private const string URI = "tcp://localhost:61616";
        private const string DESTINATION = "test.queue";

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Waiting for messages");

                IConnectionFactory connectionFactory = EIPBusConnection.CreateOrGetConnection(new Uri("activemq:tcp://localhost:61616"));
                IConnection connection = connectionFactory.CreateConnection("admin", "admin");
                connection.Start();
                ISession session = connection.CreateSession();
                IDestination dest = session.GetQueue("DemoQueue");
                IMessageConsumer consumer = session.CreateConsumer(dest);
                // Read all messages off the queue
                while (ReadNextMessageQueue(session, consumer))
                {
                    Console.WriteLine("Successfully read message");
                }

                Console.WriteLine("Finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Press <ENTER> to exit.");
                Console.Read();
            }

        }

        static bool ReadNextMessageQueue(ISession session, IMessageConsumer consumer)
        {

            IMessage msg = consumer.Receive();
            if (msg is ITextMessage)
            {
                ITextMessage txtMsg = msg as ITextMessage;
                string body = txtMsg.Text;
                //Console.WriteLine(txtMsg.ToString());
                Console.WriteLine($"Received message: {txtMsg.Text} for work order {txtMsg.Properties["NMSXGroupID"].ToString()}");

                return true;
            }
            else
            {
                Console.WriteLine("Unexpected message type: " + msg.GetType().Name);
            }
            return false;
        }
    }
}