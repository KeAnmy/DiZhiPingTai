using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ZYSK.DZPT.Base.Utility;

namespace ZYSK.DZPT.UI.Utility
{
    /// <summary>
    /// Command that works in ArcMap/Map/PageLayout
    /// </summary>
    public sealed class RemoveAll : BaseCommand
    {

        private IHookHelper m_hookHelper = null;
        private IMapControl3 m_mapControl;
        private IHookHelper m_HookHelper = new HookHelperClass();
        public RemoveAll()
        {
            //
            // TODO: Define values for the public properties
            // 
            base.m_category = "移除所有图层"; //localizable text
            base.m_caption = "移除所有图层";  //localizable text
            base.m_message = "This should work in ArcGlobe or GlobeControl";  //localizable text 
            base.m_toolTip = "移除所有图层";  //localizable text
            base.m_name = "移除所有图层";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
        }
        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                if (m_hookHelper.ActiveView == null)
                    m_hookHelper = null;
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;


            m_mapControl = (IMapControl3)hook;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            for (int i = 2; i < ((this.m_mapControl.LayerCount)); i++)
            {
                //ILayer layer = (ILayer)m_mapControl.get_Layer(0);
                i = i - 1;
                ILayer layer = (ILayer)m_mapControl.get_Layer(i - 1);
                m_mapControl.Map.DeleteLayer(layer);
                //m_mapControl.Refresh();
            }
            try
            {
                //mym-2018-10-30，此处有问题：没有图层时，进行移除，有报错
                ILayer player = (ILayer)m_mapControl.get_Layer(0);
                m_mapControl.Map.DeleteLayer(player);
                player = (ILayer)m_mapControl.get_Layer(0);
                m_mapControl.Map.DeleteLayer(player);
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
            }


        }

        #endregion
    }
}
