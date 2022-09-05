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

        public UpdInterface(Form1 form, MysqlState mc)
        {
            this.form = form;
            this.ms = mc;            
        }
        
        public void checkStateUpdateUI()
        {
            form.cts = new CancellationTokenSource();
            form.task = Task.Run(() =>
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
                    }
                    if (ms.isDisabled)
                    {
                        form.label1.Invoke(new Action(() => form.label1.Text = LangChanger.isRuLang ? "Выключено" : "Turned off"));
                    }
                    Console.WriteLine("isLoading = " + ms.isLoading +
                        " isLoaded = " + ms.isLoaded +
                        " isDisabled = " + ms.isDisabled + " thr.State = " + form.task.Status);
                    if (form.cts.IsCancellationRequested)  //прерывание потока task
                    {
                        return;
                    }

                }
            });
            
        }

    }
}

