using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PingTester
{
    public class TraceRoute
    {
        byte[] buffer = null;
        int timeout = 2000;
        public TraceRoute(int timeout, int packetSize = 32)
        {
            string data = "";
            while (data.Length < packetSize) { data += "a"; }
            buffer = Encoding.ASCII.GetBytes(data);
            this.timeout = timeout;
        }
        public IEnumerable<string> GetTraceRoute(string hostNameOrAddress)
        {
            return GetTraceRoute(hostNameOrAddress, 1);
        }
        private IEnumerable<string> GetTraceRoute(string hostNameOrAddress, int ttl)
        {
            List<string> result = new List<string>();
            using (Ping pinger = new Ping())
            {
                PingOptions pingerOptions = new PingOptions(ttl, true);
                PingReply reply = default(PingReply);
                try
                {
                    reply = pinger.Send(hostNameOrAddress, timeout, buffer, pingerOptions);
                }
                catch
                {
                    reply = null;
                }
                if (reply != null && reply.Status == IPStatus.Success)
                {
                    result.Add(reply.Address.MapToIPv4().ToString());
                }
                else if (reply != null && (reply.Status == IPStatus.TtlExpired || reply.Status == IPStatus.TimedOut))
                {
                    if (reply.Status == IPStatus.TtlExpired)
                    {
                        result.Add(reply.Address.MapToIPv4().ToString());
                    }
                    IEnumerable<string> tempResult = default(IEnumerable<string>);
                    tempResult = GetTraceRoute(hostNameOrAddress, ttl + 1);
                    result.AddRange(tempResult);
                }
                else
                {
                    result.Add("*.*.*.*");
                }
            }
            return result;
        }
    }
}