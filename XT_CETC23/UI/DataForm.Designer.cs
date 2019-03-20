namespace XT_CETC23.SonForm
{
    partial class DataForm
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
            this.auto_lbName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.data_gbQuery = new System.Windows.Forms.GroupBox();
            this.data_tbQueryContent = new System.Windows.Forms.TextBox();
            this.data_btnTable = new System.Windows.Forms.Button();
            this.data_lbQuery = new System.Windows.Forms.Label();
            this.data_cbQueryCondition = new System.Windows.Forms.ComboBox();
            this.data_btnQuery = new System.Windows.Forms.Button();
            this.data_dgvQuery = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.data_gbQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_dgvQuery)).BeginInit();
            this.SuspendLayout();
            // 
            // auto_lbName
            // 
            this.auto_lbName.AutoSize = true;
            this.auto_lbName.Font = new System.Drawing.Font("楷体", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.auto_lbName.ForeColor = System.Drawing.Color.MediumBlue;
            this.auto_lbName.Location = new System.Drawing.Point(413, 31);
            this.auto_lbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.auto_lbName.Name = "auto_lbName";
            this.auto_lbName.Size = new System.Drawing.Size(137, 30);
            this.auto_lbName.TabIndex = 1;
            this.auto_lbName.Text = "查询界面";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.data_gbQuery);
            this.panel1.Location = new System.Drawing.Point(8, 73);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(973, 393);
            this.panel1.TabIndex = 2;
            // 
            // data_gbQuery
            // 
            this.data_gbQuery.Controls.Add(this.data_tbQueryContent);
            this.data_gbQuery.Controls.Add(this.data_btnTable);
            this.data_gbQuery.Controls.Add(this.data_lbQuery);
            this.data_gbQuery.Controls.Add(this.data_cbQueryCondition);
            this.data_gbQuery.Controls.Add(this.data_btnQuery);
            this.data_gbQuery.Controls.Add(this.data_dgvQuery);
            this.data_gbQuery.Location = new System.Drawing.Point(7, 9);
            this.data_gbQuery.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_gbQuery.Name = "data_gbQuery";
            this.data_gbQuery.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_gbQuery.Size = new System.Drawing.Size(952, 371);
            this.data_gbQuery.TabIndex = 0;
            this.data_gbQuery.TabStop = false;
            this.data_gbQuery.Text = "数据查询";
            // 
            // data_tbQueryContent
            // 
            this.data_tbQueryContent.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data_tbQueryContent.Location = new System.Drawing.Point(335, 317);
            this.data_tbQueryContent.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_tbQueryContent.Name = "data_tbQueryContent";
            this.data_tbQueryContent.Size = new System.Drawing.Size(105, 30);
            this.data_tbQueryContent.TabIndex = 5;
            // 
            // data_btnTable
            // 
            this.data_btnTable.BackColor = System.Drawing.Color.LightBlue;
            this.data_btnTable.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data_btnTable.Location = new System.Drawing.Point(658, 311);
            this.data_btnTable.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_btnTable.Name = "data_btnTable";
            this.data_btnTable.Size = new System.Drawing.Size(127, 43);
            this.data_btnTable.TabIndex = 4;
            this.data_btnTable.Text = "生成报表";
            this.data_btnTable.UseVisualStyleBackColor = false;
            this.data_btnTable.Click += new System.EventHandler(this.data_btnTable_Click);
            // 
            // data_lbQuery
            // 
            this.data_lbQuery.AutoSize = true;
            this.data_lbQuery.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data_lbQuery.Location = new System.Drawing.Point(81, 321);
            this.data_lbQuery.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.data_lbQuery.Name = "data_lbQuery";
            this.data_lbQuery.Size = new System.Drawing.Size(115, 21);
            this.data_lbQuery.TabIndex = 3;
            this.data_lbQuery.Text = "查询条件：";
            // 
            // data_cbQueryCondition
            // 
            this.data_cbQueryCondition.AutoCompleteCustomSource.AddRange(new string[] {
            "按批次",
            "按种类",
            "按日期",
            "按结果"});
            this.data_cbQueryCondition.BackColor = System.Drawing.SystemColors.Window;
            this.data_cbQueryCondition.Font = new System.Drawing.Font("楷体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data_cbQueryCondition.FormattingEnabled = true;
            this.data_cbQueryCondition.Items.AddRange(new object[] {
            "dbo.BasicID",
            "dbo.TaskAxlis2",
            "dbo.TaskAxlis7",
            "dbo.TaskCabinet",
            "dbo.TaskRobot",
            "dbo.MTR",
            "dbo.Frame",
            "dbo.SortData",
            "dbo.ActualData"});
            this.data_cbQueryCondition.Location = new System.Drawing.Point(210, 317);
            this.data_cbQueryCondition.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_cbQueryCondition.Name = "data_cbQueryCondition";
            this.data_cbQueryCondition.Size = new System.Drawing.Size(105, 28);
            this.data_cbQueryCondition.TabIndex = 2;
            // 
            // data_btnQuery
            // 
            this.data_btnQuery.BackColor = System.Drawing.Color.LightBlue;
            this.data_btnQuery.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.data_btnQuery.Location = new System.Drawing.Point(485, 311);
            this.data_btnQuery.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_btnQuery.Name = "data_btnQuery";
            this.data_btnQuery.Size = new System.Drawing.Size(127, 43);
            this.data_btnQuery.TabIndex = 1;
            this.data_btnQuery.Text = "查  询";
            this.data_btnQuery.UseVisualStyleBackColor = false;
            this.data_btnQuery.Click += new System.EventHandler(this.data_btnQuery_Click);
            // 
            // data_dgvQuery
            // 
            this.data_dgvQuery.BackgroundColor = System.Drawing.Color.Azure;
            this.data_dgvQuery.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_dgvQuery.Location = new System.Drawing.Point(13, 18);
            this.data_dgvQuery.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_dgvQuery.Name = "data_dgvQuery";
            this.data_dgvQuery.RowTemplate.Height = 30;
            this.data_dgvQuery.Size = new System.Drawing.Size(928, 279);
            this.data_dgvQuery.TabIndex = 0;
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(861, 485);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.auto_lbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "DataForm";
            this.Text = "DataForm";
            this.panel1.ResumeLayout(false);
            this.data_gbQuery.ResumeLayout(false);
            this.data_gbQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_dgvQuery)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label auto_lbName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox data_gbQuery;
        private System.Windows.Forms.Label data_lbQuery;
        private System.Windows.Forms.ComboBox data_cbQueryCondition;
        private System.Windows.Forms.Button data_btnQuery;
        private System.Windows.Forms.DataGridView data_dgvQuery;
        private System.Windows.Forms.Button data_btnTable;
        private System.Windows.Forms.TextBox data_tbQueryContent;
    }
}