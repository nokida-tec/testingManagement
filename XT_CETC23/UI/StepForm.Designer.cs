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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.step_cbTrayNo = new System.Windows.Forms.ComboBox();
            this.step_btnFetch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.step_btnTestStart = new System.Windows.Forms.Button();
            this.step_btnTake = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.step_cbProductSort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.step_cbProductNo = new System.Windows.Forms.ComboBox();
            this.step_cbCabinetNo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.manul_cbCabineitType = new System.Windows.Forms.ComboBox();
            this.manul_btnStopT = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.manul_btnStartT = new System.Windows.Forms.Button();
            this.manul_cbCabineit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.manul_gbRobt = new System.Windows.Forms.GroupBox();
            this.ckbAxis7Alone = new System.Windows.Forms.CheckBox();
            this.manul_cbTrayNo = new System.Windows.Forms.ComboBox();
            this.manul_cbProductNum = new System.Windows.Forms.ComboBox();
            this.manul_btnStart1 = new System.Windows.Forms.Button();
            this.manul_cbProductSort = new System.Windows.Forms.ComboBox();
            this.manul_cbGoalPos = new System.Windows.Forms.ComboBox();
            this.manul_lbGoalPos1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.manul_cbCommand = new System.Windows.Forms.ComboBox();
            this.manul_lbProductSort = new System.Windows.Forms.Label();
            this.manul_lbCommand1 = new System.Windows.Forms.Label();
            this.manul_gbFrame = new System.Windows.Forms.GroupBox();
            this.manul_btnStartScan = new System.Windows.Forms.Button();
            this.manul_btnStart2 = new System.Windows.Forms.Button();
            this.manul_cbCommand2 = new System.Windows.Forms.ComboBox();
            this.manul_cbGoalPos2 = new System.Windows.Forms.ComboBox();
            this.manul_lbCommand2 = new System.Windows.Forms.Label();
            this.manul_lbGoalPos2 = new System.Windows.Forms.Label();
            this.auto_lbName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.manul_gbRobt.SuspendLayout();
            this.manul_gbFrame.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.manul_gbRobt);
            this.panel1.Controls.Add(this.manul_gbFrame);
            this.panel1.Location = new System.Drawing.Point(12, 101);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1458, 614);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.step_cbTrayNo);
            this.groupBox2.Controls.Add(this.step_btnFetch);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.step_btnTestStart);
            this.groupBox2.Controls.Add(this.step_btnTake);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.step_cbProductSort);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.step_cbProductNo);
            this.groupBox2.Controls.Add(this.step_cbCabinetNo);
            this.groupBox2.Location = new System.Drawing.Point(752, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(685, 343);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单循环控制";
            // 
            // step_cbTrayNo
            // 
            this.step_cbTrayNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.step_cbTrayNo.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.step_cbTrayNo.FormattingEnabled = true;
            this.step_cbTrayNo.Items.AddRange(new object[] {
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
            this.step_cbTrayNo.Location = new System.Drawing.Point(213, 121);
            this.step_cbTrayNo.Name = "step_cbTrayNo";
            this.step_cbTrayNo.Size = new System.Drawing.Size(162, 32);
            this.step_cbTrayNo.TabIndex = 4;
            // 
            // step_btnFetch
            // 
            this.step_btnFetch.BackColor = System.Drawing.Color.PowderBlue;
            this.step_btnFetch.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.step_btnFetch.Location = new System.Drawing.Point(430, 252);
            this.step_btnFetch.Name = "step_btnFetch";
            this.step_btnFetch.Size = new System.Drawing.Size(219, 46);
            this.step_btnFetch.TabIndex = 6;
            this.step_btnFetch.Text = "取回到料架";
            this.step_btnFetch.UseVisualStyleBackColor = false;
            this.step_btnFetch.Click += new System.EventHandler(this.step_btnFetch_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.PowderBlue;
            this.button1.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(430, 183);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(219, 46);
            this.button1.TabIndex = 6;
            this.button1.Text = "停止测试";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // step_btnTestStart
            // 
            this.step_btnTestStart.BackColor = System.Drawing.Color.PowderBlue;
            this.step_btnTestStart.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.step_btnTestStart.Location = new System.Drawing.Point(430, 114);
            this.step_btnTestStart.Name = "step_btnTestStart";
            this.step_btnTestStart.Size = new System.Drawing.Size(219, 46);
            this.step_btnTestStart.TabIndex = 6;
            this.step_btnTestStart.Text = "开始测试";
            this.step_btnTestStart.UseVisualStyleBackColor = false;
            this.step_btnTestStart.Click += new System.EventHandler(this.step_btnTestStart_Click);
            // 
            // step_btnTake
            // 
            this.step_btnTake.BackColor = System.Drawing.Color.PowderBlue;
            this.step_btnTake.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.step_btnTake.Location = new System.Drawing.Point(430, 45);
            this.step_btnTake.Name = "step_btnTake";
            this.step_btnTake.Size = new System.Drawing.Size(219, 46);
            this.step_btnTake.TabIndex = 6;
            this.step_btnTake.Text = "取料到测试柜";
            this.step_btnTake.UseVisualStyleBackColor = false;
            this.step_btnTake.Click += new System.EventHandler(this.step_btnTake_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(33, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 28);
            this.label4.TabIndex = 0;
            this.label4.Text = "位置号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(33, 257);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "测试柜：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(33, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 28);
            this.label2.TabIndex = 1;
            this.label2.Tag = "";
            this.label2.Text = "料盘号：";
            // 
            // step_cbProductSort
            // 
            this.step_cbProductSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.step_cbProductSort.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.step_cbProductSort.FormattingEnabled = true;
            this.step_cbProductSort.Location = new System.Drawing.Point(213, 52);
            this.step_cbProductSort.Name = "step_cbProductSort";
            this.step_cbProductSort.Size = new System.Drawing.Size(162, 32);
            this.step_cbProductSort.TabIndex = 5;
            this.step_cbProductSort.SelectedIndexChanged += new System.EventHandler(this.step_cbProductSort_SelectedIndexChanged);
            this.step_cbProductSort.MouseClick += new System.Windows.Forms.MouseEventHandler(this.step_cbProductSort_MouseClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(33, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "产品类型：";
            // 
            // step_cbProductNo
            // 
            this.step_cbProductNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.step_cbProductNo.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.step_cbProductNo.FormattingEnabled = true;
            this.step_cbProductNo.Items.AddRange(new object[] {
            "料架位",
            "1#测试位",
            "2#测试位",
            "3#测试位",
            "4#测试位",
            "5#测试位",
            "6#测试位"});
            this.step_cbProductNo.Location = new System.Drawing.Point(213, 190);
            this.step_cbProductNo.Name = "step_cbProductNo";
            this.step_cbProductNo.Size = new System.Drawing.Size(162, 32);
            this.step_cbProductNo.TabIndex = 3;
            // 
            // step_cbCabinetNo
            // 
            this.step_cbCabinetNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.step_cbCabinetNo.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.step_cbCabinetNo.FormattingEnabled = true;
            this.step_cbCabinetNo.Items.AddRange(new object[] {
            "1#测试位",
            "2#测试位",
            "3#测试位",
            "4#测试位",
            "5#测试位",
            "6#测试位"});
            this.step_cbCabinetNo.Location = new System.Drawing.Point(213, 259);
            this.step_cbCabinetNo.Name = "step_cbCabinetNo";
            this.step_cbCabinetNo.Size = new System.Drawing.Size(162, 32);
            this.step_cbCabinetNo.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.manul_cbCabineitType);
            this.groupBox1.Controls.Add(this.manul_btnStopT);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.manul_btnStartT);
            this.groupBox1.Controls.Add(this.manul_cbCabineit);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(752, 403);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(685, 171);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试机台";
            // 
            // manul_cbCabineitType
            // 
            this.manul_cbCabineitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbCabineitType.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbCabineitType.FormattingEnabled = true;
            this.manul_cbCabineitType.Location = new System.Drawing.Point(213, 103);
            this.manul_cbCabineitType.Name = "manul_cbCabineitType";
            this.manul_cbCabineitType.Size = new System.Drawing.Size(162, 32);
            this.manul_cbCabineitType.TabIndex = 8;
            this.manul_cbCabineitType.MouseClick += new System.Windows.Forms.MouseEventHandler(this.manul_cbCabineitType_MouseClick);
            // 
            // manul_btnStopT
            // 
            this.manul_btnStopT.BackColor = System.Drawing.Color.PowderBlue;
            this.manul_btnStopT.Font = new System.Drawing.Font("楷体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_btnStopT.Location = new System.Drawing.Point(430, 95);
            this.manul_btnStopT.Name = "manul_btnStopT";
            this.manul_btnStopT.Size = new System.Drawing.Size(219, 47);
            this.manul_btnStopT.TabIndex = 35;
            this.manul_btnStopT.Text = "停止测试";
            this.manul_btnStopT.UseVisualStyleBackColor = false;
            this.manul_btnStopT.Click += new System.EventHandler(this.manul_btnStopT_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(33, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 28);
            this.label5.TabIndex = 0;
            this.label5.Text = "测试柜：";
            // 
            // manul_btnStartT
            // 
            this.manul_btnStartT.BackColor = System.Drawing.Color.PowderBlue;
            this.manul_btnStartT.Font = new System.Drawing.Font("楷体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_btnStartT.Location = new System.Drawing.Point(430, 35);
            this.manul_btnStartT.Name = "manul_btnStartT";
            this.manul_btnStartT.Size = new System.Drawing.Size(219, 47);
            this.manul_btnStartT.TabIndex = 7;
            this.manul_btnStartT.Text = "启动测试";
            this.manul_btnStartT.UseVisualStyleBackColor = false;
            this.manul_btnStartT.Click += new System.EventHandler(this.manul_btnStartT_Click);
            // 
            // manul_cbCabineit
            // 
            this.manul_cbCabineit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbCabineit.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbCabineit.FormattingEnabled = true;
            this.manul_cbCabineit.Items.AddRange(new object[] {
            "1#机台",
            "2#机台",
            "3#机台",
            "4#机台",
            "5#机台",
            "6#机台"});
            this.manul_cbCabineit.Location = new System.Drawing.Point(213, 40);
            this.manul_cbCabineit.Name = "manul_cbCabineit";
            this.manul_cbCabineit.Size = new System.Drawing.Size(162, 32);
            this.manul_cbCabineit.TabIndex = 34;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(33, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 28);
            this.label6.TabIndex = 2;
            this.label6.Text = "产品类型：";
            // 
            // manul_gbRobt
            // 
            this.manul_gbRobt.Controls.Add(this.ckbAxis7Alone);
            this.manul_gbRobt.Controls.Add(this.manul_cbTrayNo);
            this.manul_gbRobt.Controls.Add(this.manul_cbProductNum);
            this.manul_gbRobt.Controls.Add(this.manul_btnStart1);
            this.manul_gbRobt.Controls.Add(this.manul_cbProductSort);
            this.manul_gbRobt.Controls.Add(this.manul_cbGoalPos);
            this.manul_gbRobt.Controls.Add(this.manul_lbGoalPos1);
            this.manul_gbRobt.Controls.Add(this.label8);
            this.manul_gbRobt.Controls.Add(this.label7);
            this.manul_gbRobt.Controls.Add(this.manul_cbCommand);
            this.manul_gbRobt.Controls.Add(this.manul_lbProductSort);
            this.manul_gbRobt.Controls.Add(this.manul_lbCommand1);
            this.manul_gbRobt.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_gbRobt.Location = new System.Drawing.Point(18, 255);
            this.manul_gbRobt.Name = "manul_gbRobt";
            this.manul_gbRobt.Size = new System.Drawing.Size(690, 319);
            this.manul_gbRobt.TabIndex = 36;
            this.manul_gbRobt.TabStop = false;
            this.manul_gbRobt.Text = "机器人取放料";
            // 
            // ckbAxis7Alone
            // 
            this.ckbAxis7Alone.AutoSize = true;
            this.ckbAxis7Alone.Location = new System.Drawing.Point(475, 243);
            this.ckbAxis7Alone.Name = "ckbAxis7Alone";
            this.ckbAxis7Alone.Size = new System.Drawing.Size(196, 22);
            this.ckbAxis7Alone.TabIndex = 8;
            this.ckbAxis7Alone.Text = "机器人轨道独立运动";
            this.ckbAxis7Alone.UseVisualStyleBackColor = true;
            // 
            // manul_cbTrayNo
            // 
            this.manul_cbTrayNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbTrayNo.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbTrayNo.FormattingEnabled = true;
            this.manul_cbTrayNo.Items.AddRange(new object[] {
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
            this.manul_cbTrayNo.Location = new System.Drawing.Point(238, 212);
            this.manul_cbTrayNo.Name = "manul_cbTrayNo";
            this.manul_cbTrayNo.Size = new System.Drawing.Size(162, 32);
            this.manul_cbTrayNo.TabIndex = 7;
            // 
            // manul_cbProductNum
            // 
            this.manul_cbProductNum.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbProductNum.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbProductNum.FormattingEnabled = true;
            this.manul_cbProductNum.Location = new System.Drawing.Point(238, 271);
            this.manul_cbProductNum.Name = "manul_cbProductNum";
            this.manul_cbProductNum.Size = new System.Drawing.Size(162, 32);
            this.manul_cbProductNum.TabIndex = 7;
            // 
            // manul_btnStart1
            // 
            this.manul_btnStart1.BackColor = System.Drawing.Color.PowderBlue;
            this.manul_btnStart1.Font = new System.Drawing.Font("楷体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_btnStart1.Location = new System.Drawing.Point(475, 79);
            this.manul_btnStart1.Name = "manul_btnStart1";
            this.manul_btnStart1.Size = new System.Drawing.Size(146, 141);
            this.manul_btnStart1.TabIndex = 6;
            this.manul_btnStart1.Text = "机器人取料/放料";
            this.manul_btnStart1.UseVisualStyleBackColor = false;
            this.manul_btnStart1.Click += new System.EventHandler(this.manul_btnStart1_Click);
            // 
            // manul_cbProductSort
            // 
            this.manul_cbProductSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbProductSort.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbProductSort.FormattingEnabled = true;
            this.manul_cbProductSort.Location = new System.Drawing.Point(238, 35);
            this.manul_cbProductSort.Name = "manul_cbProductSort";
            this.manul_cbProductSort.Size = new System.Drawing.Size(162, 32);
            this.manul_cbProductSort.TabIndex = 5;
            this.manul_cbProductSort.SelectedIndexChanged += new System.EventHandler(this.manul_cbProductSort_SelectedIndexChanged);
            this.manul_cbProductSort.Click += new System.EventHandler(this.manul_cbProductSort_Click);
            // 
            // manul_cbGoalPos
            // 
            this.manul_cbGoalPos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbGoalPos.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbGoalPos.FormattingEnabled = true;
            this.manul_cbGoalPos.Items.AddRange(new object[] {
            "料架位",
            "1#测试位",
            "2#测试位",
            "3#测试位",
            "4#测试位",
            "5#测试位",
            "6#测试位"});
            this.manul_cbGoalPos.Location = new System.Drawing.Point(238, 153);
            this.manul_cbGoalPos.Name = "manul_cbGoalPos";
            this.manul_cbGoalPos.Size = new System.Drawing.Size(162, 32);
            this.manul_cbGoalPos.TabIndex = 3;
            this.manul_cbGoalPos.SelectedIndexChanged += new System.EventHandler(this.manul_cbGoalPos_SelectedIndexChanged);
            // 
            // manul_lbGoalPos1
            // 
            this.manul_lbGoalPos1.AutoSize = true;
            this.manul_lbGoalPos1.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbGoalPos1.Location = new System.Drawing.Point(52, 155);
            this.manul_lbGoalPos1.Name = "manul_lbGoalPos1";
            this.manul_lbGoalPos1.Size = new System.Drawing.Size(152, 28);
            this.manul_lbGoalPos1.TabIndex = 0;
            this.manul_lbGoalPos1.Text = "轨道位置：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(52, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 28);
            this.label8.TabIndex = 0;
            this.label8.Text = "料盘号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(52, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 28);
            this.label7.TabIndex = 0;
            this.label7.Text = "位置号：";
            // 
            // manul_cbCommand
            // 
            this.manul_cbCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbCommand.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbCommand.FormattingEnabled = true;
            this.manul_cbCommand.Items.AddRange(new object[] {
            "取料",
            "放料"});
            this.manul_cbCommand.Location = new System.Drawing.Point(238, 94);
            this.manul_cbCommand.Name = "manul_cbCommand";
            this.manul_cbCommand.Size = new System.Drawing.Size(162, 32);
            this.manul_cbCommand.TabIndex = 4;
            // 
            // manul_lbProductSort
            // 
            this.manul_lbProductSort.AutoSize = true;
            this.manul_lbProductSort.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbProductSort.Location = new System.Drawing.Point(38, 37);
            this.manul_lbProductSort.Name = "manul_lbProductSort";
            this.manul_lbProductSort.Size = new System.Drawing.Size(166, 28);
            this.manul_lbProductSort.TabIndex = 2;
            this.manul_lbProductSort.Text = " 产品类型：";
            // 
            // manul_lbCommand1
            // 
            this.manul_lbCommand1.AutoSize = true;
            this.manul_lbCommand1.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbCommand1.Location = new System.Drawing.Point(52, 97);
            this.manul_lbCommand1.Name = "manul_lbCommand1";
            this.manul_lbCommand1.Size = new System.Drawing.Size(152, 28);
            this.manul_lbCommand1.TabIndex = 1;
            this.manul_lbCommand1.Text = "动作类型：";
            // 
            // manul_gbFrame
            // 
            this.manul_gbFrame.Controls.Add(this.manul_btnStartScan);
            this.manul_gbFrame.Controls.Add(this.manul_btnStart2);
            this.manul_gbFrame.Controls.Add(this.manul_cbCommand2);
            this.manul_gbFrame.Controls.Add(this.manul_cbGoalPos2);
            this.manul_gbFrame.Controls.Add(this.manul_lbCommand2);
            this.manul_gbFrame.Controls.Add(this.manul_lbGoalPos2);
            this.manul_gbFrame.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_gbFrame.Location = new System.Drawing.Point(18, 27);
            this.manul_gbFrame.Name = "manul_gbFrame";
            this.manul_gbFrame.Size = new System.Drawing.Size(690, 207);
            this.manul_gbFrame.TabIndex = 37;
            this.manul_gbFrame.TabStop = false;
            this.manul_gbFrame.Text = "货架取放料";
            // 
            // manul_btnStartScan
            // 
            this.manul_btnStartScan.BackColor = System.Drawing.Color.PowderBlue;
            this.manul_btnStartScan.Font = new System.Drawing.Font("楷体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_btnStartScan.Location = new System.Drawing.Point(102, 23);
            this.manul_btnStartScan.Name = "manul_btnStartScan";
            this.manul_btnStartScan.Size = new System.Drawing.Size(469, 61);
            this.manul_btnStartScan.TabIndex = 7;
            this.manul_btnStartScan.Text = "启动货架扫码";
            this.manul_btnStartScan.UseVisualStyleBackColor = false;
            this.manul_btnStartScan.Click += new System.EventHandler(this.manul_btnStartScan_Click);
            // 
            // manul_btnStart2
            // 
            this.manul_btnStart2.BackColor = System.Drawing.Color.PowderBlue;
            this.manul_btnStart2.Font = new System.Drawing.Font("楷体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_btnStart2.Location = new System.Drawing.Point(475, 100);
            this.manul_btnStart2.Name = "manul_btnStart2";
            this.manul_btnStart2.Size = new System.Drawing.Size(146, 88);
            this.manul_btnStart2.TabIndex = 6;
            this.manul_btnStart2.Text = "货架取料/放料";
            this.manul_btnStart2.UseVisualStyleBackColor = false;
            this.manul_btnStart2.Click += new System.EventHandler(this.manul_btnStart2_Click);
            // 
            // manul_cbCommand2
            // 
            this.manul_cbCommand2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbCommand2.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbCommand2.FormattingEnabled = true;
            this.manul_cbCommand2.Items.AddRange(new object[] {
            "取料",
            "放料"});
            this.manul_cbCommand2.Location = new System.Drawing.Point(238, 158);
            this.manul_cbCommand2.Name = "manul_cbCommand2";
            this.manul_cbCommand2.Size = new System.Drawing.Size(162, 32);
            this.manul_cbCommand2.TabIndex = 4;
            // 
            // manul_cbGoalPos2
            // 
            this.manul_cbGoalPos2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.manul_cbGoalPos2.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_cbGoalPos2.FormattingEnabled = true;
            this.manul_cbGoalPos2.Items.AddRange(new object[] {
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
            this.manul_cbGoalPos2.Location = new System.Drawing.Point(238, 100);
            this.manul_cbGoalPos2.Name = "manul_cbGoalPos2";
            this.manul_cbGoalPos2.Size = new System.Drawing.Size(162, 32);
            this.manul_cbGoalPos2.TabIndex = 3;
            // 
            // manul_lbCommand2
            // 
            this.manul_lbCommand2.AutoSize = true;
            this.manul_lbCommand2.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbCommand2.Location = new System.Drawing.Point(52, 160);
            this.manul_lbCommand2.Name = "manul_lbCommand2";
            this.manul_lbCommand2.Size = new System.Drawing.Size(152, 28);
            this.manul_lbCommand2.TabIndex = 1;
            this.manul_lbCommand2.Text = "动作类型：";
            // 
            // manul_lbGoalPos2
            // 
            this.manul_lbGoalPos2.AutoSize = true;
            this.manul_lbGoalPos2.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbGoalPos2.Location = new System.Drawing.Point(52, 104);
            this.manul_lbGoalPos2.Name = "manul_lbGoalPos2";
            this.manul_lbGoalPos2.Size = new System.Drawing.Size(124, 28);
            this.manul_lbGoalPos2.TabIndex = 0;
            this.manul_lbGoalPos2.Text = "料盘号：";
            // 
            // auto_lbName
            // 
            this.auto_lbName.AutoSize = true;
            this.auto_lbName.Font = new System.Drawing.Font("楷体", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.auto_lbName.ForeColor = System.Drawing.Color.MediumBlue;
            this.auto_lbName.Location = new System.Drawing.Point(630, 34);
            this.auto_lbName.Name = "auto_lbName";
            this.auto_lbName.Size = new System.Drawing.Size(200, 44);
            this.auto_lbName.TabIndex = 3;
            this.auto_lbName.Text = "单步控制";
            // 
            // StepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1482, 727);
            this.Controls.Add(this.auto_lbName);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StepForm";
            this.Tag = "";
            this.Text = "StepForm";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.manul_gbRobt.ResumeLayout(false);
            this.manul_gbRobt.PerformLayout();
            this.manul_gbFrame.ResumeLayout(false);
            this.manul_gbFrame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label auto_lbName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox manul_cbCabineitType;
        private System.Windows.Forms.Button manul_btnStopT;
        private System.Windows.Forms.Button manul_btnStartT;
        private System.Windows.Forms.ComboBox manul_cbCabineit;
        private System.Windows.Forms.GroupBox manul_gbFrame;
        private System.Windows.Forms.Button manul_btnStartScan;
        private System.Windows.Forms.Button manul_btnStart2;
        private System.Windows.Forms.ComboBox manul_cbCommand2;
        private System.Windows.Forms.ComboBox manul_cbGoalPos2;
        private System.Windows.Forms.Label manul_lbCommand2;
        private System.Windows.Forms.Label manul_lbGoalPos2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox step_cbTrayNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox step_cbProductSort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox step_cbCabinetNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox step_cbProductNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button step_btnFetch;
        private System.Windows.Forms.Button step_btnTestStart;
        private System.Windows.Forms.Button step_btnTake;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox manul_gbRobt;
        private System.Windows.Forms.CheckBox ckbAxis7Alone;
        private System.Windows.Forms.ComboBox manul_cbProductNum;
        private System.Windows.Forms.Button manul_btnStart1;
        private System.Windows.Forms.ComboBox manul_cbProductSort;
        private System.Windows.Forms.ComboBox manul_cbGoalPos;
        private System.Windows.Forms.Label manul_lbGoalPos1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox manul_cbCommand;
        private System.Windows.Forms.Label manul_lbProductSort;
        private System.Windows.Forms.Label manul_lbCommand1;
        private System.Windows.Forms.ComboBox manul_cbTrayNo;
        private System.Windows.Forms.Label label8;
    }
}