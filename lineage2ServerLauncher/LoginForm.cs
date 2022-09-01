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
        public bool isRun;
        LoginServer ls;
        public LoginForm()
        {
            InitializeComponent();
            ls = new LoginServer();
        }        

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
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
