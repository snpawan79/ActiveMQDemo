using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActiveMQ.Connection;
using Apache.NMS;
using Apache.NMS.Util;

namespace PubApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            IConnectionFactory connectionFactory = EIPBusConnection.CreateOrGetConnection(new Uri("activemq:tcp://localhost:61616"));
            IConnection connection = connectionFactory.CreateConnection("admin", "admin");
            using (var session = connection.CreateSession())
            {
                IDestination destination = SessionUtil.GetQueue(session, "DemoQueue");
                using (var producer = session.CreateProducer(destination))
                {
                    connection.Start();
                    producer.DeliveryMode = MsgDeliveryMode.Persistent;
                    var request = session.CreateTextMessage("Hello World");
                    request.NMSCorrelationID = Guid.NewGuid().ToString();
                    Task.Run(() => producer.Send(request));
                    connection.Stop();
                }
            }

        }
    }
}
