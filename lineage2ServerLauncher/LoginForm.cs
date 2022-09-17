using System;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    public partial class LoginForm : Form
    {
        public bool closed = false;
        LoginServer ls = new LoginServer();
        public LoginServer GetLoginServer { get => ls; }

        Form1 frm1;
        public LoginForm(Form1 form)
        {
            InitializeComponent();            
            frm1 = form;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            GetLoginServer.Run(this);
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GetLoginServer.Exited(this))
            {
                frm1.button7.Enabled = true;
            }
            GetLoginServer.GetLoginProcess().Kill();
        }
    }
}
