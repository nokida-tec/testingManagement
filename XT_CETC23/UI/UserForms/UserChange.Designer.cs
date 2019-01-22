namespace XT_CETC23.UserForms
{
    partial class UserChange
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
            this.userChange_btnQuit = new System.Windows.Forms.Button();
            this.userChange_btnChange = new System.Windows.Forms.Button();
            this.userChange_tbPassWord = new System.Windows.Forms.TextBox();
            this.userChange_tbUserName = new System.Windows.Forms.TextBox();
            this.userChange_lbPassWord = new System.Windows.Forms.Label();
            this.userChange_lbUserName = new System.Windows.Forms.Label();
            this.userChange_lbName = new System.Windows.Forms.Label();
            this.userChange_lbPower = new System.Windows.Forms.Label();
            this.userChange_cbPower = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // userChange_btnQuit
            // 
            this.userChange_btnQuit.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_btnQuit.Location = new System.Drawing.Point(264, 326);
            this.userChange_btnQuit.Name = "userChange_btnQuit";
            this.userChange_btnQuit.Size = new System.Drawing.Size(127, 44);
            this.userChange_btnQuit.TabIndex = 13;
            this.userChange_btnQuit.Text = "退 出";
            this.userChange_btnQuit.UseVisualStyleBackColor = true;
            this.userChange_btnQuit.Click += new System.EventHandler(this.userChange_btnQuit_Click);
            // 
            // userChange_btnChange
            // 
            this.userChange_btnChange.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_btnChange.Location = new System.Drawing.Point(100, 326);
            this.userChange_btnChange.Name = "userChange_btnChange";
            this.userChange_btnChange.Size = new System.Drawing.Size(127, 44);
            this.userChange_btnChange.TabIndex = 12;
            this.userChange_btnChange.Text = "修 改";
            this.userChange_btnChange.UseVisualStyleBackColor = true;
            this.userChange_btnChange.Click += new System.EventHandler(this.userChange_btnChange_Click);
            // 
            // userChange_tbPassWord
            // 
            this.userChange_tbPassWord.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_tbPassWord.Location = new System.Drawing.Point(191, 175);
            this.userChange_tbPassWord.Name = "userChange_tbPassWord";
            this.userChange_tbPassWord.Size = new System.Drawing.Size(181, 35);
            this.userChange_tbPassWord.TabIndex = 11;
            // 
            // userChange_tbUserName
            // 
            this.userChange_tbUserName.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_tbUserName.Location = new System.Drawing.Point(191, 101);
            this.userChange_tbUserName.Name = "userChange_tbUserName";
            this.userChange_tbUserName.ReadOnly = true;
            this.userChange_tbUserName.Size = new System.Drawing.Size(181, 35);
            this.userChange_tbUserName.TabIndex = 10;
            // 
            // userChange_lbPassWord
            // 
            this.userChange_lbPassWord.AutoSize = true;
            this.userChange_lbPassWord.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_lbPassWord.Location = new System.Drawing.Point(61, 178);
            this.userChange_lbPassWord.Name = "userChange_lbPassWord";
            this.userChange_lbPassWord.Size = new System.Drawing.Size(106, 24);
            this.userChange_lbPassWord.TabIndex = 9;
            this.userChange_lbPassWord.Text = "密  码：";
            // 
            // userChange_lbUserName
            // 
            this.userChange_lbUserName.AutoSize = true;
            this.userChange_lbUserName.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_lbUserName.Location = new System.Drawing.Point(61, 107);
            this.userChange_lbUserName.Name = "userChange_lbUserName";
            this.userChange_lbUserName.Size = new System.Drawing.Size(106, 24);
            this.userChange_lbUserName.TabIndex = 8;
            this.userChange_lbUserName.Text = "用户名：";
            // 
            // userChange_lbName
            // 
            this.userChange_lbName.AutoSize = true;
            this.userChange_lbName.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_lbName.Location = new System.Drawing.Point(173, 42);
            this.userChange_lbName.Name = "userChange_lbName";
            this.userChange_lbName.Size = new System.Drawing.Size(124, 28);
            this.userChange_lbName.TabIndex = 7;
            this.userChange_lbName.Text = "帐户修改";
            // 
            // userChange_lbPower
            // 
            this.userChange_lbPower.AutoSize = true;
            this.userChange_lbPower.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userChange_lbPower.Location = new System.Drawing.Point(61, 251);
            this.userChange_lbPower.Name = "userChange_lbPower";
            this.userChange_lbPower.Size = new System.Drawing.Size(106, 24);
            this.userChange_lbPower.TabIndex = 14;
            this.userChange_lbPower.Text = "权  限：";
            // 
            // userChange_cbPower
            // 
            this.userChange_cbPower.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userChange_cbPower.Font = new System.Drawing.Font("楷体", 12F);
            this.userChange_cbPower.FormattingEnabled = true;
            this.userChange_cbPower.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.userChange_cbPower.Items.AddRange(new object[] {
            "operator",
            "user"});
            this.userChange_cbPower.Location = new System.Drawing.Point(191, 248);
            this.userChange_cbPower.Name = "userChange_cbPower";
            this.userChange_cbPower.Size = new System.Drawing.Size(181, 32);
            this.userChange_cbPower.TabIndex = 15;
            // 
            // UserChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(452, 413);
            this.Controls.Add(this.userChange_cbPower);
            this.Controls.Add(this.userChange_lbPower);
            this.Controls.Add(this.userChange_btnQuit);
            this.Controls.Add(this.userChange_btnChange);
            this.Controls.Add(this.userChange_tbPassWord);
            this.Controls.Add(this.userChange_tbUserName);
            this.Controls.Add(this.userChange_lbPassWord);
            this.Controls.Add(this.userChange_lbUserName);
            this.Controls.Add(this.userChange_lbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserChange";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserChange";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserChange_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button userChange_btnQuit;
        private System.Windows.Forms.Button userChange_btnChange;
        private System.Windows.Forms.TextBox userChange_tbPassWord;
        private System.Windows.Forms.TextBox userChange_tbUserName;
        private System.Windows.Forms.Label userChange_lbPassWord;
        private System.Windows.Forms.Label userChange_lbUserName;
        private System.Windows.Forms.Label userChange_lbName;
        private System.Windows.Forms.Label userChange_lbPower;
        private System.Windows.Forms.ComboBox userChange_cbPower;
    }
}