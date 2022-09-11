using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lineage2ServerLauncher
{
    internal class UpdInterface
    {
        Form1 form;
        MysqlState ms;

        public UpdInterface(Form1 form, MysqlState ms)
        {
            this.form = form;
            this.ms = ms;
        }
        
        public void checkStateUpdateUI()
        {
            form.cts = new CancellationTokenSource();
            
            form.task = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    
                    Thread.Sleep(1000);
                    
                    if (ms.isLoading)
                    {
                        form.label1.Invoke(new Action(() => form.label1.Text = LangChanger.isRuLang ? "Запускается..." : "Starting..."));
                    }
                    if (ms.isLoaded)
                    {
                        form.label1.Invoke(new Action(() => form.label1.Text = LangChanger.isRuLang ? "Запущено" : "Launched"));
                        //form.cts.Cancel();
                    }
                    if (ms.isDisabled)
                    {
                        form.label1.Invoke(new Action(() => form.label1.Text = LangChanger.isRuLang ? "Выключено" : "Turned off"));
                    }
                    

                    if (ms.isInstallation)
                    {
                        form.label2.Invoke(new Action(() => form.label2.Text = LangChanger.isRuLang ? "Устанавливается..." : "Installation..."));
                    }

                    if (ms.isInstalled)
                    {
                        form.label2.Invoke(new Action(() => form.label2.Text = LangChanger.isRuLang ? "Установлена" : "Installed"));
                        //form.cts.Cancel();
                    }

                    debug();
                    if (form.cts.IsCancellationRequested)  //прерывание потока task
                    {
                        return;
                    } 
                }
            });            
        }

        void debug()
        {
            Console.WriteLine($@"isLoading = " + ms.isLoading +
                        " isLoaded = " + ms.isLoaded +
                        " isDisabled = " + ms.isDisabled + " thr.State = " + form.task.Status +
                        " isInstallation = " + ms.isInstallation +
                        " isInstalled = " + ms.isInstalled);
        }

        public void stop()
        {            
            form.cts.Cancel();
        }

    }
}

