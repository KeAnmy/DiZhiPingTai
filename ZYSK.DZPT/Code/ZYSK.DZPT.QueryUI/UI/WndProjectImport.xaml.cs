using System;
using System.Windows;
using DevExpress.Xpf.Core;
using System.Data.OleDb;
using System.Data;
using System.Linq;
//using System.Data.OracleClient;
using ZYSK.DZPT.Base.DbBase;
using System.Windows.Threading;
using DevExpress.Xpf.Editors;
using ZYSK.DZPT.Base.Utility;
using ZYSK.DZPT.Query.Class;
using ESRI.ArcGIS.Geometry;
using ZYSK.DZPT.Base.GeoDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System.Collections.Generic;
using ZYSK.DZPT.Utility.GISUtility;
using ZYSK.DZPT.Query.DAL;
using ZYSK.DZPT.Base.DbBase.Access;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Microsoft.Win32;

namespace ZYSK.DZPT.QueryUI.UI
{
    /// <summary>
    /// Interaction logic for WndProjectImport.xaml
    /// </summary>
    public partial class WndProjectImport : DXWindow
    {
        #region 字段
        /// <summary>
        /// 全局数据库操作变量
        /// </summary>
        private IDBHelper _db = null;
        private OleDbConnection _accessConn;
        private string _filepath = "";
        private string _filename = "";

        private IDBHelper _dbAccess = null;
        private string _accessConnString = "";
        private OracleConnection _orclConn = null;
        /// <summary>
        /// 全局工作空间变量
        /// </summary>
        private IFeatureWorkspace _sdeFWS = null;
        private List<IPoint> _lstZKPoints = new List<IPoint>() { };
        #endregion

        #region 初始化

        public WndProjectImport(IDBHelper db, ESRI.ArcGIS.Geodatabase.IWorkspace ws)
        {
            InitializeComponent();
            InitializeLayout();
            this._db = db;
            //this._orclConn = new OracleConnection("Data Source=orcl@10.17.5.130;Persist Security Info=True;User ID=dzpt;Password=dzpttest;");
            this._orclConn = new OracleConnection(this._db.ConnectString);
            this._sdeFWS = (IFeatureWorkspace)ws;
        }

        private void InitializeLayout()
        {
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        #endregion

        #region 窗体事件
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtPrjNo.Text = this.GetLastPrjNo();
            this._dbAccess = new AccessDBHelper();
        }
        #endregion


        #region 控件响应事件

        /// <summary>
        /// 上传文件
        /// </summary>
        private void OnBtnClick_selectFile(object sender, RoutedEventArgs e)
        {
            this.ResetImport();
            OpenFileDialog openfileDlg = new OpenFileDialog();
            openfileDlg.InitialDirectory = @"E:\05ProjectDatas\地下空间数据库\2012-2013库";
            openfileDlg.Filter = "Microsoft Access数据库(*.mdb)|*.mdb";
            openfileDlg.FilterIndex = 1;
            openfileDlg.RestoreDirectory = true;
            if (openfileDlg.ShowDialog() == false)
            {
                return;
            }
            this._filepath = openfileDlg.FileName;
            this._filename = System.IO.Path.GetFileNameWithoutExtension(this._filepath);
            if (this._filename.Contains("_"))
            {
                this._filename = this._filename.Split('_')[1];
            }
            this.txtFilePath.Text = this._filepath;
            this.txtPrjName.Text = this._filename;
            try
            {
                this._dbAccess.DBPath = this._filepath;
                this._dbAccess.ActiveConnection.ConnectionString = "";
                this._accessConn = this._dbAccess.ActiveConnection as OleDbConnection;
                this._dbAccess.DoSQL("update g_ziduan Set ZDMS='' where ZDMC='CLDJ'");
                bool isExist = this._dbAccess.IsTableExist("DCollar");
                if (isExist)
                {
                    this._dbAccess.DoSQL("delete * from DCollar");
                }
                else
                {
                    this._dbAccess.DoSQL("create table DCollar(HoleID varchar(255),	X double,	Y double,	Elevation double, ZKBH varchar(255), ZKSD float, ZKZJ float)");
                }

                this._dbAccess.DoSQL("insert INTO DCollar(HoleID,X,Y,Elevation,ZKBH,ZKSD,ZKZJ) SELECT ZKBH,zky,zkx,zkbg,zkbh,zksd,zkzj from z_zuankong");
            }
            catch (Exception ex)
            {
                this.txtErrMsg.Text = ex.Message;
            }
            finally
            {
                if (this._dbAccess.ActiveConnection.State == ConnectionState.Open)
                {
                    this._dbAccess.ActiveConnection.Close();
                }
            }
        }

        /// <summary>
        /// 将理正数据库导入至Oracle数据库合库
        /// </summary>
        private void OnBtnClick_importProject(object sender, RoutedEventArgs e)
        {
            string strErrTabName = "";

            if (_db == null)
            {
                this.txtErrMsg.Text = "提示：无数据库连接，请先配置数据库连接参数";
                return;
            }
            //this._orclConn = new OracleConnection("Data Source=orcl@10.17.5.130;Persist Security Info=True;User ID=dzpt;Password=dzpttest;");

            if (txtPrjFileNo.Text.Length != 6)
            {

            }

            if (this._dbAccess.ActiveConnection.State == ConnectionState.Closed)
            {
                this._dbAccess.ActiveConnection.Open();
            }

            //将工程信息插入数据库表中
            GCXXClass gcobj = this.GetGCXXFrontInfos();
            GCXXDAL gcdal = new GCXXDAL(_db);
            gcdal.Insert(gcobj);
            string gch = gcobj.ProjectNo;
            //获取access数据库中的所有表名称
            List<string> shemaTabNames = this.GetAccessTableNames();

            int tablecount = shemaTabNames.Count;
            this.pbarImport.Maximum = tablecount;
            this.pbarImport.Minimum = 0;
            int intValue = 0;
            double dbValue = 0.0;
            string tablename = "";
            DataTable dttempt = new DataTable();
            for (int i = 0; i < tablecount; i++)
            {
                dbValue = (double)(i + 1) / tablecount * 100;
                intValue = (int)dbValue;
                this.pbarImport.Content = intValue + "%";
                //将进度条的值通过Dispatcher.Invoke实时更新
                this.pbarImport.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.pbarImport.SetValue), DispatcherPriority.Background, ProgressBarEdit.ValueProperty, Convert.ToDouble(i + 1));

                dttempt.Dispose();
                tablename = shemaTabNames[i];
                dttempt = _dbAccess.DoQueryEx("select * from " + tablename);
                //this.InsertBatch_insert(dttempt, tablename, gch);
                this.StandardizeFeilds(ref dttempt, gch);

                this.InsertBatch(dttempt, tablename, gch);


            }

            this.pbarImport.Content = "转换···";


            //将理正数据导入数据库后，接下来需要再进行三步操作
            //第一步：对应项目编号，将DCollar表中的，该编号下的钻孔点坐标XY更新为地理坐标BL
            //第二步：将DCollar表中对应项目编号的点，插入到SDEZKPOINTS表中，Shape字段为MultiPoints，可存储多个点
            //第三步：计算出这些钻孔点的最小外接矩形，插入到SDEGCPOLYGONS表中，Shape字段为Polygon，可存储多边形
            try
            {
                DataTable dtDcollar = _db.DoQueryEx("select * from DCollar where F_PROJECTNO like '%" + gch + "%'"); //钻孔中间表
                //DataTable dtGCXX = _db.DoQueryEx("select * from GCXX where F_PROJECTNO like '%" + gch + "%'"); //工程信息表
                this.ConvertZKCoords_step1(gch, dtDcollar);
                this.ImportSdeZKPointss_step2(dtDcollar);
                //this.ImportSdeGCPolygon_step3(dtGCXX);
                this.ImportSdeGCPolygon_step3(gch, gcobj.ProjectName);
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                this.txtErrMsg.Text = ex.Message;
            }
            finally
            {
                if (this._dbAccess.ActiveConnection.State == ConnectionState.Open)
                {
                    this._dbAccess.ActiveConnection.Close();
                }
            }

            this.pbarImport.Content = "转换完毕";
            this.pbarImport.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.pbarImport.SetValue), DispatcherPriority.Background, ProgressBarEdit.ValueProperty, Convert.ToDouble(tablecount));

        }

        /// <summary>
        /// 退出
        /// </summary>
        private void OnBtnClick_Eixt(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //测试按钮
        private void btnClick_Test(object sender, RoutedEventArgs e)
        {
            this.GetLastPrjNo();
        }

        #endregion

        #region 将工程数据导入空间SDE表中

        /// <summary>
        /// 作用：将DCollar表中的对应项目编号的钻孔坐标，由平面坐标转化为经纬度坐标（同一个椭球2000下的左边转换）
        /// 时间：mym-2018-11-14
        /// </summary>
        /// <param name="prjno">项目编号</param>
        /// <param name="dtdcollar">DCollar在该项目编号下的表记录</param>
        private void ConvertZKCoords_step1(string prjno, DataTable dtdcollar)
        {
            ISpatialReference tsf = EsriUtility.CreateCGCS2000SpatialReference(4490);
            IPoint pt = new PointClass();
            double x = 0, y = 0, b = 0, l = 0;
            int dhao = 0;
            string zkNo = "";
            string strsql = "";
            for (int i = 0; i < dtdcollar.Rows.Count; i++)
            {
                x = Convert.ToDouble(dtdcollar.Rows[i]["X"].ToString()) + 118.63; //平面坐标80转2000，北坐标-7.96，东坐标+118.63
                y = Convert.ToDouble(dtdcollar.Rows[i]["Y"].ToString()) - 7.96;
                zkNo = dtdcollar.Rows[i]["HOLEID"].ToString();
                dhao = Convert.ToInt32(Math.Floor(x / 1000000));
                pt = EsriUtility.CGCS2000ConvertXY2BL_3du(dhao, x, y);
                l = pt.X;
                b = pt.Y;
                dtdcollar.Rows[i]["B"] = b;
                dtdcollar.Rows[i]["L"] = l;
                strsql = "update DCollar t set t.b =" + b + ", t.l= " + l + "  where F_PROJECTNO like '%" + prjno + "%' and HOLEID like '%" + zkNo + "%' ";
                _db.DoSQL(strsql);
            }
        }

        /// <summary>
        /// 作用：将DCollar表中对应项目编号的点，插入到SDEZKPOINTS表中，Shape字段为Point，可存储点
        /// 原理：用IFeatureClass类创建点对象
        /// </summary>
        /// <param name="dtdcollar"></param>
        private void ImportSdeZKPointss_step2_bak(DataTable dtdcollar)
        {
            this._lstZKPoints.Clear();
            IFeatureClass zkPointFC = this._sdeFWS.OpenFeatureClass(SdeZKPoints.TABLE_NAME);
            IFeature ft;
            string strprjno = "";
            string zkbh = "";
            double zksd = 0;
            double zkzj = 0;
            double x = 0;
            double y = 0;
            double z = 0;
            string sqlstr = "";
            try
            {
                for (int i = 0; i < dtdcollar.Rows.Count; i++)
                {

                    IPoint pt = new PointClass();
                    strprjno = dtdcollar.Rows[i]["F_PROJECTNO"].ToString();
                    zkbh = dtdcollar.Rows[i]["ZKBH"].ToString();
                    zksd = dtdcollar.Rows[i].IsNull("ZKSD") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["ZKSD"]);
                    zkzj = dtdcollar.Rows[i].IsNull("ZKZJ") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["ZKZJ"]);
                    x = dtdcollar.Rows[i].IsNull("X") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["X"]);
                    y = dtdcollar.Rows[i].IsNull("Y") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["Y"]);
                    z = dtdcollar.Rows[i].IsNull("ELEVATION") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["ELEVATION"]);
                    if (zksd == 0.0 || zkzj == 0.0 || x == 0.0 || y == 0.0 || z == 0.0)
                    {
                        continue;
                    }
                    pt.X = Convert.ToDouble(dtdcollar.Rows[i]["L"]);
                    pt.Y = Convert.ToDouble(dtdcollar.Rows[i]["B"]);
                    pt.Z = z;

                    ft = zkPointFC.CreateFeature();
                    ft.Shape = pt;
                    ft.set_Value(ft.Fields.FindField(SdeZKPoints.FEILD_F_PROJECTNO), strprjno);
                    ft.set_Value(ft.Fields.FindField(SdeZKPoints.FEILD_F_ZKBH), zkbh);
                    ft.set_Value(ft.Fields.FindField(SdeZKPoints.FEILD_F_ZKSD), zksd);
                    ft.set_Value(ft.Fields.FindField(SdeZKPoints.FEILD_F_ZKZJ), zkzj);
                    ft.set_Value(ft.Fields.FindField(SdeZKPoints.FEILD_F_X), x);
                    ft.set_Value(ft.Fields.FindField(SdeZKPoints.FEILD_F_Y), y);
                    ft.set_Value(ft.Fields.FindField(SdeZKPoints.FEILD_F_H), z);
                    ft.Store();

                    this._lstZKPoints.Add(pt);

                }

                if (this._lstZKPoints.Count == 0)
                {
                    LogManager.WriteToDebug("Access数据库：" + this._filename + "中有效的点为0个");
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteToError("钻孔编号为：" + zkbh + "，" + ex.Message);
                LogManager.WriteToDebug("钻孔编号为：" + zkbh + "没有正确存储到pointsde表中");
                this.txtErrMsg.Text = "钻孔编号为：" + zkbh + "，" + ex.Message;

            }
        }

        /// <summary>
        /// 根据相应的工程编号，将对应的工程保存到SdeGCPolygons表中。
        /// </summary>
        /// <param name="dtgc"></param>
        private void ImportSdeGCPolygon_step3_bak(DataTable dtgc)
        {
            if (this._lstZKPoints.Count == 0)
            {
                return;
            }

            IFeatureClass gctFC = this._sdeFWS.OpenFeatureClass(SdeGCPolygons.TABLE_NAME);
            DataRow dr = null;
            if (dtgc != null && dtgc.Rows.Count > 0)
            {
                dr = dtgc.Rows[0];
            }
            try
            {
                int fieldCount = dtgc.Columns.Count;
                IPolygon polygon = GeometryUtil.GetExternalRectangle(this._lstZKPoints);
                IFeature ft = gctFC.CreateFeature();
                ft.Shape = polygon;
                string clmName = "";
                for (int i = 0; i < fieldCount; i++)
                {
                    clmName = dtgc.Columns[i].ColumnName;
                    ft.set_Value(ft.Fields.FindField(clmName), dr[clmName]);
                }
                ft.Store();
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                LogManager.WriteToDebug("工程编号为：" + dr[SdeGCPolygons.FEILD_F_PROJECTNO].ToString() + "没有正确存储到polygonsde表中");
                this.txtErrMsg.Text = "工程编号为：" + dr[SdeGCPolygons.FEILD_F_PROJECTNO].ToString() + ex.Message;
            }
        }

        /// <summary>
        /// 直接用Oracle的SQL语句，将带有shape字段即ST_GEMETRY字段的记录插入进数据库表中
        /// </summary>
        /// <param name="dtdcollar"></param>
        private void ImportSdeZKPointss_step2(DataTable dtdcollar)
        {
            this._lstZKPoints.Clear();

            string zkid = "";
            string strprjno = "";
            string zkbh = "";
            double zksd = 0;
            double zkzj = 0;
            double x = 0;
            double y = 0;
            double z = 0;
            double l = 0;
            double b = 0;
            string strPoint = "";
            string sqlstr = "";
            try
            {
                for (int i = 0; i < dtdcollar.Rows.Count; i++)
                {
                    IPoint pt = new PointClass();
                    sqlstr = "";
                    zkid = this.GetLastZKNo();
                    strprjno = dtdcollar.Rows[i]["F_PROJECTNO"].ToString();
                    zkbh = dtdcollar.Rows[i]["ZKBH"].ToString();
                    zksd = dtdcollar.Rows[i].IsNull("ZKSD") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["ZKSD"]);
                    zkzj = dtdcollar.Rows[i].IsNull("ZKZJ") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["ZKZJ"]);
                    x = dtdcollar.Rows[i].IsNull("X") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["X"]);
                    y = dtdcollar.Rows[i].IsNull("Y") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["Y"]);
                    z = dtdcollar.Rows[i].IsNull("ELEVATION") ? 0 : Convert.ToDouble(dtdcollar.Rows[i]["ELEVATION"]);
                    if (zksd == 0.0 || zkzj == 0.0 || x == 0.0 || y == 0.0 || z == 0.0)
                    {
                        continue;
                    }
                    pt.X = Convert.ToDouble(dtdcollar.Rows[i]["L"]);
                    pt.Y = Convert.ToDouble(dtdcollar.Rows[i]["B"]);
                    pt.Z = z;
                    strPoint = "sde.st_point (" + pt.X + "," + pt.Y + ",4490)";
                    sqlstr = "insert into " + SdeZKPoints.TABLE_NAME + "(F_ID,F_PROJECTNO,F_ZKBH,F_X,F_Y,F_H,F_ZKSD,F_ZKZJ,SHAPE)" + " values ( '" + zkid + "' , '" + strprjno + "' , '" + zkbh + "' , " + x + " , " + y + " , " + z + " , " + zksd + " , " + zkzj + " , " + strPoint + " )";
                    this._db.DoSQL(sqlstr);
                    this._lstZKPoints.Add(pt);
                }

                if (this._lstZKPoints.Count == 0)
                {
                    LogManager.WriteToDebug("Access数据库：" + this._filename + "中有效的点为0个");
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteToError("钻孔编号为：" + zkbh + "，" + ex.Message);
                LogManager.WriteToDebug("钻孔编号为：" + zkbh + "没有正确存储到pointsde表中");
                this.txtErrMsg.Text = "钻孔编号为：" + zkbh + "，" + ex.Message;

            }
        }

        /// <summary>
        /// 根据相应的工程编号，将对应的工程保存到SdeGCPolygons表中。
        /// </summary>
        /// <param name="dtgc"></param>
        private void ImportSdeGCPolygon_step3(string gcbh, string gcmc)
        {
            if (this._lstZKPoints.Count == 0)
            {
                return;
            }
            try
            {
                string rectwkt = GeometryUtil.GetExternalRectWkt(this._lstZKPoints);
                //sde.st_polygon (wkt, srid integer)
                string strPolygon = "sde.st_polygon ('" + rectwkt + "', 4490)";
                string sqlstr = "insert into " + SdeGCPolygons.TABLE_NAME + "(F_PROJECTNO,F_PROJECTNAME,SHAPE)" + " values ( '" + gcbh + "' , '" + gcmc + "' , " + strPolygon + " )";
                this._db.DoSQL(sqlstr);

            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                LogManager.WriteToDebug("工程编号为：" + gcbh + "没有正确存储到polygonsde表中");
                this.txtErrMsg.Text = "工程编号为：" + gcbh + ex.Message;
            }
        }

        /// <summary>
        /// 获取最新的工程编号
        /// </summary>
        /// <returns></returns>
        private string GetLastPrjNo()
        {
            string strLastPrjno = "";
            string strsql = "select t2.f_projectno from ( select  * from GCXX t1 order by t1.f_projectno desc) t2 where rownum = 1";
            DataTable dtresult = _db.DoQueryEx(strsql);
            if (dtresult.Rows.Count > 0)
            {
                strLastPrjno = dtresult.Rows[0][0].ToString();
                int num = Convert.ToInt32(strLastPrjno.Substring(2));
                num = num + 1;
                strLastPrjno = "gc" + num.ToString().PadLeft(5, '0');
            }
            else
            {
                strLastPrjno = "gc00001";
            }

            return strLastPrjno;
        }


        /// <summary>
        /// 获取最新钻孔点的ID
        /// </summary>
        /// <returns></returns>
        private string GetLastZKNo()
        {
            string zkID = "";
            string strsql = "select t2.f_id from ( select  * from " + SdeZKPoints.TABLE_NAME + " t1 order by t1.f_id desc) t2 where rownum = 1";
            DataTable dtresult = _db.DoQueryEx(strsql);
            if (dtresult.Rows.Count > 0)
            {
                zkID = dtresult.Rows[0][0].ToString();
                int num = Convert.ToInt32(zkID.Substring(2));
                num = num + 1;
                zkID = "zk" + num.ToString().PadLeft(10, '0');
            }
            else
            {
                zkID = "zk0000000001";
            }
            return zkID;
        }

        #endregion

        #region private 方法

        /// <summary>
        /// 获取datatable的列名的集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<string> GetDataTableSchem(DataTable dt)
        {
            List<string> lstname = new List<string>();

            try
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    lstname.Add(dt.Columns[i].ColumnName);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                LogManager.WriteToError("获取" + dt.TableName + "列名集合失败！");
            }

            return lstname;


        }

        /// <summary>
        /// 标准化Access数据库表的字段
        /// </summary>
        /// <param name="dtorign">Access中的表名</param>
        /// <param name="prjno"></param>
        /// <returns></returns>
        private void StandardizeFeilds(ref DataTable dtorign, string prjno)
        {
            if (dtorign.Rows.Count == 0)
            {
                return;
            }
            dtorign.Columns.Add(new DataColumn("F_PROJECTNO", typeof(string)));
            dtorign.Columns["F_PROJECTNO"].SetOrdinal(0);
            foreach (DataRow item in dtorign.Rows)
            {
                item["F_PROJECTNO"] = prjno;
            }
            string[] arrKeyFeilds = new string[] { "LAYER", "TO", "FROM", "POINT" };
            string tempt = "";
            for (int j = 0; j < dtorign.Columns.Count; j++)
            {
                tempt = dtorign.Columns[j].ColumnName;
                if (Array.IndexOf<string>(arrKeyFeilds, tempt.ToUpper().Trim()) >= 0)
                {
                    tempt = "F_" + tempt.ToUpper().Trim();
                }
                dtorign.Columns[j].ColumnName = tempt;
            }
        }

        /// <summary>
        /// 获取前端界面所录入的工程信息
        /// </summary>
        /// <returns></returns>
        private GCXXClass GetGCXXFrontInfos()
        {
            string gch = Convert.ToString(this.txtPrjNo.Text);
            string ndgcmc = Convert.ToString(this.txtPrjName.Text);
            string gcfzr = Convert.ToString(this.txtPrjHeader.Text);
            string jsdw = Convert.ToString(this.txtPrjUnit.Text);
            string jlsj = Convert.ToString(this.txtPrjDate.DateTime);
            string dabh = Convert.ToString(this.txtPrjFileNo.Text);
            string zbxt = Convert.ToString(this.cboxCoordSys.Text);
            string gcxt = Convert.ToString(this.cboxElevationSys.Text);
            string cdwz = Convert.ToString(this.txtLocationInfo.Text);
            string cddx = Convert.ToString(this.txtTerrainInfo.Text);
            string cddm = Convert.ToString(this.txtDimaoInfo.Text);
            string kzqd = Convert.ToString(this.txtDizhen1.Text);
            string dzjsd = Convert.ToString(this.txtDizhen2.Text);
            string dzfz = Convert.ToString(this.txtDizhen3.Text);
            string jqbs = Convert.ToString(this.txtDizhen4.Text);
            string cdhd = Convert.ToString(this.txtDizhen5.Text);
            string cdlb = Convert.ToString(this.txtDizhen6.Text);
            string tzzq = Convert.ToString(this.txtDizhen7.Text);
            string djsd = Convert.ToString(this.txtDizhen8.Text);
            string dxs = Convert.ToString(this.txtDizhen9.Text);

            GCXXClass gcxxObj = new GCXXClass();
            gcxxObj.ProjectNo = gch;
            gcxxObj.ProjectName = ndgcmc;
            gcxxObj.ProjectHeader = gcfzr;
            gcxxObj.ProjectUnit = jsdw;
            gcxxObj.ProjectDate = jlsj;
            gcxxObj.FileNo = dabh;
            gcxxObj.CoordSystem = zbxt;
            gcxxObj.ElevationSystem = gcxt;
            gcxxObj.WZXX = cdwz;
            gcxxObj.DXXX = cddx;
            gcxxObj.DMXX = cddm;
            gcxxObj.KZQD = kzqd;
            gcxxObj.DZJSD = dzjsd;
            gcxxObj.DZFZ = dzfz;
            gcxxObj.JQBS = jqbs;
            gcxxObj.FGHD = cdhd;
            gcxxObj.CDLB = cdlb;
            gcxxObj.TZZQ = tzzq;
            gcxxObj.ProjectName = ndgcmc;
            gcxxObj.ProjectName = ndgcmc;

            return gcxxObj;

        }


        /// <summary>
        /// 批量插入，insert拼接
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="targetTabName"></param>
        private void InsertBatch_insert(DataTable dtData, string targetTabName, string prjno)
        {
            dtData.Columns.Add(new DataColumn("F_PROJECTNO", typeof(string)));
            foreach (DataRow item in dtData.Rows)
            {
                item["F_PROJECTNO"] = prjno;
            }

            List<string> lstzd = this.GetDataTableSchem(dtData);
            string tempt = "";//判断字段名是否为oracle关键字，如果是的话就加一个前缀"f_",如TO、FROM、LAYER等字段名
            string[] arrKeyFeilds = new string[] { "LAYER", "TO", "FROM" };
            for (int i = 0; i < lstzd.Count; i++)
            {
                tempt = lstzd[i];
                if (Array.IndexOf<string>(arrKeyFeilds, tempt.ToUpper().Trim()) >= 0)
                {
                    tempt = "F_" + tempt.ToUpper().Trim();
                }
                lstzd[i] = tempt;
            }
            int zdcount = lstzd.Count;

            OracleConnection orlConn = (OracleConnection)_db.ActiveConnection;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = orlConn;
            OracleTransaction Trans = orlConn.BeginTransaction(IsolationLevel.ReadCommitted);
            cmd.Transaction = Trans;

            //导入数据

            string strFields = "(" + string.Join(",", lstzd) + ")";
            StringBuilder daoruSqlStr = new StringBuilder();
            daoruSqlStr.Append("insert all ");

            try
            {
                //strTableName = "sde." + strTableName;
                string[] sqldr = new string[dtData.Rows.Count];

                for (int l = 0; l < dtData.Rows.Count; l++)
                {
                    string[] values = new string[zdcount];
                    string strClmValues = "";

                    for (int j = 0; j < zdcount; j++)
                    {
                        values[j] = dtData.Rows[l][lstzd[j]].ToString();
                        if (values[j].IndexOf("'") != -1) //判断字符串是否含有单引号
                        {
                            values[j] = values[j].Replace("'", "''");
                        }

                        values[j] = "'" + values[j] + "'";
                    }

                    //string daoru = "insert into " + strTableName + " (" + string.Join(",", zid) + ",工程编号) values(" + string.Join(",", values) + ",'"+gch+"')";
                    //cmd.CommandText = daoru;
                    //int count = cmd.ExecuteNonQuery();
                    sqldr[l] = "" + string.Join(",", values);
                    if (sqldr[l].IndexOf(",'',") != -1)
                    {
                        sqldr[l] = sqldr[l].Replace(",'',", ",null,");

                    }
                    if (sqldr[l].IndexOf("'',") != -1)
                    {
                        sqldr[l] = sqldr[l].Replace("'',", "null,");
                    }
                    if (sqldr[l].IndexOf(",''") != -1)
                    {
                        sqldr[l] = sqldr[l].Replace(",''", ",null");

                    }

                    daoruSqlStr.Append(" into " + targetTabName + strFields + " values ");
                    daoruSqlStr.Append("(" + sqldr[l] + ")");
                }
                if (sqldr.Length == 0)
                {
                    Trans.Commit();

                }
                else
                {
                    daoruSqlStr.Append("select 1 from dual");
                    //string daoru = "insert into " + strTableName + " (" + string.Join(",", zid) + ",工程编号) values (" + string.Join("),(", sqldr) + ")";
                    string daoru = daoruSqlStr.ToString();
                    cmd.CommandText = daoru;
                    //cmd.ExecuteNonQuery();
                    int count = cmd.ExecuteNonQuery();
                    Trans.Commit();
                }

            }
            catch (Exception ex)
            {
                LogManager.WriteToError(targetTabName + ":\n" + ex.Message);
                this.txtErrMsg.Text = targetTabName + ":" + ex.Message;
            }
            dtData.Dispose();

        }




        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="dtData"></param>
        /// <param name="targetTabName"></param>
        private void InsertBatch(DataTable dtData, string targetTabName, string prjno)
        {
            if (dtData.Rows.Count == 0)
            {
                return;
            }

            if (this._orclConn.State == ConnectionState.Closed)
            {
                this._orclConn.Open();
            }

            try
            {
                using (OracleBulkCopy bulkcopy = new OracleBulkCopy(this._orclConn, OracleBulkCopyOptions.Default))//引用SqlBulkCopy  
                {

                    bulkcopy.DestinationTableName = targetTabName;  //数据库中对应的表名  
                    bulkcopy.BatchSize = dtData.Rows.Count;  //一次批量的插入的数据量 
                    bulkcopy.BulkCopyTimeout = 60;  //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除
                    bulkcopy.NotifyAfter = dtData.Rows.Count;  //設定NotifyAfter 属性，以便在每插入10000 条数据时，呼叫相应事件 
                    //for (int i = 0; i < dtData.Columns.Count; i++)
                    //{
                    //    string colname = dtData.Columns[i].ColumnName;
                    //    bulkcopy.ColumnMappings.Add(colname, colname);
                    //}

                    bulkcopy.WriteToServer(dtData);//数据导入数据库  

                    bulkcopy.Close();//关闭连接  
                }
                LogManager.WriteToDebug("Access数据库：" + this._filename + "中的表名为" + targetTabName + "数据表录入数据库成功");

            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                LogManager.WriteToError("Access数据库：" + this._filename + "中的表名为" + targetTabName + "数据表录入数据库失败");
            }
            dtData.Dispose();
        }

        /// <summary>
        /// 获取Access数据库中所有表名，以List集合返回
        /// </summary>
        /// <returns></returns>
        private List<string> GetAccessTableNames()
        {
            List<string> lst = new List<string>();
            DataTable shemaTable = _accessConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            lst = (from t in shemaTable.AsEnumerable() select t.Field<string>("TABLE_NAME")).ToList<string>();
            return lst;
        }

        /// <summary>
        /// 重置工程导入
        /// </summary>
        private void ResetImport()
        {
            this.txtErrMsg.Text = "";
            this.pbarImport.Content = "";
            this.pbarImport.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.pbarImport.SetValue), DispatcherPriority.Background, ProgressBarEdit.ValueProperty, Convert.ToDouble(0));
            this.txtPrjNo.Text = this.GetLastPrjNo();
            this._filename = "";
            this._filepath = "";
            this._lstZKPoints.Clear();
            this.txtFilePath.Text = this._filepath;
            this.txtPrjName.Text = this._filename;


        }


        #endregion



        #region 备用
        /// <summary>
        /// 将理正数据库导入至Oracle数据库合库
        /// </summary>

        private void OnBtnClick_importProject_back(object sender, RoutedEventArgs e)
        {
            /*
              string strErrTabName = "";

            if (_db == null)
            {
                this.txtErrMsg.Text = "提示：无数据库连接，请先配置数据库连接参数";
                return;
            }
            OracleConnection orlConn = _db.ActiveConnection as OracleConnection;
            if (txtPrjFileNo.Text.Length != 6)
            {
            }

            int iLen = this.txtPrjNo.Text.Length;
            if (iLen != 6)
            {
                MessageBox.Show("您输入的工程编号不正确");
                return;
            }
            string gch = Convert.ToString(this.txtPrjNo.Text);
            string ndgcmc = Convert.ToString(this.txtPrjName.Text);
            string gcfzr = Convert.ToString(this.txtPrjHeader.Text);
            string jsdw = Convert.ToString(this.txtPrjUnit.Text);
            string jlsj = Convert.ToString(this.txtPrjDate.DateTime);
            string dabh = Convert.ToString(this.txtPrjFileNo.Text);
            string zbxt = Convert.ToString(this.cboxCoordSys.Text);
            string gcxt = Convert.ToString(this.cboxElevationSys.Text);
            string cdwz = Convert.ToString(this.txtLocationInfo.Text);
            string cddx = Convert.ToString(this.txtTerrainInfo.Text);
            string cddm = Convert.ToString(this.txtDimaoInfo.Text);
            string kzqd = Convert.ToString(this.txtDizhen1.Text);
            string dzjsd = Convert.ToString(this.txtDizhen2.Text);
            string dzfz = Convert.ToString(this.txtDizhen3.Text);
            string jqbs = Convert.ToString(this.txtDizhen4.Text);
            string cdhd = Convert.ToString(this.txtDizhen5.Text);
            string cdlb = Convert.ToString(this.txtDizhen6.Text);
            string tzzq = Convert.ToString(this.txtDizhen7.Text);
            string djsd = Convert.ToString(this.txtDizhen8.Text);
            string dxs = Convert.ToString(this.txtDizhen9.Text);

            string gcxx = string.Format("insert into GCXX (F_ProjectNo,F_ProjectName,F_ProjecyHeader,F_ProjectDate,F_ProjectUnit,F_FileNo,F_CoordSystem,F_ElevationSystem,F_wzxx,F_dxxx,F_dmxx,F_kzqd,F_dzfz,F_fghd,F_tzzq,F_dzjsd,F_jqbs,F_cdlb,F_dxsqk,F_djsd) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}')", gch, ndgcmc, gcfzr, jlsj, jsdw, dabh, zbxt, gcxt, cdwz, cddx, cddm, kzqd, dzfz, cdhd, tzzq, dzjsd, jqbs, cdlb, dxs, djsd);
            _db.DoQueryEx(gcxx);
            DataTable shemaTable = _accessConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });//获取Access数据库表名
            string[] strTable = new string[shemaTable.Rows.Count];
            int m = shemaTable.Columns.IndexOf("TABLE_NAME");
            int max = shemaTable.Rows.Count - 1;
            this.pbarImport.Maximum = max;
            this.pbarImport.Minimum = 0;
            int intValue = 0;
            double dbValue = 0.0;
            for (int i = 0; i < shemaTable.Rows.Count; i++)
            {
                dbValue = (double)(i + 1) / max * 100;
                intValue = (int)dbValue;
                this.pbarImport.Content = intValue + "%";
                //将进度条的值通过Dispatcher.Invoke实时更新
                this.pbarImport.Dispatcher.Invoke(new Action<DependencyProperty, object>(this.pbarImport.SetValue), DispatcherPriority.Background, ProgressBarEdit.ValueProperty, Convert.ToDouble(i));
                DataRow m_DataRow = shemaTable.Rows[i];
                strTable[i] = m_DataRow.ItemArray.GetValue(m).ToString();
                string strTableName = strTable[i];
                strErrTabName = strTableName;
                DataTable zd = _accessConn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, strTableName, null });//获取Access数据表中字段名
                int n = zd.Rows.Count;
                string[] zid = new string[n];
                int c = zd.Columns.IndexOf("COLUMN_NAME");
                string tempt = "";//判断字段名是否为oracle关键字，如果是的话就加一个前缀"f_",如TO、FROM、LAYER等字段名
                string[] arrKeyFeilds = new string[] { "LAYER", "TO", "FROM" };
                string[] zidNew = new string[n];//存储数据源中为关键字的字段名称修改后的值，将录入到Oracle数据库中
                for (int q = 0; q < n; q++)
                {
                    DataRow c_DataRow = zd.Rows[q];
                    zid[q] = c_DataRow.ItemArray.GetValue(c).ToString();
                    tempt = c_DataRow.ItemArray.GetValue(c).ToString().Trim();
                    if (Array.IndexOf<string>(arrKeyFeilds, tempt.ToUpper().Trim()) >= 0)
                    {
                        tempt = "F_" + tempt.ToUpper().Trim();
                    }
                    zidNew[q] = tempt;
                }
                DataSet ds = new DataSet();
                string sqlq = string.Format("select * from {0}", strTableName);//根据表名获取Access表中的数据
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlq, _accessConn);

                dataAdapter.Fill(ds, "strTableName");
                dataAdapter.Dispose();
                DataTable dTable = ds.Tables["strTableName"];

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = orlConn;
                OracleTransaction Trans = orlConn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = Trans;

                //导入数据

                string strFields = "(" + string.Join(",", zidNew) + ",F_ProjectNo)";
                StringBuilder daoruSqlStr = new StringBuilder();
                daoruSqlStr.Append("insert all ");

                try
                {
                    //strTableName = "sde." + strTableName;
                    string[] sqldr = new string[dTable.Rows.Count];

                    for (int l = 0; l < dTable.Rows.Count; l++)
                    {
                        string[] values = new string[n];
                        string strClmValues = "";

                        for (int j = 0; j < n; j++)
                        {
                            values[j] = dTable.Rows[l][zid[j]].ToString();
                            if (values[j].IndexOf("'") != -1) //判断字符串是否含有单引号
                            {
                                values[j] = values[j].Replace("'", "''");
                            }

                            values[j] = "'" + values[j] + "'";
                        }

                        //string daoru = "insert into " + strTableName + " (" + string.Join(",", zid) + ",工程编号) values(" + string.Join(",", values) + ",'"+gch+"')";
                        //cmd.CommandText = daoru;
                        //int count = cmd.ExecuteNonQuery();
                        sqldr[l] = "" + string.Join(",", values) + ",'" + gch + "'";
                        if (sqldr[l].IndexOf(",'',") != -1)
                        {
                            sqldr[l] = sqldr[l].Replace(",'',", ",null,");

                        }
                        if (sqldr[l].IndexOf("'',") != -1)
                        {
                            sqldr[l] = sqldr[l].Replace("'',", "null,");
                        }
                        if (sqldr[l].IndexOf(",''") != -1)
                        {
                            sqldr[l] = sqldr[l].Replace(",''", ",null");

                        }

                        daoruSqlStr.Append(" into " + strTableName + strFields + " values ");
                        daoruSqlStr.Append("(" + sqldr[l] + ")");
                    }
                    if (sqldr.Length == 0)
                    {
                        Trans.Commit();

                    }
                    else
                    {
                        daoruSqlStr.Append("select 1 from dual");
                        //string daoru = "insert into " + strTableName + " (" + string.Join(",", zid) + ",工程编号) values (" + string.Join("),(", sqldr) + ")";
                        string daoru = daoruSqlStr.ToString();
                        cmd.CommandText = daoru;
                        //cmd.ExecuteNonQuery();
                        int count = cmd.ExecuteNonQuery();
                        Trans.Commit();
                    }

                }
                catch (Exception ex)
                {
                    LogManager.WriteToError(strErrTabName + ":\n" + ex.Message);
                    this.txtErrMsg.Text = strErrTabName + ":" + ex.Message;
                }
                dTable.Dispose();
            }
            this.pbarImport.Content = "导入完成,正转换···";

            //将理正数据导入数据库后，接下来需要再进行三步操作
            //第一步：对应项目编号，将DCollar表中的，该编号下的钻孔点坐标XY更新为地理坐标BL
            //第二步：将DCollar表中对应项目编号的点，插入到SDEZKPOINTS表中，Shape字段为MultiPoints，可存储多个点
            //第三步：计算出这些钻孔点的最小外接矩形，插入到SDEGCPOLYGONS表中，Shape字段为Polygon，可存储多边形
            try
            {
                DataTable dtDcollar = _db.DoQueryEx("select * from DCollar where F_PROJECTNO like '%" + gch + "%'"); //钻孔中间表
                DataTable dtGCXX = _db.DoQueryEx("select * from GCXX where F_PROJECTNO like '%" + gch + "%'"); //工程信息表
                this.ConvertZKCoords_step1(gch, dtDcollar);
                this.ImportSdeZKPointss_step2(dtDcollar);
                this.ImportSdeGCPolygon_step3(dtGCXX);
            }
            catch (Exception ex)
            {
                LogManager.WriteToError(ex.Message);
                this.txtErrMsg.Text = ex.Message;
            }

            this.pbarImport.Content = "转换完毕";
             */

        }
        #endregion
    }
}
