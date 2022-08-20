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
        public bool isRuLang;
        public bool isEnLang;       

        Form1 form;

        public LangChanger(Form1 form)
        {
            this.form = form;             
        }

        public void isRuLanguage()
        {     
            isRuLang = true;
            isEnLang = false;
            change("button3","Русский");
        }
        
        public void isEnLanguage()
        {
            isRuLang = false;
            isEnLang = true;
            change("button3", "Russian");
        }      
        
        void change(string controlName, string text)
        {
            form.Controls[controlName].Text = text;
        }
    }
}
