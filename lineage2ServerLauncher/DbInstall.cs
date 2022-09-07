using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    internal class DbInstall
    {
        MySqlConnection conn = MysqlConnect.GetConnection();

        String loginPath = @"server\db_installer\sql\login";
        // String gamePath = @"server\db_installer\sql\game";       


        // public bool isInstalled; // В бд загружены все sql файлы сервера
        public void install()
        { 
            try
            {
                
                    conn.Open();
                    checkConn();
                    installer(loginPath);                    
                
            }
            catch (Exception)
            {
                Msg.Show("Ошибка подключения к бд","Error db connect",true);
            }                        
        }

        public bool checkConn()
        {
            var isConnected = conn.State == ConnectionState.Open;
            if (isConnected)
            {
                Console.WriteLine("Есть контакт");
            }
            else
            {
                Console.WriteLine("Не роботь");
            }
            return isConnected;
        }

        void setCommand(String s)
        {
            MySqlCommand set = new MySqlCommand(s);
            set.ExecuteNonQuery();
        }

        public void installer(String path)
        {
            //StreamReader sr;
            List<String> files = new List<String>();
            files.AddRange(Directory.GetFiles(path, "sql"));
            foreach (var item in files)
            {
                Console.WriteLine(item);
                
                //setCommand(b);
            }
            
        }
    }
}
