using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{

    public partial class Form1 : Form
    {
        LangChanger lc;
        MysqlStart ms;
        UpdInterface upd;
        Thread thr1;
        Thread thr;        

        public Form1()
        {
            lc = new LangChanger(this);
            ms = new MysqlStart(this);
            upd = new UpdInterface(this, ms.GetMysqlState());
            InitializeComponent();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            lc.isRuLanguage(true);
            ms.checkOtherSQL();
            button2.Enabled = false;
            button8.Enabled = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            button1.Enabled = false;            
            button5.Enabled = false;
            thr1 = new Thread(new ThreadStart(ms.Start));
            thr1.Start();
            await Task.Delay(1000);
            thr = new Thread(new ThreadStart(upd.checkStateUpdateUI));
            if (thr.IsAlive) //TODO не работает
            {
                Console.WriteLine("Поток запущен" + thr.IsAlive);
            }
            else
            {
                thr.Start();                
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ms.dbStarted)
            {
                button2.Enabled = false;
                Console.WriteLine("Останавливаю бд");
                ms.stopMysql();                
                thr1.Abort();  
                thr1.Join(500);
            }
            ms.dbStarted = false;
            button1.Enabled = true;
            button5.Enabled = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button1.Enabled=false;
            button2.PerformClick();
            button2.Enabled=false;            
            ms.resetMysql();
            button5.Enabled = true;
            button1.Enabled = true;
            try
            {
                thr.Abort();
                thr.Join(500);
            }
            catch (Exception)
            {
                Console.WriteLine("Потоки не были запущены");
            }
            
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
                thr1.Abort();
                thr1.Join(500);
                thr.Abort();
                thr.Join(500);
            }
            catch (Exception)
            {
                Console.WriteLine("Потоки не были запущены");
            }
            
            if (ms.dbStarted)
            {
                var txt = MessageBoxManager.Show("Бд запущена вы уверены что хотите выйти?", 
                    "DB is running are you sure you want to exit?",false);
                if (txt == DialogResult.Yes)
                {
                    button2.PerformClick();
                    this.Close();
                }
                if (txt == DialogResult.No)
                {
                    e.Cancel = true;
                }               
            }  
        }

        private void button6_Click(object sender, EventArgs e) //GS run
        {
            GameForm f = new GameForm();
            f.Show();
            button6.Enabled = false;
            if (f.isRun)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
            }
        }

        private void button7_Click(object sender, EventArgs e) //LS run
        {
            LoginForm f = new LoginForm();
            f.Show();
            button7.Enabled = false;
            if (f.isRun)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button5.Enabled = false;
                button8.Enabled = false;
            }
        }
    }
}
