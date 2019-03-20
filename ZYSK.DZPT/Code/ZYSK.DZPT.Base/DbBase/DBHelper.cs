using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZYSK.DZPT.Base.DbBase.MySQL;
using ZYSK.DZPT.Base.DbBase.Oracle;
using ZYSK.DZPT.Base.Public;
using ZYSK.DZPT.Base.Utility;

namespace ZYSK.DZPT.Base.DbBase
{
    public abstract class DBHelper : IDBHelper, IDisposable
    {
        #region 字段
        private string _dbServiceName = "";
        private string _dbSid = "";
        private string _dbServer = "";
        private string _dbService = "";
        private string _dbPath = "";
        private string _dbName = "";
        private string _dbPort = "";
        private string _dnUser = "";
        private string _dbPwd = "";
        private string _connStr = "";
        private string _dDTekLicensePath = "";
        private IDbConnection _dbConn = null;
        private IDbCommand _dbCmd = null;
        private IDbDataAdapter _dbDAdapter = null;
        private IDataReader _dbDReader = null;
        private DataSet _dataSet = null;
        private IDictionary<string, DataTable> _dic = new Dictionary<string, DataTable>();
        private DBHelper.enumDBType _dbType = DBHelper.enumDBType.DB_Oracle; //默认数据库类型为Oracle数据库
        private DBHelper.enumDBDriver _dbDrive = DBHelper.enumDBDriver.ADO;

        private int m = 30;
        private bool k = true;
        private string t;
        private string g = "Key";
        private bool l;

        #endregion

        #region 属性

        public string DBServiceName
        {
            get
            {
                return _dbServiceName;
            }
            set
            {
                _dbServiceName = value;
            }
        }


        public string DBSid
        {
            get
            {
                return _dbSid;
            }
            set
            {
                _dbSid = value;
            }
        }

        public string DBServer
        {
            get
            {
                return this._dbServer;
            }
            set
            {
                this._dbServer = value;
            }
        }

        public string DBPath
        {
            get
            {
                return this._dbPath;
            }
            set
            {
                this._dbPath = value;
            }
        }

        public string DBName
        {
            get
            {
                return this._dbName;
            }
            set
            {
                this._dbName = value;
            }
        }

        public string DBPort
        {
            get
            {
                return this._dbPort;
            }
            set
            {
                this._dbPort = value;
            }
        }

        public string DBUser
        {
            get
            {
                return this._dnUser;
            }
            set
            {
                this._dnUser = value;
            }
        }

        public string DBPwd
        {
            get
            {
                return this._dbPwd;
            }
            set
            {
                this._dbPwd = value;
            }
        }

        public string DBConnStr
        {
            get
            {
                return this._connStr;
            }
            set
            {
                this._connStr = value;
            }
        }

        public IDbConnection ActiveConnection
        {
            get
            {
                return this._dbConn;
            }
            set
            {
                this._dbConn = value;
            }
        }

        public IDbCommand ActiveCommand
        {
            get
            {
                return this._dbCmd;
            }
            set
            {
                this._dbCmd = value;
            }
        }

        public DBHelper.enumDBType DBType
        {
            get
            {
                return this._dbType;
            }
            set
            {
                this._dbType = value;
            }
        }

        public DBHelper.enumDBDriver DBDriver
        {
            get
            {
                return this._dbDrive;
            }
            set
            {
                this._dbDrive = value;
            }
        }

        public DataSet ActiveDataSet
        {
            get
            {
                return this._dataSet;
            }
            set
            {
                this._dataSet = value;
            }
        }
        public string ParamaterPrefix
        {
            get;
            internal set;
        }
        public string DDTekLicensePath
        {
            get
            {
                return this._dDTekLicensePath;
            }
            set
            {
                this._dDTekLicensePath = value;
            }
        }

        public string ConnectString
        {
            get
            {
                return this.t;
            }
            set
            {
                this.t = value;
            }
        }

        public string Key
        {
            get
            {
                return this.g;
            }
            set
            {
                this.g = value;
            }
        }

        public bool CloseConnectionAfterAction
        {
            set
            {
                this.l = value;
            }
        }


        #endregion

        #region 枚举

        // 摘要:
        // 数据库驱动类型枚举常量
        public enum enumDBType
        {
            /// <summary>
            /// 不支持的数据库类型
            /// </summary>
            [Obsolete]
            NotSupport = -1,
            /// <summary>
            ///             Microsoft Access ADO
            /// </summary>
            DB_ACCESS = 1,
            /// <summary>
            /// Microsoft SQLServer ADO
            /// </summary>
            DB_SQLServer,
            /// <summary>
            /// Oracle 客户端 ADO
            /// </summary>
            DB_Oracle,
            /// <summary>
            /// Oracle DDTek
            /// </summary>
            DB_Oracle_Local = 30,
            /// <summary>
            /// SQLite
            /// </summary>
            DB_SQLite = 4,
            /// <summary>
            /// MySQL
            /// </summary>
            DB_MySQL,
            /// <summary>
            /// PostgreSQL
            /// </summary>
            DB_Postgre,
            DB_Beyon
        }

        /// <summary>
        /// DB Driver
        /// </summary>
        //[Obsolete]
        public enum enumDBDriver
        {
            //[Obsolete]
            ADO = 1,
            //[Obsolete("不要使用该值，使用DBHelper.enumDBType.DB_Oracle_Local替代")]
            DDTek
        }

        #endregion

        #region 初始化

        internal DBHelper()
        {
            this.ParamaterPrefix = "@";
            this.ActiveConnection = this.CreateDbConnection();
            this.ActiveCommand = this.CreateDbCommand();
            this._dbDAdapter = this.CreateDataAdapter();
            this._dataSet = new DataSet();
        }
        #endregion

        #region 公有方法

        //判断数据库连接是否打开
        public bool IsConnected()
        {
            if (this.ActiveConnection != null)
            {
                return this.ActiveConnection.State == ConnectionState.Open;
            }
            return false;
        }

        //由连接字符串初始化数据库帮助类
        public DataTable DoQueryEx(string queryString)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = this.DoQueryEx("QueryResult", queryString, true);
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
            }
            return dt;
        }

        //由连接字符串初始化数据库帮助类
        public DataTable DoQueryEx(string table, string sql, bool release)
        {
            while (true)
            {
                table = table.ToUpper();
                DataTable dataTable = new DataTable();
                if (true)
                {
                }
                int num = 1;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            {
                                if (!release)
                                {
                                    num = 4;
                                    continue;
                                }
                                return dataTable;
                            }
                        case 1:
                            {
                                if (this._dic.ContainsKey(table))
                                {
                                    num = 3;
                                    continue;
                                }
                                dataTable = this.DoQueryEx(sql, null);
                                num = 0;
                                continue;
                            }
                        case 2:
                            {
                                return dataTable;
                            }
                        case 3:
                            goto IL_4A;
                        case 4:
                            {
                                dataTable.TableName = table;
                                this._dic.Add(table, dataTable);
                                num = 2;
                                continue;
                            }
                    }
                    break;
                }
            }
        IL_4A:
            return this._dic[table];
        }

        //由连接字符串初始化数据库帮助类
        public DataTable DoQueryEx(string sql, params IDbDataParameter[] parameters)
        {
            DataTable result = new DataTable();
            lock (this)
            {
                while (true)
                {
                    DataTable dataTable = null;
                    IEnumerator enumerator = null;
                    int num = 5;
                    while (true)
                    {
                        int num2 = 0;
                        IDbDataParameter dbDataParameter = null;
                        switch (num)
                        {
                            case 0:
                                goto IL_1F5;
                            case 1:
                                goto IL_1CC;
                            case 2:
                                num2 = 0;
                                num = 12;
                                continue;
                            case 3:
                                if (true)
                                {
                                }
                                num = 7;
                                continue;
                                goto IL_1B5;
                            case 4:
                                dbDataParameter.ParameterName = this.ParamaterPrefix + dbDataParameter.ParameterName;
                                num = 1;
                                continue;
                            case 5:
                                if (this.ToConnect())
                                {
                                    num = 16;
                                    continue;
                                }
                                goto IL_1F5;
                            case 6:
                                try
                                {
                                    num = 4;
                                    while (true)
                                    {
                                        switch (num)
                                        {
                                            case 0:
                                                {
                                                    if (!enumerator.MoveNext())
                                                    {
                                                        num = 3;
                                                        continue;
                                                    }
                                                    DataColumn dataColumn = (DataColumn)enumerator.Current;
                                                    dataColumn.ColumnName = dataColumn.ColumnName.ToUpper();
                                                    num = 1;
                                                    continue;
                                                }
                                            case 2:
                                                goto IL_167;
                                            case 3:
                                                num = 2;
                                                continue;
                                        }
                                    IL_141:
                                        num = 0;
                                        continue;
                                        goto IL_141;
                                    }
                                IL_167:
                                    goto IL_387;
                                }
                                finally
                                {
                                    while (true)
                                    {
                                        IDisposable disposable = enumerator as IDisposable;
                                        num = 2;
                                        while (true)
                                        {
                                            switch (num)
                                            {
                                                case 0:
                                                    goto IL_1B2;
                                                case 1:
                                                    disposable.Dispose();
                                                    num = 0;
                                                    continue;
                                                case 2:
                                                    if (disposable != null)
                                                    {
                                                        num = 1;
                                                        continue;
                                                    }
                                                    goto IL_1B4;
                                            }
                                            break;
                                        }
                                    }
                                IL_1B2:
                                IL_1B4:;
                                }
                                goto IL_1B5;
                            case 7:
                                //LogHelper.get_Debug().Append(sql);
                                LogManager.WriteToDebug(sql);
                                num = 17;
                                continue;
                            case 8:
                                if (num2 >= parameters.Length)
                                {
                                    num = 11;
                                    continue;
                                }
                                dbDataParameter = parameters[num2];
                                num = 13;
                                continue;
                            case 9:
                                if (parameters != null)
                                {
                                    num = 2;
                                    continue;
                                }
                                goto IL_218;
                            case 10:
                                {
                                    enumerator = dataTable.Columns.GetEnumerator();
                                    num = 6;
                                    continue;
                                }
                            case 11:
                                goto IL_218;
                            case 12:
                                goto IL_2E8;
                            case 13:
                                if (!dbDataParameter.ParameterName.StartsWith(this.ParamaterPrefix))
                                {
                                    num = 4;
                                    continue;
                                }
                                goto IL_1CC;
                            case 14:
                                goto IL_395;
                            case 15:
                                goto IL_2E8;
                            case 16:
                                this.ActiveDataSet = new DataSet();
                                this.ActiveCommand.CommandType = CommandType.Text;
                                this.ActiveCommand.CommandText = sql;
                                this.ActiveCommand.Parameters.Clear();
                                num = 9;
                                continue;
                            case 17:
                                goto IL_1B5;
                            case 18:
                                if (dataTable != null)
                                {
                                    num = 10;
                                    continue;
                                }
                                goto IL_387;
                            case 19:
                                try
                                {
                                    this.BeforeExecute();
                                    (this._dbDAdapter as DbDataAdapter).Fill(this.ActiveDataSet, "QueryResult");
                                    goto IL_329;
                                }
                                catch (Exception ex)
                                {
                                    LogManager.WriteToError(ex.Message);
                                    throw;
                                }
                                goto IL_387;
                            IL_329:
                                dataTable = this.ActiveDataSet.Tables["QueryResult"];
                                this.ActiveDataSet.Tables.Clear();
                                num = 0;
                                continue;
                        }
                        break;
                    IL_1B5:
                        this.CloseReader();
                        num = 19;
                        continue;
                    IL_1CC:
                        this.ActiveCommand.Parameters.Add(dbDataParameter);
                        num2++;
                        num = 15;
                        continue;
                    IL_1F5:
                        num = 18;
                        continue;
                    IL_218:
                        this._dbDAdapter.SelectCommand = this.ActiveCommand;
                        num = 3;
                        continue;
                    IL_2E8:
                        num = 8;
                        continue;
                    IL_387:
                        result = dataTable;
                        num = 14;
                    }
                }
            IL_395:;
            }
            return result;
        }

        //关闭Reader流
        public void CloseReader()
        {
            Monitor.Enter(this);
            try
            {
                int num = 1;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            if (!this._dbDReader.IsClosed)
                            {
                                num = 4;
                                continue;
                            }
                            goto IL_8A;
                        case 2:
                            num = 0;
                            continue;
                        case 3:
                            goto IL_92;
                        case 4:
                            this._dbDReader.Close();
                            this._dbDReader.Dispose();
                            this._dbDReader = null;
                            num = 5;
                            continue;
                        case 5:
                            goto IL_8A;
                    }
                    if (this._dbDReader != null)
                    {
                        num = 2;
                        continue;
                    }
                IL_8A:
                    num = 3;
                }
            IL_92:;
            }
            finally
            {
                if (true)
                {
                }
                Monitor.Exit(this);
            }
        }

        public void Dispose()
        {
            if (true)
            {
            }
            this.DisConnect();
            this.ActiveCommand.Dispose();
            this.ActiveCommand = null;
            this.ActiveConnection.Dispose();
            this.ActiveConnection = null;
            (this._dbDAdapter as DbDataAdapter).Dispose();
            this._dbDAdapter = null;
        }

        public void DisConnect()
        {
            if (true)
            {
            }
            lock (this)
            {
                int num = 2;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            goto IL_59;
                        case 1:
                            this.ActiveConnection.Close();
                            num = 0;
                            continue;
                        case 3:
                            goto IL_61;
                    }
                    if (this.IsConnected())
                    {
                        num = 1;
                        continue;
                    }
                IL_59:
                    num = 3;
                }
            IL_61:;
            }
        }

        public bool TryConnect()
        {
            Monitor.Enter(this);
            bool result = false;
            try
            {
                int num = 1;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            goto IL_57;
                        case 2:
                            goto IL_46;
                        case 3:
                            result = true;
                            num = 2;
                            continue;
                    }
                    if (this.IsConnected())
                    {
                        num = 3;
                    }
                    else
                    {
                        result = this.connect();
                        num = 0;
                    }
                }
            IL_46:
            IL_57:;
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                result = false;
            }
            finally
            {
                if (true)
                {
                }
                Monitor.Exit(this);
            }
            return result;
        }

       

        public int CommandTimeout
        {
            get
            {
                return this.m;
            }
            set
            {
                int num = 1;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            if (this.ActiveCommand != null)
                            {
                                num = 4;
                                continue;
                            }
                            goto IL_6B;
                        case 2:
                            goto IL_4E;
                        case 3:
                            return;
                        case 4:
                            if (true)
                            {
                            }
                            this.ActiveCommand.CommandTimeout = value;
                            num = 2;
                            continue;
                    }
                    if (value < 0)
                    {
                        num = 3;
                    }
                    else
                    {
                        num = 0;
                    }
                }
                return;
            IL_4E:
            IL_6B:
                this.m = value;
            }
        }

        //判断表是否存在
        public bool IsTableExist(string tableName)
        {
            string sql = "SELECT COUNT(*) FROM " + tableName + " WHERE 1=2";
            bool result;
            try
            {
                if (true)
                {
                }
                this.QueryScalar(sql);
                result = true;
            }
            catch (Exception ex)
            {
                //LogManager.WriteToError(ex.Message);
                result = false;
            }
            return result;
        }

        public object QueryScalar(string sql)
        {
            if (true)
            {
            }
            int num = 0;
            while (true)
            {
                switch (num)
                {
                    case 1:
                        goto IL_110;
                    case 2:
                        try
                        {
                            try
                            {
                                object result=false;
                                while (true)
                                {
                                    this.ActiveCommand.CommandType = CommandType.Text;
                                    this.ActiveCommand.CommandText = sql;
                                    this.ActiveCommand.Parameters.Clear();
                                    num = 2;
                                    while (true)
                                    {
                                        switch (num)
                                        {
                                            case 0:
                                                goto IL_C8;
                                            case 1:
                                                goto IL_AB;
                                            case 2:
                                                if (true)
                                                {
                                                    num = 3;
                                                    continue;
                                                }
                                                goto IL_AB;
                                            case 3:
                                                LogManager.WriteToDebug(sql);
                                                num = 1;
                                                continue;
                                        }
                                        break;
                                    IL_AB:
                                        this.BeforeExecute();
                                        result = this.ActiveCommand.ExecuteScalar();
                                        num = 0;
                                    }
                                }
                            IL_C8:
                                return result;
                            }
                            finally
                            {
                                num = 1;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 0:
                                            goto IL_106;
                                        case 2:
                                            this.DisConnect();
                                            num = 0;
                                            continue;
                                    }
                                    if (!this.l)
                                    {
                                        break;
                                    }
                                    num = 2;
                                }
                            IL_106:;
                            }
                        }
                        finally
                        {
                            Monitor.Exit(this);
                        }
                        goto IL_110;
                }
                if (this.connect())
                {
                    num = 1;
                    continue;
                }
                break;
            IL_110:
                Monitor.Enter(this);
                num = 2;
            }
            return null;
        }

        public int UpdateRow(string tableName, object[][] para, string filter)
        {

                        List<DBFieldItem> list;
                        while (true)
                        {
                            list = new List<DBFieldItem>();
                            int num = 0;
                            if (true)
                            {
                            }
                            int num2 = 2;
                            while (true)
                            {
                                switch (num2)
                                {
                                    case 0:
                                        {
                                            if (num >= para.Length)
                                            {
                                                num2 = 3;
                                                continue;
                                            }
                                            object[] array = para[num];
                                            list.Add(array);
                                            num++;
                                            num2 = 1;
                                            continue;
                                        }
                                    case 1:
                                        goto IL_45;
                                    case 2:
                                        goto IL_45;
                                    case 3:
                                        goto IL_5E;
                                }
                                break;
                            IL_45:
                                num2 = 0;
                            }
                        }
                    IL_5E:
                        string updateSQL = SQLStringUtility.GetUpdateSQL(tableName, list, filter, this);
                        return this.DoSQL(updateSQL);
           
     
        }

        public int DoSQL(string sql)
        {
            int result=0;
            lock (this)
            {
                int num = 0;
                while (true)
                {
                    switch (num)
                    {
                        case 1:
                            try
                            {
                                while (true)
                                {
                                    this.ActiveCommand.CommandType = CommandType.Text;
                                    this.ActiveCommand.CommandText = sql;
                                    this.ActiveCommand.Parameters.Clear();
                                    num = 4;
                                    while (true)
                                    {
                                        int arg_10A_0;
                                        int num2=0;
                                        switch (num)
                                        {
                                            case 0:
                                                arg_10A_0 = 0;
                                                goto IL_10A;
                                            case 1:
                                                goto IL_D6;
                                            case 2:
                                                arg_10A_0 = num2;
                                                goto IL_10A;
                                            case 3:
                                                LogManager.WriteToDebug(sql);
                                                num = 1;
                                                continue;
                                            case 4:
                                                if (this.k)
                                                {
                                                    num = 3;
                                                    continue;
                                                }
                                                goto IL_D6;
                                            case 5:
                                                goto IL_116;
                                            case 6:
                                                num = 0;
                                                continue;
                                            case 7:
                                                if (num2 < 0)
                                                {
                                                    num = 6;
                                                    continue;
                                                }
                                                num = 2;
                                                continue;
                                        }
                                        break;
                                    IL_D6:
                                        this.CloseReader();
                                        this.BeforeExecute();
                                        num2 = this.ActiveCommand.ExecuteNonQuery();
                                        num = 7;
                                        continue;
                                    IL_10A:
                                        result = arg_10A_0;
                                        num = 5;
                                    }
                                }
                            IL_116:
                                goto IL_170;
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                num = 2;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 0:
                                            this.DisConnect();
                                            num = 1;
                                            continue;
                                        case 1:
                                            goto IL_157;
                                    }
                                    if (!this.l)
                                    {
                                        break;
                                    }
                                    num = 0;
                                }
                            IL_157:;
                            }
                            goto IL_15A;
                        case 2:
                            goto IL_167;
                    }
                    if (this.connect())
                    {
                        num = 1;
                        continue;
                    }
                IL_15A:
                    result = -1;
                    num = 2;
                }
            IL_167:;
            }
        IL_170:
            if (true)
            {
            }
            return result;
        }

        public virtual string GetDateString(DateTime dt)
        {
            return "'" + dt.ToString("yyyy-MM-dd") + "'";
        }

        public virtual string GetDateTimeString(DateTime dt)
        {
            return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
        }

        public string getEscapeString()
        {
            return " escape '\\' ";
        }

        public virtual string GetEscapedString(string input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            return input.Replace("'", "''");
        }


        #endregion

        #region 静态方法

        public static IDBHelper Create(DBHelper.enumDBType dbType, DBHelper.enumDBDriver driver)
        {
            int num = 1;
            while (true)
            {
                switch (num)
                {
                    case 0:
                        goto IL_3E;
                    case 2:
                        num = 3;
                        continue;
                    case 3:
                        if (driver == DBHelper.enumDBDriver.DDTek)
                        {
                            if (true)
                            {
                            }
                            num = 4;
                            continue;
                        }
                        goto IL_5E;
                    case 4:
                        dbType = DBHelper.enumDBType.DB_Oracle_Local;
                        num = 0;
                        continue;
                }
                if (dbType != DBHelper.enumDBType.DB_Oracle)
                {
                    break;
                }
                num = 2;
            }
        IL_3E:
        IL_5E:
            return DBHelper.Create(dbType);
        }

        public static IDBHelper Create(DBHelper.enumDBType dbType)
        {
            while (true)
            {
                int num = 2;
                while (true)
                {
                    switch (num)
                    {
                        case 0:
                            num = 3;
                            continue;
                        case 1:
                            num = 4;
                            continue;
                        case 2:
                            switch (dbType)
                            {
                                case DBHelper.enumDBType.DB_ACCESS:
                                    goto IL_A4;
                                case DBHelper.enumDBType.DB_SQLServer:
                                    goto IL_AA;
                                case DBHelper.enumDBType.DB_Oracle:
                                    goto IL_5A;
                                case DBHelper.enumDBType.DB_SQLite:
                                    goto IL_9E;
                                case DBHelper.enumDBType.DB_MySQL:
                                    goto IL_54;
                                case DBHelper.enumDBType.DB_Postgre:
                                    goto IL_6A;
                                case DBHelper.enumDBType.DB_Beyon:
                                    goto IL_90;
                                default:
                                    num = 1;
                                    continue;
                            }
                            break;
                        case 3:
                            goto IL_68;
                        case 4:
                            if (dbType != DBHelper.enumDBType.DB_Oracle_Local)
                            {
                                num = 0;
                                continue;
                            }
                            goto IL_8A;
                    }
                    break;
                }
            }
        IL_54:
            return new MySQLDBHelper();
        IL_5A:
            return new OracleDBHelper();
        IL_68:
            return null;
        IL_6A:
            //return new PostgreSQLDBHelper();
            return null;
        IL_8A:
            return new OracleDDTekDBHelper();
        IL_90:
            if (true)
            {
            }
            //return new BeyonSQLDBHelper();
            return null;
        IL_9E:
            // return new SQLiteDBHelper();
            return null;
        IL_A4:
            //return new AccessDBHelper();
            return null;
        IL_AA:
            // return new SQLServerDBHelper();
            return null;
        }

        #endregion

        #region 抽象方法

        protected abstract IDbConnection CreateDbConnection();

        protected abstract IDbCommand CreateDbCommand();

        protected abstract IDbDataAdapter CreateDataAdapter();

        protected abstract string GetConnectString();

        public abstract IDbDataParameter CreateDbParameter();

        public abstract string GetConnInfoString();

        public abstract DateTime getServerDate();

        public abstract string GetServerDateTimeString();
        #endregion

        #region 虚方法

        public virtual DBHelper.enumDBType ActuralDBType
        {
            get
            {
                return this.DBType;
            }
        }

        //执行Commad命令之前
        protected virtual void BeforeExecute()
        {

        }

        //进行数据库连接操作
        public virtual bool ToConnect()
        {
            bool result = false;
            if (!this.IsConnected())
            {
                try
                {
                    this.ActiveConnection.ConnectionString = this.GetConnectString();
                    this._connStr = this.ActiveConnection.ConnectionString;
                    this.ActiveConnection.Open();
                    //this.ActiveCommand.CommandTimeout = this.CommandTimeout;
                    this.ActiveCommand.Connection = this.ActiveConnection;
                    result = true;
                }
                catch (Exception ex)
                {
                    LogManager.WriteToError(ex.Message);
                    result = false;
                    throw ex;
                }
            }
            else
            {
                result = true;
            }
            return result;
        }

        public virtual bool connect()
        {
            if (true)
            {
            }
            bool result = false;
            lock (this)
            {
                int num = 0;
                if (!this.IsConnected())
                {
                    goto IL_4F;
                }
                else
                {
                    goto IL_F7;
                }
            IL_4F:
                try
                {
                    while (true)
                    {
                        this.ActiveConnection.ConnectionString = this.GetConnectString();
                        this.t = this.ActiveConnection.ConnectionString;
                        num = 0;
                        while (true)
                        {
                            switch (num)
                            {
                                case 0:
                                    if (this.k)
                                    {
                                        num = 3;
                                        continue;
                                    }
                                    goto IL_BD;
                                case 1:
                                    goto IL_F7;
                                case 2:
                                    goto IL_BD;
                                case 3:
                                    LogManager.WriteToDebug(this.GetConnInfoString());
                                    num = 2;
                                    continue;
                            }
                            break;
                        IL_BD:
                            this.ActiveConnection.Open();
                            this.ActiveCommand.CommandTimeout = this.CommandTimeout;
                            this.ActiveCommand.Connection = this.ActiveConnection;
                            result = true;
                            num = 1;
                        }
                    }
               
                }
                catch (Exception ex)
                {
                    LogManager.WriteToError(ex.Message);
                    throw ex;
                }

            IL_F7:
                result = true;
            }
            return result;
        }

        #endregion
    }
}
