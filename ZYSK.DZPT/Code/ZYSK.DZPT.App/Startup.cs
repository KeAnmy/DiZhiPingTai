using ESRI.ArcGIS.esriSystem;
using System;
using System.Windows;
using ZYSK.DZPT.UI.Main;

namespace ZYSK.DZPT.App
{
    public class Startup
    {
        [STAThread]
        static void Main()
        {
            AppRoot.Init1_Base();
            AppRoot.Init2_GIS();
            if (!AppRoot.Init3_DB())
            {
                return;
            }
            if (!AppRoot.Init3_GeoDB())
            {
                return;
            }
            Window _mWnd = AppRoot.GetWinowMain();
            Application app = new Application();
            app.Run(_mWnd);
        }

    }
}
