using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ZYSK.DZPT.Base.Utility;
using ZYSK.DZPT.Query.Class;

/// <summary>
/// 描述：钻孔SDE空间数据表操作类
/// 作用：封装与钻孔要素类相关的操作
/// 时间：mym-2018-11-09
/// </summary>
namespace ZYSK.DZPT.Query.DAL
{
    public class SdeZKPointsDAL
    {
        #region 常量
  
        #endregion

        #region 字段

        #endregion


        #region 增删改查

       

        #endregion

        #region public static

        /// <summary>
        /// 获取“工程区域”要素类的编辑字段
        /// </summary>
        /// <param name="pSpatialReference"></param>
        /// <returns></returns>
        public static IFieldsEdit GetZKFeatureClassFields(ISpatialReference currentSpatialReference)
        {
            IFields pFields = new FieldsClass();
            IFieldsEdit pFieldsEdit;
            pFieldsEdit = (IFieldsEdit)pFields;
            try
            {
                IField pField = new FieldClass();
                IFieldEdit pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "Shape";
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry;
                IGeometryDef pGeometryDef = new GeometryDefClass();
                IGeometryDefEdit PgDefEdit = (IGeometryDefEdit)pGeometryDef;
                PgDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPoint;
                PgDefEdit.SpatialReference_2 = currentSpatialReference;
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_NO;
                //pFieldEdit.Length_2 = 10;
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                //pFieldEdit.DefaultValue_2 = 1;
                //pFieldEdit.AliasName_2 = "钻孔序号";
                //pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_PROJECTNO;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "工程编号";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_ZKBH;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "钻孔编号";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_X;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.AliasName_2 = "X坐标";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_Y;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.AliasName_2 = "Y坐标";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_H;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.AliasName_2 = "钻孔标高";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_ZKSD;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                pFieldEdit.AliasName_2 = "钻孔深度";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeZKPoints.FEILD_F_ZKZJ;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeSmallInteger;
                pFieldEdit.AliasName_2 = "钻孔直径";
                pFieldsEdit.AddField(pField);
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
            }
            return pFieldsEdit;
        }

        #endregion

    }
}
