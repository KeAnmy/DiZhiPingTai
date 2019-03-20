using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZYSK.DZPT.Base.DbBase;

namespace ZYSK.DZPT.Base.Utility.Class
{
   public  class GeoDB
    {
        public static bool TryConncet(IDBHelper db)
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
                IFeatureWorkspace pSW = (IFeatureWorkspace)pWorkspaceFactory.Open(pPropertySetConnect, 0);
                return true;
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                return false;
            }        
        }
    }
}
