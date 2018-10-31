namespace PingTester
{
    partial class PingTester
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machineToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.tabError = new System.Windows.Forms.TabPage();
            this.ErrorFeed = new System.Windows.Forms.ListBox();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.clearFeedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabConfig = new System.Windows.Forms.TabPage();
            this.chkLogToFile = new System.Windows.Forms.CheckBox();
            this.numPingMem = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numPacket = new System.Windows.Forms.NumericUpDown();
            this.btnChgFolder = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numTicker = new System.Windows.Forms.NumericUpDown();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.tabFastPing = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.fpStartStop = new System.Windows.Forms.Button();
            this.fpFeed = new System.Windows.Forms.GroupBox();
            this.fpInterval = new System.Windows.Forms.NumericUpDown();
            this.fpHost = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.psFromPort = new System.Windows.Forms.NumericUpDown();
            this.psRangeStart = new System.Windows.Forms.Button();
            this.psToPort = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.psPort = new System.Windows.Forms.NumericUpDown();
            this.psSingleStart = new System.Windows.Forms.Button();
            this.dgPorts = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.psHost = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.psStdPorts = new System.Windows.Forms.CheckedListBox();
            this.psStdStart = new System.Windows.Forms.Button();
            this.psStdPortTxt = new System.Windows.Forms.TextBox();
            this.psStdPortPort = new System.Windows.Forms.NumericUpDown();
            this.psStdPortAdd = new System.Windows.Forms.Button();
            this.psStdPortRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabError.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.tabConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPingMem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPacket)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTicker)).BeginInit();
            this.tabFastPing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpInterval)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.psFromPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.psToPort)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.psPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPorts)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.psStdPortPort)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(824, 557);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 24);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(238, 533);
            this.treeView1.TabIndex = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeSelectedToolStripMenuItem,
            this.removeAllToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(238, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.machineToolStripMenuItem});
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.addToolStripMenuItem.Text = "Add";
            // 
            // machineToolStripMenuItem
            // 
            this.machineToolStripMenuItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.machineToolStripMenuItem.Name = "machineToolStripMenuItem";
            this.machineToolStripMenuItem.Size = new System.Drawing.Size(152, 23);
            this.machineToolStripMenuItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.machineToolStripMenuItem_KeyUp);
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.removeSelectedToolStripMenuItem.Text = "RemoveSelected";
            this.removeSelectedToolStripMenuItem.Click += new System.EventHandler(this.removeSelectedToolStripMenuItem_Click);
            // 
            // removeAllToolStripMenuItem
            // 
            this.removeAllToolStripMenuItem.Name = "removeAllToolStripMenuItem";
            this.removeAllToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.removeAllToolStripMenuItem.Text = "RemoveAll";
            this.removeAllToolStripMenuItem.Click += new System.EventHandler(this.removeAllToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabDetails);
            this.tabControl1.Controls.Add(this.tabError);
            this.tabControl1.Controls.Add(this.tabConfig);
            this.tabControl1.Controls.Add(this.tabFastPing);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(582, 557);
            this.tabControl1.TabIndex = 6;
            // 
            // tabDetails
            // 
            this.tabDetails.Location = new System.Drawing.Point(4, 22);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabDetails.Size = new System.Drawing.Size(574, 531);
            this.tabDetails.TabIndex = 0;
            this.tabDetails.Text = "Details";
            this.tabDetails.UseVisualStyleBackColor = true;
            // 
            // tabError
            // 
            this.tabError.Controls.Add(this.ErrorFeed);
            this.tabError.Controls.Add(this.menuStrip2);
            this.tabError.Location = new System.Drawing.Point(4, 22);
            this.tabError.Name = "tabError";
            this.tabError.Padding = new System.Windows.Forms.Padding(3);
            this.tabError.Size = new System.Drawing.Size(574, 531);
            this.tabError.TabIndex = 1;
            this.tabError.Text = "Errorfeed";
            this.tabError.UseVisualStyleBackColor = true;
            // 
            // ErrorFeed
            // 
            this.ErrorFeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ErrorFeed.FormattingEnabled = true;
            this.ErrorFeed.Location = new System.Drawing.Point(3, 27);
            this.ErrorFeed.Name = "ErrorFeed";
            this.ErrorFeed.Size = new System.Drawing.Size(568, 501);
            this.ErrorFeed.TabIndex = 5;
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearFeedToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(3, 3);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(568, 24);
            this.menuStrip2.TabIndex = 6;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // clearFeedToolStripMenuItem
            // 
            this.clearFeedToolStripMenuItem.Name = "clearFeedToolStripMenuItem";
            this.clearFeedToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.clearFeedToolStripMenuItem.Text = "Clear Feed";
            this.clearFeedToolStripMenuItem.Click += new System.EventHandler(this.clearFeedToolStripMenuItem_Click);
            // 
            // tabConfig
            // 
            this.tabConfig.Controls.Add(this.chkLogToFile);
            this.tabConfig.Controls.Add(this.numPingMem);
            this.tabConfig.Controls.Add(this.label2);
            this.tabConfig.Controls.Add(this.numPacket);
            this.tabConfig.Controls.Add(this.btnChgFolder);
            this.tabConfig.Controls.Add(this.label4);
            this.tabConfig.Controls.Add(this.label1);
            this.tabConfig.Controls.Add(this.label3);
            this.tabConfig.Controls.Add(this.numTicker);
            this.tabConfig.Controls.Add(this.txtFolder);
            this.tabConfig.Location = new System.Drawing.Point(4, 22);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfig.Size = new System.Drawing.Size(574, 531);
            this.tabConfig.TabIndex = 2;
            this.tabConfig.Text = "Configuration";
            this.tabConfig.UseVisualStyleBackColor = true;
            // 
            // chkLogToFile
            // 
            this.chkLogToFile.AutoSize = true;
            this.chkLogToFile.Location = new System.Drawing.Point(86, 167);
            this.chkLogToFile.Margin = new System.Windows.Forms.Padding(2);
            this.chkLogToFile.Name = "chkLogToFile";
            this.chkLogToFile.Size = new System.Drawing.Size(143, 17);
            this.chkLogToFile.TabIndex = 7;
            this.chkLogToFile.Text = "Log failed attempts to file";
            this.chkLogToFile.UseVisualStyleBackColor = true;
            // 
            // numPingMem
            // 
            this.numPingMem.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numPingMem.Location = new System.Drawing.Point(86, 114);
            this.numPingMem.Margin = new System.Windows.Forms.Padding(2);
            this.numPingMem.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numPingMem.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numPingMem.Name = "numPingMem";
            this.numPingMem.Size = new System.Drawing.Size(58, 20);
            this.numPingMem.TabIndex = 6;
            this.numPingMem.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "PacketSize";
            // 
            // numPacket
            // 
            this.numPacket.Increment = this.numPacket.Value;
            this.numPacket.Location = new System.Drawing.Point(86, 31);
            this.numPacket.Maximum = new decimal(new int[] {
            32768,
            0,
            0,
            0});
            this.numPacket.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numPacket.Name = "numPacket";
            this.numPacket.Size = new System.Drawing.Size(58, 20);
            this.numPacket.TabIndex = 4;
            this.numPacket.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numPacket.MouseDown += new System.Windows.Forms.MouseEventHandler(this.numPacket_MouseDown);
            // 
            // btnChgFolder
            // 
            this.btnChgFolder.Location = new System.Drawing.Point(429, 185);
            this.btnChgFolder.Name = "btnChgFolder";
            this.btnChgFolder.Size = new System.Drawing.Size(75, 23);
            this.btnChgFolder.TabIndex = 3;
            this.btnChgFolder.Text = "Change";
            this.btnChgFolder.UseVisualStyleBackColor = true;
            this.btnChgFolder.Click += new System.EventHandler(this.btnChgFolder_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "PingMemory";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Interval";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Log folder";
            // 
            // numTicker
            // 
            this.numTicker.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numTicker.Location = new System.Drawing.Point(86, 73);
            this.numTicker.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.numTicker.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numTicker.Name = "numTicker";
            this.numTicker.Size = new System.Drawing.Size(58, 20);
            this.numTicker.TabIndex = 4;
            this.numTicker.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numTicker.ValueChanged += new System.EventHandler(this.numTicker_ValueChanged);
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(86, 188);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(337, 20);
            this.txtFolder.TabIndex = 1;
            // 
            // tabFastPing
            // 
            this.tabFastPing.Controls.Add(this.label6);
            this.tabFastPing.Controls.Add(this.label5);
            this.tabFastPing.Controls.Add(this.fpStartStop);
            this.tabFastPing.Controls.Add(this.fpFeed);
            this.tabFastPing.Controls.Add(this.fpInterval);
            this.tabFastPing.Controls.Add(this.fpHost);
            this.tabFastPing.Location = new System.Drawing.Point(4, 22);
            this.tabFastPing.Name = "tabFastPing";
            this.tabFastPing.Padding = new System.Windows.Forms.Padding(3);
            this.tabFastPing.Size = new System.Drawing.Size(574, 531);
            this.tabFastPing.TabIndex = 3;
            this.tabFastPing.Text = "FastPing";
            this.tabFastPing.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(196, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Interval (ms)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Host";
            // 
            // fpStartStop
            // 
            this.fpStartStop.Location = new System.Drawing.Point(303, 40);
            this.fpStartStop.Name = "fpStartStop";
            this.fpStartStop.Size = new System.Drawing.Size(75, 23);
            this.fpStartStop.TabIndex = 3;
            this.fpStartStop.Text = "Start";
            this.fpStartStop.UseVisualStyleBackColor = true;
            this.fpStartStop.Click += new System.EventHandler(this.fpStartStop_Click);
            // 
            // fpFeed
            // 
            this.fpFeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fpFeed.Location = new System.Drawing.Point(37, 90);
            this.fpFeed.Name = "fpFeed";
            this.fpFeed.Size = new System.Drawing.Size(341, 413);
            this.fpFeed.TabIndex = 2;
            this.fpFeed.TabStop = false;
            this.fpFeed.Text = "Feed";
            // 
            // fpInterval
            // 
            this.fpInterval.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.fpInterval.Location = new System.Drawing.Point(199, 43);
            this.fpInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.fpInterval.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.fpInterval.Name = "fpInterval";
            this.fpInterval.Size = new System.Drawing.Size(75, 20);
            this.fpInterval.TabIndex = 1;
            this.fpInterval.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.fpInterval.ValueChanged += new System.EventHandler(this.fpInterval_ValueChanged);
            // 
            // fpHost
            // 
            this.fpHost.Location = new System.Drawing.Point(37, 43);
            this.fpHost.Name = "fpHost";
            this.fpHost.Size = new System.Drawing.Size(156, 20);
            this.fpHost.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.dgPorts);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.psHost);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(574, 531);
            this.tabPage1.TabIndex = 4;
            this.tabPage1.Text = "PortScanner";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.psFromPort);
            this.groupBox2.Controls.Add(this.psRangeStart);
            this.groupBox2.Controls.Add(this.psToPort);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(28, 171);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 116);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Port Range";
            // 
            // psFromPort
            // 
            this.psFromPort.Location = new System.Drawing.Point(23, 32);
            this.psFromPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.psFromPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.psFromPort.Name = "psFromPort";
            this.psFromPort.Size = new System.Drawing.Size(120, 20);
            this.psFromPort.TabIndex = 0;
            this.psFromPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.psFromPort.ValueChanged += new System.EventHandler(this.psFromPort_ValueChanged);
            // 
            // psRangeStart
            // 
            this.psRangeStart.Location = new System.Drawing.Point(176, 51);
            this.psRangeStart.Name = "psRangeStart";
            this.psRangeStart.Size = new System.Drawing.Size(75, 23);
            this.psRangeStart.TabIndex = 4;
            this.psRangeStart.Text = "Start";
            this.psRangeStart.UseVisualStyleBackColor = true;
            this.psRangeStart.Click += new System.EventHandler(this.psRangeStart_Click);
            // 
            // psToPort
            // 
            this.psToPort.Location = new System.Drawing.Point(23, 74);
            this.psToPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.psToPort.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.psToPort.Name = "psToPort";
            this.psToPort.Size = new System.Drawing.Size(120, 20);
            this.psToPort.TabIndex = 1;
            this.psToPort.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.psToPort.ValueChanged += new System.EventHandler(this.psToPort_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(68, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "To";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.psPort);
            this.groupBox1.Controls.Add(this.psSingleStart);
            this.groupBox1.Location = new System.Drawing.Point(28, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 79);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Single Port";
            // 
            // psPort
            // 
            this.psPort.Location = new System.Drawing.Point(23, 31);
            this.psPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.psPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.psPort.Name = "psPort";
            this.psPort.Size = new System.Drawing.Size(120, 20);
            this.psPort.TabIndex = 2;
            this.psPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // psSingleStart
            // 
            this.psSingleStart.Location = new System.Drawing.Point(176, 28);
            this.psSingleStart.Name = "psSingleStart";
            this.psSingleStart.Size = new System.Drawing.Size(75, 23);
            this.psSingleStart.TabIndex = 4;
            this.psSingleStart.Text = "Start";
            this.psSingleStart.UseVisualStyleBackColor = true;
            this.psSingleStart.Click += new System.EventHandler(this.psSingleStart_Click);
            // 
            // dgPorts
            // 
            this.dgPorts.AllowUserToAddRows = false;
            this.dgPorts.AllowUserToDeleteRows = false;
            this.dgPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgPorts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPorts.Location = new System.Drawing.Point(332, 6);
            this.dgPorts.Name = "dgPorts";
            this.dgPorts.RowHeadersVisible = false;
            this.dgPorts.Size = new System.Drawing.Size(234, 517);
            this.dgPorts.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Host";
            // 
            // psHost
            // 
            this.psHost.Location = new System.Drawing.Point(28, 37);
            this.psHost.Name = "psHost";
            this.psHost.Size = new System.Drawing.Size(154, 20);
            this.psHost.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.psStdPortRemove);
            this.groupBox3.Controls.Add(this.psStdPortAdd);
            this.groupBox3.Controls.Add(this.psStdPortPort);
            this.groupBox3.Controls.Add(this.psStdPortTxt);
            this.groupBox3.Controls.Add(this.psStdStart);
            this.groupBox3.Controls.Add(this.psStdPorts);
            this.groupBox3.Location = new System.Drawing.Point(28, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(272, 230);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Standard Ports";
            // 
            // psStdPorts
            // 
            this.psStdPorts.FormattingEnabled = true;
            this.psStdPorts.Location = new System.Drawing.Point(23, 19);
            this.psStdPorts.Name = "psStdPorts";
            this.psStdPorts.Size = new System.Drawing.Size(120, 199);
            this.psStdPorts.TabIndex = 0;
            // 
            // psStdStart
            // 
            this.psStdStart.Location = new System.Drawing.Point(176, 19);
            this.psStdStart.Name = "psStdStart";
            this.psStdStart.Size = new System.Drawing.Size(75, 23);
            this.psStdStart.TabIndex = 1;
            this.psStdStart.Text = "Start";
            this.psStdStart.UseVisualStyleBackColor = true;
            this.psStdStart.Click += new System.EventHandler(this.psStdStart_Click);
            // 
            // psStdPortTxt
            // 
            this.psStdPortTxt.Location = new System.Drawing.Point(149, 99);
            this.psStdPortTxt.Name = "psStdPortTxt";
            this.psStdPortTxt.Size = new System.Drawing.Size(80, 20);
            this.psStdPortTxt.TabIndex = 2;
            // 
            // psStdPortPort
            // 
            this.psStdPortPort.Location = new System.Drawing.Point(149, 73);
            this.psStdPortPort.Name = "psStdPortPort";
            this.psStdPortPort.Size = new System.Drawing.Size(80, 20);
            this.psStdPortPort.TabIndex = 4;
            // 
            // psStdPortAdd
            // 
            this.psStdPortAdd.Location = new System.Drawing.Point(150, 126);
            this.psStdPortAdd.Name = "psStdPortAdd";
            this.psStdPortAdd.Size = new System.Drawing.Size(75, 23);
            this.psStdPortAdd.TabIndex = 5;
            this.psStdPortAdd.Text = "Add";
            this.psStdPortAdd.UseVisualStyleBackColor = true;
            this.psStdPortAdd.Click += new System.EventHandler(this.psStdPortAdd_Click);
            // 
            // psStdPortRemove
            // 
            this.psStdPortRemove.Location = new System.Drawing.Point(150, 156);
            this.psStdPortRemove.Name = "psStdPortRemove";
            this.psStdPortRemove.Size = new System.Drawing.Size(75, 23);
            this.psStdPortRemove.TabIndex = 6;
            this.psStdPortRemove.Text = "Remove";
            this.psStdPortRemove.UseVisualStyleBackColor = true;
            this.psStdPortRemove.Click += new System.EventHandler(this.psStdPortRemove_Click);
            // 
            // PingTester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 557);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "PingTester";
            this.Text = "PingTester";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PingTester_FormClosing);
            this.Load += new System.EventHandler(this.PingTester_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabError.ResumeLayout(false);
            this.tabError.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.tabConfig.ResumeLayout(false);
            this.tabConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPingMem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPacket)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTicker)).EndInit();
            this.tabFastPing.ResumeLayout(false);
            this.tabFastPing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fpInterval)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.psFromPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.psToPort)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.psPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPorts)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.psStdPortPort)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numTicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPacket;
        private System.Windows.Forms.Button btnChgFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabDetails;
        private System.Windows.Forms.TabPage tabError;
        private System.Windows.Forms.TabPage tabConfig;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem removeAllToolStripMenuItem;
        private System.Windows.Forms.ListBox ErrorFeed;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem clearFeedToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox machineToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numPingMem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkLogToFile;
        private System.Windows.Forms.TabPage tabFastPing;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button fpStartStop;
        private System.Windows.Forms.GroupBox fpFeed;
        private System.Windows.Forms.NumericUpDown fpInterval;
        private System.Windows.Forms.TextBox fpHost;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown psFromPort;
        private System.Windows.Forms.Button psRangeStart;
        private System.Windows.Forms.NumericUpDown psToPort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown psPort;
        private System.Windows.Forms.Button psSingleStart;
        private System.Windows.Forms.DataGridView dgPorts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox psHost;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button psStdStart;
        private System.Windows.Forms.CheckedListBox psStdPorts;
        private System.Windows.Forms.NumericUpDown psStdPortPort;
        private System.Windows.Forms.TextBox psStdPortTxt;
        private System.Windows.Forms.Button psStdPortRemove;
        private System.Windows.Forms.Button psStdPortAdd;
    }
}

