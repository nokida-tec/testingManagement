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
            this.cbTrayNo_Loop = new System.Windows.Forms.ComboBox();
            this.btnPutback_Loop = new System.Windows.Forms.Button();
            this.btnTestStop_Loop = new System.Windows.Forms.Button();
            this.btnTestStart_Loop = new System.Windows.Forms.Button();
            this.btnTake_Loop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbProductType_Loop = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSlotNo_Loop = new System.Windows.Forms.ComboBox();
            this.cbCabinetNo_Loop = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbProductType_Cabinet = new System.Windows.Forms.ComboBox();
            this.btnTestStop_Cabinet = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnTestStart_Cabinet = new System.Windows.Forms.Button();
            this.cbCabinetNo_Cabinet = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.manul_gbRobt = new System.Windows.Forms.GroupBox();
            this.ckbAxis7Alone = new System.Windows.Forms.CheckBox();
            this.cbSlotNo_Robot = new System.Windows.Forms.ComboBox();
            this.btnStart_Robot = new System.Windows.Forms.Button();
            this.cbProductType_Robot = new System.Windows.Forms.ComboBox();
            this.cbPos_Robot = new System.Windows.Forms.ComboBox();
            this.manul_lbGoalPos1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbOrder_Robot = new System.Windows.Forms.ComboBox();
            this.manul_lbProductSort = new System.Windows.Forms.Label();
            this.manul_lbCommand1 = new System.Windows.Forms.Label();
            this.manul_gbFrame = new System.Windows.Forms.GroupBox();
            this.btnStartScan = new System.Windows.Forms.Button();
            this.btnStart_Frame = new System.Windows.Forms.Button();
            this.cbOrder_Frame = new System.Windows.Forms.ComboBox();
            this.cbGoalPos_Frame = new System.Windows.Forms.ComboBox();
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
            this.panel1.Location = new System.Drawing.Point(8, 67);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 411);
            this.panel1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbTrayNo_Loop);
            this.groupBox2.Controls.Add(this.btnPutback_Loop);
            this.groupBox2.Controls.Add(this.btnTestStop_Loop);
            this.groupBox2.Controls.Add(this.btnTestStart_Loop);
            this.groupBox2.Controls.Add(this.btnTake_Loop);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbProductType_Loop);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbSlotNo_Loop);
            this.groupBox2.Controls.Add(this.cbCabinetNo_Loop);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(501, 18);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(457, 229);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "单循环控制";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(22, 127);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "位置号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(22, 171);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "测试柜：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(22, 82);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 1;
            this.label2.Tag = "";
            this.label2.Text = "料盘号：";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(22, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "产品类型：";
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbProductType_Cabinet);
            this.groupBox1.Controls.Add(this.btnTestStop_Cabinet);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnTestStart_Cabinet);
            this.groupBox1.Controls.Add(this.cbCabinetNo_Cabinet);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(501, 269);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(457, 114);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测试机台";
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(22, 29);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 19);
            this.label5.TabIndex = 0;
            this.label5.Text = "测试柜：";
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(22, 71);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "产品类型：";
            // 
            // manul_gbRobt
            // 
            this.manul_gbRobt.Controls.Add(this.ckbAxis7Alone);
            this.manul_gbRobt.Controls.Add(this.cbSlotNo_Robot);
            this.manul_gbRobt.Controls.Add(this.btnStart_Robot);
            this.manul_gbRobt.Controls.Add(this.cbProductType_Robot);
            this.manul_gbRobt.Controls.Add(this.cbPos_Robot);
            this.manul_gbRobt.Controls.Add(this.manul_lbGoalPos1);
            this.manul_gbRobt.Controls.Add(this.label7);
            this.manul_gbRobt.Controls.Add(this.cbOrder_Robot);
            this.manul_gbRobt.Controls.Add(this.manul_lbProductSort);
            this.manul_gbRobt.Controls.Add(this.manul_lbCommand1);
            this.manul_gbRobt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_gbRobt.Location = new System.Drawing.Point(12, 170);
            this.manul_gbRobt.Margin = new System.Windows.Forms.Padding(2);
            this.manul_gbRobt.Name = "manul_gbRobt";
            this.manul_gbRobt.Padding = new System.Windows.Forms.Padding(2);
            this.manul_gbRobt.Size = new System.Drawing.Size(460, 213);
            this.manul_gbRobt.TabIndex = 36;
            this.manul_gbRobt.TabStop = false;
            this.manul_gbRobt.Text = "机器人取放料";
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
            this.cbProductType_Robot.SelectedIndexChanged += new System.EventHandler(this.manul_cbProductSort_SelectedIndexChanged);
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
            // manul_lbGoalPos1
            // 
            this.manul_lbGoalPos1.AutoSize = true;
            this.manul_lbGoalPos1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbGoalPos1.Location = new System.Drawing.Point(35, 122);
            this.manul_lbGoalPos1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.manul_lbGoalPos1.Name = "manul_lbGoalPos1";
            this.manul_lbGoalPos1.Size = new System.Drawing.Size(104, 19);
            this.manul_lbGoalPos1.TabIndex = 0;
            this.manul_lbGoalPos1.Text = "轨道位置：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(35, 161);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "位置号：";
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
            // manul_lbProductSort
            // 
            this.manul_lbProductSort.AutoSize = true;
            this.manul_lbProductSort.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbProductSort.Location = new System.Drawing.Point(25, 44);
            this.manul_lbProductSort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.manul_lbProductSort.Name = "manul_lbProductSort";
            this.manul_lbProductSort.Size = new System.Drawing.Size(114, 19);
            this.manul_lbProductSort.TabIndex = 2;
            this.manul_lbProductSort.Text = " 产品类型：";
            // 
            // manul_lbCommand1
            // 
            this.manul_lbCommand1.AutoSize = true;
            this.manul_lbCommand1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbCommand1.Location = new System.Drawing.Point(35, 83);
            this.manul_lbCommand1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.manul_lbCommand1.Name = "manul_lbCommand1";
            this.manul_lbCommand1.Size = new System.Drawing.Size(104, 19);
            this.manul_lbCommand1.TabIndex = 1;
            this.manul_lbCommand1.Text = "动作类型：";
            // 
            // manul_gbFrame
            // 
            this.manul_gbFrame.Controls.Add(this.btnStartScan);
            this.manul_gbFrame.Controls.Add(this.btnStart_Frame);
            this.manul_gbFrame.Controls.Add(this.cbOrder_Frame);
            this.manul_gbFrame.Controls.Add(this.cbGoalPos_Frame);
            this.manul_gbFrame.Controls.Add(this.manul_lbCommand2);
            this.manul_gbFrame.Controls.Add(this.manul_lbGoalPos2);
            this.manul_gbFrame.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_gbFrame.Location = new System.Drawing.Point(12, 18);
            this.manul_gbFrame.Margin = new System.Windows.Forms.Padding(2);
            this.manul_gbFrame.Name = "manul_gbFrame";
            this.manul_gbFrame.Padding = new System.Windows.Forms.Padding(2);
            this.manul_gbFrame.Size = new System.Drawing.Size(460, 138);
            this.manul_gbFrame.TabIndex = 37;
            this.manul_gbFrame.TabStop = false;
            this.manul_gbFrame.Text = "货架取放料";
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
            // manul_lbCommand2
            // 
            this.manul_lbCommand2.AutoSize = true;
            this.manul_lbCommand2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbCommand2.Location = new System.Drawing.Point(35, 107);
            this.manul_lbCommand2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.manul_lbCommand2.Name = "manul_lbCommand2";
            this.manul_lbCommand2.Size = new System.Drawing.Size(104, 19);
            this.manul_lbCommand2.TabIndex = 1;
            this.manul_lbCommand2.Text = "动作类型：";
            // 
            // manul_lbGoalPos2
            // 
            this.manul_lbGoalPos2.AutoSize = true;
            this.manul_lbGoalPos2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.manul_lbGoalPos2.Location = new System.Drawing.Point(35, 71);
            this.manul_lbGoalPos2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.manul_lbGoalPos2.Name = "manul_lbGoalPos2";
            this.manul_lbGoalPos2.Size = new System.Drawing.Size(85, 19);
            this.manul_lbGoalPos2.TabIndex = 0;
            this.manul_lbGoalPos2.Text = "料盘号：";
            // 
            // auto_lbName
            // 
            this.auto_lbName.AutoSize = true;
            this.auto_lbName.Font = new System.Drawing.Font("楷体", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.auto_lbName.ForeColor = System.Drawing.Color.MediumBlue;
            this.auto_lbName.Location = new System.Drawing.Point(420, 23);
            this.auto_lbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.auto_lbName.Name = "auto_lbName";
            this.auto_lbName.Size = new System.Drawing.Size(137, 30);
            this.auto_lbName.TabIndex = 3;
            this.auto_lbName.Text = "单步控制";
            // 
            // StepForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(988, 485);
            this.Controls.Add(this.auto_lbName);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
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
        private System.Windows.Forms.ComboBox cbProductType_Cabinet;
        private System.Windows.Forms.Button btnTestStop_Cabinet;
        private System.Windows.Forms.Button btnTestStart_Cabinet;
        private System.Windows.Forms.ComboBox cbCabinetNo_Cabinet;
        private System.Windows.Forms.GroupBox manul_gbFrame;
        private System.Windows.Forms.Button btnStartScan;
        private System.Windows.Forms.Button btnStart_Frame;
        private System.Windows.Forms.ComboBox cbOrder_Frame;
        private System.Windows.Forms.ComboBox cbGoalPos_Frame;
        private System.Windows.Forms.Label manul_lbCommand2;
        private System.Windows.Forms.Label manul_lbGoalPos2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbTrayNo_Loop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbProductType_Loop;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbCabinetNo_Loop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbSlotNo_Loop;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPutback_Loop;
        private System.Windows.Forms.Button btnTestStart_Loop;
        private System.Windows.Forms.Button btnTake_Loop;
        private System.Windows.Forms.Button btnTestStop_Loop;
        private System.Windows.Forms.GroupBox manul_gbRobt;
        private System.Windows.Forms.CheckBox ckbAxis7Alone;
        private System.Windows.Forms.ComboBox cbSlotNo_Robot;
        private System.Windows.Forms.Button btnStart_Robot;
        private System.Windows.Forms.ComboBox cbProductType_Robot;
        private System.Windows.Forms.ComboBox cbPos_Robot;
        private System.Windows.Forms.Label manul_lbGoalPos1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbOrder_Robot;
        private System.Windows.Forms.Label manul_lbProductSort;
        private System.Windows.Forms.Label manul_lbCommand1;
    }
}