using ESRI.ArcGIS.Geodatabase;
using ZYSK.DZPT.Base.DbBase;

namespace ZYSK.DZPT.Base.Utility.Class
{
  public class SysParams
    {
        //全局数据库帮助对象
        public static IDBHelper GlobalDBHelper
        {
            get;
            set;
        }

        public static IWorkspace BizEsriWS
        {
            get;
            set;
        }
    }
}
