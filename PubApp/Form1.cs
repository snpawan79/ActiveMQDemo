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
        int sequence = 0;
        IMessageProducer producer = null;
        ISession session = null;
        public Form1()
        {
            InitializeComponent();
            sequence = 0;
            IConnectionFactory connectionFactory = EIPBusConnection.CreateOrGetConnection(new Uri("activemq:tcp://localhost:61616"));
            IConnection connection = connectionFactory.CreateConnection("admin", "admin");
            session = connection.CreateSession();
            IDestination destination = SessionUtil.GetQueue(session, "DemoQueue");
            producer = producer = session.CreateProducer(destination);
            connection.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            producer.DeliveryMode = MsgDeliveryMode.NonPersistent;
            var request = session.CreateTextMessage(textBox1.Text);
            request.NMSCorrelationID = Guid.NewGuid().ToString();
            if (!request.Properties.Contains("NMSXGroupID"))
                request.Properties.SetString("NMSXGroupID", comboBox1.Text);
            
            producer.Send(request);
            // connection.Stop();



        }
    }
}
