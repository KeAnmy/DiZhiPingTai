
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;


namespace ZYSK.DZPT.Utility.GISUtility
{
    public class GeometryUtil
    {

        public static IPolygon GetExternalRectangle(IList<IPoint> listpoint)
        {
            IPolygon rect = null;
            double minx = 0;
            double miny = 0;
            double maxx = 0;
            double maxy = 0;
            double temptx = 0;
            double tempty = 0;
            for (int i = 0; i < listpoint.Count; i++)
            {
                if (i == 0)
                {
                    minx = listpoint[i].X;
                    miny = listpoint[i].Y;
                    maxx = listpoint[i].X;
                    maxy = listpoint[i].Y;
                }
                else
                {
                    temptx = listpoint[i].X;
                    tempty = listpoint[i].Y;
                    if (temptx < minx)
                    {
                        minx = temptx;
                    }
                    else if (temptx > maxx)
                    {
                        maxx = temptx;
                    }
                    if (tempty < miny)
                    {
                        miny = tempty;
                    }
                    else if (tempty > maxy)
                    {
                        maxy = tempty;
                    }
                }
            }
            rect = GeometryUtil.GetRectFromCornerPoints(minx, miny, maxx, maxy);
            return rect;
        }

        /// <summary>
        /// 根据对角点生成矩形
        /// </summary>
        /// <param name="minx"></param>
        /// <param name="miny"></param>
        /// <param name="maxx"></param>
        /// <param name="maxy"></param>
        /// <returns></returns>
        public static IPolygon GetRectFromCornerPoints(double minx,double miny,double maxx,double maxy)
        {
            IPointCollection ptcollection = new PolygonClass();
            object missing = Type.Missing;
            IPoint pt1 = new PointClass();
            pt1.PutCoords(minx, miny);
            ptcollection.AddPoint(pt1,ref missing, ref missing);
            pt1.PutCoords(minx, maxy);
            ptcollection.AddPoint(pt1, ref missing, ref missing);
            pt1.PutCoords(maxx, maxy);
            ptcollection.AddPoint(pt1, ref missing, ref missing);
            pt1.PutCoords(maxx, miny);
            ptcollection.AddPoint(pt1, ref missing, ref missing);
            pt1.PutCoords(minx, miny);
            ptcollection.AddPoint(pt1, ref missing, ref missing);
            return (IPolygon)ptcollection;
        }

        public  static double[]  GetCornerPoint(IList<IPoint> listpoint)
        {
            double[] arrMaxMin = new double[4] ;
            double minx = 0;
            double miny = 0;
            double maxx = 0;
            double maxy = 0;
            double temptx = 0;
            double tempty = 0;
            for (int i = 0; i < listpoint.Count; i++)
            {
                if (i == 0)
                {
                    minx = listpoint[i].X;
                    miny = listpoint[i].Y;
                    maxx = listpoint[i].X;
                    maxy = listpoint[i].Y;
                }
                else
                {
                    temptx = listpoint[i].X;
                    tempty = listpoint[i].Y;
                    if (temptx < minx)
                    {
                        minx = temptx;
                    }
                    else if (temptx > maxx)
                    {
                        maxx = temptx;
                    }
                    if (tempty < miny)
                    {
                        miny = tempty;
                    }
                    else if (tempty > maxy)
                    {
                        maxy = tempty;
                    }
                }
            }
            arrMaxMin[0] = minx;
            arrMaxMin[1] = miny;
            arrMaxMin[2] = maxx;
            arrMaxMin[3] = maxy;
            return arrMaxMin;
        }

        public static string GetExternalRectWkt(IList<IPoint> listpoint)
        {
            double[] coord=GetCornerPoint(listpoint);
            string wkt = "POLYGON  (( ";
            //POLYGON  (( 123.47148393 41.69020223, 123.47497875 41.69020223, 123.47497875 41.69190069, 123.47148393 41.69190069, 123.47148393 41.69020223))
            wkt += coord[0] + " " + coord[1] + "," + coord[2] + " " + coord[1] + "," + coord[2] + " " + coord[3] + "," + coord[0] + " " + coord[3] + "," + coord[0] + " " + coord[1] + "))";
            return wkt;
        }

    }
}
