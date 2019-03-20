using ESRI.ArcGIS.esriSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ZYSK.DZPT.UI
{
    class App
    {
        /*
         
            [STAThread]
        static void Main()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeEngineLicense();
            
            //更新设计界面时设置的应用程序主题
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
            Application app = new Application();           
            MainWindow mainWindow = new MainWindow();
            app.Run(mainWindow);

        }

        /// <summary>
        /// 初始化Esri的license控件
        /// </summary>
        private static void InitializeEngineLicense()
        {
            AoInitialize aoi = new AoInitializeClass();
            esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeEngine;
            if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
            {
                aoi.Initialize(productCode);
            }
        }  
             
             */

    }
}
