namespace PostPHP
    {
    partial class UserListDiag
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
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.idOK = new System.Windows.Forms.Button();
            this.idCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(12, 12);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(362, 186);
            this.lbUsers.TabIndex = 0;
            // 
            // idOK
            // 
            this.idOK.Location = new System.Drawing.Point(381, 13);
            this.idOK.Name = "idOK";
            this.idOK.Size = new System.Drawing.Size(75, 23);
            this.idOK.TabIndex = 1;
            this.idOK.Text = "OK";
            this.idOK.UseVisualStyleBackColor = true;
            this.idOK.Click += new System.EventHandler(this.idOK_Click);
            // 
            // idCancel
            // 
            this.idCancel.Location = new System.Drawing.Point(381, 43);
            this.idCancel.Name = "idCancel";
            this.idCancel.Size = new System.Drawing.Size(75, 23);
            this.idCancel.TabIndex = 2;
            this.idCancel.Text = "Annulla";
            this.idCancel.UseVisualStyleBackColor = true;
            this.idCancel.Click += new System.EventHandler(this.idCancel_Click);
            // 
            // UserListDiag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 227);
            this.Controls.Add(this.idCancel);
            this.Controls.Add(this.idOK);
            this.Controls.Add(this.lbUsers);
            this.Name = "UserListDiag";
            this.Text = "UserListDiag";
            this.ResumeLayout(false);

            }

        #endregion
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.Button idOK;
        private System.Windows.Forms.Button idCancel;
        }
    }