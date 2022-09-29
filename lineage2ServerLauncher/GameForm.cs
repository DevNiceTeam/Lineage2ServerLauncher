using System;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    public partial class GameForm : Form    
    {        
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

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            GetGameServer.GetGameProcess().Kill();

            GetGameServer.isRun = false;

            frm1.button6.Enabled = true;
            frm1.button2.Enabled = true;            
        }
    }        
}

