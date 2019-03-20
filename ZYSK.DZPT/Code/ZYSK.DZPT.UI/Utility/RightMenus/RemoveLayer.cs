using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;


namespace ZYSK.DZPT.UI.Utility
{
    /// <summary>
    /// 删除图层
    /// </summary>
    class RemoveLayer : BaseCommand
    {
         private IMapControl3 m_mapControl;
        public RemoveLayer()
        {
            base.m_caption = "删除图层";
        }
        public override void OnClick()
        {
            ILayer layer = (ILayer)m_mapControl.CustomProperty;
            m_mapControl.Map.DeleteLayer(layer);
        }
        public override void OnCreate(object hook)

        {
            m_mapControl = (IMapControl3)hook;
        }
    }
}
