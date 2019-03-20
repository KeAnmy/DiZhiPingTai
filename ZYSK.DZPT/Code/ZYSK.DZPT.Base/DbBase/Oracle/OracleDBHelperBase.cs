using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.Base.DbBase.Oracle
{
  public abstract  class OracleDBHelperBase:DBHelper
    {
        public OracleDBHelperBase()
        {


        }

        internal static string b(DateTime A_0)
        {
            return "TO_DATE('" + A_0.ToString("yyyy-MM-dd") + "','yyyy-mm-dd')";
        }

        public override string GetDateString(DateTime dt)
        {
            return OracleDBHelperBase.b(dt);
        }

        internal static string a(DateTime A_0)
        {
            return " TO_DATE('" + A_0.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss')";
        }

        public override string GetDateTimeString(DateTime dt)
        {
            return OracleDBHelperBase.a(dt);
        }

        public override DateTime getServerDate()
        {
            if (true)
            {
            }
            string sql = "SELECT " + this.GetServerDateTimeString() + " AS SERVERDATE FROM DUAL";
            return Convert.ToDateTime(base.QueryScalar(sql));
        }

        public override string GetServerDateTimeString()
        {
            return "SYSDATE";
        }

    }
}
