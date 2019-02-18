namespace XT_CETC23.SonForm
{
    partial class ParamForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGV_Product = new System.Windows.Forms.DataGridView();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serialNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productDefBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dB23DataSet = new XT_CETC23.DB23DataSet();
            this.btnSaveProdCode = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGV_Operate = new System.Windows.Forms.DataGridView();
            this.productTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.opSeqDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.operateDefBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnSaveProdStatus = new System.Windows.Forms.Button();
            this.operateDefTableAdapter = new XT_CETC23.DB23DataSetTableAdapters.OperateDefTableAdapter();
            this.productDefTableAdapter = new XT_CETC23.DB23DataSetTableAdapters.ProductDefTableAdapter();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_Product)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productDefBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB23DataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_Operate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.operateDefBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGV_Product);
            this.groupBox1.Controls.Add(this.btnSaveProdCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(15);
            this.groupBox1.Size = new System.Drawing.Size(481, 443);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "产品编码配置";
            // 
            // dataGV_Product
            // 
            this.dataGV_Product.AllowUserToOrderColumns = true;
            this.dataGV_Product.AutoGenerateColumns = false;
            this.dataGV_Product.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGV_Product.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV_Product.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeDataGridViewTextBoxColumn,
            this.serialNoDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn1});
            this.dataGV_Product.DataSource = this.productDefBindingSource;
            this.dataGV_Product.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGV_Product.Location = new System.Drawing.Point(15, 37);
            this.dataGV_Product.Name = "dataGV_Product";
            this.dataGV_Product.RowTemplate.Height = 23;
            this.dataGV_Product.Size = new System.Drawing.Size(451, 356);
            this.dataGV_Product.TabIndex = 3;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "产品类型";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            // 
            // serialNoDataGridViewTextBoxColumn
            // 
            this.serialNoDataGridViewTextBoxColumn.DataPropertyName = "SerialNo";
            this.serialNoDataGridViewTextBoxColumn.HeaderText = "产品编号";
            this.serialNoDataGridViewTextBoxColumn.Name = "serialNoDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "产品名称";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            // 
            // productDefBindingSource
            // 
            this.productDefBindingSource.DataMember = "ProductDef";
            this.productDefBindingSource.DataSource = this.dB23DataSet;
            // 
            // dB23DataSet
            // 
            this.dB23DataSet.DataSetName = "DB23DataSet";
            this.dB23DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnSaveProdCode
            // 
            this.btnSaveProdCode.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSaveProdCode.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveProdCode.Location = new System.Drawing.Point(15, 393);
            this.btnSaveProdCode.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.btnSaveProdCode.Name = "btnSaveProdCode";
            this.btnSaveProdCode.Size = new System.Drawing.Size(451, 35);
            this.btnSaveProdCode.TabIndex = 2;
            this.btnSaveProdCode.Text = "保  存";
            this.btnSaveProdCode.UseVisualStyleBackColor = true;
            this.btnSaveProdCode.Click += new System.EventHandler(this.btnSaveProdCode_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGV_Operate);
            this.groupBox2.Controls.Add(this.btnSaveProdStatus);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(507, 10);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(15);
            this.groupBox2.Size = new System.Drawing.Size(471, 443);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "产品状态配置";
            // 
            // dataGV_Operate
            // 
            this.dataGV_Operate.AllowUserToOrderColumns = true;
            this.dataGV_Operate.AutoGenerateColumns = false;
            this.dataGV_Operate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGV_Operate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGV_Operate.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.productTypeDataGridViewTextBoxColumn,
            this.opSeqDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn});
            this.dataGV_Operate.DataSource = this.operateDefBindingSource;
            this.dataGV_Operate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGV_Operate.Location = new System.Drawing.Point(15, 38);
            this.dataGV_Operate.Name = "dataGV_Operate";
            this.dataGV_Operate.RowTemplate.Height = 23;
            this.dataGV_Operate.Size = new System.Drawing.Size(441, 355);
            this.dataGV_Operate.TabIndex = 2;
            // 
            // productTypeDataGridViewTextBoxColumn
            // 
            this.productTypeDataGridViewTextBoxColumn.DataPropertyName = "ProductType";
            this.productTypeDataGridViewTextBoxColumn.HeaderText = "产品类型";
            this.productTypeDataGridViewTextBoxColumn.Name = "productTypeDataGridViewTextBoxColumn";
            // 
            // opSeqDataGridViewTextBoxColumn
            // 
            this.opSeqDataGridViewTextBoxColumn.DataPropertyName = "OpSeq";
            this.opSeqDataGridViewTextBoxColumn.HeaderText = "状态编号";
            this.opSeqDataGridViewTextBoxColumn.Name = "opSeqDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "状态名称";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // operateDefBindingSource
            // 
            this.operateDefBindingSource.DataMember = "OperateDef";
            this.operateDefBindingSource.DataSource = this.dB23DataSet;
            // 
            // btnSaveProdStatus
            // 
            this.btnSaveProdStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSaveProdStatus.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveProdStatus.Location = new System.Drawing.Point(15, 393);
            this.btnSaveProdStatus.Name = "btnSaveProdStatus";
            this.btnSaveProdStatus.Size = new System.Drawing.Size(441, 35);
            this.btnSaveProdStatus.TabIndex = 1;
            this.btnSaveProdStatus.Text = "保  存";
            this.btnSaveProdStatus.UseVisualStyleBackColor = true;
            this.btnSaveProdStatus.Click += new System.EventHandler(this.btnSaveProdStatus_Click);
            // 
            // operateDefTableAdapter
            // 
            this.operateDefTableAdapter.ClearBeforeFill = true;
            // 
            // productDefTableAdapter
            // 
            this.productDefTableAdapter.ClearBeforeFill = true;
            // 
            // ParamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 463);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParamForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "ParamForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ParamForm_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_Product)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productDefBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dB23DataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGV_Operate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.operateDefBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSaveProdCode;
        private System.Windows.Forms.Button btnSaveProdStatus;
        private DB23DataSet dB23DataSet;
        private System.Windows.Forms.BindingSource operateDefBindingSource;
        private DB23DataSetTableAdapters.OperateDefTableAdapter operateDefTableAdapter;
        private System.Windows.Forms.DataGridView dataGV_Product;
        private System.Windows.Forms.BindingSource productDefBindingSource;
        private DB23DataSetTableAdapters.ProductDefTableAdapter productDefTableAdapter;
        private System.Windows.Forms.DataGridView dataGV_Operate;
        private System.Windows.Forms.DataGridViewTextBoxColumn productTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn opSeqDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialNoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
    }
}