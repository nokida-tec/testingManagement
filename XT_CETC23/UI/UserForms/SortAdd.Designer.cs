namespace XT_CETC23.UserForms
{
    partial class SortAdd
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
            this.sort_btnQuit = new System.Windows.Forms.Button();
            this.sort_btnAdd = new System.Windows.Forms.Button();
            this.sort_tbSortNum = new System.Windows.Forms.TextBox();
            this.sort_tbSortName = new System.Windows.Forms.TextBox();
            this.sort_lbSortNum = new System.Windows.Forms.Label();
            this.sort_lbSortName = new System.Windows.Forms.Label();
            this.sort_lbName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sort_btnQuit
            // 
            this.sort_btnQuit.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sort_btnQuit.Location = new System.Drawing.Point(229, 281);
            this.sort_btnQuit.Name = "sort_btnQuit";
            this.sort_btnQuit.Size = new System.Drawing.Size(127, 44);
            this.sort_btnQuit.TabIndex = 22;
            this.sort_btnQuit.Text = "退 出";
            this.sort_btnQuit.UseVisualStyleBackColor = true;
            this.sort_btnQuit.Click += new System.EventHandler(this.sort_btnQuit_Click);
            // 
            // sort_btnAdd
            // 
            this.sort_btnAdd.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sort_btnAdd.Location = new System.Drawing.Point(65, 281);
            this.sort_btnAdd.Name = "sort_btnAdd";
            this.sort_btnAdd.Size = new System.Drawing.Size(127, 44);
            this.sort_btnAdd.TabIndex = 21;
            this.sort_btnAdd.Text = "增 加";
            this.sort_btnAdd.UseVisualStyleBackColor = true;
            this.sort_btnAdd.Click += new System.EventHandler(this.sort_btnAdd_Click);
            // 
            // sort_tbSortNum
            // 
            this.sort_tbSortNum.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sort_tbSortNum.Location = new System.Drawing.Point(191, 202);
            this.sort_tbSortNum.Name = "sort_tbSortNum";
            this.sort_tbSortNum.Size = new System.Drawing.Size(181, 35);
            this.sort_tbSortNum.TabIndex = 20;
            // 
            // sort_tbSortName
            // 
            this.sort_tbSortName.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sort_tbSortName.Location = new System.Drawing.Point(191, 101);
            this.sort_tbSortName.Name = "sort_tbSortName";
            this.sort_tbSortName.Size = new System.Drawing.Size(181, 35);
            this.sort_tbSortName.TabIndex = 19;
            // 
            // sort_lbSortNum
            // 
            this.sort_lbSortNum.AutoSize = true;
            this.sort_lbSortNum.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sort_lbSortNum.Location = new System.Drawing.Point(61, 205);
            this.sort_lbSortNum.Name = "sort_lbSortNum";
            this.sort_lbSortNum.Size = new System.Drawing.Size(106, 24);
            this.sort_lbSortNum.TabIndex = 18;
            this.sort_lbSortNum.Text = "数  量：";
            // 
            // sort_lbSortName
            // 
            this.sort_lbSortName.AutoSize = true;
            this.sort_lbSortName.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sort_lbSortName.Location = new System.Drawing.Point(61, 107);
            this.sort_lbSortName.Name = "sort_lbSortName";
            this.sort_lbSortName.Size = new System.Drawing.Size(106, 24);
            this.sort_lbSortName.TabIndex = 17;
            this.sort_lbSortName.Text = "类  名：";
            // 
            // sort_lbName
            // 
            this.sort_lbName.AutoSize = true;
            this.sort_lbName.Font = new System.Drawing.Font("楷体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sort_lbName.Location = new System.Drawing.Point(161, 42);
            this.sort_lbName.Name = "sort_lbName";
            this.sort_lbName.Size = new System.Drawing.Size(124, 28);
            this.sort_lbName.TabIndex = 16;
            this.sort_lbName.Text = "增加种类";
            // 
            // SortAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(452, 376);
            this.Controls.Add(this.sort_btnQuit);
            this.Controls.Add(this.sort_btnAdd);
            this.Controls.Add(this.sort_tbSortNum);
            this.Controls.Add(this.sort_tbSortName);
            this.Controls.Add(this.sort_lbSortNum);
            this.Controls.Add(this.sort_lbSortName);
            this.Controls.Add(this.sort_lbName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SortAdd";
            this.Text = "SortAdd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button sort_btnQuit;
        private System.Windows.Forms.Button sort_btnAdd;
        private System.Windows.Forms.TextBox sort_tbSortNum;
        private System.Windows.Forms.TextBox sort_tbSortName;
        private System.Windows.Forms.Label sort_lbSortNum;
        private System.Windows.Forms.Label sort_lbSortName;
        private System.Windows.Forms.Label sort_lbName;
    }
}