using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Moamam.Lib
{
    public class CommonNet
    {

        /// <summary>
        /// 사용자 IP 확인
        /// </summary>
        /// <returns></returns>
        public static string getUserIP()
        {
            string IPAddress = string.Empty;
            IPAddress = GetIPAddress(Dns.GetHostName()).ToString();
            return IPAddress;
        }

        public static IPAddress GetIPAddress(string hostName)
        {
            Ping ping = new Ping();
            var replay = ping.Send(hostName);

            if (replay.Status == IPStatus.Success)
            {
                return replay.Address;
            }
            return null;
        }

        public static IEnumerable<string> GetAddresses()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return (from ip in host.AddressList where ip.AddressFamily == AddressFamily.InterNetwork select ip.ToString()).ToList();
        }
    }
}
