using System.Data;
using System.Data.OracleClient;

namespace ZYSK.DZPT.Base.DbBase.Oracle
{
    /// <summary>
    /// 使用Oracle客户端
    /// </summary>
    public class OracleDBHelper:OracleDBHelperBase
    {
        public OracleDBHelper()
        {
            base.DBType = DBHelper.enumDBType.DB_Oracle;
            base.ParamaterPrefix = string.Empty;
        }

        protected override IDbConnection CreateDbConnection()
        {
            return new OracleConnection();
        }

        protected override IDbCommand CreateDbCommand()
        {
            return new OracleCommand();
        }

        protected override IDbDataAdapter CreateDataAdapter()
        {
            return new OracleDataAdapter();
        }

        protected override string GetConnectString()
        {
            //return string.Format("User ID={0};Password ={1};Data Source={2};Integrated Security=false;Unicode=True;", base.DBUser, base.DBPwd, base.DBServiceName);
            return string.Format("User ID={0};Password ={1};Data Source={2};Persist Security Info=True;", base.DBUser, base.DBPwd, base.DBServiceName);

        }

        public override IDbDataParameter CreateDbParameter()
        {
            return new OracleParameter();
        }

        public override string GetConnInfoString()
        {
            return string.Format("Oracle:{0}/{1}", base.DBServiceName, base.DBUser);
        }

    }
}
