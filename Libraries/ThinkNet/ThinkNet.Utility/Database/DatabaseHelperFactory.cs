using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkNet.Utility
{
    public class DatabaseHelperFactory
    {
        public static DatabaseHelper Create(DatabaseType mDatabaseType, string connectionString)
        {
            DatabaseHelper obj = null;
            switch (mDatabaseType)
            {
                case DatabaseType.MySQL:
                    obj = new MySQLHelper(connectionString);
                    break;
                case DatabaseType.OleDB:
                    obj = new OleDBHelper(connectionString);
                    break;
                case DatabaseType.Oracle:
                    obj = new OracleHelper(connectionString);
                    break;
                case DatabaseType.SQLLite:
                    obj = new SQLLiteHelper(connectionString);
                    break;
                case DatabaseType.SQLServer:
                    obj = new SQLServerHelper(connectionString);
                    break;
            }
            return obj;
        }
    }
}