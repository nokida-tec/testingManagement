namespace XT_CETC23.UserForms
{
    partial class UserLogin
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
            this.userLogin_lbName = new System.Windows.Forms.Label();
            this.userLogin_lbUserName = new System.Windows.Forms.Label();
            this.userLogin_lbUserPassWord = new System.Windows.Forms.Label();
            this.userLogin_tbUserName = new System.Windows.Forms.TextBox();
            this.userLogin_tbUserPassWord = new System.Windows.Forms.TextBox();
            this.userLogin_btnLogin = new System.Windows.Forms.Button();
            this.userLogin_btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // userLogin_lbName
            // 
            this.userLogin_lbName.AutoSize = true;
            this.userLogin_lbName.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLogin_lbName.Location = new System.Drawing.Point(155, 47);
            this.userLogin_lbName.Name = "userLogin_lbName";
            this.userLogin_lbName.Size = new System.Drawing.Size(124, 28);
            this.userLogin_lbName.TabIndex = 0;
            this.userLogin_lbName.Text = "用户登录";
            // 
            // userLogin_lbUserName
            // 
            this.userLogin_lbUserName.AutoSize = true;
            this.userLogin_lbUserName.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLogin_lbUserName.Location = new System.Drawing.Point(44, 135);
            this.userLogin_lbUserName.Name = "userLogin_lbUserName";
            this.userLogin_lbUserName.Size = new System.Drawing.Size(106, 24);
            this.userLogin_lbUserName.TabIndex = 1;
            this.userLogin_lbUserName.Text = "用户名：";
            // 
            // userLogin_lbUserPassWord
            // 
            this.userLogin_lbUserPassWord.AutoSize = true;
            this.userLogin_lbUserPassWord.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLogin_lbUserPassWord.Location = new System.Drawing.Point(44, 230);
            this.userLogin_lbUserPassWord.Name = "userLogin_lbUserPassWord";
            this.userLogin_lbUserPassWord.Size = new System.Drawing.Size(106, 24);
            this.userLogin_lbUserPassWord.TabIndex = 2;
            this.userLogin_lbUserPassWord.Text = "密  码：";
            // 
            // userLogin_tbUserName
            // 
            this.userLogin_tbUserName.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLogin_tbUserName.Location = new System.Drawing.Point(174, 133);
            this.userLogin_tbUserName.Name = "userLogin_tbUserName";
            this.userLogin_tbUserName.Size = new System.Drawing.Size(181, 35);
            this.userLogin_tbUserName.TabIndex = 3;
            // 
            // userLogin_tbUserPassWord
            // 
            this.userLogin_tbUserPassWord.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLogin_tbUserPassWord.Location = new System.Drawing.Point(174, 224);
            this.userLogin_tbUserPassWord.Name = "userLogin_tbUserPassWord";
            this.userLogin_tbUserPassWord.PasswordChar = '*';
            this.userLogin_tbUserPassWord.Size = new System.Drawing.Size(181, 35);
            this.userLogin_tbUserPassWord.TabIndex = 4;
            // 
            // userLogin_btnLogin
            // 
            this.userLogin_btnLogin.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLogin_btnLogin.Location = new System.Drawing.Point(82, 331);
            this.userLogin_btnLogin.Name = "userLogin_btnLogin";
            this.userLogin_btnLogin.Size = new System.Drawing.Size(127, 44);
            this.userLogin_btnLogin.TabIndex = 5;
            this.userLogin_btnLogin.Text = "登 录";
            this.userLogin_btnLogin.UseVisualStyleBackColor = true;
            this.userLogin_btnLogin.Click += new System.EventHandler(this.userLogin_btnLogin_Click);
            // 
            // userLogin_btnExit
            // 
            this.userLogin_btnExit.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userLogin_btnExit.Location = new System.Drawing.Point(246, 331);
            this.userLogin_btnExit.Name = "userLogin_btnExit";
            this.userLogin_btnExit.Size = new System.Drawing.Size(127, 44);
            this.userLogin_btnExit.TabIndex = 6;
            this.userLogin_btnExit.Text = "退 出";
            this.userLogin_btnExit.UseVisualStyleBackColor = true;
            this.userLogin_btnExit.Click += new System.EventHandler(this.userLogin_btnExit_Click);
            // 
            // UserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PowderBlue;
            this.ClientSize = new System.Drawing.Size(452, 413);
            this.Controls.Add(this.userLogin_btnExit);
            this.Controls.Add(this.userLogin_btnLogin);
            this.Controls.Add(this.userLogin_tbUserPassWord);
            this.Controls.Add(this.userLogin_tbUserName);
            this.Controls.Add(this.userLogin_lbUserPassWord);
            this.Controls.Add(this.userLogin_lbUserName);
            this.Controls.Add(this.userLogin_lbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UserLogin";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserLogin_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userLogin_lbName;
        private System.Windows.Forms.Label userLogin_lbUserName;
        private System.Windows.Forms.Label userLogin_lbUserPassWord;
        private System.Windows.Forms.TextBox userLogin_tbUserName;
        private System.Windows.Forms.TextBox userLogin_tbUserPassWord;
        private System.Windows.Forms.Button userLogin_btnLogin;
        private System.Windows.Forms.Button userLogin_btnExit;
    }
}