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
    internal class MysqlStart 
    {
        LangChanger lc;
        Form1 form;
        MysqlState ms;
        public bool dbStarted;

        public MysqlStart(Form1 form)
        {
            this.form = form;
            ms = new MysqlState();
            lc = new LangChanger(form);
        }

        public async void Connect()
        {
            var mysqlConfig = @"mariadb\my.cnf";
            var fullPathDataDir = Path.GetFullPath(@"mariadb\data"); 

            List<String> path = new List<String>
            {
                @"mariadb\bin\mariadb-install-db.exe",
                @"mariadb\bin\mysqld.exe"
            };

            dbStarted = true;
            ms.isDisabled = false;

            if (File.Exists(mysqlConfig)) //Проверяем наличие конфига
            {

            }
            else
            {
                var p = Path.GetFullPath("mariadb");
                var fileText = "[mysqld]" + "\n" + "datadir=\"" + p + @"\data" + "\"";
                using (File.Create(mysqlConfig))
                {}
                File.WriteAllText(mysqlConfig, fileText);
                
            }

            if (Directory.Exists(fullPathDataDir)) //Проверяем наличие дириктории Data
            {  
                Console.WriteLine(fullPathDataDir + "Существует");
                path.RemoveAt(0);
                ms.isFirstRun = true;
            } 
            else
            {
                if (File.Exists(mysqlConfig)) //Проверяем наличие конфига
                {
                    //подчищаем хвосты если есть
                    try
                    {
                        Directory.Delete(fullPathDataDir);
                        File.Delete(mysqlConfig);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Хвосты");
                    }
                }
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
                    if (ms.isFirstRun)
                    {
                        await Task.Delay(4000);                        
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = item,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });                       
                        ms.isLoading = false;
                        ms.isLoaded = true;
                        form.button8.Enabled = true;
                    }
                    else
                    {
                        await Task.Delay(4000);
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = item,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });
                        ms.isLoading = false;
                        ms.isLoaded = true;
                    }
                }
                if (ms.isFirstRun)
                {
                    Console.WriteLine("sssss");
                    Directory.CreateDirectory(@"mariadb/data/gs");
                    Directory.CreateDirectory(@"mariadb/data/ls");
                }
            }
            catch (Exception)
            {
                
            }
        }    
        
        public void stopMysql()
        {           
            if (ms.isLoaded)
            {
                ms.isLoaded = false;                
                ms.isDisabled = true;                
                foreach (var process in Process.GetProcessesByName("mysqld"))
                {
                    process.Kill();
                }
            }
        }
        
        public void checkOtherSQL()
        {
            List<String> sqlName = new List<String>
            { 
                "mysql",
                "mysqld"
            };

            foreach (var sql in sqlName)
            {
                foreach (var proc in Process.GetProcesses())
                {
                    if (proc.ProcessName == sql)
                    {
                        var txt = MessageBoxManager.Show("Запущена сторанняя бд отключить её??", "A third-party database is running to disable it??",false);
                        if (txt == DialogResult.Yes)
                        {
                            foreach (var process in Process.GetProcessesByName(sql))
                            {
                                process.Kill();
                            }
                        }                        
                    }
                }
            }            
        }

        public async void resetMysql()
        {
            ms.isDisabled = true;            
            var mysqlCnf = @"mariadb\my.cnf";
            var fullPath = Path.GetFullPath(@"mariadb\data");

            try
            {
                File.Delete(mysqlCnf);
                Directory.Delete(fullPath, true);
                ms.isLoading = false;
                ms.isLoaded = false;
                ms.isFirstRun = false;
                ms.isReadyToLaunch = false;
                await Task.Delay(2000);
            }
            catch (Exception)
            {
                Console.WriteLine("Сброс не удался");
            }    
        }

        public async void checkStateUpdateUI()
        {
            for (;;)
            {
                await Task.Delay(500);
                Console.WriteLine(ms.isFirstRun);
                if (ms.isLoading)
                {
                    form.label1.Invoke(new Action(() => form.label1.Text = LangChanger.isRuLang ? "Запускается..." : "Starting..."));
                }
                if (ms.isLoaded)
                {
                    form.label1.Invoke(new Action(() => form.label1.Text = LangChanger.isRuLang ? "Запущено" : "Launched"));
                }
                if (ms.isDisabled)
                {
                    form.label1.Invoke(new Action(() => form.label1.Text = LangChanger.isRuLang ? "Выключено" : "Turned off"));
                }
                //Console.WriteLine("isLoading = " + ms.isLoading +
                //    " isLoaded = " + ms.isLoaded + 
                //    " isDisabled = " + ms.isDisabled);
            }            
        }
    }
}
