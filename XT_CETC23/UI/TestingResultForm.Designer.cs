namespace XT_CETC23.UI
{
    partial class TestingResultForm
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
            this.dateTimePickerToday = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panelCommand = new System.Windows.Forms.Panel();
            this.comboFrameLot = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.dB23DataSet = new XT_CETC23.DB23DataSet();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.productIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkResultDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkCabinetADataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.frameLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salverLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.checkBatchDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.beginTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endTimeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelCommand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dB23DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerToday
            // 
            this.dateTimePickerToday.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dateTimePickerToday.Location = new System.Drawing.Point(77, 10);
            this.dateTimePickerToday.Name = "dateTimePickerToday";
            this.dateTimePickerToday.Size = new System.Drawing.Size(200, 30);
            this.dateTimePickerToday.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "日期:";
            // 
            // btnSearch
            // 
            this.btnSearch.AutoSize = true;
            this.btnSearch.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(611, 11);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // panelCommand
            // 
            this.panelCommand.Controls.Add(this.comboFrameLot);
            this.panelCommand.Controls.Add(this.label2);
            this.panelCommand.Controls.Add(this.btnExport);
            this.panelCommand.Controls.Add(this.label1);
            this.panelCommand.Controls.Add(this.btnSearch);
            this.panelCommand.Controls.Add(this.dateTimePickerToday);
            this.panelCommand.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCommand.Location = new System.Drawing.Point(0, 0);
            this.panelCommand.Name = "panelCommand";
            this.panelCommand.Size = new System.Drawing.Size(948, 54);
            this.panelCommand.TabIndex = 3;
            // 
            // comboFrameLot
            // 
            this.comboFrameLot.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboFrameLot.FormattingEnabled = true;
            this.comboFrameLot.Location = new System.Drawing.Point(392, 12);
            this.comboFrameLot.Name = "comboFrameLot";
            this.comboFrameLot.Size = new System.Drawing.Size(213, 28);
            this.comboFrameLot.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(293, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "料架批号:";
            // 
            // btnExport
            // 
            this.btnExport.AutoSize = true;
            this.btnExport.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExport.Location = new System.Drawing.Point(881, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 30);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "导出";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // dB23DataSet
            // 
            this.dB23DataSet.DataSetName = "DB23DataSet";
            this.dB23DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridView
            // 
            this.dataGridView.AutoGenerateColumns = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productIDDataGridViewTextBoxColumn,
            this.productTypeDataGridViewTextBoxColumn,
            this.checkResultDataGridViewTextBoxColumn,
            this.checkCabinetADataGridViewTextBoxColumn,
            this.frameLocationDataGridViewTextBoxColumn,
            this.salverLocationDataGridViewTextBoxColumn,
            this.checkBatchDataGridViewTextBoxColumn,
            this.beginTimeDataGridViewTextBoxColumn,
            this.endTimeDataGridViewTextBoxColumn});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 54);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(948, 393);
            this.dataGridView.TabIndex = 4;
            this.dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellContentClick);
            // 
            // productIDDataGridViewTextBoxColumn
            // 
            this.productIDDataGridViewTextBoxColumn.DataPropertyName = "ProductID";
            this.productIDDataGridViewTextBoxColumn.HeaderText = "产品编码";
            this.productIDDataGridViewTextBoxColumn.Name = "productIDDataGridViewTextBoxColumn";
            // 
            // productTypeDataGridViewTextBoxColumn
            // 
            this.productTypeDataGridViewTextBoxColumn.DataPropertyName = "ProductType";
            this.productTypeDataGridViewTextBoxColumn.HeaderText = "产品类型";
            this.productTypeDataGridViewTextBoxColumn.Name = "productTypeDataGridViewTextBoxColumn";
            // 
            // checkResultDataGridViewTextBoxColumn
            // 
            this.checkResultDataGridViewTextBoxColumn.DataPropertyName = "CheckResult";
            this.checkResultDataGridViewTextBoxColumn.HeaderText = "测试结果";
            this.checkResultDataGridViewTextBoxColumn.Name = "checkResultDataGridViewTextBoxColumn";
            // 
            // checkCabinetADataGridViewTextBoxColumn
            // 
            this.checkCabinetADataGridViewTextBoxColumn.DataPropertyName = "CheckCabinetA";
            this.checkCabinetADataGridViewTextBoxColumn.HeaderText = "测试柜";
            this.checkCabinetADataGridViewTextBoxColumn.Name = "checkCabinetADataGridViewTextBoxColumn";
            // 
            // frameLocationDataGridViewTextBoxColumn
            // 
            this.frameLocationDataGridViewTextBoxColumn.DataPropertyName = "FrameLocation";
            this.frameLocationDataGridViewTextBoxColumn.HeaderText = "仓位";
            this.frameLocationDataGridViewTextBoxColumn.Name = "frameLocationDataGridViewTextBoxColumn";
            // 
            // salverLocationDataGridViewTextBoxColumn
            // 
            this.salverLocationDataGridViewTextBoxColumn.DataPropertyName = "SalverLocation";
            this.salverLocationDataGridViewTextBoxColumn.HeaderText = "盘位";
            this.salverLocationDataGridViewTextBoxColumn.Name = "salverLocationDataGridViewTextBoxColumn";
            // 
            // checkBatchDataGridViewTextBoxColumn
            // 
            this.checkBatchDataGridViewTextBoxColumn.DataPropertyName = "CheckBatch";
            this.checkBatchDataGridViewTextBoxColumn.HeaderText = "批号";
            this.checkBatchDataGridViewTextBoxColumn.Name = "checkBatchDataGridViewTextBoxColumn";
            // 
            // beginTimeDataGridViewTextBoxColumn
            // 
            this.beginTimeDataGridViewTextBoxColumn.DataPropertyName = "BeginTime";
            this.beginTimeDataGridViewTextBoxColumn.HeaderText = "测试开始时间";
            this.beginTimeDataGridViewTextBoxColumn.Name = "beginTimeDataGridViewTextBoxColumn";
            // 
            // endTimeDataGridViewTextBoxColumn
            // 
            this.endTimeDataGridViewTextBoxColumn.DataPropertyName = "EndTime";
            this.endTimeDataGridViewTextBoxColumn.HeaderText = "测试结束时间";
            this.endTimeDataGridViewTextBoxColumn.Name = "endTimeDataGridViewTextBoxColumn";
            // 
            // TestingResultForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(948, 447);
            this.ControlBox = false;
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panelCommand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TestingResultForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.TestingResultForm_Load);
            this.panelCommand.ResumeLayout(false);
            this.panelCommand.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dB23DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerToday;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panelCommand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ComboBox comboFrameLot;
        private DB23DataSet dB23DataSet;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.BindingSource actualDataBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn productIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn productTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkResultDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkCabinetADataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn frameLocationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn salverLocationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn checkBatchDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn beginTimeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn endTimeDataGridViewTextBoxColumn;
    }
}