using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    internal class LangChanger
    {
        public static bool isRuLang;
        public static bool isEnLang; 
      
        Form1 form;
        Msg msg = new Msg();

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
                    msg.Ru();                    
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
                    msg.En();
                    change("button3", "Russian");
                    change("label2", "Run MySQL");
                    change("label1", "Turned off");
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
