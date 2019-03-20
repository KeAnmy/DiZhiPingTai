using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ZYSK.DZPT.Base.DbBase.Access
{
    public class AccessDBHelper :DBHelper
    {

        public AccessDBHelper()
        {
            base.DBType = DBHelper.enumDBType.DB_ACCESS;
        }

        protected override IDbConnection CreateDbConnection()
        {
            return new OleDbConnection();
        }

        protected override IDbCommand CreateDbCommand()
        {
            return new OleDbCommand();
        }

        protected override IDbDataAdapter CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }

        protected override string GetConnectString()
        {
            string arg ="Microsoft.Jet.OLEDB.4.0"; //32位用
            //string arg = "Microsoft.ACE.OLEDB.12.0";  //64位用
            return string.Format("Provider={0};Data Source={1};Persist Security Info=False", arg, base.DBPath);
          
        }

        public override IDbDataParameter CreateDbParameter()
        {
            return new OleDbParameter();
        }

        public override string GetConnInfoString()
        {
            return "Access:" + base.DBPath;
        }


        public override DateTime getServerDate()
        {
            return DateTime.Now;
        }

        public override string GetServerDateTimeString()
        {
            return " Now() ";
        }

        public string GetConnString()
        {
            string arg = "Microsoft.Jet.OLEDB.4.0"; //32位用
            //string arg = "Microsoft.ACE.OLEDB.12.0";  //64位用
            return string.Format("Provider={0};Data Source={1};Persist Security Info=False", arg, base.DBPath);

        }
    }
}
