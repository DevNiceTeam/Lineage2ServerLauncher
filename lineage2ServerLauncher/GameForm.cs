using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    public partial class GameForm : Form
    {
        public bool isRun;
        GameServer gs;
        public GameForm()
        {
            InitializeComponent();
            gs = new GameServer();
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            isRun = false;
            gs.Stop();            
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            isRun = true;
            gs.Run(this);            
        }
    }        
}

