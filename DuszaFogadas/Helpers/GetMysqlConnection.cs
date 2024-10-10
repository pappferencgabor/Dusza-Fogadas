using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuszaFogadas.Helpers
{
    class GetMysqlConnection
    {
        public static string connectionString = "datasource=localhost;" +
                                  "port=3306;" +
                                  "username=root;" +
                                  "password=;" +
                                  "database=betting;";

        public static MySqlConnection getMysqlConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
