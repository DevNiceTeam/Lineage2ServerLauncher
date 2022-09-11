using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    public partial class LoginForm : Form
    {
        public bool closed = false;
        public bool isRun;
        LoginServer ls;
        Form1 frm1;
        public LoginForm(Form1 form)
        {
            InitializeComponent();
            ls = new LoginServer();
            frm1 = form;
        }        

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ls.Exited(this))
            {
                frm1.button7.Enabled = true;
            }            
            isRun = false;
            ls.Stop();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            isRun = true;
            ls.Run(this);
        }
    }
}
