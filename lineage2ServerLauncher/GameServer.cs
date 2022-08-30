using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace lineage2ServerLauncher
{
    internal class GameServer
    {       
        public string getPath()
        {    
            return Path.GetFullPath(@"java\bin\java.exe"); 
        }

        public Task<int> run()
        {
            var tcs = new TaskCompletionSource<int>();
            String txt = "";

            Process proc = Process.Start(new ProcessStartInfo
            {
                FileName = getPath(),
                WorkingDirectory = @"server\game",
                Arguments = @"-server -Dfile.encoding=UTF-8 -Xms128m -Xmx256m -cp config;../libs/LoginServer.jar org.l2jmobius.loginserver.LoginServer",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
            });

            proc.BeginErrorReadLine();
            proc.ErrorDataReceived += (s, a) =>
            {
                txt += a.Data + Environment.NewLine;
            };  
            
            proc.WaitForExit();

            return tcs.Task;
        }
    }
}