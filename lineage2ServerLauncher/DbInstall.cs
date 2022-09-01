using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lineage2ServerLauncher
{
    internal class DbInstall
    {
        
        public void install()
        {
             //TODO
            MySqlConnection conn = MysqlConnect.GetConnection();
            conn.Open();
        }
        
    }
}
