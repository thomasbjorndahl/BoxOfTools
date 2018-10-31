using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingTester
{
    public class StatusPanel : Panel
    {
        PingItem pi = null;
        CheckBox chkHost = new CheckBox();
        CheckBox chkRoute = new CheckBox();
        Button btnReTrace = new Button();
        PingGrapgh pg = new PingGrapgh();
        public StatusPanel(PingItem pi)
        {
            this.pi = pi;
            this.DoubleBuffered = true;
            this.Paint += StatusPanel_Paint;
            chkHost.Location = new Point(10000, 10000);
            chkRoute.Location = new Point(10000, 10000);
            pg.Location = new Point(10000, 10000);
            btnReTrace.Location = new Point(10000, 10000);
            btnReTrace.Size = new System.Drawing.Size(70, 21);
            chkHost.CheckedChanged += chkHost_CheckedChanged;
            chkRoute.CheckedChanged += chkRoute_CheckedChanged;
            btnReTrace.Click += btnReTrace_Click;
            chkHost.Text = "Ping Host";
            chkRoute.Text = "Ping Trace";
            btnReTrace.Text = "Re-Trace";
            this.Controls.Add(chkHost);
            this.Controls.Add(chkRoute);
            this.Controls.Add(pg);
            this.Controls.Add(btnReTrace);
        }
        void btnReTrace_Click(object sender, EventArgs e)
        {
            pi.TraceRoute = new List<TraceItem>();
            pi.Traced = false;
        }
        void chkRoute_CheckedChanged(object sender, EventArgs e)
        {
            pi.PauseTrace = !chkRoute.Checked;
        }
        void chkHost_CheckedChanged(object sender, EventArgs e)
        {
            pi.Paused = !chkHost.Checked;
        }
        private void StatusPanel_Paint(object sender, PaintEventArgs e)
        {
            if (pi != null && pi.PingLog.Count > 0)
            {
                int y = 12;
                int x = 12;
                int fontSize = 8;
                var b = Brushes.Black;
                if (pi.PingLog.OrderByDescending(p => p.TimeStamp).First().Failed) { b = Brushes.Red; }
                chkHost.Location = new Point(x, y);
                y += 30;
                y = DrawPingDetails(e, y, x, pi, fontSize, b);
                chkRoute.Location = new Point(x, y);
                btnReTrace.Location = new Point(150, y);
                y += 30;
                y = DrawTraceDetails(e, y, x, fontSize);
            }
        }
        private int DrawTraceDetails(PaintEventArgs e, int y, int x, int fontSize)
        {
            if (pi.TraceRoute.Count() > 0)
            {
                var b = Brushes.Black;
                e.Graphics.DrawString("Trace route for " + pi.Host, new Font("Arial", fontSize, FontStyle.Bold), b, new Point(x, y));
                y += fontSize + 12;
                e.Graphics.DrawString("TTL", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(x, y));
                e.Graphics.DrawString("Ping", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(40, y));
                e.Graphics.DrawString("CurrentLoss", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(85, y));
                e.Graphics.DrawString("Attempt", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(170, y));
                e.Graphics.DrawString("Fail", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(240, y));
                e.Graphics.DrawString("TotalLoss", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(280, y));
                e.Graphics.DrawString("Address", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(350, y));
                e.Graphics.DrawString("DNSName", new Font("Arial", fontSize, FontStyle.Bold), b, new Point(450, y));
                y += fontSize + 6;
                e.Graphics.DrawLine(Pens.Black, new Point(x, y), new Point(600, y));
                y += 3;
                foreach (TraceItem ti in pi.TraceRoute)
                {
                    b = ti.RoundTrip == -1 ? Brushes.Red : Brushes.Black;
                    string t = string.Format("{0}", ti.Index);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
                    t = string.Format("{0}ms", ti.RoundTrip);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(45, y));
                    t = string.Format("{0}%", ti.CurrentLoss);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(105, y));


                    t = string.Format("{0}", ti.TotalAttemps);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(170, y));

                    t = string.Format("{0}", ti.TotalFails);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(240, y));

                    t = string.Format("{0}%", ti.TotalLoss);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(290, y));


                    string ip = "";
                    foreach (var i in ti.DNSAddess) { ip = ip == "" ? i : ";" + i; }
                    if (ip == "") { ip = ti.Address; }
                    t = string.Format("{0}", ip);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(350, y));
                    t = string.Format("{0}", ti.DNSHost);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(450, y));
                    y += fontSize + 7;
                    e.Graphics.DrawLine(Pens.Black, new Point(x, y), new Point(600, y));
                    y += 3;
                }
            }
            return y;
        }
        private int DrawPingDetails(PaintEventArgs e, int y, int x, PingItem pi, int fontSize, Brush b)
        {
            int pgy1 = y;
            e.Graphics.DrawString(pi.Host, new Font("Arial", fontSize, FontStyle.Bold), b, new Point(x, y));
            y += fontSize + 4;
            string t = string.Format("LastRT: {0}", pi.LastRoundTrip);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            t = string.Format("AvgRT: {0}", pi.AvgRoundTrip);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            t = string.Format("CurrentLoss: {0}%", pi.CurrentLoss);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            t = string.Format("Attempts: {0}", pi.TotalAttemps);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            t = string.Format("Fails: {0}", pi.TotalFails);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            t = string.Format("TotalLoss: {0}%", pi.TotalLoss);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            t = string.Format("PingIP: {0}", pi.LastPingIP);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            string ip = "";
            foreach (var i in pi.DNSAddess) { ip = ip == "" ? i : ";" + i; }
            t = string.Format("DNSIP: {0}", ip);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            y += fontSize + 4;
            t = string.Format("DNSHost: {0}", pi.DNSHostName);
            e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(x, y));
            int pgy2 = y + fontSize;
            pg.Location = new Point(200, pgy1);
            pg.Size = new System.Drawing.Size(400, pgy2 - pgy1);
            y += fontSize + 40;
            return y;
        }
        public void RefreshData(PingItem pi)
        {
            this.pi = pi;
            chkHost.Checked = !pi.Paused;
            chkRoute.Checked = !pi.PauseTrace;
            pg.Pings = new List<int>();
            foreach (PingLogItem pl in pi.PingLog) { pg.Pings.Add((int)pl.RoundTrip); }
            this.Refresh();
        }
    }
    public class PingGrapgh : Panel
    {
        public List<int> Pings = new List<int>();
        public PingGrapgh()
        {
            this.DoubleBuffered = true;
            this.Paint += PingGrapgh_Paint;
        }
        void PingGrapgh_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                int offset = 30;
                int w = this.Width - 1 - offset;
                int h = this.Height - 3;
                e.Graphics.DrawLine(Pens.Black, new Point(offset, 1), new Point(offset, h));
                e.Graphics.DrawLine(Pens.Black, new Point(offset, h), new Point(this.Width, h));

                string s = RO(Pings.Max()).ToString();
                SizeF sz = e.Graphics.MeasureString(s, new Font("Arial", 8));
                int x = offset - (int)sz.Width - 3;
                double y = 0;
                e.Graphics.DrawString(s, new Font("Arial", 8), Brushes.Blue, new Point(x, (int)y));

                s = RO(Pings.Max() / 3 * 2).ToString();
                sz = e.Graphics.MeasureString(s, new Font("Arial", 8));
                x = offset - (int)sz.Width - 3;
                y = (Convert.ToDouble(h - (sz.Height)) / 3.0 * 1.0);
                e.Graphics.DrawString(s, new Font("Arial", 8), Brushes.Blue, new Point(x, (int)y));

                s = RO(Pings.Max() / 3 * 1).ToString();
                sz = e.Graphics.MeasureString(s, new Font("Arial", 8));
                x = offset - (int)sz.Width - 3;
                y = (Convert.ToDouble(h - (sz.Height)) / 3.0 * 2.0);
                e.Graphics.DrawString(s, new Font("Arial", 8), Brushes.Blue, new Point(x, (int)y));

                s = "0";
                sz = e.Graphics.MeasureString(s, new Font("Arial", 8));
                x = offset - (int)sz.Width - 3;
                y = (Convert.ToDouble(h - (sz.Height)));
                e.Graphics.DrawString(s, new Font("Arial", 8), Brushes.Blue, new Point(x, (int)y));

                if (Pings.Count > 0)
                {
                    double max = Pings.Max();
                    double hh = h;
                    double ww = Convert.ToDouble(w) / Convert.ToDouble(Pings.Count);
                    double px1 = Convert.ToDouble(offset);
                    double py1 = max == 0 ? -1 : Convert.ToDouble(h) - ((Convert.ToDouble(Pings[0]) / max) * hh);
                    foreach (int i in Pings)
                    {
                        var p = i > -1 ? Pens.Blue : Pens.Red;
                        double px2 = px1 + ww;
                        double py2 = Convert.ToDouble(h) - ((Convert.ToDouble(i) / max) * hh);
                        py2 = py2 > h ? h + 2 : py2;
                        e.Graphics.DrawLine(p, new Point((int)px1, (int)py1), new Point((int)px2, (int)py2));
                        px1 = px2;
                        py1 = py2;
                    }
                }
            }
            catch { }
        }
        private int RO(int i)
        {
            return i > 5 ? ((int)Math.Round(i / 10.0)) * 10 : i;
        }
    }
    public class FastPingPanel : Panel
    {
        public FastPingPanel()
        {
            this.DoubleBuffered = true;
            this.Paint += FastPingPanel_Paint;
        }
        long counter = 0;
        long fail = 0;
        string summary = "";
        List<FastPingItem> fpi = new List<FastPingItem>();
        public void AddItem(FastPingItem fp)
        {
            fp.id = counter += 1;
            fpi.Add(fp);
            fail += fp.response == -1 ? 1 : 0;
            while (fpi.Count > 100)
            {
                fpi.Remove(fpi.OrderBy(i => i.id).First());
            }
            this.Refresh();
        }
        public void ClearItems()
        {
            double f = Convert.ToDouble(fail);
            double c = Convert.ToDouble(counter);
            double l = (f/c)*100;
            summary = string.Format("{0} pings, {1} fails, {2}% loss", counter, fail, (int)l);
            fpi = new List<FastPingItem>();
            counter = 0;
            fail = 0;
            this.Refresh();
        }
        void FastPingPanel_Paint(object sender, PaintEventArgs e)
        {
            int y = 20;
            int fontSize = 8;
            if (counter == 0)
            {
                e.Graphics.DrawString(summary, new Font("Arial", fontSize), Brushes.Black, new Point(20, y));
            }
            else
            {
                foreach (FastPingItem fp in fpi.OrderByDescending(i => i.id))
                {
                    var b = fp.response > -1 ? Brushes.Black : Brushes.Red;
                    string t = string.Format("Count: {0}", fp.id);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(20, y));
                    t = string.Format("Time: {0}", fp.response);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(80, y));
                    t = string.Format("Host: {0}", fp.host);
                    e.Graphics.DrawString(t, new Font("Arial", fontSize), b, new Point(150, y));
                    y += fontSize + 4;
                }
            }
        }
    }
}