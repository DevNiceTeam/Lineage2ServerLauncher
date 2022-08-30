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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GameServer gs = new GameServer();
            textBox1.Text += gs.run();
        }

        private void button1_Click(object sender, EventArgs e)
        {
                     
        }       
    }

        
    }

