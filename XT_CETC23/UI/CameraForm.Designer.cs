namespace XT_CETC23.SonForm
{
    partial class CameraForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CameraForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Editbutton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.RMSBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AngleBox = new System.Windows.Forms.TextBox();
            this.YBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectcomboBox = new System.Windows.Forms.ComboBox();
            this.TriggerButton = new System.Windows.Forms.Button();
            this.XBox = new System.Windows.Forms.TextBox();
            this.Calibbutton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SelectcomboBox1 = new System.Windows.Forms.ComboBox();
            this.cogRecordDisplay1 = null;
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            if (Config.Config.ENABLED_VISIONPRO == true)
            {
                this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
                ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            }
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            if (Config.Config.ENABLED_VISIONPRO == true)
            {
                this.tableLayoutPanel1.Controls.Add(this.cogRecordDisplay1, 0, 0);
            }
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1482, 727);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.Editbutton, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.RMSBox, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.textBox4, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.AngleBox, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.YBox, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.SelectcomboBox, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.TriggerButton, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.XBox, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.Calibbutton, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.checkBox1, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.SelectcomboBox1, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(1040, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 12;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(439, 721);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // Editbutton
            // 
            this.Editbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Editbutton.BackColor = System.Drawing.Color.PowderBlue;
            this.Editbutton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Editbutton.Location = new System.Drawing.Point(202, 459);
            this.Editbutton.Name = "Editbutton";
            this.Editbutton.Size = new System.Drawing.Size(210, 50);
            this.Editbutton.TabIndex = 17;
            this.Editbutton.Text = "Edit";
            this.Editbutton.UseVisualStyleBackColor = false;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(22, 472);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 24);
            this.label8.TabIndex = 16;
            this.label8.Text = "视觉编辑：";
            // 
            // RMSBox
            // 
            this.RMSBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.RMSBox.BackColor = System.Drawing.SystemColors.Window;
            this.RMSBox.Location = new System.Drawing.Point(202, 402);
            this.RMSBox.Multiline = true;
            this.RMSBox.Name = "RMSBox";
            this.RMSBox.Size = new System.Drawing.Size(210, 50);
            this.RMSBox.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(52, 415);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 24);
            this.label6.TabIndex = 14;
            this.label6.Text = "RMS：";
            // 
            // textBox4
            // 
            this.textBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox4.Location = new System.Drawing.Point(202, 345);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(210, 50);
            this.textBox4.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(34, 358);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "Result：";
            // 
            // AngleBox
            // 
            this.AngleBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.AngleBox.Location = new System.Drawing.Point(202, 288);
            this.AngleBox.Multiline = true;
            this.AngleBox.Name = "AngleBox";
            this.AngleBox.Size = new System.Drawing.Size(210, 50);
            this.AngleBox.TabIndex = 11;
            // 
            // YBox
            // 
            this.YBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.YBox.BackColor = System.Drawing.SystemColors.Window;
            this.YBox.Location = new System.Drawing.Point(202, 231);
            this.YBox.Multiline = true;
            this.YBox.Name = "YBox";
            this.YBox.Size = new System.Drawing.Size(210, 50);
            this.YBox.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(40, 301);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 24);
            this.label7.TabIndex = 8;
            this.label7.Text = "Angle：";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(64, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 24);
            this.label5.TabIndex = 6;
            this.label5.Text = "Y：";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(64, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "X：";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(22, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 24);
            this.label2.TabIndex = 2;
            this.label2.Text = "相机触发：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "程序选择：";
            // 
            // SelectcomboBox
            // 
            this.SelectcomboBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SelectcomboBox.BackColor = System.Drawing.SystemColors.Window;
            this.SelectcomboBox.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectcomboBox.FormattingEnabled = true;
            this.SelectcomboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.SelectcomboBox.Location = new System.Drawing.Point(202, 10);
            this.SelectcomboBox.Name = "SelectcomboBox";
            this.SelectcomboBox.Size = new System.Drawing.Size(210, 36);
            this.SelectcomboBox.TabIndex = 1;
            // 
            // TriggerButton
            // 
            this.TriggerButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TriggerButton.BackColor = System.Drawing.Color.PowderBlue;
            this.TriggerButton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TriggerButton.Location = new System.Drawing.Point(202, 117);
            this.TriggerButton.Name = "TriggerButton";
            this.TriggerButton.Size = new System.Drawing.Size(210, 50);
            this.TriggerButton.TabIndex = 3;
            this.TriggerButton.Text = "Trigger";
            this.TriggerButton.UseVisualStyleBackColor = false;
            this.TriggerButton.Click += new System.EventHandler(this.TriggerButton_Click);
            // 
            // XBox
            // 
            this.XBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.XBox.BackColor = System.Drawing.SystemColors.Window;
            this.XBox.Location = new System.Drawing.Point(202, 174);
            this.XBox.Multiline = true;
            this.XBox.Name = "XBox";
            this.XBox.Size = new System.Drawing.Size(210, 50);
            this.XBox.TabIndex = 9;
            // 
            // Calibbutton
            // 
            this.Calibbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Calibbutton.BackColor = System.Drawing.Color.PowderBlue;
            this.Calibbutton.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Calibbutton.Location = new System.Drawing.Point(202, 573);
            this.Calibbutton.Name = "Calibbutton";
            this.Calibbutton.Size = new System.Drawing.Size(210, 50);
            this.Calibbutton.TabIndex = 19;
            this.Calibbutton.Text = "Calib";
            this.Calibbutton.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(46, 586);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 24);
            this.label9.TabIndex = 18;
            this.label9.Text = "标定：";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(232, 521);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(150, 40);
            this.checkBox1.TabIndex = 21;
            this.checkBox1.Text = "标定选择";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(22, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(130, 24);
            this.label10.TabIndex = 22;
            this.label10.Text = "数量选择：";
            // 
            // SelectcomboBox1
            // 
            this.SelectcomboBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.SelectcomboBox1.BackColor = System.Drawing.SystemColors.Window;
            this.SelectcomboBox1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SelectcomboBox1.FormattingEnabled = true;
            this.SelectcomboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.SelectcomboBox1.Location = new System.Drawing.Point(202, 67);
            this.SelectcomboBox1.Name = "SelectcomboBox1";
            this.SelectcomboBox1.Size = new System.Drawing.Size(210, 36);
            this.SelectcomboBox1.TabIndex = 23;
            // 
            // cogRecordDisplay1
            // 
            if (Config.Config.ENABLED_VISIONPRO == true)
            {
                this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
                this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
                this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
                this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
                this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
                this.cogRecordDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
                this.cogRecordDisplay1.Location = new System.Drawing.Point(3, 3);
                this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
                this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
                this.cogRecordDisplay1.Name = "cogRecordDisplay1";
                this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
                this.cogRecordDisplay1.Size = new System.Drawing.Size(1031, 721);
                this.cogRecordDisplay1.TabIndex = 2;
            }
            // 
            // CameraForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(1482, 727);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CameraForm";
            this.Text = "CameraForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            if (Config.Config.ENABLED_VISIONPRO == true)
            {
                ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            }
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button Editbutton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox RMSBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox AngleBox;
        private System.Windows.Forms.TextBox YBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox SelectcomboBox;
        private System.Windows.Forms.Button TriggerButton;
        private System.Windows.Forms.TextBox XBox;
        private System.Windows.Forms.Button Calibbutton;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox SelectcomboBox1;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
    }
}