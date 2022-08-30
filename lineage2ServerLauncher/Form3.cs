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
    public partial class Form3 : Form
    {
        LoginServer ls;
        public Form3()
        {
            InitializeComponent();
            ls = new LoginServer();
        }

        private void Form3_Load(object sender, EventArgs e)
        {             
            ls.run(this);
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {            
            ls.Stop();
        }
    }
}
