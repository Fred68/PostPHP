using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PostPHP
	{
	public partial class Login : Form
		{
		public string user;
		public string passwd;
		public string key;
		public string url;
		
		public Login()
			{
			InitializeComponent();
			}

		public Login(string usr, string pwd, string k, string u)
			{
			InitializeComponent();
			user = usr;
			passwd = pwd;
			key = k;
			url = u;
			}

		private void Login_Load(object sender, EventArgs e)
			{
			this.tb_user.Text = user;
			this.tb_passwd.Text = passwd;
			this.tb_key.Text = key;
			this.tb_url.Text = url;
			}

		private void btOK_Click(object sender, EventArgs e)
			{
			user = this.tb_user.Text;
			passwd = this.tb_passwd.Text;
			key = this.tb_key.Text;
			url = this.tb_url.Text;
			this.Close();
			}

		private void bt_cancel_Click(object sender, EventArgs e)
			{
			}

		private void bt_help_Click(object sender, EventArgs e)
			{
			MessageBox.Show("pippo antani 12345678901234567890123456789012 \npluto blinda 21098765432109876543210987654321"  );
			}

		
		}
	}
