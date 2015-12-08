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
    public partial class UserListDiag : Form
        {
        LoginForm lf;
        public UserListDiag(LoginForm lf)
            {
            InitializeComponent();
            lbUsers.Items.Add("pippo\tantani\t12345678901234567890123456789012");
            lbUsers.Items.Add("pluto\tblinda\t21098765432109876543210987654321");
            this.lf = lf;
            Invalidate();
            }
        protected string GetListSelectedData()
            {
            int indx = lbUsers.SelectedIndex;
            string sl = "";
            if (indx != -1)
                sl = lbUsers.Items[indx].ToString();
            //MessageBox.Show(sl);
            return sl;
            }
        private void idCancel_Click(object sender, EventArgs e)
            {
            Close();
            }

        private void idOK_Click(object sender, EventArgs e)
            {
            string sl = GetListSelectedData();
            if(sl.Length>0)
                {
                string[] pt = sl.Split('\t');
                lf.SetUserData(pt[0], pt[1], pt[2]);
                //MessageBox.Show(pt[0] + " - " + pt[1] + " - " + pt[2]);
                }
            this.Close();
            }
        }
    }
