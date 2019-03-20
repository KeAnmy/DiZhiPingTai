using DDTek.Oracle;
using System.Data;
using System.IO;
using ZYSK.DZPT.Base.Utility.Tools;

namespace ZYSK.DZPT.Base.DbBase.Oracle
{
    public class OracleDDTekDBHelper :OracleDBHelperBase
    {
        private new const string a = "1521";

        public override DBHelper.enumDBType ActuralDBType
        {
            get
            {
                return DBHelper.enumDBType.DB_Oracle_Local;
            }
        }

        public OracleDDTekDBHelper()
        {
            base.DBType = DBHelper.enumDBType.DB_Oracle;
            base.DBDriver = DBHelper.enumDBDriver.DDTek;
            base.DBPort = "1521";
            if (ConfigUtil.GetConfigValueBoolean("oracle_log", false))
            {
                OracleTrace.TraceFile = "oracle_log.log";
                OracleTrace.EnableTrace = 1;
                OracleTrace.RecreateTrace = 2;
            }
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
            string text;
            while (true)
            {
                text = string.Format("Host = {0}; User ID={1}; Password ={2}; Service Name={3};Pooling=false", new object[]
                {
                    base.DBServer,
                    base.DBUser,
                    base.DBPwd,
                    base.DBSid
                });
                int num = 0;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            if (!string.IsNullOrEmpty(base.DDTekLicensePath))
                            {
                                num = 4;
                                continue;
                            }
                            goto IL_C0;
                        case 1:
                            goto IL_C0;
                        case 2:
                            if (!string.IsNullOrEmpty(base.DBPort))
                            {
                                num = 6;
                                continue;
                            }
                            return text;
                        case 3:
                            if (!File.Exists(base.DDTekLicensePath))
                            {
                                num = 1;
                                continue;
                            }
                            goto IL_86;
                        case 4:
                            if (true)
                            {
                            }
                            num = 3;
                            continue;
                        case 5:
                            goto IL_86;
                        case 6:
                            text = text + "; Port=" + base.DBPort;
                            num = 7;
                            continue;
                        case 7:
                            return text;
                    }
                    break;
                IL_86:
                    text = text + ";  License Path=" + base.DDTekLicensePath;
                    num = 2;
                    continue;
                IL_C0:
                    base.DDTekLicensePath = Path.Combine(Directory.GetParent(base.GetType().Assembly.Location).FullName, "DDTEK.LIC");
                    num = 5;
                }
            }
            return text;
        }

        public override IDbDataParameter CreateDbParameter()
        {
            return new OracleParameter();
        }

        public override string GetConnInfoString()
        {
            return string.Format("Oracle:{0}/{1}/{2}", base.DBServer, base.DBSid, base.DBUser);
        }

    }
}
