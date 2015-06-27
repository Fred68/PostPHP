namespace PostPHP
	{
	partial class Login
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
			if(disposing && (components != null))
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
			this.tb_user = new System.Windows.Forms.TextBox();
			this.tb_passwd = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.bt_cancel = new System.Windows.Forms.Button();
			this.btOK = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.tb_key = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.tb_url = new System.Windows.Forms.TextBox();
			this.bt_help = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// tb_user
			// 
			this.tb_user.Location = new System.Drawing.Point(114, 13);
			this.tb_user.Name = "tb_user";
			this.tb_user.Size = new System.Drawing.Size(339, 20);
			this.tb_user.TabIndex = 0;
			// 
			// tb_passwd
			// 
			this.tb_passwd.Location = new System.Drawing.Point(114, 39);
			this.tb_passwd.Name = "tb_passwd";
			this.tb_passwd.Size = new System.Drawing.Size(339, 20);
			this.tb_passwd.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(39, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Utente";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Password";
			// 
			// bt_cancel
			// 
			this.bt_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.bt_cancel.Location = new System.Drawing.Point(191, 122);
			this.bt_cancel.Name = "bt_cancel";
			this.bt_cancel.Size = new System.Drawing.Size(128, 34);
			this.bt_cancel.TabIndex = 4;
			this.bt_cancel.Text = "Annulla";
			this.bt_cancel.UseVisualStyleBackColor = true;
			this.bt_cancel.Click += new System.EventHandler(this.bt_cancel_Click);
			// 
			// btOK
			// 
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOK.Location = new System.Drawing.Point(325, 122);
			this.btOK.Name = "btOK";
			this.btOK.Size = new System.Drawing.Size(128, 34);
			this.btOK.TabIndex = 5;
			this.btOK.Text = "OK";
			this.btOK.UseVisualStyleBackColor = true;
			this.btOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(16, 71);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(25, 13);
			this.label3.TabIndex = 7;
			this.label3.Text = "Key";
			// 
			// tb_key
			// 
			this.tb_key.Location = new System.Drawing.Point(114, 65);
			this.tb_key.Name = "tb_key";
			this.tb_key.Size = new System.Drawing.Size(339, 20);
			this.tb_key.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 97);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(20, 13);
			this.label4.TabIndex = 9;
			this.label4.Text = "Url";
			// 
			// tb_url
			// 
			this.tb_url.Location = new System.Drawing.Point(114, 91);
			this.tb_url.Name = "tb_url";
			this.tb_url.Size = new System.Drawing.Size(339, 20);
			this.tb_url.TabIndex = 8;
			// 
			// bt_help
			// 
			this.bt_help.Location = new System.Drawing.Point(73, 13);
			this.bt_help.Name = "bt_help";
			this.bt_help.Size = new System.Drawing.Size(35, 20);
			this.bt_help.TabIndex = 10;
			this.bt_help.Text = "?";
			this.bt_help.UseVisualStyleBackColor = true;
			this.bt_help.Click += new System.EventHandler(this.bt_help_Click);
			// 
			// Login
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(463, 169);
			this.Controls.Add(this.bt_help);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tb_url);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.tb_key);
			this.Controls.Add(this.btOK);
			this.Controls.Add(this.bt_cancel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.tb_passwd);
			this.Controls.Add(this.tb_user);
			this.Name = "Login";
			this.Text = "Login";
			this.Load += new System.EventHandler(this.Login_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.TextBox tb_user;
		private System.Windows.Forms.TextBox tb_passwd;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button bt_cancel;
		private System.Windows.Forms.Button btOK;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tb_key;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tb_url;
		private System.Windows.Forms.Button bt_help;
		}
	}