using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

namespace ZYSK.DZPT.UI.Utility
{
    public sealed class AddAnnotation : BaseCommand
    {
        private IMapControl3 m_mapControl;
        private IHookHelper m_HookHelper = new HookHelperClass();
        private ILayer player;
        private IGeoFeatureLayer pGeoLayer;

        public AddAnnotation()
        {
            base.m_category = "标注"; //localizable text
            base.m_caption = "标注";  //localizable text
            base.m_message = "标注";  //localizable text 
            base.m_toolTip = "标注";  //localizable text 
            base.m_name = "标注";   //unique id, non-localizable (e.g. "MyCategory_MyCommand")
        }

        public override void OnCreate(object hook)
        {
            m_mapControl = (IMapControl3)hook;
            player = (ILayer)m_mapControl.CustomProperty;//实例化选中图层
            pGeoLayer = player as IGeoFeatureLayer;//IGeoFeatureLayer比IFeatureLayer多了标注
            if (pGeoLayer.DisplayAnnotation == true)
            {
                this.m_checked = true;
            }
            else
            {
                this.m_checked = false;
            }
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            player = (ILayer)m_mapControl.CustomProperty;//实例化选中图层

            if (this.m_checked == true)
            {
                RemoveAnnotations(player);
            }
            else
            {
                AddAnnotations(player, player.Name);
            }
        }

        public void AddAnnotations(ILayer layer, string fieldName)
        {
            pGeoLayer.DisplayAnnotation = true;
            (m_mapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_mapControl.Extent);
        }

        public void RemoveAnnotations(ILayer layer)
        {
            //IAnnotateLayerPropertiesCollection IPALPColl = pGeoLayer.AnnotationProperties;
            //IPALPColl.Clear(); //清除所有标注
            pGeoLayer.DisplayAnnotation = false;
            (m_mapControl.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, m_mapControl.Extent);
        }
    }
}
