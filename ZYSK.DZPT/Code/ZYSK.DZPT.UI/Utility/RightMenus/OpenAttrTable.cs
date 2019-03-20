using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.UI.Utility
{
    public class OpenAttrTable : BaseCommand
    {
        private IMapControl3 pMapControl;
        private ILayer m_pLayer;

        public OpenAttrTable()
        {
            base.m_caption = "打开属性表";
        }
        public override void OnClick()
        {
            openattribute();
        }
        public override void OnCreate(object hook)
        {
            pMapControl = (IMapControl3)hook;
            m_pLayer = pMapControl.CustomProperty as ILayer;
        }
        private void openattribute()
        {
            AttributeTableFrm attributeTable = new AttributeTableFrm();
            attributeTable.CreateAttributeTable(m_pLayer);
            attributeTable.ShowDialog();
        }
    }

}

