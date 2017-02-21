namespace AgentApplication
{
    partial class AgentMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgentMainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newAccountMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadAccountMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAccountMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.agentTabControl = new System.Windows.Forms.TabControl();
            this.communicationLogTabPage = new System.Windows.Forms.TabPage();
            this.communicationLogColorListBox = new CustomUserControlsLibrary.ColorListBox();
            this.workingMemoryTabPage = new System.Windows.Forms.TabPage();
            this.memoryViewer = new AgentLibrary.Visualization.MemoryViewer();
            this.actionToolStrip = new System.Windows.Forms.ToolStrip();
            this.startButton = new System.Windows.Forms.ToolStripButton();
            this.stopButton = new System.Windows.Forms.ToolStripButton();
            this.stocksTabControl = new System.Windows.Forms.TabControl();
            this.seachStockTabPage = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.stockInfoTextBox = new System.Windows.Forms.TextBox();
            this.portfolioTabPage = new System.Windows.Forms.TabPage();
            this.portfolioHeaderPanel = new System.Windows.Forms.TableLayoutPanel();
            this.portfolioHeaderCol6 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol5 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol4 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol3 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol2 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol1 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol0 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol8 = new System.Windows.Forms.Label();
            this.portfolioHeaderCol7 = new System.Windows.Forms.Label();
            this.portfolioLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TotalEquityLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TotalEquityTextLabel = new System.Windows.Forms.Label();
            this.TotalEquityLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.agentTabControl.SuspendLayout();
            this.communicationLogTabPage.SuspendLayout();
            this.workingMemoryTabPage.SuspendLayout();
            this.actionToolStrip.SuspendLayout();
            this.stocksTabControl.SuspendLayout();
            this.seachStockTabPage.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.portfolioTabPage.SuspendLayout();
            this.portfolioHeaderPanel.SuspendLayout();
            this.portfolioLayoutPanel.SuspendLayout();
            this.TotalEquityLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(733, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newAccountMenuItem,
            this.loadAccountMenuItem,
            this.saveAccountMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // newAccountMenuItem
            // 
            this.newAccountMenuItem.Name = "newAccountMenuItem";
            this.newAccountMenuItem.Size = new System.Drawing.Size(148, 22);
            this.newAccountMenuItem.Text = "New Account";
            this.newAccountMenuItem.Click += new System.EventHandler(this.NewAccountMenuItem_Click);
            // 
            // loadAccountMenuItem
            // 
            this.loadAccountMenuItem.Name = "loadAccountMenuItem";
            this.loadAccountMenuItem.Size = new System.Drawing.Size(148, 22);
            this.loadAccountMenuItem.Text = "Load Account";
            this.loadAccountMenuItem.Click += new System.EventHandler(this.LoadAccountMenuItem_Click);
            // 
            // saveAccountMenuItem
            // 
            this.saveAccountMenuItem.Name = "saveAccountMenuItem";
            this.saveAccountMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveAccountMenuItem.Text = "Save Account";
            this.saveAccountMenuItem.Click += new System.EventHandler(this.SaveAccountMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 621);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(733, 22);
            this.mainStatusStrip.TabIndex = 7;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // agentTabControl
            // 
            this.agentTabControl.Controls.Add(this.communicationLogTabPage);
            this.agentTabControl.Controls.Add(this.workingMemoryTabPage);
            this.agentTabControl.Location = new System.Drawing.Point(0, 416);
            this.agentTabControl.Name = "agentTabControl";
            this.agentTabControl.SelectedIndex = 0;
            this.agentTabControl.Size = new System.Drawing.Size(734, 202);
            this.agentTabControl.TabIndex = 6;
            // 
            // communicationLogTabPage
            // 
            this.communicationLogTabPage.Controls.Add(this.communicationLogColorListBox);
            this.communicationLogTabPage.Location = new System.Drawing.Point(4, 22);
            this.communicationLogTabPage.Name = "communicationLogTabPage";
            this.communicationLogTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.communicationLogTabPage.Size = new System.Drawing.Size(726, 176);
            this.communicationLogTabPage.TabIndex = 1;
            this.communicationLogTabPage.Text = "Communication log";
            this.communicationLogTabPage.UseVisualStyleBackColor = true;
            // 
            // communicationLogColorListBox
            // 
            this.communicationLogColorListBox.BackColor = System.Drawing.Color.Black;
            this.communicationLogColorListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.communicationLogColorListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.communicationLogColorListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.communicationLogColorListBox.ForeColor = System.Drawing.Color.Lime;
            this.communicationLogColorListBox.FormattingEnabled = true;
            this.communicationLogColorListBox.Location = new System.Drawing.Point(3, 3);
            this.communicationLogColorListBox.Name = "communicationLogColorListBox";
            this.communicationLogColorListBox.SelectedItemBackColor = System.Drawing.Color.Empty;
            this.communicationLogColorListBox.SelectedItemForeColor = System.Drawing.Color.Empty;
            this.communicationLogColorListBox.Size = new System.Drawing.Size(720, 170);
            this.communicationLogColorListBox.TabIndex = 0;
            // 
            // workingMemoryTabPage
            // 
            this.workingMemoryTabPage.Controls.Add(this.memoryViewer);
            this.workingMemoryTabPage.Location = new System.Drawing.Point(4, 22);
            this.workingMemoryTabPage.Name = "workingMemoryTabPage";
            this.workingMemoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.workingMemoryTabPage.Size = new System.Drawing.Size(726, 176);
            this.workingMemoryTabPage.TabIndex = 3;
            this.workingMemoryTabPage.Text = "Working memory";
            this.workingMemoryTabPage.UseVisualStyleBackColor = true;
            // 
            // memoryViewer
            // 
            this.memoryViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryViewer.InvocationListVisible = false;
            this.memoryViewer.Location = new System.Drawing.Point(3, 3);
            this.memoryViewer.Name = "memoryViewer";
            this.memoryViewer.Size = new System.Drawing.Size(720, 170);
            this.memoryViewer.TabIndex = 0;
            // 
            // actionToolStrip
            // 
            this.actionToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startButton,
            this.stopButton});
            this.actionToolStrip.Location = new System.Drawing.Point(0, 24);
            this.actionToolStrip.Name = "actionToolStrip";
            this.actionToolStrip.Size = new System.Drawing.Size(733, 25);
            this.actionToolStrip.TabIndex = 5;
            this.actionToolStrip.Text = "toolStrip1";
            // 
            // startButton
            // 
            this.startButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startButton.Enabled = false;
            this.startButton.Image = ((System.Drawing.Image)(resources.GetObject("startButton.Image")));
            this.startButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(35, 22);
            this.startButton.Text = "Start";
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.stopButton.Enabled = false;
            this.stopButton.Image = ((System.Drawing.Image)(resources.GetObject("stopButton.Image")));
            this.stopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(35, 22);
            this.stopButton.Text = "Stop";
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // stocksTabControl
            // 
            this.stocksTabControl.Controls.Add(this.seachStockTabPage);
            this.stocksTabControl.Controls.Add(this.portfolioTabPage);
            this.stocksTabControl.Location = new System.Drawing.Point(0, 52);
            this.stocksTabControl.Name = "stocksTabControl";
            this.stocksTabControl.SelectedIndex = 0;
            this.stocksTabControl.Size = new System.Drawing.Size(734, 362);
            this.stocksTabControl.TabIndex = 1;
            // 
            // seachStockTabPage
            // 
            this.seachStockTabPage.Controls.Add(this.flowLayoutPanel1);
            this.seachStockTabPage.Location = new System.Drawing.Point(4, 22);
            this.seachStockTabPage.Name = "seachStockTabPage";
            this.seachStockTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.seachStockTabPage.Size = new System.Drawing.Size(726, 336);
            this.seachStockTabPage.TabIndex = 0;
            this.seachStockTabPage.Text = "Search stock";
            this.seachStockTabPage.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.stockInfoTextBox);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(788, 336);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // stockInfoTextBox
            // 
            this.stockInfoTextBox.Location = new System.Drawing.Point(3, 3);
            this.stockInfoTextBox.Multiline = true;
            this.stockInfoTextBox.Name = "stockInfoTextBox";
            this.stockInfoTextBox.Size = new System.Drawing.Size(313, 330);
            this.stockInfoTextBox.TabIndex = 0;
            // 
            // portfolioTabPage
            // 
            this.portfolioTabPage.Controls.Add(this.TotalEquityLayoutPanel);
            this.portfolioTabPage.Controls.Add(this.portfolioHeaderPanel);
            this.portfolioTabPage.Controls.Add(this.portfolioLayoutPanel);
            this.portfolioTabPage.Location = new System.Drawing.Point(4, 22);
            this.portfolioTabPage.Name = "portfolioTabPage";
            this.portfolioTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.portfolioTabPage.Size = new System.Drawing.Size(726, 336);
            this.portfolioTabPage.TabIndex = 1;
            this.portfolioTabPage.Text = "portfolio";
            this.portfolioTabPage.UseVisualStyleBackColor = true;
            // 
            // portfolioHeaderPanel
            // 
            this.portfolioHeaderPanel.ColumnCount = 9;
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.31858F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.68142F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.portfolioHeaderPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol6, 6, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol5, 5, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol4, 4, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol3, 3, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol2, 2, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol1, 1, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol0, 0, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol8, 8, 0);
            this.portfolioHeaderPanel.Controls.Add(this.portfolioHeaderCol7, 7, 0);
            this.portfolioHeaderPanel.Location = new System.Drawing.Point(0, 4);
            this.portfolioHeaderPanel.Name = "portfolioHeaderPanel";
            this.portfolioHeaderPanel.RowCount = 1;
            this.portfolioHeaderPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.portfolioHeaderPanel.Size = new System.Drawing.Size(723, 29);
            this.portfolioHeaderPanel.TabIndex = 1;
            // 
            // portfolioHeaderCol6
            // 
            this.portfolioHeaderCol6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol6.AutoSize = true;
            this.portfolioHeaderCol6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol6.Location = new System.Drawing.Point(422, 5);
            this.portfolioHeaderCol6.Name = "portfolioHeaderCol6";
            this.portfolioHeaderCol6.Size = new System.Drawing.Size(89, 18);
            this.portfolioHeaderCol6.TabIndex = 6;
            this.portfolioHeaderCol6.Text = "Target Profit";
            // 
            // portfolioHeaderCol5
            // 
            this.portfolioHeaderCol5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol5.AutoSize = true;
            this.portfolioHeaderCol5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol5.Location = new System.Drawing.Point(335, 5);
            this.portfolioHeaderCol5.Name = "portfolioHeaderCol5";
            this.portfolioHeaderCol5.Size = new System.Drawing.Size(76, 18);
            this.portfolioHeaderCol5.TabIndex = 5;
            this.portfolioHeaderCol5.Text = "Stop Loss";
            // 
            // portfolioHeaderCol4
            // 
            this.portfolioHeaderCol4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol4.AutoSize = true;
            this.portfolioHeaderCol4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol4.Location = new System.Drawing.Point(244, 5);
            this.portfolioHeaderCol4.Name = "portfolioHeaderCol4";
            this.portfolioHeaderCol4.Size = new System.Drawing.Size(80, 18);
            this.portfolioHeaderCol4.TabIndex = 4;
            this.portfolioHeaderCol4.Text = "Entry Price";
            // 
            // portfolioHeaderCol3
            // 
            this.portfolioHeaderCol3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol3.AutoSize = true;
            this.portfolioHeaderCol3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol3.Location = new System.Drawing.Point(172, 5);
            this.portfolioHeaderCol3.Name = "portfolioHeaderCol3";
            this.portfolioHeaderCol3.Size = new System.Drawing.Size(62, 18);
            this.portfolioHeaderCol3.TabIndex = 3;
            this.portfolioHeaderCol3.Text = "Quantity";
            // 
            // portfolioHeaderCol2
            // 
            this.portfolioHeaderCol2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol2.AutoSize = true;
            this.portfolioHeaderCol2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol2.Location = new System.Drawing.Point(118, 5);
            this.portfolioHeaderCol2.Name = "portfolioHeaderCol2";
            this.portfolioHeaderCol2.Size = new System.Drawing.Size(47, 18);
            this.portfolioHeaderCol2.TabIndex = 2;
            this.portfolioHeaderCol2.Text = "Stock";
            // 
            // portfolioHeaderCol1
            // 
            this.portfolioHeaderCol1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol1.AutoSize = true;
            this.portfolioHeaderCol1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol1.Location = new System.Drawing.Point(35, 5);
            this.portfolioHeaderCol1.Name = "portfolioHeaderCol1";
            this.portfolioHeaderCol1.Size = new System.Drawing.Size(77, 18);
            this.portfolioHeaderCol1.TabIndex = 1;
            this.portfolioHeaderCol1.Text = "Entry Date";
            // 
            // portfolioHeaderCol0
            // 
            this.portfolioHeaderCol0.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol0.AutoSize = true;
            this.portfolioHeaderCol0.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol0.Location = new System.Drawing.Point(5, 5);
            this.portfolioHeaderCol0.Name = "portfolioHeaderCol0";
            this.portfolioHeaderCol0.Size = new System.Drawing.Size(22, 18);
            this.portfolioHeaderCol0.TabIndex = 0;
            this.portfolioHeaderCol0.Text = "ID";
            // 
            // portfolioHeaderCol8
            // 
            this.portfolioHeaderCol8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol8.AutoSize = true;
            this.portfolioHeaderCol8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol8.Location = new System.Drawing.Point(626, 5);
            this.portfolioHeaderCol8.Name = "portfolioHeaderCol8";
            this.portfolioHeaderCol8.Size = new System.Drawing.Size(80, 18);
            this.portfolioHeaderCol8.TabIndex = 6;
            this.portfolioHeaderCol8.Text = "Profit/Loss";
            // 
            // portfolioHeaderCol7
            // 
            this.portfolioHeaderCol7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.portfolioHeaderCol7.AutoSize = true;
            this.portfolioHeaderCol7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.portfolioHeaderCol7.Location = new System.Drawing.Point(520, 5);
            this.portfolioHeaderCol7.Name = "portfolioHeaderCol7";
            this.portfolioHeaderCol7.Size = new System.Drawing.Size(86, 18);
            this.portfolioHeaderCol7.TabIndex = 5;
            this.portfolioHeaderCol7.Text = "Latest Price";
            // 
            // portfolioLayoutPanel
            // 
            this.portfolioLayoutPanel.ColumnCount = 9;
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.69565F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.30434F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 71F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 91F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.portfolioLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.portfolioLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.portfolioLayoutPanel.Location = new System.Drawing.Point(0, 30);
            this.portfolioLayoutPanel.Name = "portfolioLayoutPanel";
            this.portfolioLayoutPanel.RowCount = 1;
            this.portfolioLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.portfolioLayoutPanel.Size = new System.Drawing.Size(723, 30);
            this.portfolioLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID";
            // 
            // TotalEquityLayoutPanel
            // 
            this.TotalEquityLayoutPanel.ColumnCount = 2;
            this.TotalEquityLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.33498F));
            this.TotalEquityLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.66502F));
            this.TotalEquityLayoutPanel.Controls.Add(this.TotalEquityLabel, 0, 0);
            this.TotalEquityLayoutPanel.Controls.Add(this.TotalEquityTextLabel, 0, 0);
            this.TotalEquityLayoutPanel.Location = new System.Drawing.Point(516, 306);
            this.TotalEquityLayoutPanel.Name = "TotalEquityLayoutPanel";
            this.TotalEquityLayoutPanel.RowCount = 1;
            this.TotalEquityLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TotalEquityLayoutPanel.Size = new System.Drawing.Size(210, 30);
            this.TotalEquityLayoutPanel.TabIndex = 2;
            this.TotalEquityLayoutPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // TotalEquityTextLabel
            // 
            this.TotalEquityTextLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TotalEquityTextLabel.AutoSize = true;
            this.TotalEquityTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalEquityTextLabel.Location = new System.Drawing.Point(4, 6);
            this.TotalEquityTextLabel.Name = "TotalEquityTextLabel";
            this.TotalEquityTextLabel.Size = new System.Drawing.Size(85, 18);
            this.TotalEquityTextLabel.TabIndex = 6;
            this.TotalEquityTextLabel.Text = "Total Equity";
            // 
            // TotalEquityLabel
            // 
            this.TotalEquityLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TotalEquityLabel.AutoSize = true;
            this.TotalEquityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TotalEquityLabel.Location = new System.Drawing.Point(123, 6);
            this.TotalEquityLabel.Name = "TotalEquityLabel";
            this.TotalEquityLabel.Size = new System.Drawing.Size(56, 18);
            this.TotalEquityLabel.TabIndex = 7;
            this.TotalEquityLabel.Text = "100000";
            // 
            // AgentMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 643);
            this.Controls.Add(this.stocksTabControl);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.agentTabControl);
            this.Controls.Add(this.actionToolStrip);
            this.Controls.Add(this.menuStrip1);
            this.Name = "AgentMainForm";
            this.Text = "Agent (demonstration)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgentMainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.agentTabControl.ResumeLayout(false);
            this.communicationLogTabPage.ResumeLayout(false);
            this.workingMemoryTabPage.ResumeLayout(false);
            this.actionToolStrip.ResumeLayout(false);
            this.actionToolStrip.PerformLayout();
            this.stocksTabControl.ResumeLayout(false);
            this.seachStockTabPage.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.portfolioTabPage.ResumeLayout(false);
            this.portfolioHeaderPanel.ResumeLayout(false);
            this.portfolioHeaderPanel.PerformLayout();
            this.portfolioLayoutPanel.ResumeLayout(false);
            this.portfolioLayoutPanel.PerformLayout();
            this.TotalEquityLayoutPanel.ResumeLayout(false);
            this.TotalEquityLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadAccountMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAccountMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.TabControl agentTabControl;
        private System.Windows.Forms.TabPage communicationLogTabPage;
        private System.Windows.Forms.ToolStrip actionToolStrip;
        private System.Windows.Forms.TabPage workingMemoryTabPage;
        private System.Windows.Forms.ToolStripButton startButton;
        private AgentLibrary.Visualization.MemoryViewer memoryViewer;
        private CustomUserControlsLibrary.ColorListBox communicationLogColorListBox;
        private System.Windows.Forms.ToolStripButton stopButton;
        private System.Windows.Forms.ToolStripMenuItem newAccountMenuItem;
        private System.Windows.Forms.TabControl stocksTabControl;
        private System.Windows.Forms.TabPage seachStockTabPage;
        private System.Windows.Forms.TabPage portfolioTabPage;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox stockInfoTextBox;
        private System.Windows.Forms.TableLayoutPanel portfolioLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel portfolioHeaderPanel;
        private System.Windows.Forms.Label portfolioHeaderCol3;
        private System.Windows.Forms.Label portfolioHeaderCol2;
        private System.Windows.Forms.Label portfolioHeaderCol1;
        private System.Windows.Forms.Label portfolioHeaderCol0;
        private System.Windows.Forms.Label portfolioHeaderCol4;
        private System.Windows.Forms.Label portfolioHeaderCol7;
        private System.Windows.Forms.Label portfolioHeaderCol8;
        private System.Windows.Forms.Label portfolioHeaderCol5;
        private System.Windows.Forms.Label portfolioHeaderCol6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel TotalEquityLayoutPanel;
        private System.Windows.Forms.Label TotalEquityTextLabel;
        private System.Windows.Forms.Label TotalEquityLabel;
    }
}

