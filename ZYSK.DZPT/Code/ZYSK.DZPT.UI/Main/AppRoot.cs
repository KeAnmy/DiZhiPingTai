using ESRI.ArcGIS;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZYSK.DZPT.Base.DbBase;
using ZYSK.DZPT.Base.GeoDB;
using ZYSK.DZPT.Base.Utility;
using ZYSK.DZPT.Base.Utility.Class;
using ZYSK.DZPT.Query.DAL;

namespace ZYSK.DZPT.UI.Main
{
    public class AppRoot
    {
        #region private static 字段

        //系统主窗体
        private static WindowMain _wndMian = null;
        private static IDBHelper _db = null;
        private static IWorkspace _bizEsriWS = null;


        #endregion

        #region public static 字段

        //全部局数据库连接
        public static IDBHelper DB
        {
            get
            {
                return _db;
            }
            set
            {

            }
        }
        //全局工作空间
        public static IWorkspace BizEsriWS
        {
            get
            {
                return _bizEsriWS;
            }
            set
            {
            }
        }

        #endregion

        #region public static 属性

        #endregion

        #region public static 方法

        /// <summary>
        /// 初始化系统基本设置
        /// </summary>
        public static void Init1_Base()
        {
            //初始化系统界面主题
            InitializeAppTheme();

        }

        /// <summary>
        /// 验证 是否安装了　ArcEngine ，装的 ArcEngine　是否有许可！
        /// </summary>
        /// <returns></returns>
        public static bool Init2_GIS()
        {
            if (RuntimeManager.Bind(ProductCode.EngineOrDesktop))
            {
                try
                {
                    RuntimeManager.BindLicense(ProductCode.EngineOrDesktop, LicenseLevel.GeodatabaseUpdate);
                    return true;
                }
                catch (Exception e)
                {
                    LogManager.WriteToError(e.Message);
                    MessageBox.Show("需要的ArcGIS组件未许可，程序不能运行");
                    return false;
                }
            }
            return false;

        }

        /// <summary>
        /// 初始化关系型数据库
        /// </summary>
        /// <returns></returns>
        public static bool Init3_DB()
        {
            bool blResult = false;
            WndDbConnect wndConn = new WndDbConnect();
            wndConn.ShowDialog();
            if (wndConn.ConnState)
            {
               _db = SysParams.GlobalDBHelper;
               _bizEsriWS= SysParams.BizEsriWS;
                blResult = true;
            }
            else
            {
                MessageBox.Show("连接数据库失败");
            }
            return blResult;
        }


        /// <summary>
        /// 初始化SDE空间数据库(表)
        /// </summary>
        /// <returns></returns>
        public static bool Init3_GeoDB()
        {
            try
            {
                /*
                if (!_db.IsTableExist("SdeGCPolygons")) //如果SdeGCPolygons表不存在则创建
                {
                    IFieldsEdit flds = SdeGCPolygonsDAL.GetGCFeatureClassFields(EsriUtility.CreateCGCS2000SpatialReference(4490));//GCS_China_Geodetic_Coordinate_System_2000（天地图坐标系,WKID=4490）
                    EsriUtility.CreateFeatureClass("SdeGCPolygons", flds, (IFeatureWorkspace)AppRoot.BizEsriWS);
                }
                if (!_db.IsTableExist("SdeZKPoints")) //如果SdeZKPoints表不存在则创建
                {
                    IFieldsEdit flds = SdeZKPointsDAL.GetZKFeatureClassFields(EsriUtility.CreateCGCS2000SpatialReference(4490));//GCS_China_Geodetic_Coordinate_System_2000（天地图坐标系,WKID=4490）
                    EsriUtility.CreateFeatureClass("SdeZKPoints", flds, (IFeatureWorkspace)AppRoot.BizEsriWS);

                }
                 */
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                MessageBox.Show("SDE空间数据库创建失败，请先安装ArcSDE！");
                return false;
            }

        }

        public static bool Init3_DbPlugins(IDBHelper db)
        {
            return true;
        }

        public static bool Init4_VerifySubSysMark()
        {
            return true;
        }

        public static bool Init5_InitSysParas()
        {
            return true;
        }
		
        public static Window GetWinowMain()
        {
            LogManager.WriteToDebug("开始获取主窗体......");
            _wndMian = new WindowMain(AppRoot.DB, AppRoot.BizEsriWS);
            return _wndMian;
        }
        #endregion

        #region private static 方法

        /// <summary>
        /// 初始化设计界面时设置的应用程序主题
        /// </summary>
        private static void InitializeAppTheme()
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }

        #endregion
    }
}
