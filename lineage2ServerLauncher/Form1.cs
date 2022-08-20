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

    public partial class Form1 : Form
    {
        LangChanger lc;
        public Form1()
        {
            lc = new LangChanger(this);
            InitializeComponent();           
        }

        private void button1_Click(object sender, EventArgs e)
        { 
           
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
    }
}
