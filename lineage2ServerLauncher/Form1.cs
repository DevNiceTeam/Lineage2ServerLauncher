using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        Thread thr;
        
        public Form1()
        {
            lc = new LangChanger(this);
            mc = new MysqlConnect(this);
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            thr = new Thread(mc.Connect);
            thr.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {  
            lc.isRuLanguage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lc.isEnLanguage();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            lc.isRuLanguage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mc.stopMysql();
            thr.Abort();
            thr.Join(500);
        }        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine(thr.ThreadState);
            if (thr.ThreadState == System.Threading.ThreadState.Running)
            {
                MessageBox.Show("Бд запущена вы уверены что хотите выйти?" ,"", MessageBoxButtons.YesNo);
               
                e.Cancel = true;
            }
            
        }
    }
}
