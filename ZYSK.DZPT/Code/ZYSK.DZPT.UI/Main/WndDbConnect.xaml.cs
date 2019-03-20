using System.Windows;
using ZYSK.DZPT.Base.Utility.Class;
using ZYSK.DZPT.Base.DbBase.Oracle;
using ZYSK.DZPT.Base.DbBase;
using ZYSK.DZPT.Base.GeoDB;
using DevExpress.Xpf.Core;
using System.Windows.Media;

namespace ZYSK.DZPT.UI.Main
{
    /// <summary>
    /// Interaction logic for WndDbConnect.xaml
    /// </summary>
    public partial class WndDbConnect : DXWindow
    {
        #region 字段
        private IDBHelper _db = null;
        private SdeDataSource _sdeDS;
        private string _strConnResult = "";
        private bool _blConnState = false;
        
        #endregion

        #region 属性

        public ESRI.ArcGIS.Geodatabase.IWorkspace BizEsriWS { get; set; }

        public SdeDataSource SdeDS
        {
            get { return _sdeDS; }
        }

        public bool ConnState
        {
            get
            {
                return this._blConnState;
            }
        }
        #endregion

        #region 初始化

        public WndDbConnect()
        {
            InitializeComponent();
            InitDefaultParas();
            this.WindowStartupLocation= WindowStartupLocation.CenterScreen;
        }

        //初始化数据库连接默认参数
        public void InitDefaultParas()
        {
            this.cmbDbPlatform.SelectedIndex = 0;
            this.txtHostIP.Text= "10.17.5.86";
            this.txtDbInstance.Text = "sde:oracle11g:smgeOracleDB@10.17.5.86";//sde:oracle11g:smgeOracleDB@10.17.5.86
            this.txtDbName.Text = "dzpttest";
            this.txtDbUserName.Text = "dzpttest";
            this.txtDbPassword.Text = "dzpttest";
        }



        #endregion

        #region 响应事件

        //测试数据库参数是否有效
        private void btnConnTest_Click(object sender, RoutedEventArgs e)
        {
            string strIP = this.txtHostIP.Text.Trim();
            string strInstance = this.txtDbInstance.Text.Trim();
            string strDbName = this.txtDbName.Text.Trim();
            string strUserName = this.txtDbUserName.Text.Trim();
            string strPwd = this.txtDbPassword.Text.Trim();
            string strVersion = "sde.DEFAULT";

          
            _sdeDS = new SdeDataSource(strIP, strUserName, strPwd, strInstance, strDbName, strVersion, "");
   
            if (_sdeDS.TestConnection())
            { 
                this.txtTestResult.Foreground = new SolidColorBrush(Colors.Green);
                if (strInstance.Contains("sde:oracle"))
                {
                    _db = new OracleDBHelper();
                    _db.DBServer = strIP;
                    _db.DBServiceName = strInstance.Substring(strInstance.LastIndexOf(':')+1);
                    _db.DBName = strDbName;
                    _db.DBUser = strUserName;
                    _db.DBPwd = strPwd;
                    if (_db.TryConnect())
                    {
                        this._blConnState = true;
                        _strConnResult = "连接成功！";
                    }

                 
                }
            }
            else
            {
                _strConnResult = "连接失败！";
                this.txtTestResult.Foreground = new SolidColorBrush(Colors.Red);
                this._blConnState = false;
            }
            this.txtTestResult.Text = _strConnResult;
        }

        //如果有效，保存参数
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_db != null)
            {
                if (_sdeDS.TestConnection())
                {
                    BizEsriWS = _sdeDS.Workspace;
                    SysParams.BizEsriWS = _sdeDS.Workspace;
                    SysParams.GlobalDBHelper = _db;
                    this.Close();
                }
                else
                {
                    if (MessageBox.Show("参数无效，是否继续设置？", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                if (MessageBox.Show("参数无效，是否继续设置？", "", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    this.Close();
                }
            }
            this.txtTestResult.Text = _strConnResult;
        }
        #endregion


    }
}
