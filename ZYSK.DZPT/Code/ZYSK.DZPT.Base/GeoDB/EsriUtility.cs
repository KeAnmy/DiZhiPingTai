using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZYSK.DZPT.Base.DbBase;
using ZYSK.DZPT.Base.Utility;

namespace ZYSK.DZPT.Base.GeoDB
{
    /// <summary>
    /// 描述：Esri应用类
    /// 作用：与arcgis有关的操作都封装在这个类中
    /// 时间：mym-2018-11-08
    /// </summary>
    public class EsriUtility
    {
        #region 字段
        private IDBHelper _db = null;
        private IWorkspace _esriWS = null;
        private IFeatureWorkspace _esriFWS = null;
        private ISpatialReference _currentSpatialReference = null;
        private static  ISpatialReferenceFactory _sreferEnvironemnt = new SpatialReferenceEnvironment();
        private static ISpatialReference _CGCS2000GeoSRefer = null;
        private static ISpatialReference _XiAn80GeoSRefer = null;
        #endregion

        #region 初始化

        public EsriUtility(IDBHelper db, IWorkspace ws, ISpatialReference sr)
        {
            _db = db;
            _esriWS = ws;
            _esriFWS = ws as IFeatureWorkspace;
            _currentSpatialReference = sr;
        }

        #endregion

        #region public

        /// <summary>
        /// 创建要素类
        /// </summary>
        /// <param name="fclassName">要素类的名称</param>
        /// <param name="fldEdit">要素所包含的属性字段集合</param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClass(string fclassName, IFieldsEdit fldEdit, IFeatureWorkspace fws)
        {
            try
            {
                IFeatureClass fc = fws.CreateFeatureClass(fclassName, fldEdit, null, null, esriFeatureType.esriFTSimple, "Shape", "");
                return fc;
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                return null;
            }

        }

        /// <summary>
        /// 从Table的XY字段创建点对象的要素类
        /// </summary>
        /// <param name="mTable"></param>
        /// <returns></returns>
        public static IFeatureClass CreateFeatureClassFormXYTable(ITable mTable)
        {
            try
            {
                IXYEvent2FieldsProperties xyEvent2FieldsProperties = new XYEvent2FieldsPropertiesClass();
                xyEvent2FieldsProperties.XFieldName = "l";
                xyEvent2FieldsProperties.YFieldName = "b";
                //xyEvent2FieldsProperties.ZFieldName = "elevation";
                IDataset sourceDataset = (IDataset)mTable;
                IName sourceName = sourceDataset.FullName;
                IXYEventSourceName xyEventSourceName = new XYEventSourceNameClass();
                xyEventSourceName.EventProperties = xyEvent2FieldsProperties;
                xyEventSourceName.EventTableName = sourceName;
                //xyEventSourceName.SpatialReference = spatialReference;
                IName name = (IName)xyEventSourceName;
                IXYEventSource xyEventSource = (IXYEventSource)name.Open();
                IFeatureClass pFeatureClass = (IFeatureClass)xyEventSource;
                return pFeatureClass;
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                return null;
            }

        }


        #endregion

        #region public static
        /// <summary>
        /// 创建地理坐标系
        /// </summary>
        /// <param name="gcsType"></param>
        /// <returns></returns>
        public static ISpatialReference CreateGeographicCoordinate(esriSRProjCS4Type gcsType)
        {
            ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference pSpatialReference = pSpatialReferenceFactory.CreateGeographicCoordinateSystem((int)gcsType);
            return pSpatialReference;
        }

        /// <summary>
        /// 创建投影坐标系
        /// </summary>
        /// <param name="pcsType"></param>
        /// <returns></returns>
        public static ISpatialReference CreateProjectedCoordinate(esriSRProjCS4Type pcsType)
        {
            ISpatialReferenceFactory2 pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference pSpatialReference = pSpatialReferenceFactory.CreateProjectedCoordinateSystem((int)pcsType);
            return pSpatialReference;
        }



        /// <summary>
        /// 根据prj文件创建空间参考
        /// </summary>
        /// <param name="strProFile">空间参照文件</param>
        /// <returns></returns>
        public static ISpatialReference CreateSpatialReference(string strProFile)
        {
            ISpatialReferenceFactory pSpatialReferenceFactory = new SpatialReferenceEnvironmentClass();
            ISpatialReference pSpatialReference = pSpatialReferenceFactory.CreateESRISpatialReferenceFromPRJFile(strProFile);
            return pSpatialReference;
        }


        /// <summary>
        /// 生成CGCS2000坐标新系统投影   wkid:  4490gcs  4491-4554proj
        /// </summary>
        /// <returns></returns>
        public static ISpatialReference CreateCGCS2000SpatialReference(int WKID) //int wkID
        {
            ISpatialReferenceFactory pSpatialReferenceEnvironemnt = new SpatialReferenceEnvironment();
            ISpatialReference pSpatialRef = null;
            if (WKID >= 4490 && WKID < 4555)
            {
                if (WKID == 4490)
                {
                    pSpatialRef = pSpatialReferenceEnvironemnt.CreateGeographicCoordinateSystem(WKID);
                }
                else
                {
                    pSpatialRef = pSpatialReferenceEnvironemnt.CreateProjectedCoordinateSystem(WKID);
                }
            }
            return pSpatialRef;
        }


        /// <summary>
        ///CGCS2000坐标转换：由3°带平面坐标转到经纬度坐标（同一椭球CGCS2000）
        /// </summary>
        /// <param name="sourceRef">源空间参考</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static IPoint CGCS2000ConvertXY2BL_3du(int dh, double x, double y)
        {
            try
            {
                if (_CGCS2000GeoSRefer==null)
                {
                    _CGCS2000GeoSRefer=_sreferEnvironemnt.CreateGeographicCoordinateSystem(4490);
                }
                ISpatialReference sourceRef = GetCGCS2000Prj_3du(dh);
                ISpatialReference targetRef = _CGCS2000GeoSRefer;
                IPoint pt = new PointClass();
                pt.PutCoords(x, y);
                IGeometry geo = (IGeometry)pt;
                geo.SpatialReference = sourceRef;
                geo.Project(targetRef);
                return pt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 西安80坐标转换：由3°带平面坐标转到经纬度坐标
        /// </summary>
        /// <param name="dh"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static IPoint XiAn80ConvertXY2BL_3du(int dh, double x, double y)
        {
            try
            {
                if (_CGCS2000GeoSRefer == null)
                {
                    _CGCS2000GeoSRefer = _sreferEnvironemnt.CreateGeographicCoordinateSystem((int)esriSRProjCS4Type.esriSRProjCS_Xian1980_3_Degree_GK_Zone_41); //2365
                }
                ISpatialReference sourceRef = GetCGCS2000Prj_3du(dh);
                ISpatialReference targetRef = _CGCS2000GeoSRefer;
                IPoint pt = new PointClass();
                pt.PutCoords(x, y);
                IGeometry geo = (IGeometry)pt;
                geo.SpatialReference = sourceRef;
                geo.Project(targetRef);
                return pt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        #endregion


        #region private static

        /// <summary>
        /// 获取CGCS2000D的3度带的投影坐标系（X坐标含带号）
        /// </summary>
        /// <param name="daihao">3°带的带号</param>
        /// <returns></returns>
        private static ISpatialReference GetCGCS2000Prj_3du(int daihao)
        {
            ISpatialReference targetRef = null;
            int WKID = -1;
            if (daihao >= 25 && daihao <= 45)
            {
                WKID = 4513 + daihao - 25;
            }
            if (WKID>0)
            {
                ISpatialReferenceFactory pSpatialReferenceEnvironemnt = new SpatialReferenceEnvironment();
                 targetRef = pSpatialReferenceEnvironemnt.CreateProjectedCoordinateSystem(WKID);
            }
            return targetRef;
          
        }
        #endregion
    }
}
