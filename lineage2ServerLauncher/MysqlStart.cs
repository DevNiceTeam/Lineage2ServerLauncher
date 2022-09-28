using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lineage2ServerLauncher
{
    internal class MysqlStart 
    {
        Form1 form;
        MysqlState ms;       
        public bool dbStarted;

        public MysqlStart(Form1 form)
        {
            this.form = form;
            ms = new MysqlState();            
        }

        public async void Start()
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

            if (Directory.Exists(fullPathDataDir) | File.Exists(mysqlConfig)) //Проверяем наличие дириктории Data
            {                
                Console.WriteLine(fullPathDataDir + "Существует");
                path.RemoveAt(0);                
            } 
            else
            {
                ms.isFirstRun = true;

                try              //подчищаем хвосты если есть
                {                    
                    Directory.Delete(fullPathDataDir);
                    File.Delete(mysqlConfig);
                    Console.WriteLine("Хвосты есть");
                }
                catch (Exception)
                {
                    Console.WriteLine("Хвостов нету");
                }
                

                var p = Path.GetFullPath("mariadb");
                var fileText = "[mysqld]" + "\n" + "datadir=\"" + p + @"\data" + "\"";
                using (File.Create(mysqlConfig))
                { }
                File.WriteAllText(mysqlConfig, fileText);
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
                }                
                if (ms.isFirstRun) 
                {
                    Directory.CreateDirectory(@"mariadb/data/server");
                }
                ms.isLoading = false;
                ms.isLoaded = true;
                form.button2.Enabled = true;
                if (!ms.isInstalled)
                {
                    form.button8.Enabled = true;
                    form.label2.Text = LangChanger.isRuLang ? "Сервер DB не установлена" : "Server DB is not installed";
                }
                else
                {
                    form.button8.Enabled = false;
                }
                form.button6.Enabled = true;
                form.button7.Enabled = true;
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
                        Console.WriteLine($"Кильнул {sql}") ;
                        foreach (var process in Process.GetProcessesByName(sql))
                        {
                            process.Kill();
                        }
                    }
                }
            }            
        }

        public void resetMysql()
        {
            ms.isDisabled = true;            
            var mysqlCnf = @"mariadb\my.cnf";
            var fullPath = Path.GetFullPath(@"mariadb\data");
            var progress = @"mariadb\PROGRESS";
            var installed = @"mariadb\INSTALLED";

            try
            {
                File.Delete(mysqlCnf);
                File.Delete(progress);
                File.Delete(installed);

                Directory.Delete(fullPath, true);                
            }
            catch (Exception)
            {
                Console.WriteLine("Сброс не удался");
            }
            ms.isLoading = false;
            ms.isLoaded = false;
            ms.isFirstRun = false;
            ms.isReadyToLaunch = false;
            ms.isInstallation = false;
            ms.isInstalled = false;
        }

        public MysqlState GetMysqlState()
        {
            return ms;
        }
    }
}
