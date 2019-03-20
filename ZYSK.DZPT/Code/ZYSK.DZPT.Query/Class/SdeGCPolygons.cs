using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.Query.Class
{
    public class SdeGCPolygons
    {
        #region 常量
        public const string TABLE_NAME = "SdeGCPolygons";
        //public const string FEILD_F_OID = "F_OID";
        public const string FEILD_F_PROJECTNO = "F_ProjectNo";
        public const string FEILD_F_PROJECTNAME = "F_ProjectName";
        //public const string FEILD_F_XMIN = "F_Xmin";
        //public const string FEILD_F_XMAX = "F_Xmax";
        //public const string FEILD_F_YMIN = "F_Ymin";
        //public const string FEILD_F_YMAX = "F_Ymax";
        public const string FEILD_F_COORDSYSTEM = "F_CoordSystem";
        public const string FEILD_F_ELEVATIONSYSTEM = "F_ElevationSystem";
        public const string FEILD_F_PROJECTHEADER = "F_ProjecyHeader";
        public const string FEILD_F_PROJRCTDATE= "F_ProjectDate";
        public const string FEILD_F_PROJECTUNIT = "F_ProjectUnit";
        public const string FEILD_F_FILENO = "F_FileNo";
        public const string FEILD_F_WZXX = "F_wzxx";
        public const string FEILD_F_DXXX = "F_dxxx";
        public const string FEILD_F_DMXX= "F_dmxx";
        public const string FEILD_F_KZQD = "F_kzqd";
        public const string FEILD_F_DZJSD = "F_dzjsd";
        public const string FEILD_F_DZFZ = "F_dzfz";
        public const string FEILD_F_JQBS = "F_jqbs";
        public const string FEILD_F_FGHD = "F_fghd";
        public const string FEILD_F_CDLB = "F_cdlb";
        public const string FEILD_F_TZZQ = "F_tzzq";
        public const string FEILD_F_DJSD= "F_djsd";
        public const string FEILD_F_DXSQK = "F_dxsqk";


        #endregion

        #region 字段
        private string _prjNo = "";
        private double _gcminX = 0;
        private double _gcminY = 0;
        private double _gcmaxX = 0;//_gcmaxX
        private double _gcmaxY = 0;
        #endregion

        #region 属性

        public string ProjectNo
        {
            set
            {
                _prjNo = value;
            }
            get
            {
                return _prjNo;
            }
        }

        #endregion


        #region 初始化

        public SdeGCPolygons(string pno,double minx,double miny,double maxx,double maxy)
        {
            _prjNo = pno;
            _gcminX = minx;
            _gcminY = miny;
            _gcmaxX = maxx;
            _gcmaxY = maxy;
        }

        #endregion


        #region public methods

        public IPointCollection GCPolygon()
        {
            IPointCollection pPointCollection = new PolygonClass();
            object o = Type.Missing;
            object missing = Type.Missing;
            IPoint pPoint = new PointClass();
            pPoint.PutCoords(_gcminX, _gcminY);
            pPointCollection.AddPoint(pPoint, ref missing, ref missing);
            pPoint.PutCoords(_gcminX, _gcmaxY);
            pPointCollection.AddPoint(pPoint, ref missing, ref missing);
            pPoint.PutCoords(_gcmaxX, _gcmaxY);
            pPointCollection.AddPoint(pPoint, ref missing, ref missing);
            pPoint.PutCoords(_gcmaxX, _gcminY);
            pPointCollection.AddPoint(pPoint, ref missing, ref missing);
            IClone pClone = pPointCollection.get_Point(0) as IClone;
            IPoint pEndPoint = pClone.Clone() as IPoint;
            pPointCollection.AddPoint(pEndPoint, ref o, ref o);
            return pPointCollection;
        }
        public void Checkxy(double x, double y)
        {
            if (x > _gcmaxX)
            {
                _gcmaxX = x;
            }
            if (x < _gcminX)
            {
                _gcminX = x;
            }
            if (y > _gcmaxY)
            {
                _gcmaxY = y;
            }
            if (y < _gcminY)
            {
                _gcminY = y;
            }
        }

        #endregion
    }
}
