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
    internal class MysqlConnect 
    {
        Form1 form;
        MysqlState ms;

        public MysqlConnect(Form1 form)
        {
            this.form = form;
            ms = new MysqlState();
        }

        public async void Connect()
        {
            List<String> path = new List<String>
            {
                @"mariadb\bin\mariadb-install-db.exe",
                @"mariadb\bin\mysqld.exe"                
            };           
            var mysqlCnf = @"mariadb\my.cnf";

            var fullPath = Path.GetFullPath(@"mariadb\data");

            if (Directory.Exists(fullPath))
            {
                ms.isAlreadyLaunched = true;
                Console.WriteLine(fullPath + "Существует");
                path.RemoveAt(0);
            }
            else
            {
                Console.WriteLine(fullPath + "Нету");                
            }

            if (!File.Exists(mysqlCnf))
            {
                var p = Path.GetFullPath("mariadb");
                var fileText = "[mysqld]" + "\n" + "datadir=\"" + p + @"\data" + "\"";
                createFile(mysqlCnf);
                File.WriteAllText(mysqlCnf, fileText);                             
            }            

            try
            {
                foreach (var process in Process.GetProcessesByName("mysqld"))
                {
                    process.Kill();
                }               

                foreach (var item in path)
                {                    
                    ms.isLoading = true;
                    if (ms.isAlreadyLaunched)
                    {
                        await Task.Delay(4000);                        
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = item,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });                        
                    }
                    else
                    {
                        await Task.Delay(4000);
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = item,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });
                    }

                    if (path.Count >= 2)
                    {
                        ms.isLoaded = true;
                        //Console.WriteLine(ms.isLoaded);                        
                    }
                } 
            }
            catch (Exception)
            {
                
            }
        }        

        void createFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            fs.Close();
        }
    }
}
