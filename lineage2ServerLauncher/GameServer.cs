using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lineage2ServerLauncher
{
    public class GameServer
    {
        public bool isRun;        
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
                f.textBox1.BeginInvoke(new Action(() =>
                {
                    f.textBox1.Text += ea.Data + Environment.NewLine;
                }));
            };
            proc.ErrorDataReceived += (s, a) =>
            {
                f.textBox1.BeginInvoke(new Action(() =>
                {
                    f.textBox1.Text += a.Data + Environment.NewLine;
                }));
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