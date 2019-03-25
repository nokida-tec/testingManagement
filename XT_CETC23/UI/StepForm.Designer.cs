namespace XT_CETC23.SonForm
{
    partial class StepForm
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
            this.panel = new System.Windows.Forms.Panel();
            this.gbLoop = new System.Windows.Forms.GroupBox();
            this.cbTrayNo_Loop = new System.Windows.Forms.ComboBox();
            this.btnPutback_Loop = new System.Windows.Forms.Button();
            this.btnTestStop_Loop = new System.Windows.Forms.Button();
            this.btnTestStart_Loop = new System.Windows.Forms.Button();
            this.btnTake_Loop = new System.Windows.Forms.Button();
            this.lbSlotNoOfLoop = new System.Windows.Forms.Label();
            this.lbCabinetOfLoop = new System.Windows.Forms.Label();
            this.lbTrayNoOfLoop = new System.Windows.Forms.Label();
            this.cbProductType_Loop = new System.Windows.Forms.ComboBox();
            this.lbProductTypeOfLoop = new System.Windows.Forms.Label();
            this.cbSlotNo_Loop = new System.Windows.Forms.ComboBox();
            this.cbCabinetNo_Loop = new System.Windows.Forms.ComboBox();
            this.gbCabinet = new System.Windows.Forms.GroupBox();
            this.cbProductType_Cabinet = new System.Windows.Forms.ComboBox();
            this.btnTestStop_Cabinet = new System.Windows.Forms.Button();
            this.lbCabinetOfCabinet = new System.Windows.Forms.Label();
            this.btnTestStart_Cabinet = new System.Windows.Forms.Button();
            this.cbCabinetNo_Cabinet = new System.Windows.Forms.ComboBox();
            this.lbProductTypeOfCabinet = new System.Windows.Forms.Label();
            this.gbRobot = new System.Windows.Forms.GroupBox();
            this.ckbAxis7Alone = new System.Windows.Forms.CheckBox();
            this.cbSlotNo_Robot = new System.Windows.Forms.ComboBox();
            this.btnStart_Robot = new System.Windows.Forms.Button();
            this.cbProductType_Robot = new System.Windows.Forms.ComboBox();
            this.cbPos_Robot = new System.Windows.Forms.ComboBox();
            this.lbPosOfRobot = new System.Windows.Forms.Label();
            this.lbSlotNoOfRobot = new System.Windows.Forms.Label();
            this.cbOrder_Robot = new System.Windows.Forms.ComboBox();
            this.lbProductTypeOfRobot = new System.Windows.Forms.Label();
            this.lbOrderOfRobot = new System.Windows.Forms.Label();
            this.gbFrame = new System.Windows.Forms.GroupBox();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.btnStart_Frame = new System.Windows.Forms.Button();
            this.cbOrder_Frame = new System.Windows.Forms.ComboBox();
            this.cbGoalPos_Frame = new System.Windows.Forms.ComboBox();
            this.lbOrderOfFrame = new System.Windows.Forms.Label();
            this.lbTrayNoOfFrame = new System.Windows.Forms.Label();
            this.lbTitle = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.gbLoop.SuspendLayout();
            this.gbCabinet.SuspendLayout();
            this.gbRobot.SuspendLayout();
            this.gbFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel.Controls.Add(this.gbLoop);
            this.panel.Controls.Add(this.gbCabinet);
            this.panel.Controls.Add(this.gbRobot);
            this.panel.Controls.Add(this.gbFrame);
            this.panel.Location = new System.Drawing.Point(8, 67);
            this.panel.Margin = new System.Windows.Forms.Padding(2);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(973, 411);
            this.panel.TabIndex = 0;
            // 
            // gbLoop
            // 
            this.gbLoop.Controls.Add(this.cbTrayNo_Loop);
            this.gbLoop.Controls.Add(this.btnPutback_Loop);
            this.gbLoop.Controls.Add(this.btnTestStop_Loop);
            this.gbLoop.Controls.Add(this.btnTestStart_Loop);
            this.gbLoop.Controls.Add(this.btnTake_Loop);
            this.gbLoop.Controls.Add(this.lbSlotNoOfLoop);
            this.gbLoop.Controls.Add(this.lbCabinetOfLoop);
            this.gbLoop.Controls.Add(this.lbTrayNoOfLoop);
            this.gbLoop.Controls.Add(this.cbProductType_Loop);
            this.gbLoop.Controls.Add(this.lbProductTypeOfLoop);
            this.gbLoop.Controls.Add(this.cbSlotNo_Loop);
            this.gbLoop.Controls.Add(this.cbCabinetNo_Loop);
            this.gbLoop.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbLoop.Location = new System.Drawing.Point(501, 18);
            this.gbLoop.Margin = new System.Windows.Forms.Padding(2);
            this.gbLoop.Name = "gbLoop";
            this.gbLoop.Padding = new System.Windows.Forms.Padding(2);
            this.gbLoop.Size = new System.Drawing.Size(457, 229);
            this.gbLoop.TabIndex = 39;
            this.gbLoop.TabStop = false;
            this.gbLoop.Text = "单循环控制";
            // 
            // cbTrayNo_Loop
            // 
            this.cbTrayNo_Loop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTrayNo_Loop.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbTrayNo_Loop.FormattingEnabled = true;
            this.cbTrayNo_Loop.Items.AddRange(new object[] {
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "B1",
            "B2",
            "B3",
            "B4",
            "B5",
            "C1",
            "C2",
            "C3",
            "C4",
            "C5",
            "D1",
            "D2",
            "D3",
            "D4",
            "D5",
            "E1",
            "E2",
            "E3",
            "E4",
            "E5",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "G1",
            "G2",
            "G3",
            "G4",
            "G5",
            "H1",
            "H2",
            "H3",
            "H4",
            "H5"});
            this.cbTrayNo_Loop.Location = new System.Drawing.Point(142, 81);
            this.cbTrayNo_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.cbTrayNo_Loop.Name = "cbTrayNo_Loop";
            this.cbTrayNo_Loop.Size = new System.Drawing.Size(109, 24);
            this.cbTrayNo_Loop.TabIndex = 4;
            // 
            // btnPutback_Loop
            // 
            this.btnPutback_Loop.BackColor = System.Drawing.Color.PowderBlue;
            this.btnPutback_Loop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPutback_Loop.Location = new System.Drawing.Point(287, 168);
            this.btnPutback_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.btnPutback_Loop.Name = "btnPutback_Loop";
            this.btnPutback_Loop.Size = new System.Drawing.Size(146, 31);
            this.btnPutback_Loop.TabIndex = 6;
            this.btnPutback_Loop.Text = "取回到料架";
            this.btnPutback_Loop.UseVisualStyleBackColor = false;
            this.btnPutback_Loop.Click += new System.EventHandler(this.onClick_PutBack);
            // 
            // btnTestStop_Loop
            // 
            this.btnTestStop_Loop.BackColor = System.Drawing.Color.PowderBlue;
            this.btnTestStop_Loop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestStop_Loop.Location = new System.Drawing.Point(287, 122);
            this.btnTestStop_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestStop_Loop.Name = "btnTestStop_Loop";
            this.btnTestStop_Loop.Size = new System.Drawing.Size(146, 31);
            this.btnTestStop_Loop.TabIndex = 6;
            this.btnTestStop_Loop.Text = "停止测试";
            this.btnTestStop_Loop.UseVisualStyleBackColor = false;
            this.btnTestStop_Loop.Click += new System.EventHandler(this.onClick_TestStop);
            // 
            // btnTestStart_Loop
            // 
            this.btnTestStart_Loop.BackColor = System.Drawing.Color.PowderBlue;
            this.btnTestStart_Loop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestStart_Loop.Location = new System.Drawing.Point(287, 76);
            this.btnTestStart_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestStart_Loop.Name = "btnTestStart_Loop";
            this.btnTestStart_Loop.Size = new System.Drawing.Size(146, 31);
            this.btnTestStart_Loop.TabIndex = 6;
            this.btnTestStart_Loop.Text = "开始测试";
            this.btnTestStart_Loop.UseVisualStyleBackColor = false;
            this.btnTestStart_Loop.Click += new System.EventHandler(this.onClick_TestStart);
            // 
            // btnTake_Loop
            // 
            this.btnTake_Loop.BackColor = System.Drawing.Color.PowderBlue;
            this.btnTake_Loop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTake_Loop.Location = new System.Drawing.Point(287, 30);
            this.btnTake_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.btnTake_Loop.Name = "btnTake_Loop";
            this.btnTake_Loop.Size = new System.Drawing.Size(146, 31);
            this.btnTake_Loop.TabIndex = 6;
            this.btnTake_Loop.Text = "取料到测试柜";
            this.btnTake_Loop.UseVisualStyleBackColor = false;
            this.btnTake_Loop.Click += new System.EventHandler(this.onClick_Take);
            // 
            // lbSlotNoOfLoop
            // 
            this.lbSlotNoOfLoop.AutoSize = true;
            this.lbSlotNoOfLoop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSlotNoOfLoop.Location = new System.Drawing.Point(22, 127);
            this.lbSlotNoOfLoop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSlotNoOfLoop.Name = "lbSlotNoOfLoop";
            this.lbSlotNoOfLoop.Size = new System.Drawing.Size(85, 19);
            this.lbSlotNoOfLoop.TabIndex = 0;
            this.lbSlotNoOfLoop.Text = "位置号：";
            // 
            // lbCabinetOfLoop
            // 
            this.lbCabinetOfLoop.AutoSize = true;
            this.lbCabinetOfLoop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCabinetOfLoop.Location = new System.Drawing.Point(22, 171);
            this.lbCabinetOfLoop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCabinetOfLoop.Name = "lbCabinetOfLoop";
            this.lbCabinetOfLoop.Size = new System.Drawing.Size(85, 19);
            this.lbCabinetOfLoop.TabIndex = 0;
            this.lbCabinetOfLoop.Text = "测试柜：";
            // 
            // lbTrayNoOfLoop
            // 
            this.lbTrayNoOfLoop.AutoSize = true;
            this.lbTrayNoOfLoop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTrayNoOfLoop.Location = new System.Drawing.Point(22, 82);
            this.lbTrayNoOfLoop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTrayNoOfLoop.Name = "lbTrayNoOfLoop";
            this.lbTrayNoOfLoop.Size = new System.Drawing.Size(85, 19);
            this.lbTrayNoOfLoop.TabIndex = 1;
            this.lbTrayNoOfLoop.Tag = "";
            this.lbTrayNoOfLoop.Text = "料盘号：";
            // 
            // cbProductType_Loop
            // 
            this.cbProductType_Loop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductType_Loop.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbProductType_Loop.FormattingEnabled = true;
            this.cbProductType_Loop.Location = new System.Drawing.Point(142, 35);
            this.cbProductType_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.cbProductType_Loop.Name = "cbProductType_Loop";
            this.cbProductType_Loop.Size = new System.Drawing.Size(109, 24);
            this.cbProductType_Loop.TabIndex = 5;
            this.cbProductType_Loop.SelectedIndexChanged += new System.EventHandler(this.onSelectedChangedProductTypeOfLoop);
            this.cbProductType_Loop.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onClickProductTypeOfLoop);
            // 
            // lbProductTypeOfLoop
            // 
            this.lbProductTypeOfLoop.AutoSize = true;
            this.lbProductTypeOfLoop.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProductTypeOfLoop.Location = new System.Drawing.Point(22, 37);
            this.lbProductTypeOfLoop.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProductTypeOfLoop.Name = "lbProductTypeOfLoop";
            this.lbProductTypeOfLoop.Size = new System.Drawing.Size(104, 19);
            this.lbProductTypeOfLoop.TabIndex = 2;
            this.lbProductTypeOfLoop.Text = "产品类型：";
            // 
            // cbSlotNo_Loop
            // 
            this.cbSlotNo_Loop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSlotNo_Loop.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSlotNo_Loop.FormattingEnabled = true;
            this.cbSlotNo_Loop.Items.AddRange(new object[] {
            "料架位",
            "1#测试位",
            "2#测试位",
            "3#测试位",
            "4#测试位",
            "5#测试位",
            "6#测试位"});
            this.cbSlotNo_Loop.Location = new System.Drawing.Point(142, 127);
            this.cbSlotNo_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.cbSlotNo_Loop.Name = "cbSlotNo_Loop";
            this.cbSlotNo_Loop.Size = new System.Drawing.Size(109, 24);
            this.cbSlotNo_Loop.TabIndex = 3;
            // 
            // cbCabinetNo_Loop
            // 
            this.cbCabinetNo_Loop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCabinetNo_Loop.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCabinetNo_Loop.FormattingEnabled = true;
            this.cbCabinetNo_Loop.Items.AddRange(new object[] {
            "1#测试位",
            "2#测试位",
            "3#测试位",
            "4#测试位",
            "5#测试位",
            "6#测试位"});
            this.cbCabinetNo_Loop.Location = new System.Drawing.Point(142, 173);
            this.cbCabinetNo_Loop.Margin = new System.Windows.Forms.Padding(2);
            this.cbCabinetNo_Loop.Name = "cbCabinetNo_Loop";
            this.cbCabinetNo_Loop.Size = new System.Drawing.Size(109, 24);
            this.cbCabinetNo_Loop.TabIndex = 3;
            // 
            // gbCabinet
            // 
            this.gbCabinet.Controls.Add(this.cbProductType_Cabinet);
            this.gbCabinet.Controls.Add(this.btnTestStop_Cabinet);
            this.gbCabinet.Controls.Add(this.lbCabinetOfCabinet);
            this.gbCabinet.Controls.Add(this.btnTestStart_Cabinet);
            this.gbCabinet.Controls.Add(this.cbCabinetNo_Cabinet);
            this.gbCabinet.Controls.Add(this.lbProductTypeOfCabinet);
            this.gbCabinet.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbCabinet.Location = new System.Drawing.Point(501, 269);
            this.gbCabinet.Margin = new System.Windows.Forms.Padding(2);
            this.gbCabinet.Name = "gbCabinet";
            this.gbCabinet.Padding = new System.Windows.Forms.Padding(2);
            this.gbCabinet.Size = new System.Drawing.Size(457, 114);
            this.gbCabinet.TabIndex = 38;
            this.gbCabinet.TabStop = false;
            this.gbCabinet.Text = "测试机台";
            // 
            // cbProductType_Cabinet
            // 
            this.cbProductType_Cabinet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductType_Cabinet.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbProductType_Cabinet.FormattingEnabled = true;
            this.cbProductType_Cabinet.Location = new System.Drawing.Point(142, 69);
            this.cbProductType_Cabinet.Margin = new System.Windows.Forms.Padding(2);
            this.cbProductType_Cabinet.Name = "cbProductType_Cabinet";
            this.cbProductType_Cabinet.Size = new System.Drawing.Size(109, 24);
            this.cbProductType_Cabinet.TabIndex = 8;
            this.cbProductType_Cabinet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onClickProductTypeOfCabinet);
            // 
            // btnTestStop_Cabinet
            // 
            this.btnTestStop_Cabinet.BackColor = System.Drawing.Color.PowderBlue;
            this.btnTestStop_Cabinet.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestStop_Cabinet.Location = new System.Drawing.Point(287, 63);
            this.btnTestStop_Cabinet.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestStop_Cabinet.Name = "btnTestStop_Cabinet";
            this.btnTestStop_Cabinet.Size = new System.Drawing.Size(146, 31);
            this.btnTestStop_Cabinet.TabIndex = 35;
            this.btnTestStop_Cabinet.Text = "停止测试";
            this.btnTestStop_Cabinet.UseVisualStyleBackColor = false;
            this.btnTestStop_Cabinet.Click += new System.EventHandler(this.onClick_SingleTestStop);
            // 
            // lbCabinetOfCabinet
            // 
            this.lbCabinetOfCabinet.AutoSize = true;
            this.lbCabinetOfCabinet.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCabinetOfCabinet.Location = new System.Drawing.Point(22, 29);
            this.lbCabinetOfCabinet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbCabinetOfCabinet.Name = "lbCabinetOfCabinet";
            this.lbCabinetOfCabinet.Size = new System.Drawing.Size(85, 19);
            this.lbCabinetOfCabinet.TabIndex = 0;
            this.lbCabinetOfCabinet.Text = "测试柜：";
            // 
            // btnTestStart_Cabinet
            // 
            this.btnTestStart_Cabinet.BackColor = System.Drawing.Color.PowderBlue;
            this.btnTestStart_Cabinet.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestStart_Cabinet.Location = new System.Drawing.Point(287, 23);
            this.btnTestStart_Cabinet.Margin = new System.Windows.Forms.Padding(2);
            this.btnTestStart_Cabinet.Name = "btnTestStart_Cabinet";
            this.btnTestStart_Cabinet.Size = new System.Drawing.Size(146, 31);
            this.btnTestStart_Cabinet.TabIndex = 7;
            this.btnTestStart_Cabinet.Text = "开始测试";
            this.btnTestStart_Cabinet.UseVisualStyleBackColor = false;
            this.btnTestStart_Cabinet.Click += new System.EventHandler(this.onClick_SingleTestStart);
            // 
            // cbCabinetNo_Cabinet
            // 
            this.cbCabinetNo_Cabinet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCabinetNo_Cabinet.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbCabinetNo_Cabinet.FormattingEnabled = true;
            this.cbCabinetNo_Cabinet.Items.AddRange(new object[] {
            "1#机台",
            "2#机台",
            "3#机台",
            "4#机台",
            "5#机台",
            "6#机台"});
            this.cbCabinetNo_Cabinet.Location = new System.Drawing.Point(142, 27);
            this.cbCabinetNo_Cabinet.Margin = new System.Windows.Forms.Padding(2);
            this.cbCabinetNo_Cabinet.Name = "cbCabinetNo_Cabinet";
            this.cbCabinetNo_Cabinet.Size = new System.Drawing.Size(109, 24);
            this.cbCabinetNo_Cabinet.TabIndex = 34;
            // 
            // lbProductTypeOfCabinet
            // 
            this.lbProductTypeOfCabinet.AutoSize = true;
            this.lbProductTypeOfCabinet.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProductTypeOfCabinet.Location = new System.Drawing.Point(22, 71);
            this.lbProductTypeOfCabinet.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProductTypeOfCabinet.Name = "lbProductTypeOfCabinet";
            this.lbProductTypeOfCabinet.Size = new System.Drawing.Size(104, 19);
            this.lbProductTypeOfCabinet.TabIndex = 2;
            this.lbProductTypeOfCabinet.Text = "产品类型：";
            // 
            // gbRobot
            // 
            this.gbRobot.Controls.Add(this.ckbAxis7Alone);
            this.gbRobot.Controls.Add(this.cbSlotNo_Robot);
            this.gbRobot.Controls.Add(this.btnStart_Robot);
            this.gbRobot.Controls.Add(this.cbProductType_Robot);
            this.gbRobot.Controls.Add(this.cbPos_Robot);
            this.gbRobot.Controls.Add(this.lbPosOfRobot);
            this.gbRobot.Controls.Add(this.lbSlotNoOfRobot);
            this.gbRobot.Controls.Add(this.cbOrder_Robot);
            this.gbRobot.Controls.Add(this.lbProductTypeOfRobot);
            this.gbRobot.Controls.Add(this.lbOrderOfRobot);
            this.gbRobot.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbRobot.Location = new System.Drawing.Point(12, 170);
            this.gbRobot.Margin = new System.Windows.Forms.Padding(2);
            this.gbRobot.Name = "gbRobot";
            this.gbRobot.Padding = new System.Windows.Forms.Padding(2);
            this.gbRobot.Size = new System.Drawing.Size(460, 213);
            this.gbRobot.TabIndex = 36;
            this.gbRobot.TabStop = false;
            this.gbRobot.Text = "机器人取放料";
            // 
            // ckbAxis7Alone
            // 
            this.ckbAxis7Alone.AutoSize = true;
            this.ckbAxis7Alone.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbAxis7Alone.Location = new System.Drawing.Point(296, 162);
            this.ckbAxis7Alone.Margin = new System.Windows.Forms.Padding(2);
            this.ckbAxis7Alone.Name = "ckbAxis7Alone";
            this.ckbAxis7Alone.Size = new System.Drawing.Size(152, 18);
            this.ckbAxis7Alone.TabIndex = 8;
            this.ckbAxis7Alone.Text = "机器人轨道独立运动";
            this.ckbAxis7Alone.UseVisualStyleBackColor = true;
            // 
            // cbSlotNo_Robot
            // 
            this.cbSlotNo_Robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSlotNo_Robot.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbSlotNo_Robot.FormattingEnabled = true;
            this.cbSlotNo_Robot.Location = new System.Drawing.Point(159, 159);
            this.cbSlotNo_Robot.Margin = new System.Windows.Forms.Padding(2);
            this.cbSlotNo_Robot.Name = "cbSlotNo_Robot";
            this.cbSlotNo_Robot.Size = new System.Drawing.Size(109, 24);
            this.cbSlotNo_Robot.TabIndex = 7;
            // 
            // btnStart_Robot
            // 
            this.btnStart_Robot.BackColor = System.Drawing.Color.PowderBlue;
            this.btnStart_Robot.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart_Robot.Location = new System.Drawing.Point(317, 53);
            this.btnStart_Robot.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart_Robot.Name = "btnStart_Robot";
            this.btnStart_Robot.Size = new System.Drawing.Size(97, 94);
            this.btnStart_Robot.TabIndex = 6;
            this.btnStart_Robot.Text = "机器人取料/放料";
            this.btnStart_Robot.UseVisualStyleBackColor = false;
            this.btnStart_Robot.Click += new System.EventHandler(this.onClick_RobotAction);
            // 
            // cbProductType_Robot
            // 
            this.cbProductType_Robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProductType_Robot.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbProductType_Robot.FormattingEnabled = true;
            this.cbProductType_Robot.Location = new System.Drawing.Point(159, 42);
            this.cbProductType_Robot.Margin = new System.Windows.Forms.Padding(2);
            this.cbProductType_Robot.Name = "cbProductType_Robot";
            this.cbProductType_Robot.Size = new System.Drawing.Size(109, 24);
            this.cbProductType_Robot.TabIndex = 5;
            this.cbProductType_Robot.SelectedIndexChanged += new System.EventHandler(this.onSelectedChangedProductType);
            this.cbProductType_Robot.Click += new System.EventHandler(this.onClickProductTypeOfRobot);
            // 
            // cbPos_Robot
            // 
            this.cbPos_Robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPos_Robot.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbPos_Robot.FormattingEnabled = true;
            this.cbPos_Robot.Items.AddRange(new object[] {
            "料架位",
            "1#测试位",
            "2#测试位",
            "3#测试位",
            "4#测试位",
            "5#测试位",
            "6#测试位"});
            this.cbPos_Robot.Location = new System.Drawing.Point(159, 120);
            this.cbPos_Robot.Margin = new System.Windows.Forms.Padding(2);
            this.cbPos_Robot.Name = "cbPos_Robot";
            this.cbPos_Robot.Size = new System.Drawing.Size(109, 24);
            this.cbPos_Robot.TabIndex = 3;
            this.cbPos_Robot.SelectedIndexChanged += new System.EventHandler(this.onSelectedChangedPosOfRobot);
            // 
            // lbPosOfRobot
            // 
            this.lbPosOfRobot.AutoSize = true;
            this.lbPosOfRobot.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbPosOfRobot.Location = new System.Drawing.Point(35, 122);
            this.lbPosOfRobot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbPosOfRobot.Name = "lbPosOfRobot";
            this.lbPosOfRobot.Size = new System.Drawing.Size(104, 19);
            this.lbPosOfRobot.TabIndex = 0;
            this.lbPosOfRobot.Text = "轨道位置：";
            // 
            // lbSlotNoOfRobot
            // 
            this.lbSlotNoOfRobot.AutoSize = true;
            this.lbSlotNoOfRobot.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSlotNoOfRobot.Location = new System.Drawing.Point(35, 161);
            this.lbSlotNoOfRobot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbSlotNoOfRobot.Name = "lbSlotNoOfRobot";
            this.lbSlotNoOfRobot.Size = new System.Drawing.Size(85, 19);
            this.lbSlotNoOfRobot.TabIndex = 0;
            this.lbSlotNoOfRobot.Text = "位置号：";
            // 
            // cbOrder_Robot
            // 
            this.cbOrder_Robot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder_Robot.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbOrder_Robot.FormattingEnabled = true;
            this.cbOrder_Robot.Items.AddRange(new object[] {
            "取料",
            "放料"});
            this.cbOrder_Robot.Location = new System.Drawing.Point(159, 81);
            this.cbOrder_Robot.Margin = new System.Windows.Forms.Padding(2);
            this.cbOrder_Robot.Name = "cbOrder_Robot";
            this.cbOrder_Robot.Size = new System.Drawing.Size(109, 24);
            this.cbOrder_Robot.TabIndex = 4;
            // 
            // lbProductTypeOfRobot
            // 
            this.lbProductTypeOfRobot.AutoSize = true;
            this.lbProductTypeOfRobot.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbProductTypeOfRobot.Location = new System.Drawing.Point(25, 44);
            this.lbProductTypeOfRobot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbProductTypeOfRobot.Name = "lbProductTypeOfRobot";
            this.lbProductTypeOfRobot.Size = new System.Drawing.Size(114, 19);
            this.lbProductTypeOfRobot.TabIndex = 2;
            this.lbProductTypeOfRobot.Text = " 产品类型：";
            // 
            // lbOrderOfRobot
            // 
            this.lbOrderOfRobot.AutoSize = true;
            this.lbOrderOfRobot.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrderOfRobot.Location = new System.Drawing.Point(35, 83);
            this.lbOrderOfRobot.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbOrderOfRobot.Name = "lbOrderOfRobot";
            this.lbOrderOfRobot.Size = new System.Drawing.Size(104, 19);
            this.lbOrderOfRobot.TabIndex = 1;
            this.lbOrderOfRobot.Text = "动作类型：";
            // 
            // gbFrame
            // 
            this.gbFrame.Controls.Add(this.btnStartScan);
            this.gbFrame.Controls.Add(this.btnStart_Frame);
            this.gbFrame.Controls.Add(this.cbOrder_Frame);
            this.gbFrame.Controls.Add(this.cbGoalPos_Frame);
            this.gbFrame.Controls.Add(this.lbOrderOfFrame);
            this.gbFrame.Controls.Add(this.lbTrayNoOfFrame);
            this.gbFrame.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbFrame.Location = new System.Drawing.Point(12, 18);
            this.gbFrame.Margin = new System.Windows.Forms.Padding(2);
            this.gbFrame.Name = "gbFrame";
            this.gbFrame.Padding = new System.Windows.Forms.Padding(2);
            this.gbFrame.Size = new System.Drawing.Size(460, 138);
            this.gbFrame.TabIndex = 37;
            this.gbFrame.TabStop = false;
            this.gbFrame.Text = "货架取放料";
            // 
            // btnStartScan
            // 
            this.btnStartScan.BackColor = System.Drawing.Color.PowderBlue;
            this.btnStartScan.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStartScan.Location = new System.Drawing.Point(68, 20);
            this.btnStartScan.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartScan.Name = "btnStartScan";
            this.btnStartScan.Size = new System.Drawing.Size(313, 41);
            this.btnStartScan.TabIndex = 7;
            this.btnStartScan.Text = "启动货架扫码";
            this.btnStartScan.UseVisualStyleBackColor = false;
            this.btnStartScan.Click += new System.EventHandler(this.onClick_Scan);
            // 
            // btnStart_Frame
            // 
            this.btnStart_Frame.BackColor = System.Drawing.Color.PowderBlue;
            this.btnStart_Frame.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart_Frame.Location = new System.Drawing.Point(317, 69);
            this.btnStart_Frame.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart_Frame.Name = "btnStart_Frame";
            this.btnStart_Frame.Size = new System.Drawing.Size(97, 59);
            this.btnStart_Frame.TabIndex = 6;
            this.btnStart_Frame.Text = "货架取料/放料";
            this.btnStart_Frame.UseVisualStyleBackColor = false;
            this.btnStart_Frame.Click += new System.EventHandler(this.onClick_FrameAction);
            // 
            // cbOrder_Frame
            // 
            this.cbOrder_Frame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder_Frame.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbOrder_Frame.FormattingEnabled = true;
            this.cbOrder_Frame.Items.AddRange(new object[] {
            "取料",
            "放料"});
            this.cbOrder_Frame.Location = new System.Drawing.Point(159, 105);
            this.cbOrder_Frame.Margin = new System.Windows.Forms.Padding(2);
            this.cbOrder_Frame.Name = "cbOrder_Frame";
            this.cbOrder_Frame.Size = new System.Drawing.Size(109, 24);
            this.cbOrder_Frame.TabIndex = 4;
            // 
            // cbGoalPos_Frame
            // 
            this.cbGoalPos_Frame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGoalPos_Frame.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbGoalPos_Frame.FormattingEnabled = true;
            this.cbGoalPos_Frame.Items.AddRange(new object[] {
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "B1",
            "B2",
            "B3",
            "B4",
            "B5",
            "C1",
            "C2",
            "C3",
            "C4",
            "C5",
            "D1",
            "D2",
            "D3",
            "D4",
            "D5",
            "E1",
            "E2",
            "E3",
            "E4",
            "E5",
            "F1",
            "F2",
            "F3",
            "F4",
            "F5",
            "G1",
            "G2",
            "G3",
            "G4",
            "G5",
            "H1",
            "H2",
            "H3",
            "H4",
            "H5"});
            this.cbGoalPos_Frame.Location = new System.Drawing.Point(159, 69);
            this.cbGoalPos_Frame.Margin = new System.Windows.Forms.Padding(2);
            this.cbGoalPos_Frame.Name = "cbGoalPos_Frame";
            this.cbGoalPos_Frame.Size = new System.Drawing.Size(109, 24);
            this.cbGoalPos_Frame.TabIndex = 3;
            // 
            // lbOrderOfFrame
            // 
            this.lbOrderOfFrame.AutoSize = true;
            this.lbOrderOfFrame.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOrderOfFrame.Location = new System.Drawing.Point(35, 107);
            this.lbOrderOfFrame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbOrderOfFrame.Name = "lbOrderOfFrame";
            this.lbOrderOfFrame.Size = new System.Drawing.Size(104, 19);
            this.lbOrderOfFrame.TabIndex = 1;
            this.lbOrderOfFrame.Text = "动作类型：";
            // 
            // lbTrayNoOfFrame
            // 
            this.lbTrayNoOfFrame.AutoSize = true;
            this.lbTrayNoOfFrame.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTrayNoOfFrame.Location = new System.Drawing.Point(35, 71);
            this.lbTrayNoOfFrame.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTrayNoOfFrame.Name = "lbTrayNoOfFrame";
            this.lbTrayNoOfFrame.Size = new System.Drawing.Size(85, 19);
            this.lbTrayNoOfFrame.TabIndex = 0;
            this.lbTrayNoOfFrame.Text = "料盘号：";
            // 
            // lbTitle
            // 
            this.lbTitle.AutoSize = true;
            this.lbTitle.Font = new System.Drawing.Font("楷体", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTitle.ForeColor = System.Drawing.Color.MediumBlue;
            this.lbTitle.Location = new System.Drawing.Point(420, 23);
            this.lbTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbTitle.Name = "lbTitle";
            this.lbTitle.Size = new System.Drawing.Size(137, 30);
            this.lbTitle.TabIndex = 3;
            this.lbTitle.Text = "单步控制";
            // 
            // StepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(988, 485);
            this.Controls.Add(this.lbTitle);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "StepForm";
            this.Tag = "";
            this.Text = "StepForm";
            this.panel.ResumeLayout(false);
            this.gbLoop.ResumeLayout(false);
            this.gbLoop.PerformLayout();
            this.gbCabinet.ResumeLayout(false);
            this.gbCabinet.PerformLayout();
            this.gbRobot.ResumeLayout(false);
            this.gbRobot.PerformLayout();
            this.gbFrame.ResumeLayout(false);
            this.gbFrame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lbTitle;
        private System.Windows.Forms.GroupBox gbCabinet;
        private System.Windows.Forms.ComboBox cbProductType_Cabinet;
        private System.Windows.Forms.Button btnTestStop_Cabinet;
        private System.Windows.Forms.Button btnTestStart_Cabinet;
        private System.Windows.Forms.ComboBox cbCabinetNo_Cabinet;
        private System.Windows.Forms.GroupBox gbFrame;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.Button btnStart_Frame;
        private System.Windows.Forms.ComboBox cbOrder_Frame;
        private System.Windows.Forms.ComboBox cbGoalPos_Frame;
        private System.Windows.Forms.Label lbOrderOfFrame;
        private System.Windows.Forms.Label lbTrayNoOfFrame;
        private System.Windows.Forms.GroupBox gbLoop;
        private System.Windows.Forms.ComboBox cbTrayNo_Loop;
        private System.Windows.Forms.Label lbCabinetOfLoop;
        private System.Windows.Forms.Label lbTrayNoOfLoop;
        private System.Windows.Forms.ComboBox cbProductType_Loop;
        private System.Windows.Forms.Label lbProductTypeOfLoop;
        private System.Windows.Forms.ComboBox cbCabinetNo_Loop;
        private System.Windows.Forms.Label lbSlotNoOfLoop;
        private System.Windows.Forms.ComboBox cbSlotNo_Loop;
        private System.Windows.Forms.Label lbCabinetOfCabinet;
        private System.Windows.Forms.Label lbProductTypeOfCabinet;
        private System.Windows.Forms.Button btnPutback_Loop;
        private System.Windows.Forms.Button btnTestStart_Loop;
        private System.Windows.Forms.Button btnTake_Loop;
        private System.Windows.Forms.Button btnTestStop_Loop;
        private System.Windows.Forms.GroupBox gbRobot;
        private System.Windows.Forms.CheckBox ckbAxis7Alone;
        private System.Windows.Forms.ComboBox cbSlotNo_Robot;
        private System.Windows.Forms.Button btnStart_Robot;
        private System.Windows.Forms.ComboBox cbProductType_Robot;
        private System.Windows.Forms.ComboBox cbPos_Robot;
        private System.Windows.Forms.Label lbPosOfRobot;
        private System.Windows.Forms.Label lbSlotNoOfRobot;
        private System.Windows.Forms.ComboBox cbOrder_Robot;
        private System.Windows.Forms.Label lbProductTypeOfRobot;
        private System.Windows.Forms.Label lbOrderOfRobot;
    }
}