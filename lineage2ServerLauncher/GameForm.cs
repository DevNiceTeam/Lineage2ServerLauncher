using System;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    public partial class GameForm : Form    
    {        
        public bool closed = false;
        GameServer gs = new GameServer();

        public GameServer GetGameServer { get => gs; }

        Form1 frm1;

        public GameForm(Form1 frm)
        {
            InitializeComponent();
            frm1 = frm;
        }        

        private void GameForm_Load(object sender, EventArgs e)
        {
            GetGameServer.Run(this);            
        }

        private void GameForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (GetGameServer.Exited(this))
            {
                frm1.button6.Enabled = true;
            }
            GetGameServer.GetGameProcess().Kill();
        }
    }        
}

