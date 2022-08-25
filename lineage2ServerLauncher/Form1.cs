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
        MysqlConnect mc;
        Thread thr1;
        Thread thr;



        public Form1()
        {
            lc = new LangChanger(this);
            mc = new MysqlConnect(this);            
            InitializeComponent();           
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            lc.isRuLanguage(true);
            mc.checkOtherSQL();
            button2.Enabled = false;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;
            button5.Enabled = false;
            thr = new Thread(mc.checkStateUpdateUI);
            thr1 = new Thread(mc.Connect);

            thr.Start();
            await Task.Delay(2000);
            thr1.Start();                     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (mc.dbStarted)
            {
                button2.Enabled = false;
                Console.WriteLine("Останавливаю бд");
                mc.stopMysql();
                thr1.Abort();
                thr1.Join(500);                
            }            
            button1.Enabled = true;
            button5.Enabled = true;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            button5.Enabled = false;
            button1.Enabled=false;
            button2.PerformClick();
            button2.Enabled=false;            
            mc.resetMysql();
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
                thr1.Abort();
                thr1.Join(500);
                thr.Abort();
                thr.Join(500);
            }
            catch (Exception)
            {
                Console.WriteLine("Потоки не были запущены");
            }
            
            if (mc.dbStarted)
            {
                var txt = MessageBoxManager.Show("Бд запущена вы уверены что хотите выйти?", 
                    "DB is running are you sure you want to exit?");
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
    }
}
