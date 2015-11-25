using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;							// Per WebClient
using System.Collections.Specialized;
using System.IO;							// Per memory stream
using System.Security.Cryptography;			// Per AES (chiave simmetrica)

namespace PostPHP
	{
	public partial class Form1 : Form
		{
		
		string enciv = "";
		string deciv = "";
		string user = "";					// Utente e password per il login della sessione
		string password = "";
		string key = new string('-',32);	// Chiave aes
		string url = "";
		Connessione conn;
		string selected_query = "";
		string selected_command = "";
		Timer locRefreshTimer = null;

		public Form1()
			{
			InitializeComponent();

			user = "pippo";
			password = "antani";
			key = "12345678901234567890123456789012";
			// pluto blinda 21098765432109876543210987654321
			url = "http://localhost/webpage/command.php";
			conn = new Connessione();

			this.lb_queries.Items.Add("SELECT ID_att, Nome FROM att");
			this.lb_queries.Items.Add("SELECT nome FROM att;");
			this.lb_queries.Items.Add("SELECT aID, nome, dur FROM att WHERE dur>30;");
			this.lb_queries.Items.Add("SELECT * FROM att");
			this.lb_queries.SelectedIndex = 0;
			selected_query = (string)this.lb_queries.SelectedItem;
			
			this.lb_commands.Items.Add("INSERT INTO att(pos, nome, dur) VALUES ('3.0.1','extra',60)");
			this.lb_commands.Items.Add("INSERT INTO att(aID, pos, nome, dur) VALUES (100,'3.0.1','extra',60)");
			this.lb_commands.SelectedIndex = 0;
			selected_command = (string)this.lb_commands.SelectedItem;

			locRefreshTimer = new Timer();
			locRefreshTimer.Interval = 1000;
			locRefreshTimer.Tick += locRefreshTimer_Tick;
			locRefreshTimer.Start();
			}
		void locRefreshTimer_Tick(object sender, EventArgs e)
			{
			eseguiLocalRefresh();
			}
		protected void eseguiLocalRefresh()
			{
			lbl_queuecount.Text = conn.WaitingMessages.ToString();
			lbl_queuecount.Invalidate();
			}
		public string EncryptMsg(string txt, string key, string sep)
			{
			string enc = "";
			RijndaelManaged aes = new RijndaelManaged();
			aes.KeySize = 256;
			aes.BlockSize = 256;
			aes.Padding = PaddingMode.PKCS7;
			aes.Mode = CipherMode.CBC;
			try
				{
				aes.Key = Convert.FromBase64String(key);		// Attenzione: la chiave deve essere di lunghezza corretta ! 256bit 32caratteri
				aes.GenerateIV();
				string iv = Convert.ToBase64String(aes.IV);
				this.enciv = iv;
				var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
				byte[] xBuff = null;
				using (var ms = new MemoryStream())
					{
					try
						{
						using(var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
							{
							byte[] xXml = Encoding.UTF8.GetBytes(txt);
							cs.Write(xXml, 0, xXml.Length);
							}
						}
					catch(Exception ex)
						{
						MessageBox.Show(ex.Message);
						}
					xBuff = ms.ToArray();
					}
				enc = Convert.ToBase64String(xBuff);
				enc += sep + iv;
				}
			catch(Exception ex)
				{
				MessageBox.Show(ex.Message);
				}
			return enc;
			}
		public string DecryptMsg(string txt, string key, string sep)
			{
			string dec="";
			RijndaelManaged aes = new RijndaelManaged();
			aes.KeySize = 256;
			aes.BlockSize = 256;
			aes.Padding = PaddingMode.PKCS7;
			aes.Mode = CipherMode.CBC;
			try
				{ 
				aes.Key = Convert.FromBase64String(key);		// Attenzione: chiave e iv devono essere di lunghezza corretta !
				string iv = txt;
				int indx = iv.IndexOf(sep);
				if(indx >= 0)
					{
					iv = iv.Substring(indx);
					iv = iv.Replace(sep, "");
					txt = txt.Replace(sep + iv, "");
					aes.IV = Convert.FromBase64String(iv);			// size=256, L=32 caratteri? Risulta 44 caratteri
					this.deciv = iv;
					var decrypt = aes.CreateDecryptor();
					byte[] xBuff = null;
					using(var ms = new MemoryStream())
						{
						try
							{
							using(var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
								{
								byte[] xXml = Convert.FromBase64String(txt);
								cs.Write(xXml, 0, xXml.Length);
								}
							}
						catch(Exception ex)
							{
							MessageBox.Show(ex.Message);
							}
						xBuff = ms.ToArray();
						}
					dec = Encoding.UTF8.GetString(xBuff); 
					}
				}
			catch(Exception ex)
				{
				MessageBox.Show(ex.Message);
				}
			return dec;
			}
		private void but_loginData_Click(object sender, EventArgs e)
			{
			Login lg = new Login(user,password,key,url);
			if(lg.ShowDialog()==DialogResult.OK)
				{
				user = lg.user;
				password = lg.passwd;
				key = lg.key;
				url = lg.url;
				}
			}
		private void but_quit_Click(object sender, EventArgs e)
			{
			Close();
			}
		private void bt_Login_Click(object sender, EventArgs e)
			{
			conn.ImpostaParametri(this.user, this.password, this.key, this.url);
            if (!conn.Login())
                {
                MessageBox.Show("Fallito login!\n" + conn.ConnectionMessages());
                }
            else
                {
                this.bt_Login.Enabled = false;
                MessageBox.Show(conn.ConnectionMessages());
                }
			return;
			}
		private void bt_Status_Click(object sender, EventArgs e)
			{
			conn.Status();
			eseguiLocalRefresh();
			MessageBox.Show(conn.ConnectionMessages());
			}
		private void bt_Logout_Click(object sender, EventArgs e)
			{
            bool ok;
			ok = conn.Logout();
			MessageBox.Show(conn.ConnectionMessages());
            if (ok)
                this.bt_Login.Enabled = true;
			}
		private void bt_Exe_Click(object sender, EventArgs e)
			{
			conn.Message = "Messaggio di prova della connessione";
			conn.ProcessMessage("exe");
			MessageBox.Show(conn.ConnectionMessages());
			}
		private void but_ExeAsync_Click(object sender, EventArgs e)
			{
			conn.Message = "Messaggio di prova asincrono della connessione";
			conn.ProcessMessageAsync("exe");
			eseguiLocalRefresh();
			}
		private void but_dequeue_Click(object sender, EventArgs e)
			{
			MessageBox.Show(conn.QueueMessage());
			eseguiLocalRefresh();
			}
		private void but_query_Click(object sender, EventArgs e)
			{
			conn.Message = (string)lb_queries.SelectedItem;
			conn.ProcessMessageAsync("query");
			//eseguiLocalRefresh();
			}
		private void but_query_no_async_Click(object sender, EventArgs e)
			{
			conn.Message = (string)lb_queries.SelectedItem;
			conn.ProcessMessage("query");
			MessageBox.Show(conn.ConnectionMessages());
			}
		private void but_query_noCrypt_Click(object sender, EventArgs e)
			{
			conn.Message = (string)lb_queries.SelectedItem;
			conn.ProcessMessage("query",false);
			MessageBox.Show(conn.ConnectionMessages());
			}
		private void lb_queries_SelectedValueChanged(object sender, EventArgs e)
			{
			this.selected_query = (string)lb_queries.SelectedItem;
			}
		private void but_exec_Click(object sender, EventArgs e)
			{
			conn.Message = (string)lb_commands.SelectedItem;
			conn.ProcessMessageAsync("exec");
			eseguiLocalRefresh();
			}
		private void but_exec_no_async_Click(object sender, EventArgs e)
			{
			conn.Message = (string)lb_commands.SelectedItem;
			conn.ProcessMessage("exec");
			MessageBox.Show(conn.ConnectionMessages());
			}
        private void bt_clearLogged_Click(object sender, EventArgs e)
            {
            bool ok;
            ok = conn.ClearLogged();
            MessageBox.Show(conn.ConnectionMessages());
            }
        }
	}


	
	