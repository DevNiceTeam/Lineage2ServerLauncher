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
        public static bool isRuLang;
        public static bool isEnLang; 
        

        Form1 form;

        public LangChanger(Form1 form)
        {
            this.form = form;             
        }

        public bool isRuLanguage(bool RuLang)
        {            
            if (RuLang)
            {
                isRuLang = true;
                if (isRuLang)
                {
                    isEnLang = false;
                    change("button3", "Русский");
                    return true;
                }
            }
            return false;
        }
        
        public bool isEnLanguage(bool EnLang)
        {    
            if (EnLang)
            {
                isEnLang = true;
                if (isEnLang)
                {
                   
                    isRuLang = false;
                    change("button3", "Russian");
                    return true;
                }              
            }    
            return false;                       
        }        

        void change(string controlName, string text)
        {
            form.Controls[controlName].Text = text;
        }
    }
}
