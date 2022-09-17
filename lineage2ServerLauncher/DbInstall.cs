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
        CancellationTokenSource cts = new CancellationTokenSource();
        
        MySqlConnection conn = MysqlConnect.GetConnection();
        MysqlState ms;
        UpdInterface upd;       

        public DbInstall(MysqlState ms, UpdInterface upd)
        {
            this.ms = ms;
            this.upd = upd;            
        }

        String loginPath = @"server\db_installer\sql\login";
        String gamePath = @"server\db_installer\sql\game"; 

        public void install()
        {
            try
            {
                conn.Open();
                checkConn();
            }
            catch (Exception)
            {
                Msg.Show("Ошибка подключения к бд", "Error db connect", true);
            }

            upd.Pause();
            upd.Resume();

            Task.Factory.StartNew(() =>
            {                
                installer(loginPath);
                installer(gamePath);
                ms.isInstallation = false;
                ms.isInstalled = true;
                checkInstall();
            });            
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
            MySqlCommand set = new MySqlCommand(s,conn);
            set.ExecuteNonQuery();            
        }

        void installer(String path)
        {  
            List<String> files = new List<String>();
            files.AddRange(Directory.GetFiles(path));
            foreach (var item in files)
            {                
                using (var reader = new StreamReader(item))
                {
                    Thread.Sleep(5);
                    string line = reader.ReadToEnd();                    
                    setCommand(line);
                }

                ms.isInstallation = true;
                checkInstall();
            }           
        }

        void checkInstall()
        {
            var progress = @"mariadb\PROGRESS";
            var installed = @"mariadb\INSTALLED";
            if (ms.isInstallation)
            {
                if (!File.Exists(progress))
                {
                    File.Create(progress);
                    File.Delete(installed);
                }
            }
            else if (ms.isInstalled)
            {
                if (!File.Exists(installed))
                {
                    File.Create(installed);
                    File.Delete(progress);
                }
            }
        }

        public void checkInstall(bool onLoad)
        {
            var progress = @"mariadb\PROGRESS";
            var installed = @"mariadb\INSTALLED";
            if (onLoad)
            {
                if (File.Exists(progress))
                {
                    ms.isInstallation = true;
                }
                else if (File.Exists(installed))
                {
                    ms.isInstalled = true;
                }
            }
        }
    }    
}
