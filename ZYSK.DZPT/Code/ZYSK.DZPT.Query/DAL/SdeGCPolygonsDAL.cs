using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using ZYSK.DZPT.Base.Utility;
using ZYSK.DZPT.Query.Class;

namespace ZYSK.DZPT.Query.DAL
{
    /// <summary>
    /// 描述：“GCPolygons”空间数据库表操作类
    /// 作用：“工程区域”要素类对应数据库表对象
    /// 时间：mym-2018-11-08
    /// </summary>
    public class SdeGCPolygonsDAL
    {
        #region 常量
        public const string TABLE_NAME = "SdeGCPolygons";

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
        public static IFieldsEdit GetGCFeatureClassFields(ISpatialReference currentSpatialReference)
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
                PgDefEdit.GeometryType_2 = esriGeometryType.esriGeometryPolygon;
                PgDefEdit.SpatialReference_2 = currentSpatialReference;
                pFieldEdit.GeometryDef_2 = pGeometryDef;
                pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_OID;
                //pFieldEdit.Length_2 = 6;
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                //pFieldEdit.AliasName_2 = "OID";
                //pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_PROJECTNO;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "工程编号";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_PROJECTNAME;
                pFieldEdit.Length_2 = 255;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "工程名称";
                pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_XMIN;
                //pFieldEdit.Length_2 = 20;
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //pFieldEdit.AliasName_2 = "Xmin";
                //pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_XMAX;
                //pFieldEdit.Length_2 = 20;
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //pFieldEdit.AliasName_2 = "Xmax";
                //pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_YMIN;
                //pFieldEdit.Length_2 = 20;
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //pFieldEdit.AliasName_2 = "Ymin";
                //pFieldsEdit.AddField(pField);

                //pField = new FieldClass();
                //pFieldEdit = (IFieldEdit)pField;
                //pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_YMAX;
                //pFieldEdit.Length_2 = 20;
                //pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                //pFieldEdit.AliasName_2 = "Ymax";
                //pFieldsEdit.AddField(pField);



                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_COORDSYSTEM;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "坐标系统";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_ELEVATIONSYSTEM;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "高程系统";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_PROJECTHEADER;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "工程负责人";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_PROJRCTDATE;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "建立时间";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 =SdeGCPolygons.FEILD_F_PROJECTUNIT;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "建设单位";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_FILENO;
                pFieldEdit.Length_2 = 20;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "档案编号";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_WZXX; //场地位置信息
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "场地位置信息";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_DXXX; //场地地形信息
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "场地地形信息";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_DMXX; //场地地貌信息
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "场地地貌信息";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_KZQD; //抗震设防强度
                pFieldEdit.Length_2 = 6;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.AliasName_2 = "抗震设防强度";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_DZJSD; //地震加速度值
                pFieldEdit.Length_2 = 6;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.AliasName_2 = "基本地震加速度值";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_DZFZ; //地震分组
                pFieldEdit.Length_2 = 6;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "地震分组";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = "F_jqbs"; //等效剪切波速
                pFieldEdit.Length_2 = 16;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.AliasName_2 = "等效剪切波速";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_FGHD; //场地覆盖厚度
                pFieldEdit.Length_2 = 16;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.AliasName_2 = "场地覆盖厚度";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_CDLB; //建筑物场地类别
                pFieldEdit.Length_2 = 6;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger;
                pFieldEdit.AliasName_2 = "建筑物场地类别";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_TZZQ; //设计特征周期
                pFieldEdit.Length_2 = 6;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.AliasName_2 = "设计特征周期";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_DJSD; //标准冻结深度
                pFieldEdit.Length_2 = 6;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeDouble;
                pFieldEdit.AliasName_2 = "标准冻结深度";
                pFieldsEdit.AddField(pField);

                pField = new FieldClass();
                pFieldEdit = (IFieldEdit)pField;
                pFieldEdit.Name_2 = SdeGCPolygons.FEILD_F_DXSQK; //地下水情况介绍
                pFieldEdit.Length_2 = 80;
                pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                pFieldEdit.AliasName_2 = "地下水情况介绍";
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
