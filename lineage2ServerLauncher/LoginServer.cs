using System;
using System.Diagnostics;
using System.IO;

namespace lineage2ServerLauncher
{
    public class LoginServer
    {        
        public bool isRun = false;
        int p;        

        public void Run(LoginForm f)
        {     
            Process proc = Process.Start(new ProcessStartInfo
            {
                FileName = Path.GetFullPath(@"java\bin\javaw.exe"),
                WorkingDirectory = @"server/login",
                Arguments = @"-server -Xms1024m -Xmx1024m -jar ../libs/LoginServer.jar",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            });
            proc.EnableRaisingEvents = true;
            p = proc.Id;
            proc.Exited += (sae, sea) =>
            {  
                //TODO не выходит из потока
                //Form1.ActiveForm.Controls["button7"].BeginInvoke(new Action(() =>
                //{
                //    Form1.ActiveForm.Controls["button7"].Enabled = true;
                //}));
            };
            proc.OutputDataReceived += (sa, ea) =>
            {
                if (f.IsHandleCreated)
                {
                    f.textBox1.BeginInvoke(new Action(() =>
                    {
                        f.textBox1.AppendText(ea.Data + Environment.NewLine);
                    }));
                }                
            };
            proc.ErrorDataReceived += (s, a) =>
            {
                if (f.IsHandleCreated)
                {
                    f.textBox1.BeginInvoke(new Action(() =>
                    {
                        f.textBox1.AppendText(a.Data + Environment.NewLine);
                    }));
                }                
            };

            isRun = true;
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
        }     

        public Process GetLoginProcess()
        {
            return Process.GetProcessById(p);
        }        
    }
}