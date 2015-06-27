namespace PostPHP
	{
	partial class Form1
		{
		/// <summary>
		/// Variabile di progettazione necessaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Pulire le risorse in uso.
		/// </summary>
		/// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
		protected override void Dispose(bool disposing)
			{
			if(disposing && (components != null))
				{
				components.Dispose();
				}
			base.Dispose(disposing);
			}

		#region Codice generato da Progettazione Windows Form

		/// <summary>
		/// Metodo necessario per il supporto della finestra di progettazione. Non modificare
		/// il contenuto del metodo con l'editor di codice.
		/// </summary>
		private void InitializeComponent()
			{
			this.components = new System.ComponentModel.Container();
			this.but_loginData = new System.Windows.Forms.Button();
			this.but_quit = new System.Windows.Forms.Button();
			this.bt_Login = new System.Windows.Forms.Button();
			this.bt_Logout = new System.Windows.Forms.Button();
			this.bt_Status = new System.Windows.Forms.Button();
			this.bt_Exe = new System.Windows.Forms.Button();
			this.but_ExeAsync = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lbl_queuecount = new System.Windows.Forms.Label();
			this.but_dequeue = new System.Windows.Forms.Button();
			this.but_query = new System.Windows.Forms.Button();
			this.but_query_no_async = new System.Windows.Forms.Button();
			this.but_query_noCrypt = new System.Windows.Forms.Button();
			this.lb_queries = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.lb_commands = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.but_exec = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.but_exec_no_async = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// but_loginData
			// 
			this.but_loginData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_loginData.Location = new System.Drawing.Point(12, 73);
			this.but_loginData.Name = "but_loginData";
			this.but_loginData.Size = new System.Drawing.Size(137, 35);
			this.but_loginData.TabIndex = 12;
			this.but_loginData.Text = "Login data";
			this.toolTip1.SetToolTip(this.but_loginData, "Dati per il login: utente, password, chiave di cifratura");
			this.but_loginData.UseVisualStyleBackColor = true;
			this.but_loginData.Click += new System.EventHandler(this.but_loginData_Click);
			// 
			// but_quit
			// 
			this.but_quit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_quit.Location = new System.Drawing.Point(583, 410);
			this.but_quit.Name = "but_quit";
			this.but_quit.Size = new System.Drawing.Size(137, 35);
			this.but_quit.TabIndex = 17;
			this.but_quit.Text = "Esci";
			this.but_quit.UseVisualStyleBackColor = true;
			this.but_quit.Click += new System.EventHandler(this.but_quit_Click);
			// 
			// bt_Login
			// 
			this.bt_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bt_Login.Location = new System.Drawing.Point(12, 32);
			this.bt_Login.Name = "bt_Login";
			this.bt_Login.Size = new System.Drawing.Size(137, 35);
			this.bt_Login.TabIndex = 18;
			this.bt_Login.Text = "Login";
			this.toolTip1.SetToolTip(this.bt_Login, "Login alla pagina php");
			this.bt_Login.UseVisualStyleBackColor = true;
			this.bt_Login.Click += new System.EventHandler(this.bt_Login_Click);
			// 
			// bt_Logout
			// 
			this.bt_Logout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bt_Logout.Location = new System.Drawing.Point(12, 155);
			this.bt_Logout.Name = "bt_Logout";
			this.bt_Logout.Size = new System.Drawing.Size(137, 35);
			this.bt_Logout.TabIndex = 19;
			this.bt_Logout.Text = "Logout";
			this.toolTip1.SetToolTip(this.bt_Logout, "Disconnette dalla pagina php");
			this.bt_Logout.UseVisualStyleBackColor = true;
			this.bt_Logout.Click += new System.EventHandler(this.bt_Logout_Click);
			// 
			// bt_Status
			// 
			this.bt_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bt_Status.Location = new System.Drawing.Point(12, 114);
			this.bt_Status.Name = "bt_Status";
			this.bt_Status.Size = new System.Drawing.Size(137, 35);
			this.bt_Status.TabIndex = 20;
			this.bt_Status.Text = "Status";
			this.toolTip1.SetToolTip(this.bt_Status, "Stato della connessione alla pagina");
			this.bt_Status.UseVisualStyleBackColor = true;
			this.bt_Status.Click += new System.EventHandler(this.bt_Status_Click);
			// 
			// bt_Exe
			// 
			this.bt_Exe.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.bt_Exe.Location = new System.Drawing.Point(155, 32);
			this.bt_Exe.Name = "bt_Exe";
			this.bt_Exe.Size = new System.Drawing.Size(137, 35);
			this.bt_Exe.TabIndex = 21;
			this.bt_Exe.Text = "Exec PHP call";
			this.toolTip1.SetToolTip(this.bt_Exe, "Esegue una chiamata");
			this.bt_Exe.UseVisualStyleBackColor = true;
			this.bt_Exe.Click += new System.EventHandler(this.bt_Exe_Click);
			// 
			// but_ExeAsync
			// 
			this.but_ExeAsync.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_ExeAsync.Location = new System.Drawing.Point(298, 32);
			this.but_ExeAsync.Name = "but_ExeAsync";
			this.but_ExeAsync.Size = new System.Drawing.Size(137, 35);
			this.but_ExeAsync.TabIndex = 22;
			this.but_ExeAsync.Text = "Exec PHP async";
			this.toolTip1.SetToolTip(this.but_ExeAsync, "Esegue una chiamata asincrona");
			this.but_ExeAsync.UseVisualStyleBackColor = true;
			this.but_ExeAsync.Click += new System.EventHandler(this.but_ExeAsync_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(438, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(90, 13);
			this.label1.TabIndex = 23;
			this.label1.Text = "Messaggi in coda";
			// 
			// lbl_queuecount
			// 
			this.lbl_queuecount.AutoSize = true;
			this.lbl_queuecount.Location = new System.Drawing.Point(534, 16);
			this.lbl_queuecount.Name = "lbl_queuecount";
			this.lbl_queuecount.Size = new System.Drawing.Size(13, 13);
			this.lbl_queuecount.TabIndex = 24;
			this.lbl_queuecount.Text = "0";
			// 
			// but_dequeue
			// 
			this.but_dequeue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_dequeue.Location = new System.Drawing.Point(441, 32);
			this.but_dequeue.Name = "but_dequeue";
			this.but_dequeue.Size = new System.Drawing.Size(137, 35);
			this.but_dequeue.TabIndex = 25;
			this.but_dequeue.Text = "Read queue";
			this.toolTip1.SetToolTip(this.but_dequeue, "Legge un messaggio in coda (delle chiamate asincronte)");
			this.but_dequeue.UseVisualStyleBackColor = true;
			this.but_dequeue.Click += new System.EventHandler(this.but_dequeue_Click);
			// 
			// but_query
			// 
			this.but_query.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_query.Location = new System.Drawing.Point(298, 73);
			this.but_query.Name = "but_query";
			this.but_query.Size = new System.Drawing.Size(137, 35);
			this.but_query.TabIndex = 26;
			this.but_query.Text = "Query async";
			this.toolTip1.SetToolTip(this.but_query, "Esegue la query selezionata asincrona");
			this.but_query.UseVisualStyleBackColor = true;
			this.but_query.Click += new System.EventHandler(this.but_query_Click);
			// 
			// but_query_no_async
			// 
			this.but_query_no_async.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_query_no_async.Location = new System.Drawing.Point(155, 73);
			this.but_query_no_async.Name = "but_query_no_async";
			this.but_query_no_async.Size = new System.Drawing.Size(137, 35);
			this.but_query_no_async.TabIndex = 27;
			this.but_query_no_async.Text = "Query";
			this.toolTip1.SetToolTip(this.but_query_no_async, "Esegue la query selezionata");
			this.but_query_no_async.UseVisualStyleBackColor = true;
			this.but_query_no_async.Click += new System.EventHandler(this.but_query_no_async_Click);
			// 
			// but_query_noCrypt
			// 
			this.but_query_noCrypt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_query_noCrypt.Location = new System.Drawing.Point(155, 155);
			this.but_query_noCrypt.Name = "but_query_noCrypt";
			this.but_query_noCrypt.Size = new System.Drawing.Size(137, 35);
			this.but_query_noCrypt.TabIndex = 28;
			this.but_query_noCrypt.Text = "Query (no crypt)";
			this.toolTip1.SetToolTip(this.but_query_noCrypt, "Esegue la query senza cifrare i dati (solo per test)");
			this.but_query_noCrypt.UseVisualStyleBackColor = true;
			this.but_query_noCrypt.Click += new System.EventHandler(this.but_query_noCrypt_Click);
			// 
			// lb_queries
			// 
			this.lb_queries.FormattingEnabled = true;
			this.lb_queries.Location = new System.Drawing.Point(12, 243);
			this.lb_queries.Name = "lb_queries";
			this.lb_queries.Size = new System.Drawing.Size(407, 147);
			this.lb_queries.TabIndex = 29;
			this.toolTip1.SetToolTip(this.lb_queries, "Queries");
			this.lb_queries.SelectedValueChanged += new System.EventHandler(this.lb_queries_SelectedValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 224);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 13);
			this.label2.TabIndex = 30;
			this.label2.Text = "Queries";
			// 
			// lb_commands
			// 
			this.lb_commands.FormattingEnabled = true;
			this.lb_commands.Location = new System.Drawing.Point(441, 243);
			this.lb_commands.Name = "lb_commands";
			this.lb_commands.Size = new System.Drawing.Size(407, 147);
			this.lb_commands.TabIndex = 31;
			this.toolTip1.SetToolTip(this.lb_commands, "Comandi");
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(438, 224);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(59, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = "Commands";
			// 
			// but_exec
			// 
			this.but_exec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_exec.Location = new System.Drawing.Point(298, 114);
			this.but_exec.Name = "but_exec";
			this.but_exec.Size = new System.Drawing.Size(137, 35);
			this.but_exec.TabIndex = 33;
			this.but_exec.Text = "Exec async";
			this.toolTip1.SetToolTip(this.but_exec, "Esegue il comando selezionato asincrono");
			this.but_exec.UseVisualStyleBackColor = true;
			this.but_exec.Click += new System.EventHandler(this.but_exec_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 5000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 100;
			// 
			// but_exec_no_async
			// 
			this.but_exec_no_async.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.but_exec_no_async.Location = new System.Drawing.Point(155, 114);
			this.but_exec_no_async.Name = "but_exec_no_async";
			this.but_exec_no_async.Size = new System.Drawing.Size(137, 35);
			this.but_exec_no_async.TabIndex = 34;
			this.but_exec_no_async.Text = "Exec";
			this.toolTip1.SetToolTip(this.but_exec_no_async, "Esegue il comando selezionato asincrono");
			this.but_exec_no_async.UseVisualStyleBackColor = true;
			this.but_exec_no_async.Click += new System.EventHandler(this.but_exec_no_async_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(864, 459);
			this.Controls.Add(this.but_exec_no_async);
			this.Controls.Add(this.but_exec);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lb_commands);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lb_queries);
			this.Controls.Add(this.but_query_noCrypt);
			this.Controls.Add(this.but_query_no_async);
			this.Controls.Add(this.but_query);
			this.Controls.Add(this.but_dequeue);
			this.Controls.Add(this.lbl_queuecount);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.but_ExeAsync);
			this.Controls.Add(this.bt_Exe);
			this.Controls.Add(this.bt_Status);
			this.Controls.Add(this.bt_Logout);
			this.Controls.Add(this.bt_Login);
			this.Controls.Add(this.but_quit);
			this.Controls.Add(this.but_loginData);
			this.Name = "Form1";
			this.Text = "Connessione PHP criptata";
			this.ResumeLayout(false);
			this.PerformLayout();

			}

		#endregion

		private System.Windows.Forms.Button but_loginData;
		private System.Windows.Forms.Button but_quit;
		private System.Windows.Forms.Button bt_Login;
		private System.Windows.Forms.Button bt_Logout;
		private System.Windows.Forms.Button bt_Status;
		private System.Windows.Forms.Button bt_Exe;
		private System.Windows.Forms.Button but_ExeAsync;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lbl_queuecount;
		private System.Windows.Forms.Button but_dequeue;
		private System.Windows.Forms.Button but_query;
		private System.Windows.Forms.Button but_query_no_async;
		private System.Windows.Forms.Button but_query_noCrypt;
		private System.Windows.Forms.ListBox lb_queries;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lb_commands;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button but_exec;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button but_exec_no_async;
		}
	}

