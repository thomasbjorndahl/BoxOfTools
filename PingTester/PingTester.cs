using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Net.Sockets;

namespace PingTester
{
    public partial class PingTester : Form
    {
        MyFormState state = new MyFormState();
        Timer ticker = new Timer();
        Timer fastping = new Timer();
        Timer porttimer = new Timer();
        string dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        DateTime dStart = DateTime.Now;
        List<PingItem> PingItems = new List<PingItem>();
        List<LogBuffer> Log = new List<LogBuffer>();
        List<FastPingItem> FastPingItems = new List<FastPingItem>();
        List<PortItem> PortItems = new List<PortItem>();
        List<PortInfo> InfoItems = new List<PortInfo>();
        StatusPanel statPanel;
        FastPingPanel fpPanel;
        int changeLog = 0;
        public PingTester()
        {
            FixFolderStuff();
            InitializeComponent();
            ticker.Tick += t_Tick;
            ticker.Interval = (int)numTicker.Value;
            ticker.Start();
            fastping.Tick += fastping_Tick;
            fastping.Interval = (int)fpInterval.Value;
            porttimer.Interval = 100;
            porttimer.Tick += Porttimer_Tick;
            porttimer.Start();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            this.Text = assembly.GetName().Name + " v." + fvi.FileVersion + " by Shadow IT";
            statPanel = new StatusPanel(null);
            statPanel.Dock = DockStyle.Fill;
            tabDetails.Controls.Add(statPanel);
            fpPanel = new FastPingPanel();
            fpPanel.Dock = DockStyle.Fill;
            fpFeed.Controls.Add(fpPanel);
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            ReadPortInfo();
        }
        private void ReadPortInfo()
        {
            try
            {
                psStdPorts.Items.Clear();
                using (var sr = new StreamReader("etc/stdports.txt"))
                {
                    while (sr.Peek() > -1)
                    {
                        var s = sr.ReadLine().Split(';');
                        int tst = 0;
                        if (s.Count() >= 2 && int.TryParse(s[1], out tst))
                        {
                            InfoItems.Add(new PortInfo { Name = s[0], Port = s[1] });
                            psStdPorts.Items.Add(string.Format("{0} ({1})", s[1], s[0]), true);
                        }
                    }
                }
            }
            catch { MessageBox.Show("Error reading stdports file!"); }
        }
        private void ShowChangeLog()
        {

        }
        private void FixFolderStuff()
        {
            if (!Directory.Exists(GetDataFolder()))
            {
                Directory.CreateDirectory(GetDataFolder());
            }
            if (!File.Exists(string.Format("{0}config.xml", GetDataFolder())))
            {
                string s = "";
                s += "<?xml version=\"1.0\" encoding=\"utf-8\"?>                                                                          \n";
                s += "<MyFormState xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\n";
                s += "  <lstMachines_items />                                                                                             \n";
                s += "  <numPacket>32</numPacket>                                                                                         \n";
                s += "  <numTicker>2000</numTicker>                                                                                       \n";
                s += "  <winpos>                                                                                                          \n";
                s += "    <X>200</X>                                                                                                      \n";
                s += "    <Y>200</Y>                                                                                                      \n";
                s += "  </winpos>                                                                                                         \n";
                s += "  <winsize>                                                                                                         \n";
                s += "    <Width>1000</Width>                                                                                             \n";
                s += "    <Height>600</Height>                                                                                            \n";
                s += "  </winsize>                                                                                                        \n";
                s += "  <splitpos>250</splitpos>                                                                                          \n";
                s += "  <logfolder></logfolder>                                                                                           \n";
                s += "  <winstate>Normal</winstate>                                                                                       \n";
                s += "  <lstPing />                                                                                                       \n";
                s += "  <numPingmem>100</numPingmem>                                                                                      \n";
                s += "  <chkWriteLog>false</chkWriteLog>                                                                                  \n";
                s += "</MyFormState>                                                                                                      \n";
                using (StreamWriter sw = new StreamWriter(string.Format("{0}config.xml", GetDataFolder())))
                {
                    sw.WriteLine(s);
                }
            }
        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                foreach (PingItem pi in PingItems)
                {
                    if (pi.uid == e.Node.Name)
                    {
                        statPanel.RefreshData(pi);
                    }
                }
            }
            catch { }
        }
        private void t_Tick(object sender, EventArgs e)
        {
            if (dir.Contains("Error!")) { txtFolder.Text = dir; } else { dir = txtFolder.Text; }
            ticker.Stop();
            UpdateTree();
            if (PingItems != null)
            {
                try
                {
                    foreach (PingItem pi in PingItems)
                    {
                        if (treeView1.SelectedNode != null && pi.uid == treeView1.SelectedNode.Name)
                        {
                            statPanel.RefreshData(pi);
                        }
                    }
                }
                catch { }
                List<string> data = new List<string>();
                foreach (LogBuffer l in Log)
                {
                    ErrorFeed.Items.Add(l.data);
                    if (chkLogToFile.Checked) { Task.Run(() => WriteToLogFile(l)); }
                }
                Log = new List<LogBuffer>();
                foreach (PingItem pi in PingItems.Where(i => !i.Paused))
                {
                    Task.Run(() => PingHost(pi));
                }
                foreach (PingItem pi in PingItems.Where(i => !i.Traced))
                {
                    pi.Traced = true;
                    Task.Run(() => TraceRoute(pi));
                }
                foreach (PingItem pi in PingItems.Where(i => !i.PauseTrace))
                {
                    foreach (TraceItem ti in pi.TraceRoute)
                    {
                        Task.Run(() => PingTrace(ti));
                    }
                }
            }
            ticker.Start();

        }
        private void DNSEntry(PingItem pi)
        {
            try
            {
                var host = Dns.GetHostEntry(pi.Host);
                pi.DNSAddess = new List<string>();
                foreach (var a in host.AddressList)
                {
                    pi.DNSAddess.Add(a.MapToIPv4().ToString());
                }
                pi.DNSHostName = host.HostName;
            }
            catch
            {
                pi.DNSAddess.Add("");
                pi.DNSHostName = "";
            }
        }
        private void LogOutput(PingItem pi)
        {
            LogBuffer lb = new LogBuffer();
            //                           This failed if the input was null (Thomas)
            PingLogItem pii = pi.PingLog.Where(i => null != i).OrderByDescending(i => i.ID).First();
            if (pii.Failed)
            {
                string ip = "";
                foreach (var i in pi.DNSAddess) { ip = ip == "" ? i : ";" + i; }
                lb.data = string.Format("\n{0} - {1} - {2} | Count: {3} | Fails: {4} | LastRT: {5} | AvgRT: {6} | PingIP: {7} | DnsIP: {8} | DNSHost: {9}",
                    pii.TimeStamp, pi.Host, pii.Message, pi.TotalAttemps, pi.TotalFails, pii.RoundTrip, pi.AvgRoundTrip, pi.LastPingIP, ip, pi.DNSHostName);
                lb.name = pi.Host;
                Log.Add(lb);
            }
        }
        private static bool FileLocker = false;
        private void WriteToLogFile(LogBuffer l)
        {
            try
            {
                string logdir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (Directory.Exists(dir))
                {
                    logdir = dir;
                }
                else
                {
                    dir = "Error! Defaulting to Desktop...";
                }
                var d = dStart;
                while (FileLocker) { }
                FileLocker = true;
                string path = string.Format("{0}\\pinglogger_{1}_{2}.txt", logdir, l.name,
                    d.Year.ToString() + d.Month.ToString() + d.Day.ToString() + d.Hour.ToString() + d.Minute.ToString() + d.Second.ToString());
                File.AppendAllText(path, l.data + Environment.NewLine);
                FileLocker = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        private void PingHost(PingItem pi)
        {
            using (Ping pinger = new Ping())
            {
                string data = "";
                while (data.Length < numPacket.Value) { data += "a"; }
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = (int)numTicker.Value - 500;
                PingLogItem pii = new PingLogItem();
                pi.pingMemory = (int)numPingMem.Value;
                pi.TotalAttemps += 1;
                pii.PacketSize = buffer.Length;
                pii.TimeOut = timeout;
                try
                {
                    PingReply reply = ConvertAndPingHost(pi.Host, pinger, timeout, buffer);
                    pii.Message = reply == null ? "N/A" : reply.Status.ToString();
                    if (reply != null)
                    {
                        if (reply.Address != null) { pii.ReplyAddress = reply.Address.ToString(); }
                        if (reply.Status != IPStatus.Success)
                        {
                            pi.TotalFails += 1;
                            pii.Failed = true;
                            pii.RoundTrip = -1;
                        }
                        else
                        {
                            pii.RoundTrip = reply.RoundtripTime;
                        }
                    }
                    else
                    {
                        pi.TotalFails += 1;
                        pii.Failed = true;
                        pii.RoundTrip = -1;
                    }
                }
                catch (PingException pex)
                {
                    pii.Message = pex.Message;
                    pi.TotalFails += 1;
                    pii.Failed = true;
                    pii.RoundTrip = -1;
                }
                pi.AddLogItem(pii);
                LogOutput(pi);
            }
        }
        private void PingTrace(TraceItem ti)
        {
            using (Ping pinger = new Ping())
            {
                ti.pingMemory = (int)numPingMem.Value;
                string data = "";
                while (data.Length < numPacket.Value) { data += "a"; }
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                int timeout = (int)numTicker.Value - 500;
                try
                {
                    ti.TotalAttemps += 1;
                    PingReply reply = ConvertAndPingHost(ti.Address, pinger, timeout, buffer);
                    int rt = -1;
                    if (reply != null && reply.Status == IPStatus.Success)
                    {
                        rt = (int)reply.RoundtripTime;
                    }
                    ti.AddLogItem(rt);
                    ti.TotalFails += rt == -1 ? 1 : 0;
                }
                catch
                {
                }
            }
        }
        private void AddMachine(string host)
        {
            if (host != "" && TestMachine(host) && PingItems.Where(p => p.Host == host).Count() == 0)
            {
                var pi = new PingItem();
                pi.Host = host;
                DNSEntry(pi);
                PingItems.Add(pi);
                machineToolStripMenuItem.BackColor = SystemColors.Window;
                RefreshTree();
                machineToolStripMenuItem.Text = "";
            }
            else
            {
                machineToolStripMenuItem.BackColor = Color.LightSalmon;
                if (host != "" && PingItems.Where(p => p.Host == host).Count() == 0 &&
                    MessageBox.Show(host + " does not respond,\ndo you want to add it anyways?", "eew...",
                    MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    var pi = new PingItem();
                    pi.Host = host;
                    DNSEntry(pi);
                    PingItems.Add(pi);
                    machineToolStripMenuItem.BackColor = SystemColors.Window;
                    RefreshTree();
                    machineToolStripMenuItem.Text = "";
                }
            }
        }
        private void RefreshTree()
        {
            treeView1.Nodes.Clear();
            if (PingItems != null)
            {
                foreach (var pi in PingItems)
                {
                    TreeNode tn = new TreeNode(pi.Host);
                    tn.Name = pi.uid;
                    treeView1.Nodes.Add(tn);
                    string ip = "";
                    foreach (var i in pi.DNSAddess) { ip = ip == "" ? i : ";" + i; }
                    AddNodeData(tn, pi.uid + "_lastfail", "LastFail: " + pi.LastFail.ToString());
                    AddNodeData(tn, pi.uid + "_lastsuccess", "LastSuccess: " + pi.LastSuccess.ToString());
                }
            }
        }
        private void AddNodeData(TreeNode tn, string name, string txt)
        {
            TreeNode t = new TreeNode(txt);
            t.Name = name;
            tn.Nodes.Add(t);
        }
        private void UpdateTree()
        {
            if (PingItems != null)
            {
                foreach (var pi in PingItems)
                {
                    foreach (TreeNode tn in treeView1.Nodes)
                    {
                        if (tn.Name == pi.uid)
                        {
                            tn.BackColor = pi.Failing ? Color.LightSalmon : Color.Empty;
                            tn.Text = pi.Host + " (" + pi.LastRoundTrip + "ms)";
                            string ip = "";
                            foreach (var i in pi.DNSAddess) { ip = ip == "" ? i : ";" + i; }
                            UpdateNodeData(tn, pi.uid + "_lastfail", "LastFail: " + pi.LastFail.ToString());
                            UpdateNodeData(tn, pi.uid + "_lastsuccess", "LastSuccess: " + pi.LastSuccess.ToString());
                            break;
                        }
                    }
                }
            }
        }
        private void UpdateNodeData(TreeNode tn, string name, string txt)
        {
            foreach (TreeNode t in tn.Nodes)
            {
                if (t.Name == name)
                {
                    t.Text = txt;
                    break;
                }
            }
        }
        private bool TestMachine(string name)
        {
            using (Ping pinger = new Ping())
            {
                try
                {
                    string b = "";
                    while (b.Length < 32) { b += "a"; }
                    byte[] buffer = Encoding.ASCII.GetBytes(b);
                    PingReply reply = ConvertAndPingHost(name, pinger, (int)numTicker.Value - 500, buffer);
                    if (reply != null && reply.Status == IPStatus.Success)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
        private PingReply ConvertAndPingHost(string name, Ping pinger, int timeout, byte[] buffer)
        {
            PingReply reply;
            Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
            MatchCollection result = ip.Matches(name);
            if (result.Count == 1)
            {
                IPAddress i = IPAddress.Parse(name);
                reply = pinger.Send(i, timeout, buffer);
            }
            else
            {
                try
                {
                    reply = pinger.Send(name, timeout, buffer);

                }
                catch
                {
                    reply = null;
                }
            }

            return reply;
        }
        private void numTicker_ValueChanged(object sender, EventArgs e)
        {
            ticker.Interval = (int)numTicker.Value;
        }
        private void numPacket_MouseDown(object sender, MouseEventArgs e)
        {
            numPacket.Increment = numPacket.Value;
        }
        private void PingTester_Load(object sender, EventArgs e)
        {
            if (File.Exists(string.Format("{0}config.xml", GetDataFolder())))
            {
                try { loadConfig(); }
                catch { }
                try { this.Location = state.winpos; }
                catch { }
                try { this.Size = state.winsize; }
                catch { }
                try { this.WindowState = state.winstate; }
                catch { }
                try { splitContainer1.SplitterDistance = state.splitpos; }
                catch { }
                try { txtFolder.Text = state.logfolder; }
                catch { }
                try { numTicker.Value = state.numTicker; }
                catch { }
                try { numPacket.Value = state.numPacket; }
                catch { }
                try { numPingMem.Value = state.numPingmem; }
                catch { }
                try { chkLogToFile.Checked = state.chkWriteLog; }
                catch { }
                try { PingItems = state.lstPing; }
                catch { }
                try { RefreshTree(); }
                catch { }
                try { changeLog = state.changeLog; }
                catch { }
            }
            if (txtFolder.Text == "") { dir = txtFolder.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); }
            ShowChangeLog();
        }
        private void loadConfig()
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(MyFormState));
                using (FileStream fs = File.OpenRead(string.Format("{0}config.xml", GetDataFolder())))
                {
                    state = (MyFormState)ser.Deserialize(fs);
                }
            }
            catch { }
        }
        private void writeConfig()
        {
            using (StreamWriter sw = new StreamWriter(string.Format("{0}config.xml", GetDataFolder())))
            {
                state.lstMachines_items = new List<string>();
                state.numPacket = numPacket.Value;
                state.numTicker = numTicker.Value;
                state.numPingmem = numPingMem.Value;
                state.chkWriteLog = chkLogToFile.Checked;
                state.lstPing = PingItems;
                state.winstate = this.WindowState;
                if (this.WindowState != FormWindowState.Maximized)
                {
                    state.winpos = this.Location;
                    state.winsize = this.Size;
                    state.splitpos = splitContainer1.SplitterDistance;
                }
                state.logfolder = txtFolder.Text;
                state.changeLog = changeLog;
                XmlSerializer ser = new XmlSerializer(typeof(MyFormState));
                ser.Serialize(sw, state);
            }
        }
        private string GetDataFolder()
        {
            return string.Format("{0}\\PingTester\\", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));
        }
        private void PingTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeConfig();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sure you want to wipe all from list..?", "eeh...", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {

            }
        }
        private void btnChgFolder_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFolder.Text = fbd.SelectedPath;
            }
        }
        private void machineToolStripMenuItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AddMachine(((ToolStripTextBox)sender).Text);
            }
        }
        private void removeSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PingItems.Remove(PingItems.Where(p => p.uid == treeView1.SelectedNode.Name).First());
                RefreshTree();
            }
            catch { }
        }
        private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Sure you want to wipe entire list?", "eeeh", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    PingItems = new List<PingItem>();
                    RefreshTree();
                }
            }
            catch { }
        }
        private void TraceRoute(PingItem pi)
        {
            var tr = new TraceRoute((int)numTicker.Value - 500).GetTraceRoute(pi.Host);
            int i = 0;
            Ping pinger = new Ping();
            foreach (var t in tr)
            {
                TraceItem ti = new TraceItem();
                ti.Index = i += 1;
                ti.Address = t;
                if (!t.Contains('*'))
                {
                    try
                    {
                        var host = Dns.GetHostEntry(ti.Address);
                        foreach (var a in host.AddressList)
                        {
                            ti.DNSAddess.Add(a.MapToIPv4().ToString());
                        }
                        ti.DNSHost = host.HostName;
                    }
                    catch { }
                    try
                    {
                        for (int x = 0; x < 3; x++)
                        {
                            ti.TotalAttemps += 1;
                            PingReply p = pinger.Send(t);
                            int rt = -1;
                            if (p.Status == IPStatus.Success)
                            {
                                rt = (int)p.RoundtripTime;
                            }
                            ti.AddLogItem(rt);
                            ti.TotalFails += rt == -1 ? 1 : 0;
                        }
                    }
                    catch { }
                }
                pi.TraceRoute.Add(ti);
            }
        }
        private void clearFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ErrorFeed.Items.Clear();
        }

        //Fastping
        void fastping_Tick(object sender, EventArgs e)
        {
            fastping.Stop();
            FastPingItem fp = new FastPingItem();
            fp.host = fpHost.Text;
            using (Ping pinger = new Ping())
            {
                try
                {
                    string b = "";
                    while (b.Length < 32) { b += "a"; }
                    byte[] buffer = Encoding.ASCII.GetBytes(b);
                    PingReply reply = ConvertAndPingHost(fp.host, pinger, 1000, buffer);
                    if (reply != null && reply.Status == IPStatus.Success)
                    {
                        fp.response = (int)reply.RoundtripTime;
                    }
                    else
                    {
                        fp.response = -1;
                    }
                }
                catch
                {
                    fp.response = -1;
                }
            }
            fpPanel.AddItem(fp);
            if (fpRunning) { fastping.Start(); }
        }
        private void fpInterval_ValueChanged(object sender, EventArgs e)
        {
            fastping.Interval = (int)fpInterval.Value;
        }
        bool fpRunning = false;
        private void fpStartStop_Click(object sender, EventArgs e)
        {
            if (fpRunning)
            {
                fastping.Stop();
                fpRunning = false;
                fpStartStop.Text = "Start";
                fpPanel.ClearItems();
            }
            else if (TestMachine(fpHost.Text))
            {
                fpHost.BackColor = SystemColors.Window;
                fastping.Start();
                fpRunning = true;
                fpStartStop.Text = "Stop";
            }
            else
            {
                fpHost.BackColor = Color.LightSalmon;
            }
        }

        //PortScanner
        private void psFromPort_ValueChanged(object sender, EventArgs e)
        {
            psToPort.Value = psFromPort.Value >= psToPort.Value ? psFromPort.Value + 1 : psToPort.Value;
        }
        private void psToPort_ValueChanged(object sender, EventArgs e)
        {
            psFromPort.Value = psToPort.Value <= psFromPort.Value ? psToPort.Value - 1 : psFromPort.Value;
        }
        private void psRangeStart_Click(object sender, EventArgs e)
        {
            scanPorts(psHost.Text, null, (int)psFromPort.Value, (int)psToPort.Value);
        }
        private void psSingleStart_Click(object sender, EventArgs e)
        {
            scanPorts(psHost.Text, null, (int)psPort.Value, (int)psPort.Value);
        }
        private void scanPorts(string host, int[] sel = null, int from = 88, int to = 80)
        {
            var dt = new DataTable();
            var h = new PingItem();
            h.Host = host;
            PingHost(h);
            bool ok = !h.Failing;
            if (!ok)
            {
                if (MessageBox.Show("Host does not respond to ping!\nDo you want to continue?", "Warning!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ok = true;
                }
            }
            if (ok)
            {
                PortItems.Clear();
                if (sel == null)
                {
                    for (int i = from; i <= to; i++)
                    {
                        PortItems.Add((new PortItem { port = i, host = host }));
                    }
                }
                else
                {
                    foreach (var i in sel)
                    {
                        PortItems.Add((new PortItem { port = i, host = host }));
                    }
                }

                dt.Columns.Add("Port");
                dt.Columns.Add("Open");
                foreach (var p in PortItems)
                {
                    var r = dt.Rows.Add();
                    if (InfoItems.Where(t => t.Port == p.port.ToString()).Count() > 0)
                    {
                        r["Port"] = string.Format("{0} ({1})", p.port.ToString(), InfoItems.Where(t => t.Port == p.port.ToString()).First().Name);
                    }
                    else
                    {
                        r["Port"] = p.port.ToString();
                    }
                    r["Open"] = "Testing...";
                }
                foreach (var p in PortItems)
                {
                    Task.Run(() => CheckPort(p));
                }
            }
            dgPorts.DataSource = dt;
        }
        private void CheckPort(PortItem p)
        {
            p.open = portOpen(p.host, p.port);
        }
        private bool portOpen(string host, int port)
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect(host, port);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        private void Porttimer_Tick(object sender, EventArgs e)
        {
            if (PortItems.Count > 0)
            {
                foreach (var p in PortItems)
                {
                    foreach (DataGridViewRow r in dgPorts.Rows)
                    {
                        if (r.Cells["Port"].Value.ToString().Split(' ')[0] == p.port.ToString() && r.Cells["Open"].Value.ToString() == "Testing...")
                        {
                            r.Cells["Open"].Value = p.open == null ? "Testing..." : p.open == true ? "Open" : "Closed";
                        }
                    }
                }
            }
        }
        private void psStdStart_Click(object sender, EventArgs e)
        {
            List<int> sel = new List<int>();
            foreach (var i in psStdPorts.CheckedItems)
            {
                string p = i.ToString().Split(' ')[0];
                sel.Add(Convert.ToInt32(p));
            }
            scanPorts(psHost.Text, sel.ToArray());
        }

        private void psStdPortAdd_Click(object sender, EventArgs e)
        {
            if (psStdPortPort.Value != 0 && psStdPortTxt.Text != "")
            {
                InfoItems.Add(new PortInfo { Port = psStdPortPort.Value.ToString(), Name = psStdPortTxt.Text });
                InfoItems.Sort((x, y) => Convert.ToInt32(x.Port).CompareTo(Convert.ToInt32(y.Port)));
                using (var sw = new StreamWriter("etc/stdports.txt"))
                {
                    foreach (var i in InfoItems)
                    {
                        sw.WriteLine(i.Name + ";" + i.Port);
                    }
                }
                ReadPortInfo();
            }
        }

        private void psStdPortRemove_Click(object sender, EventArgs e)
        {
            if (psStdPorts.SelectedItems.Count == 1)
            {
                psStdPorts.Items.RemoveAt(psStdPorts.SelectedIndex);
            }
        }
    }
}
