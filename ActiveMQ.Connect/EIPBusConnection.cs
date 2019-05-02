using Apache.NMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveMQ.Connection
{
    public static class EIPBusConnection
    {
        private static IConnectionFactory CreateOrGetConnection(Uri connectionUri)
        {
            IConnectionFactory connectionFactory;

            connectionFactory = new NMSConnectionFactory(connectionUri);

            if (connectionFactory.BrokerUri.OriginalString != connectionUri.LocalPath)
                connectionFactory = new NMSConnectionFactory(connectionUri);

            return connectionFactory;
        }
    }
}
