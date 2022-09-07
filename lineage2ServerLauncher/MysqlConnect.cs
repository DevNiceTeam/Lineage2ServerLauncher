using MySql.Data.MySqlClient;
using System;

namespace lineage2ServerLauncher
{
    class MysqlConnect
    {        
        private static MySqlConnection Connection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password;

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }

        public static MySqlConnection GetConnection()
        {
            string host = "127.0.0.1";
            int port = 3306;
            string database = "server";
            string username = "root";
            string password = "";

            return Connection(host, port, database, username, password);
        }
    }
}
