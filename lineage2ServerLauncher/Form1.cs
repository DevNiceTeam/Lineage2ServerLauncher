using System;
using System.Threading;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    public partial class Form1 : Form
    {
        LangChanger lc;
        MysqlStart ms;
        UpdInterface upd;
        DbInstall db;   
        LoginForm lf;
        GameForm gf;

        public Form1()
        {
            lc = new LangChanger(this);
            ms = new MysqlStart(this);
            upd = new UpdInterface(this, ms.GetMysqlState());
            db = new DbInstall(ms.GetMysqlState(),upd);            
            
            InitializeComponent();
        }       

        private void Form1_Load(object sender, EventArgs e)
        {       
            lc.isRuLanguage(true);
            ms.checkOtherSQL();
            button2.Enabled = false;
            button8.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button5.Enabled = false;
            Console.WriteLine(upd.Check());
            if (upd.Check())
            {
                Console.WriteLine("Поток запущен");
                upd.Pause();
                upd.Resume();
            }
            else
            {
                Console.WriteLine("Поток не запущен");
                upd.checkStateUpdateUI();
                
            }
            db.checkInstall(true);

            ms.Start();// TODO мб поместить в поток???                                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ms.dbStarted)
            {
                button2.Enabled = false;
                Console.WriteLine("Останавливаю бд");
                ms.stopMysql(); 
                upd.Pause();
            }
            ms.dbStarted = false;
            button8.Enabled = false;
            button1.Enabled = true;
            button5.Enabled = true;
            Thread.Sleep(1000);
            label2.Text = LangChanger.isRuLang ? "Запустите MySQL" : "Run MySQL";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button1.Enabled = false;
            button2.PerformClick();
            button2.Enabled = false;
            ms.resetMysql();
            button5.Enabled = true;
            button1.Enabled = true; 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lc.isRuLanguage(true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lc.isEnLanguage(true);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                upd.Pause();
            }
            catch (Exception)
            {
                Console.WriteLine("Потоки не были запущены");
            }

            if (ms.dbStarted)
            {                
                var txt = Msg.Show("Бд запущена вы уверены что хотите выйти?",
                    "DB is running are you sure you want to exit?", false);
                if (txt == DialogResult.Yes)
                {                    
                    upd.Exited();
                    button2_Click(sender, e);
                }

                if (txt == DialogResult.No)
                {
                    upd.Resume();
                    e.Cancel = true;                    
                }
            }          
        }

        private void button6_Click(object sender, EventArgs e) //GS run
        {
            gf = new GameForm(this);
            gf.Show();
            button6.Enabled = false;
            if (gf.GetGameServer.isRun)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e) //LS run
        {
            lf = new LoginForm(this);
            Console.WriteLine(lf.WindowState);
            lf.Show();
            button7.Enabled = false;
            if (lf.GetLoginServer.isRun)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {           
            db.install();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Проверка в случае закрытия мейн формы убиваем фоновый процесс гс и лс
            if (lf != null)
            {
                if (lf.GetLoginServer.isRun == true)
                {
                    lf.GetLoginServer.GetLoginProcess().Kill();
                    lf.Dispose();
                }
            }

            if (gf != null)
            {
                if (gf.GetGameServer.isRun == true)
                {
                    gf.GetGameServer.GetGameProcess().Kill();
                    gf.Dispose();
                }
            }                     
        }
    }
}
