using System;
using System.Threading;
using System.Threading.Tasks;

namespace lineage2ServerLauncher
{
    internal class UpdInterface
    {
        Form1 form;
        MysqlState ms;
        CancellationTokenSource cts = new CancellationTokenSource();
        ManualResetEvent _manualEvent = new ManualResetEvent(true);
        Task tsk;

        public UpdInterface(Form1 f, MysqlState ms)
        {
            this.form = f;
            this.ms = ms;
        }
        
        public void checkStateUpdateUI()
        {              
            tsk = Task.Factory.StartNew(()=>
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

                    //debug();
                    _manualEvent.WaitOne();

                    if (cts.IsCancellationRequested)  //прерывание потока task
                    {
                        return;
                    } 
                }
            });            
        }

        void debug()
        {
            Console.WriteLine("isLoading = " + ms.isLoading +
                        " isLoaded = " + ms.isLoaded +
                        " isDisabled = " + ms.isDisabled +
                        " thr.State = " + tsk.Status +
                        " isInstallation = " + ms.isInstallation +
                        " isInstalled = " + ms.isInstalled +
                        " isResume = " + isResume +
                        " isPause = " + isPause);
        }

        public void Exited()
        {            
            cts.Cancel();
        }

        bool isResume, isPause = false;

        public bool Resume()
        {
            isPause = false;
            isResume = true;
            return _manualEvent.Set();
        }

        public bool Pause()
        {
            isResume = false;
            isPause = true;
            return _manualEvent.Reset();
        }

        public bool Check()
        {
            if (isPause)
            {
                return true;
            } 

            return false;
        }
    }
}

