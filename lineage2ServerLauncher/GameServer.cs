using System;
using System.Diagnostics;
using System.IO;

namespace lineage2ServerLauncher
{
    public class GameServer
    {
        public bool isRun = false;        
        int p;          

        public void Run(GameForm f)
        {
            isRun = true;
            Process proc = Process.Start(new ProcessStartInfo
            {
                FileName = Path.GetFullPath(@"java\bin\javaw.exe"),
                WorkingDirectory = @"server/game",
                Arguments = @"-server -Xms2048m -Xmx2048m -jar ../libs/GameServer.jar",
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
                //Form1.ActiveForm.Controls["button6"].BeginInvoke(new Action(() =>
                //{
                //    Form1.ActiveForm.Controls["button6"].Enabled = true;
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

        public Process GetGameProcess()
        {
            return Process.GetProcessById(p);
        }      
    }
}