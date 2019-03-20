using System.Data;
using System;
using MySql.Data.MySqlClient;

namespace ZYSK.DZPT.Base.DbBase.MySQL
{
   public class MySQLDBHelper :  DBHelper
    {
        private const string a = "3306";

        public MySQLDBHelper()
        {
            base.DBPort = "3306";
            base.DBType = DBHelper.enumDBType.DB_MySQL;
            base.ParamaterPrefix = "?";
        }

        protected override IDbConnection CreateDbConnection()
        {
            return new MySqlConnection();
        }

        protected override IDbCommand CreateDbCommand()
        {
            return new MySqlCommand();
        }

        protected override IDbDataAdapter CreateDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        protected override string GetConnectString()
        {
            string text;
            while (true)
            {
                if (true)
                {
                }
                text = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", new object[]
                {
                    base.DBServer,
                    base.DBName,
                    base.DBUser,
                    base.DBPwd
                });
                int num = 2;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            text = text + "Port=" + base.DBPort + ";";
                            num = 1;
                            continue;
                        case 1:
                            return text;
                        case 2:
                            if (base.DBPort != "3306")
                            {
                                num = 0;
                                continue;
                            }
                            return text;
                    }
                    break;
                }
            }
            return text;
        }

        public override string GetConnInfoString()
        {
            return string.Format("MySQL:{0}/{1}/{2}", base.DBServer, base.DBName, base.DBUser);
        }

        public override IDbDataParameter CreateDbParameter()
        {
            return new MySqlParameter();
        }

        public override DateTime getServerDate()
        {
            object obj = base.QueryScalar("SELECT NOW() AS SERVERDATE FROM DUAL");
            return DateTime.Parse(obj.ToString());
        }

        public override string GetServerDateTimeString()
        {
            return "NOW()";
        }


    }
}
