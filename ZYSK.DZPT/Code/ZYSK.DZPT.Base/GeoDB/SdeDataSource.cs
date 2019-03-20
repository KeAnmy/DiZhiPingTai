using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYSK.DZPT.Base.DbBase;
using ZYSK.DZPT.Base.Utility;

namespace ZYSK.DZPT.Base.GeoDB
{
    /// <summary>
	/// SDE空间数据源连接
	/// </summary>
	[Serializable]
    public class SdeDataSource
    {
        private const string SDE_CONN = "_ArcSDE_Connection.sde";

        /// <summary>
        /// 系统SDE连接文件存储文件夹路径
        /// </summary>
        public const string SDE_CONN_FOLDER = "DataSource";

        private UnicodeEncoding _ue;

        private string _server;

        private string _user;

        private string _password;

        private string _instance;

        private string _version;

        private string _database;

        private string _sDBType;

        private IWorkspace _workspace;

        private static Dictionary<string, GwWorkspaceState> workSpaceStates = new Dictionary<string, GwWorkspaceState>();

        protected DBHelper.enumDBType dbType;

        private IDBHelper _db;

        /// <summary>
        /// 服务器
        /// </summary>
        public string Server
        {
            get
            {
                return this._server;
            }
            set
            {
                this._server = value;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string User
        {
            get
            {
                return this._user;
            }
            set
            {
                this._user = value;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        /// <summary>
        /// 实例名
        /// </summary>
        public string Instance
        {
            get
            {
                return this._instance;
            }
            set
            {
                this._instance = value;
            }
        }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }

        /// <summary>
        /// 数据库
        /// </summary>
        public string Database
        {
            get
            {
                return this._database;
            }
            set
            {
                this._database = value;
            }
        }

        public string SDBType
        {
            get
            {
                return this._sDBType;
            }
            set
            {
                this._sDBType = value;
            }
        }

        public DBHelper.enumDBType DBType
        {
            get
            {
                return this.dbType;
            }
            set
            {
                this.dbType = value;
            }
        }

        /// <summary>
        /// 数据源连接类型
        /// </summary>
        public  enumDataSourceType DataSourceType
        {
            get
            {
                return enumDataSourceType.SDE;
            }
        }

        /// <summary>
        /// ESRI工作空间
        /// </summary>
        public IWorkspace Workspace
        {
            get
            {
                if (this._workspace == null)
                {
                    this._workspace = this.a();
                }
                return this._workspace;
            }
        }

        /// <summary>
        /// 隐藏默认构造函数
        /// </summary>
        private SdeDataSource()
        {
            this._ue = new UnicodeEncoding();
        }

        /// <summary>
        /// 通过加密连接信息构造对象
        /// </summary>
        /// <param name="connectionProperties"></param>
        public SdeDataSource(string connectionProperties) : this()
        {
            this.SetConnectionProperties(connectionProperties);
        }

        /// <summary>
        /// 通过指定的连接信息构造对象
        /// </summary>
        /// <param name="sServer">服务器</param>
        /// <param name="sUser">用户</param>
        /// <param name="sPassword">密码</param>
        /// <param name="sInstance">实例</param>
        /// <param name="sDatabase">数据库（SqlServer时有用）</param>
        /// <param name="sVersion">版本</param>
        /// 柴源
        public SdeDataSource(string sServer, string sUser, string sPassword, string sInstance, string sDatabase, string sVersion, string sDBType) : this()
        {
            this.Server = sServer;
            this.User = sUser;
            this.Password = sPassword;
            this.Instance = sInstance;
            this.Database = sDatabase;
            this.Version = sVersion;
            this._sDBType = sDBType;
        }

        public SdeDataSource(IDBHelper db)
        {
            this._db = db;
        }


        /// <summary>
        /// 根据获取的连接参数和密码测试是否能连接数据库
        /// </summary>
        /// <returns></returns>
        public  bool TestConnection()
        {
            return this.a() != null;
        }

        private IWorkspace a()
        {
            switch (0)
            {
                case 0:
                    {
                    IL_0E:
                        if (true)
                        {
                        }
                        IWorkspace workspace = null;
                        try
                        {
                            while (true)
                            {
                                IWorkspaceFactory workspaceFactory = new SdeWorkspaceFactoryClass();
                                IPropertySet propertySet = new PropertySetClass();
                                propertySet.SetProperty("SERVER", this.Server);
                                propertySet.SetProperty("INSTANCE", this.Instance);
                                propertySet.SetProperty("DATABASE", this.Database);
                                propertySet.SetProperty("USER", this.User);
                                propertySet.SetProperty("PASSWORD", this.Password);
                                propertySet.SetProperty("VERSION", this.Version);
                                string connectionProperties = this.GetConnectionProperties();
                                int num = 5;
                                while (true)
                                {
                                    switch (num)
                                    {
                                        case 0:
                                            {
                                                GwWorkspaceState value = new GwWorkspaceState();
                                                SdeDataSource.workSpaceStates.Add(connectionProperties, value);
                                                num = 3;
                                                continue;
                                            }
                                        case 1:
                                            goto IL_164;
                                        case 2:
                                            goto IL_158;
                                        case 3:
                                            goto IL_132;
                                        case 4:
                                            if (workspace != null)
                                            {
                                                num = 6;
                                                continue;
                                            }
                                            goto IL_158;
                                        case 5:
                                            if (!SdeDataSource.workSpaceStates.ContainsKey(connectionProperties))
                                            {
                                                num = 0;
                                                continue;
                                            }
                                            goto IL_132;
                                        case 6:
                                            {
                                                GwWorkspaceState gwWorkspaceState = SdeDataSource.workSpaceStates[connectionProperties];
                                                gwWorkspaceState.ConnectedDateTime = DateTime.Now;
                                                gwWorkspaceState.IsConnected = true;
                                                num = 2;
                                                continue;
                                            }
                                    }
                                    break;
                                IL_132:
                                    workspace = workspaceFactory.Open(propertySet, 0);
                                    num = 4;
                                    continue;
                                IL_158:
                                    num = 1;
                                }
                            }
                        IL_164:;
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteToError(ex.Message);
                            if (ex.Message.IndexOf("Operation Failed") >= 0)
                            {
                                string connectionProperties2 = this.GetConnectionProperties();
                                GwWorkspaceState gwWorkspaceState2 = SdeDataSource.workSpaceStates[connectionProperties2];
                                gwWorkspaceState2.ConnectedDateTime = DateTime.Now;
                                gwWorkspaceState2.IsConnected = false;
                            }
                        }
                        this._workspace = workspace;
                        return workspace;
                    }
            }
      
        }

        /// <summary>
        /// 获取连接参数信息
        /// </summary>
        /// <returns></returns>
        /// 柴源
        public string GetConnectionProperties()
        {
            int num = 2;
            while (true)
            {
                switch (num)
                {
                    case 0:
                        goto IL_49;
                    case 1:
                        this.Password = "";
                        num = 0;
                        continue;
                }
                if (true)
                {
                }
                if (this.Password != null)
                {
                    break;
                }
                num = 1;
            }
        IL_49:
            string s = string.Format("{0}={1};{2}={3};{4}={5};{6}={7};{8}={9};{10}={11};{12}={13};{14}={15};{16}={17}", new object[]
            {
                "DBType",
                this.dbType,
                "DBDriver",
                DBHelper.enumDBDriver.DDTek,
                //DBHelper.enumDBDriver.ADO,
                "DBServer",
                this.Server.Trim(),
                "DBSid",
                this.Database.Trim(),
                "DBUser",
                this._user.Trim(),
                "DBPwd",
                this._password,
                "SDBType",
                0,
                "Instance",
                this.Instance.Trim(),
                "Version",
                this.Version.Trim()
            });
            return s;
        }

        public  void SetConnectionProperties(string s)
        {
            int num = 1;
            while (true)
            {
                Dictionary<string, string> dictionary=new Dictionary<string, string> ();
                switch (num)
                {
                    case 0:
                        if (true)
                        {
                        }
                        try
                        {
                            this._sDBType = dictionary["DBType"];
                            this.Database = dictionary["DBSid"];
                            this.Version = dictionary["Version"];
                            return;
                        }
                        catch
                        {
                            return;
                        }
                        goto IL_6E;
                    case 2:
                        return;
                }
                if (s == "")
                {
                    num = 2;
                    continue;
                }
            IL_6E:
                string a_ = s;
                dictionary = f(a_);
                this.Server = dictionary["DBServer"];
                this.Instance = dictionary["Instance"];
                this.User = dictionary["DBUser"];
                this.Password = dictionary["DBPwd"];
                num = 0;
            }
        }

        internal Dictionary<string, string> f(string A_0)
        {
            switch (0)
            {
                case 0:
                    {
                    IL_0E:
                        if (true)
                        {
                        }
                        Dictionary<string, string> dictionary = new Dictionary<string, string>();
                        try
                        {
                            while (true)
                            {
                                string[] array = A_0.Split(new char[]
                                {
                    ';'
                                });
                                string[] array2 = null;
                                string arg_65_0 = string.Empty;
                                int num = 0;
                                int num2 = 3;
                                while (true)
                                {
                                    switch (num2)
                                    {
                                        case 0:
                                            goto IL_AD;
                                        case 1:
                                            goto IL_105;
                                        case 2:
                                            dictionary.Add(array2[0], array2[1]);
                                            num2 = 6;
                                            continue;
                                        case 3:
                                            goto IL_AD;
                                        case 4:
                                            if (array2.GetLength(0) >= 2)
                                            {
                                                num2 = 2;
                                                continue;
                                            }
                                            goto IL_E7;
                                        case 5:
                                            num2 = 1;
                                            continue;
                                        case 6:
                                            goto IL_E7;
                                        case 7:
                                            if (num >= array.Length)
                                            {
                                                num2 = 5;
                                                continue;
                                            }
                                            array2 = array[num].Split(new char[]
                                            {
                            '='
                                            });
                                            num2 = 4;
                                            continue;
                                    }
                                    break;
                                IL_AD:
                                    num2 = 7;
                                    continue;
                                IL_E7:
                                    num++;
                                    num2 = 0;
                                }
                            }
                        IL_105:;
                        }
                        catch (Exception ex)
                        {
                            LogManager.WriteToError(ex.Message);
                        }
                        return dictionary;
                    }
            }
        }

        public bool TryConncet(IDBHelper db)
        {
            //连接sde
            IPropertySet ptemp = new PropertySetClass();
            PropertySet pPropertySetConnect = new PropertySetClass();

            pPropertySetConnect.SetProperty("SERVER", db.DBServer);
            pPropertySetConnect.SetProperty("INSTANCE", db.DBServiceName);
            pPropertySetConnect.SetProperty("DATABASE", db.DBName);
            pPropertySetConnect.SetProperty("USER", db.DBName);
            pPropertySetConnect.SetProperty("PASSWORD", db.DBPwd);
            pPropertySetConnect.SetProperty("VERSION", "sde.DEFAULT");

            try
            {
                IWorkspaceFactory pWorkspaceFactory = new SdeWorkspaceFactoryClass();
               this._workspace= pWorkspaceFactory.Open(pPropertySetConnect, 0);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                return false;
            }
        }

    }

    /// <summary>
	/// 工作空间状态
	/// </summary>
	public class GwWorkspaceState
    {
        public const long intervalTime = 600000000L;

        private bool a = true;

        private DateTime b;

        public bool IsConnected
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = value;
            }
        }

        public DateTime ConnectedDateTime
        {
            get
            {
                return this.b;
            }
            set
            {
                this.b = value;
            }
        }
    }


    /// <summary>
	/// 空间数据源类型
	/// </summary>
	public enum enumDataSourceType
    {
        NotSupport = -1,
        /// <summary>
        /// 一般为SDE
        /// </summary>
        [Description("SDE")]
        SDE,
        /// <summary>
        ///
        /// </summary>
        [Description("PersonalGDB")]
        PersonalGDB,
        /// <summary>
        ///
        /// </summary>
        [Description("FileGDB")]
        FileGDB,
        /// <summary>
        /// 瓦片数据库
        /// </summary>
        [Description("TileDB")]
        TileDB = 11,
        /// <summary>
        /// 瓦片文件
        /// </summary>
        [Description("TileFile")]
        TileFile,
        /// <summary>
        /// Sqlite文件
        /// </summary>
        [Description("TileSqlite")]
        TileSqlite,
        [Description("OracleSpatial")]
        OracleSpatial = 3,
        [Description("WMS")]
        WMS,
        [Description("Shapefile")]
        Shapefile = 6,
        [Description("Coverage")]
        Coverage,
        [Description("ArcGISServer")]
        ArcGISServer
    }
}
