//using Apache.NMS;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActiveMQ.Connection
{
    public static class EIPBusConnection
    {
        public static IConnectionFactory CreateOrGetConnection(Uri connectionUri)
        {
            IConnectionFactory connectionFactory;

            connectionFactory = new NMSConnectionFactory(connectionUri);
            if (connectionFactory.BrokerUri.OriginalString != connectionUri.LocalPath)
                connectionFactory = new NMSConnectionFactory(connectionUri);
            return connectionFactory;
        }
    }
}
