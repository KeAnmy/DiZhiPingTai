using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYSK.DZPT.Base.Enum;
using ZYSK.DZPT.Base.Public;

namespace ZYSK.DZPT.Base.DbBase
{
    public class SQLStringUtility
    {

        private const string a = "\\";

        /// <summary>
        /// 将文本转换成适合在Sql语句里使用的字符串。
        /// </summary>
        /// <returns>转换后文本</returns>	
        [Obsolete("不要使用该方法。不再维护。请使用IDBHelper.GetEscapedString方法或SQLStringUtility.GetQuotedString的静态重载函数。")]
        public static string GetQuotedString(string pStr)
        {
            if (true)
            {
            }
            return string.Format("'{0}'", (pStr == null) ? "" : pStr.Replace("'", "''"));
        }

        /// <summary>
        /// 将文本转换成适合在Sql语句里使用的字符串。替代原有的 GetQuotedString(string)方法
        /// </summary>
        /// <param name="input">待处理的字符串</param>
        /// <param name="db">转换后的文本</param>
        /// <returns></returns>
        public static string GetQuotedString(string input, IDBHelper db)
        {
            return "'" + db.GetEscapedString(input) + "'";
        }

        /// <summary>
        /// 返回用于Sql语句的 日期 包装字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDateString(IDBHelper iDBH, DateTime dt)
        {
            return iDBH.GetDateString(dt);
        }

        /// <summary>
        /// 在sql语句中,用来取数据库服务器时间的描述字符串
        /// 陈进 2006-1-16
        /// </summary>
        /// <returns></returns>
        public static string GetServerDateTimeString(IDBHelper iDBH)
        {
            return iDBH.GetServerDateTimeString();
        }

        /// <summary>
        /// 返回用于Sql语句的 日期 时间 包装字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetDateTimeString(IDBHelper iDBH, DateTime dt)
        {
            return iDBH.GetDateTimeString(dt);
        }


        public static string GetUpdateSQL(string tableName, IList<DBFieldItem> dbFields, string strfilter, IDBHelper iDBH)
        {
            int num = 8;
            StringBuilder stringBuilder = new StringBuilder();
            while (true)
            {
                int num2 = 0;
                switch (num)
                {
                    case 0:
                        stringBuilder.Append(",");
                        num = 21;
                        continue;
                    case 1:
                        goto IL_2E3;
                    case 2:
                        goto IL_A3;
                    case 3:
                        goto IL_A3;
                    case 4:
                        {
                            if (dbFields.Count == 0)
                            {
                                num = 1;
                                continue;
                            }
                            if (true)
                            {
                            }
                            stringBuilder = new StringBuilder();
                            stringBuilder.Append(" update ");
                            stringBuilder.Append(tableName);
                            stringBuilder.Append(" set ");
                            int count = dbFields.Count;
                            DBFieldItem dBFieldItem = null;
                            num2 = 0;
                            num = 6;
                            continue;
                        }
                    case 5:
                        {
                            int count = 0;
                            if (num2 != count - 1)
                            {
                                num = 0;
                                continue;
                            }
                            goto IL_138;
                        }
                    case 6:
                        goto IL_23C;
                    case 7:
                        if (stringBuilder.ToString().EndsWith(","))
                        {
                            num = 10;
                            continue;
                        }
                        goto IL_37D;
                    case 9:
                        goto IL_A3;
                    case 10:
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                        num = 23;
                        continue;
                    case 11:
                        num = 7;
                        continue;
                    case 12:
                        {
                            int count = 0;
                            if (num2 >= count)
                            {
                                num = 11;
                                continue;
                            }
                            DBFieldItem dBFieldItem = dbFields[num2];
                            stringBuilder.Append(dBFieldItem.Name);
                            stringBuilder.Append(" = ");
                            num = 22;
                            continue;
                        }
                    case 13:
                        goto IL_A3;
                    case 14:
                        goto IL_A3;
                    case 15:
                        goto IL_A3;
                    case 16:
                        {
                            EnumDBFieldType fieldType = new EnumDBFieldType();
                            switch (fieldType)
                            {
                                case EnumDBFieldType.FTNumber:
                                    {
                                        DBFieldItem dBFieldItem = new DBFieldItem();
                                        stringBuilder.Append(dBFieldItem.Value);
                                        num = 26;
                                        continue;
                                    }
                                case EnumDBFieldType.FTString:
                                    {
                                        DBFieldItem dBFieldItem = new DBFieldItem();
                                        stringBuilder.Append(SQLStringUtility.GetQuotedString(Convert.ToString(dBFieldItem.Value), iDBH));
                                        num = 15;
                                        continue;
                                    }
                                case EnumDBFieldType.FTDate:
                                    {
                                        DBFieldItem dBFieldItem = new DBFieldItem();
                                        stringBuilder.Append(SQLStringUtility.GetDateString(iDBH, Convert.ToDateTime(dBFieldItem.Value)));
                                        num = 3;
                                        continue;
                                    }
                                case EnumDBFieldType.FTDatetime:
                                    {
                                        DBFieldItem dBFieldItem = new DBFieldItem();
                                        stringBuilder.Append(SQLStringUtility.GetDateTimeString(iDBH, Convert.ToDateTime(dBFieldItem.Value)));
                                        num = 9;
                                        continue;
                                    }
                                case EnumDBFieldType.FTServerNowDatetime:
                                    stringBuilder.Append(SQLStringUtility.GetServerDateTimeString(iDBH));
                                    num = 2;
                                    continue;
                                default:
                                    num = 25;
                                    continue;
                            }
                            break;
                        }
                    case 17:
                        num = 4;
                        continue;
                    case 18:
                        goto IL_23C;
                    case 19:
                        num = 24;
                        continue;
                    case 20:
                        goto IL_2E5;
                    case 21:
                        goto IL_138;
                    case 22:
                        {
                            DBFieldItem dBFieldItem = new DBFieldItem();
                            if (dBFieldItem.Value != null)
                            {
                                num = 19;
                                continue;
                            }
                            goto IL_2E5;
                        }
                    case 23:
                        goto IL_27C;
                    case 24:
                        {
                            DBFieldItem dBFieldItem = new DBFieldItem();
                            if (string.IsNullOrEmpty(dBFieldItem.Value.ToString()))
                            {
                                num = 20;
                                continue;
                            }
                            EnumDBFieldType fieldType = dBFieldItem.FieldType;
                            num = 16;
                            continue;
                        }
                    case 25:
                        num = 14;
                        continue;
                    case 26:
                        goto IL_A3;
                }
                if (dbFields != null)
                {
                    num = 17;
                    continue;
                }
                goto IL_302;
            IL_A3:
                num = 5;
                continue;
            IL_138:
                num2++;
                num = 18;
                continue;
            IL_23C:
                num = 12;
                continue;
            IL_2E5:
                stringBuilder.Append("NULL");
                num = 13;
            }
        IL_27C:
            goto IL_37D;
        IL_2E3:
        IL_302:
            return string.Empty;
        IL_37D:
            stringBuilder.Append(" where ");
            stringBuilder.Append(strfilter);
            return stringBuilder.ToString();
        }

        public static string GetInsertSQL(string tableName, IList<DBFieldItem> dbFields, IDBHelper iDBH)
        {

            int num = 30;
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder2 = new StringBuilder();
            DBFieldItem dBFieldItem = new DBFieldItem();
            int num2 = 0;
            int count = 0;
            while (true)
            {

                switch (num)
                {
                    case 0:
                        num = 27;
                        continue;
                    case 1:
                        goto IL_1C4;
                    case 2:
                        goto IL_396;
                    case 3:
                        stringBuilder.Remove(stringBuilder.Length - 1, 1);
                        num = 26;
                        continue;
                    case 4:
                        stringBuilder2.Append(",");
                        stringBuilder.Append(",");
                        num = 13;
                        continue;
                    case 5:
                        goto IL_1C4;
                    case 6:
                        goto IL_1C4;
                    case 7:
                        {
                            EnumDBFieldType fieldType = dBFieldItem.FieldType;
                            switch (fieldType)
                            {
                                case EnumDBFieldType.FTNumber:
                                    stringBuilder.Append(dBFieldItem.Value);
                                    num = 5;
                                    continue;
                                case EnumDBFieldType.FTString:
                                    stringBuilder.Append(SQLStringUtility.GetQuotedString(Convert.ToString(dBFieldItem.Value), iDBH));
                                    num = 1;
                                    continue;
                                case EnumDBFieldType.FTDate:
                                    stringBuilder.Append(SQLStringUtility.GetDateString(iDBH, Convert.ToDateTime(dBFieldItem.Value)));
                                    num = 20;
                                    continue;
                                case EnumDBFieldType.FTDatetime:
                                    stringBuilder.Append(SQLStringUtility.GetDateTimeString(iDBH, Convert.ToDateTime(dBFieldItem.Value)));
                                    num = 21;
                                    continue;
                                case EnumDBFieldType.FTServerNowDatetime:
                                    stringBuilder.Append(SQLStringUtility.GetServerDateTimeString(iDBH));
                                    num = 24;
                                    continue;
                                default:
                                    num = 19;
                                    continue;
                            }
                            break;
                        }
                    case 8:
                        num = 22;
                        continue;
                    case 9:
                        if (stringBuilder2.ToString().EndsWith(","))
                        {
                            num = 12;
                            continue;
                        }
                        goto IL_1EB;
                    case 10:
                        goto IL_328;
                    case 11:
                        if (dBFieldItem.Value != null)
                        {
                            num = 8;
                            continue;
                        }
                        goto IL_379;
                    case 12:
                        stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
                        num = 14;
                        continue;
                    case 13:
                        goto IL_43F;
                    case 14:
                        goto IL_1EB;
                    case 15:
                        goto IL_1C4;
                    case 16:
                        if (num2 >= count)
                        {
                            num = 23;
                            continue;
                        }
                        dBFieldItem = dbFields[num2];
                        stringBuilder2.Append(dBFieldItem.Name);
                        num = 11;
                        continue;
                    case 17:
                        goto IL_396;
                    case 18:
                        stringBuilder2.Append(" columns");
                        num = 31;
                        continue;
                    case 19:
                        num = 6;
                        continue;
                    case 20:
                        goto IL_1C4;
                    case 21:
                        goto IL_1C4;
                    case 22:
                        {
                            if (string.IsNullOrEmpty(dBFieldItem.Value.ToString()))
                            {
                                num = 25;
                                continue;
                            }
                            EnumDBFieldType fieldType = dBFieldItem.FieldType;
                            num = 7;
                            continue;
                        }
                    case 23:
                        num = 9;
                        continue;
                    case 24:
                        if (true)
                        {
                        }
                        goto IL_1C4;
                    case 25:
                        goto IL_379;
                    case 26:
                        goto IL_2BE;
                    case 27:
                        {
                            if (dbFields.Count == 0)
                            {
                                num = 10;
                                continue;
                            }
                            stringBuilder2 = new StringBuilder();
                            stringBuilder = new StringBuilder();
                            stringBuilder2.Append(" insert into ");
                            stringBuilder2.Append(tableName);
                            DBHelper.enumDBType dBType = iDBH.DBType;
                            num = 28;
                            continue;
                        }
                    case 28:
                        {
                            DBHelper.enumDBType dBType = iDBH.DBType;
                            if (dBType == DBHelper.enumDBType.DB_Oracle)
                            {
                                num = 18;
                                continue;
                            }
                            goto IL_110;
                        }
                    case 29:
                        if (stringBuilder.ToString().EndsWith(","))
                        {
                            num = 3;
                            continue;
                        }
                        goto IL_456;
                    case 31:
                        goto IL_110;
                    case 32:
                        if (num2 != count - 1)
                        {
                            num = 4;
                            continue;
                        }
                        goto IL_43F;
                }
                if (dbFields != null)
                {
                    num = 0;
                    continue;
                }
                goto IL_439;
            IL_110:
                stringBuilder2.Append(" ( ");
                stringBuilder.Append(" values ( ");
                count = dbFields.Count;
                dBFieldItem = null;
                num2 = 0;
                num = 2;
                continue;
            IL_1C4:
                num = 32;
                continue;
            IL_1EB:
                num = 29;
                continue;
            IL_379:
                stringBuilder.Append("NULL");
                num = 15;
                continue;
            IL_396:
                num = 16;
                continue;
            IL_43F:
                num2++;
                num = 17;
            }
        IL_2BE:
            goto IL_456;
        IL_328:
        IL_439:
            return string.Empty;
        IL_456:
            stringBuilder2.Append(" ) ");
            stringBuilder.Append(" ) ");
            stringBuilder2.Append(stringBuilder.ToString());
            return stringBuilder2.ToString();


        }
        public static string GetInsertSQL(IDBHelper db, string tableName, params DBFieldItem[] dbFields)
        {
            int num = 1;
            while (true)
            {
                switch (num)
                {
                    case 0:
                        num = 3;
                        continue;
                    case 2:
                        goto IL_50;
                    case 3:
                        if (true)
                        {
                        }
                        if (dbFields.Length == 0)
                        {
                            num = 2;
                            continue;
                        }
                        goto IL_52;
                }
                if (dbFields == null)
                {
                    break;
                }
                num = 0;
            }
        IL_2D:
            return string.Empty;
        IL_50:
            goto IL_2D;
        IL_52:
            return SQLStringUtility.GetInsertSQL(tableName, new List<DBFieldItem>(dbFields), db);
        }



    }


}
