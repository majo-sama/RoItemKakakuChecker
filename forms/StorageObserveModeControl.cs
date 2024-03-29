using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoItemKakakuChecker
{
    public partial class StorageObserveModeControl : UserControl
    {
        public StorageObserveModeControl()
        {
            InitializeComponent();
        }

        private void btnObserve_Click(object sender, EventArgs e)
        {


            var he = Dns.GetHostEntry(Dns.GetHostName());
            var addr = he.AddressList.Where((h) => h.AddressFamily == AddressFamily.InterNetwork).ToList();
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            socket.Bind(new IPEndPoint(addr[0], 0));
            socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, 1);
            byte[] ib = new byte[] { 1, 0, 0, 0 };
            byte[] ob = new byte[] { 0, 0, 0, 0 };
            socket.IOControl(IOControlCode.ReceiveAll, ib, ob);//SIO_RCVALL
            byte[] buf = new byte[4096*10];
            int i = 0;
            while (true)
            {
                IAsyncResult iares = socket.BeginReceive(buf, 0, buf.Length, SocketFlags.None, null, null);
                var len = socket.EndReceive(iares);

                if (Ip(buf, 16) == "18.182.57.203")
                {
                    Console.WriteLine("[{0}] Protocol={1} src={2} dst={3} TTL={4} Len={5}"
                      , i++, Proto(buf[9]), Ip(buf, 12), Ip(buf, 16), buf[8], len);

                }


            }
        }
        static string Ip(byte[] buf, int i)
        {
            return string.Format("{0}.{1}.{2}.{3}", buf[i], buf[i + 1], buf[i + 2], buf[i + 3]);
        }
        static string Proto(byte b)
        {
            if (b == 6)
                return "TCP";
            else if (b == 17)
                return "UDP";
            return "Other";
        }
    }
}
