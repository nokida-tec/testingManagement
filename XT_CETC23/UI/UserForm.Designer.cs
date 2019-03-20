namespace XT_CETC23.SonForm
{
    partial class UserForm
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
            this.user_lbName = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.user_gbUser = new System.Windows.Forms.GroupBox();
            this.user_btnChange = new System.Windows.Forms.Button();
            this.user_btnDelete = new System.Windows.Forms.Button();
            this.user_btnRegister = new System.Windows.Forms.Button();
            this.user_btnLogin = new System.Windows.Forms.Button();
            this.user_btnQuery = new System.Windows.Forms.Button();
            this.user_dgvUser = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.user_gbUser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.user_dgvUser)).BeginInit();
            this.SuspendLayout();
            // 
            // user_lbName
            // 
            this.user_lbName.AutoSize = true;
            this.user_lbName.Font = new System.Drawing.Font("楷体", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_lbName.ForeColor = System.Drawing.Color.MediumBlue;
            this.user_lbName.Location = new System.Drawing.Point(413, 23);
            this.user_lbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.user_lbName.Name = "user_lbName";
            this.user_lbName.Size = new System.Drawing.Size(137, 30);
            this.user_lbName.TabIndex = 3;
            this.user_lbName.Text = "用户管理";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.user_gbUser);
            this.panel1.Location = new System.Drawing.Point(11, 65);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 411);
            this.panel1.TabIndex = 4;
            // 
            // user_gbUser
            // 
            this.user_gbUser.Controls.Add(this.user_btnChange);
            this.user_gbUser.Controls.Add(this.user_btnDelete);
            this.user_gbUser.Controls.Add(this.user_btnRegister);
            this.user_gbUser.Controls.Add(this.user_btnLogin);
            this.user_gbUser.Controls.Add(this.user_btnQuery);
            this.user_gbUser.Controls.Add(this.user_dgvUser);
            this.user_gbUser.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_gbUser.Location = new System.Drawing.Point(7, 13);
            this.user_gbUser.Margin = new System.Windows.Forms.Padding(2);
            this.user_gbUser.Name = "user_gbUser";
            this.user_gbUser.Padding = new System.Windows.Forms.Padding(2);
            this.user_gbUser.Size = new System.Drawing.Size(946, 385);
            this.user_gbUser.TabIndex = 0;
            this.user_gbUser.TabStop = false;
            this.user_gbUser.Text = "用户管理";
            // 
            // user_btnChange
            // 
            this.user_btnChange.BackColor = System.Drawing.Color.PowderBlue;
            this.user_btnChange.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_btnChange.Location = new System.Drawing.Point(767, 310);
            this.user_btnChange.Margin = new System.Windows.Forms.Padding(2);
            this.user_btnChange.Name = "user_btnChange";
            this.user_btnChange.Size = new System.Drawing.Size(133, 53);
            this.user_btnChange.TabIndex = 5;
            this.user_btnChange.Text = "修改密码";
            this.user_btnChange.UseVisualStyleBackColor = false;
            this.user_btnChange.Click += new System.EventHandler(this.user_btnChange_Click);
            // 
            // user_btnDelete
            // 
            this.user_btnDelete.BackColor = System.Drawing.Color.PowderBlue;
            this.user_btnDelete.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_btnDelete.Location = new System.Drawing.Point(585, 310);
            this.user_btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.user_btnDelete.Name = "user_btnDelete";
            this.user_btnDelete.Size = new System.Drawing.Size(133, 53);
            this.user_btnDelete.TabIndex = 4;
            this.user_btnDelete.Text = "删除帐户";
            this.user_btnDelete.UseVisualStyleBackColor = false;
            this.user_btnDelete.Click += new System.EventHandler(this.user_btnDelete_Click);
            // 
            // user_btnRegister
            // 
            this.user_btnRegister.BackColor = System.Drawing.Color.PowderBlue;
            this.user_btnRegister.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_btnRegister.Location = new System.Drawing.Point(403, 310);
            this.user_btnRegister.Margin = new System.Windows.Forms.Padding(2);
            this.user_btnRegister.Name = "user_btnRegister";
            this.user_btnRegister.Size = new System.Drawing.Size(133, 53);
            this.user_btnRegister.TabIndex = 3;
            this.user_btnRegister.Text = "添加帐户";
            this.user_btnRegister.UseVisualStyleBackColor = false;
            this.user_btnRegister.Click += new System.EventHandler(this.user_btnRegister_Click);
            // 
            // user_btnLogin
            // 
            this.user_btnLogin.BackColor = System.Drawing.Color.PowderBlue;
            this.user_btnLogin.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_btnLogin.Location = new System.Drawing.Point(221, 310);
            this.user_btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.user_btnLogin.Name = "user_btnLogin";
            this.user_btnLogin.Size = new System.Drawing.Size(133, 53);
            this.user_btnLogin.TabIndex = 2;
            this.user_btnLogin.Text = "登录系统";
            this.user_btnLogin.UseVisualStyleBackColor = false;
            this.user_btnLogin.Click += new System.EventHandler(this.user_btnLogin_Click);
            // 
            // user_btnQuery
            // 
            this.user_btnQuery.BackColor = System.Drawing.Color.PowderBlue;
            this.user_btnQuery.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.user_btnQuery.Location = new System.Drawing.Point(39, 310);
            this.user_btnQuery.Margin = new System.Windows.Forms.Padding(2);
            this.user_btnQuery.Name = "user_btnQuery";
            this.user_btnQuery.Size = new System.Drawing.Size(133, 53);
            this.user_btnQuery.TabIndex = 1;
            this.user_btnQuery.Text = "查询帐户";
            this.user_btnQuery.UseVisualStyleBackColor = false;
            this.user_btnQuery.Click += new System.EventHandler(this.user_btnQuery_Click);
            // 
            // user_dgvUser
            // 
            this.user_dgvUser.BackgroundColor = System.Drawing.Color.Azure;
            this.user_dgvUser.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.user_dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.user_dgvUser.Location = new System.Drawing.Point(11, 25);
            this.user_dgvUser.Margin = new System.Windows.Forms.Padding(2);
            this.user_dgvUser.Name = "user_dgvUser";
            this.user_dgvUser.RowTemplate.Height = 30;
            this.user_dgvUser.Size = new System.Drawing.Size(924, 261);
            this.user_dgvUser.TabIndex = 0;
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(988, 485);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.user_lbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UserForm";
            this.Text = "UserForm";
            this.panel1.ResumeLayout(false);
            this.user_gbUser.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.user_dgvUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label user_lbName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox user_gbUser;
        private System.Windows.Forms.Button user_btnDelete;
        private System.Windows.Forms.Button user_btnRegister;
        private System.Windows.Forms.Button user_btnLogin;
        private System.Windows.Forms.Button user_btnQuery;
        private System.Windows.Forms.DataGridView user_dgvUser;
        private System.Windows.Forms.Button user_btnChange;
    }
}