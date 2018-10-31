using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingTester
{
    public class PingItem
    {
        string uid_ = "";
        DateTime? nd = null;
        public PingItem()
        {
            uid_ = Guid.NewGuid().ToString("N");
            StartTime = DateTime.Now;
        }
        public void AddLogItem(PingLogItem pii)
        {
            pii.ID = TotalAttemps;
            PingLog.Add(pii);
            while (PingLog.Count > pingMemory)
            {
                PingLog.Remove(PingLog.OrderBy(i => i.ID).First());
            }
        }
        public int pingMemory = 100;
        public string Host { set; get; }
        public List<string> DNSAddess = new List<string>();
        public string DNSHostName { set; get; }
        public long TotalAttemps = 0;
        public long TotalFails = 0;
        public DateTime StartTime { set; get; }
        public bool Paused = false;
        public bool Failing { get { return PingLog.Count > 0 ? PingLog.OrderByDescending(i => i.ID).First().Failed : false; } }
        public List<PingLogItem> PingLog = new List<PingLogItem>();
        public int AvgRoundTrip
        {
            get
            {
                return PingLog.Where(i => i.RoundTrip >= 0).Count() > 0 ? Convert.ToInt32(PingLog.Where(i => i.RoundTrip >= 0).Sum(i => i.RoundTrip) / PingLog.Where(i => i.RoundTrip >= 0).Count()) : -1;
            }
        }
        public int LastRoundTrip { get { return PingLog.Count > 0 ? Convert.ToInt32(PingLog.OrderByDescending(i => i.ID).First().RoundTrip) : -1; } }
        public string LastPingIP { get { return PingLog.Count > 0 ? PingLog.Where(i => i.ReplyAddress != "").OrderByDescending(i => i.ID).First().ReplyAddress : ""; } }
        public int CurrentLoss
        {
            get
            {
                if (PingLog.Count > 0)
                {
                    double total = PingLog.Count();
                    double failed = PingLog.Where(i => i.Failed == true).Count();
                    double loss = (failed / total) * 100.0;
                    return Convert.ToInt32(loss);
                }
                else { return -1; }
            }
        }
        public int TotalLoss
        {
            get
            {
                double att = Convert.ToDouble(TotalAttemps);
                double fai = Convert.ToDouble(TotalFails);
                double los = (fai / att) * 100;
                return (int)los;
            }
        }
        public string uid { get { return uid_; } }
        public List<TraceItem> TraceRoute = new List<TraceItem>();
        public bool Traced = false;
        public DateTime? LastFail { get { return PingLog.Where(i => i.Failed).Count() > 0 ? PingLog.Where(i => i.Failed).OrderByDescending(i => i.TimeStamp).First().TimeStamp : nd; } }
        public DateTime? LastSuccess { get { return PingLog.Where(i => !i.Failed).Count() > 0 ? PingLog.Where(i => !i.Failed).OrderByDescending(i => i.TimeStamp).First().TimeStamp : nd; } }
        public TimeSpan RunTime { get { return DateTime.Now - StartTime; } }
        public bool PauseTrace = true;
    }
    public class TraceItem
    {
        public void AddLogItem(int RoundTrip)
        {
            TraceLogItem tli = new TraceLogItem();
            tli.RoundTrip = RoundTrip;
            TraceLog.Add(tli);
            while (TraceLog.Count > pingMemory)
            {
                TraceLog.Remove(TraceLog.OrderBy(i => i.TimeStamp).First());
            }
        }
        public int pingMemory = 100;
        public int Index = 0;
        public string Address = "";
        public string DNSHost = "";
        public long TotalAttemps = 0;
        public long TotalFails = 0;
        public List<string> DNSAddess = new List<string>();
        public List<TraceLogItem> TraceLog = new List<TraceLogItem>();
        public int RoundTrip { get { return TraceLog.Count() > 0 ? TraceLog.OrderByDescending(t => t.TimeStamp).First().RoundTrip : -1; } }
        public int CurrentLoss
        {
            get
            {
                if (TraceLog.Count > 0)
                {
                    double total = TraceLog.Count();
                    double failed = TraceLog.Where(i => i.RoundTrip == -1).Count();
                    double loss = (failed / total) * 100.0;
                    return Convert.ToInt32(loss);
                }
                else { return -1; }
            }
        }
        public int TotalLoss
        {
            get
            {
                double att = Convert.ToDouble(TotalAttemps);
                double fai = Convert.ToDouble(TotalFails);
                double los = (fai / att) * 100;
                return (int)los;
            }
        }
    }
    public class TraceLogItem
    {
        public TraceLogItem()
        {
            TimeStamp = DateTime.Now;
        }
        public DateTime TimeStamp { set; get; }
        public int RoundTrip = -1;
    }
    public class PingLogItem
    {
        public PingLogItem()
        {
            TimeStamp = DateTime.Now;
        }
        public string Host { set; get; }
        public bool Failed = false;
        public long ID { set; get; }
        public string Result { set; get; }
        public DateTime TimeStamp;
        public string Message { get; set; }
        public string ReplyAddress { get; set; }
        public int PacketSize = -1;
        public int TimeOut = -1;
        public long RoundTrip = -1;
    }
    public class LogBuffer
    {
        public string data = "";
        public string name = "";
    }
    public class FastPingItem
    {
        public string host;
        public int response;
        public long id { get; set; }
    }
    public class MyFormState
    {
        public string boxMachine_text { get; set; }
        public List<string> lstMachines_items { get; set; }
        public decimal numPacket { get; set; }
        public decimal numTicker { get; set; }
        public Point winpos { get; set; }
        public Size winsize { get; set; }
        public int splitpos { get; set; }
        public string logfolder { get; set; }
        public FormWindowState winstate { get; set; }
        public List<PingItem> lstPing { get; set; }
        public decimal numPingmem { get; set; }
        public bool chkWriteLog { get; set; }
        public int changeLog { get; set; }
    }
    public class PortItem
    {
        public string host;
        public int port;
        public bool? open = null;
    }
    public class PortInfo
    {
        public string Name { get; internal set; }
        public string Port { get; internal set; }
        public string Protocol { get; internal set; }
    }
}
