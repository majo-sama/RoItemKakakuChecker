using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RoItemKakakuChecker
{
    public class MyNetworkInterface
    {
        public string Name { get => networkInterface.Name; }
        public IPAddress Address { get; set; }

        private NetworkInterface networkInterface;

        public MyNetworkInterface(NetworkInterface networkInterface, IPAddress address)
        {
            this.networkInterface = networkInterface;
            this.Address = address;
        }
    }
}
