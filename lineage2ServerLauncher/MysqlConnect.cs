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
        void Connect()
        {            
            string[] path =
            {
                @"mariadb\bin\mysqld.exe",
                @"mariadb-install-db.exe"
            };           
            var mysqlCnf = @"mariadb\my.cnf";

            if (!File.Exists(mysqlCnf))
            {
                var p = Path.GetFullPath("mariadb");
                var fileText = "[mysqld]" + "\n" + "datadir=\"" + p + @"\data" + "\"";
                createFile(mysqlCnf);
                File.WriteAllText(mysqlCnf, fileText);

            }
            else
            {

            }

            try
            {
                for (int i = 0; i < path.Length; i++)
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path[i],
                        WindowStyle = ProcessWindowStyle.Hidden,



                    });
                }


            }
            catch (Exception)
            {

                throw;
            }
        }

        void createFile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            fs.Close();

        }
    }
}
