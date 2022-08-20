using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    internal class LangChanger
    {
        Form1 form;
        public LangChanger(Form1 form)
        {
            this.form = form;
        }

        public void isRuLang()
        {
            form.Controls["button3"].Text = "";
        }
        bool isEnLang()
        {
            return true;
        }
        
    }
}
