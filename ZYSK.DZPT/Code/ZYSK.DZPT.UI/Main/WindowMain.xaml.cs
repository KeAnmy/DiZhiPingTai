using DevExpress.Xpf.Ribbon;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Controls;
using System.Windows;
using ZYSK.DZPT.Base.DbBase;
using System.Data;
using System;
using DevExpress.Xpf.Bars;
using ZYSK.DZPT.UI.Utility;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ZYSK.DZPT.Base.Utility.Class;
using ESRI.ArcGIS.Geodatabase;
using System.Collections;
using ESRI.ArcGIS.Geometry;
using ZYSK.DZPT.QueryUI.UI;
using ZYSK.DZPT.Base.GeoDB;
using ZYSK.DZPT.Query.DAL;
using ZYSK.DZPT.Query.Class;
using System.Windows.Forms;

namespace ZYSK.DZPT.UI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WindowMain : DXRibbonWindow
    {
        #region 字段

        private IDBHelper _dbHelper = null;
        private ESRI.ArcGIS.Geodatabase.IWorkspace _bizEsriWS = null;

        private AxMapControl _axmapCtrl = null;
        private AxTOCControl _axtocCtrl = null;
        private AxToolbarControl _axtoolbarCtrl = null;

        #region  给map及toc加右键菜单
        private IMapControl3 _mapCtrlObject; //地图控件的辅助动工具控件
        private ITOCControl2 _tocCtrlObject; //内容表控件的辅助工具控件
        private IToolbarMenu _menuOfMap;
        private IToolbarMenu _menuOfToc;
        private ITOCControl _tocCtrlControl;//内容表控件的辅助工具控件，
        private ILayer _lyrActive;
        #endregion




        #endregion

        #region 初始化

        /// <summary>
        /// 初始化窗体与AE控件
        /// </summary>
        public WindowMain(IDBHelper db, IWorkspace ws)
        {
            InitializeComponent();
            InitEngineControls();
            InitializeEngineLicense();
            _dbHelper = db;
            _bizEsriWS = ws;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// 初始化ArcGIS控件
        /// </summary>
        private void InitEngineControls()
        {
            _axmapCtrl = new AxMapControl();
            _axtocCtrl = new AxTOCControl();
            _axtoolbarCtrl = new AxToolbarControl();
            winformHostToc.Child = _axtocCtrl;
            winformHostMap.Child = _axmapCtrl;
            winformHostTools.Child = _axtoolbarCtrl;
        }

        /// <summary>
        /// 初始化Esri的license控件
        /// </summary>
        private static void InitializeEngineLicense()
        {
            AoInitialize aoi = new AoInitializeClass();
            esriLicenseProductCode productCode = esriLicenseProductCode.esriLicenseProductCodeEngine;
            if (aoi.IsProductCodeAvailable(productCode) == esriLicenseStatus.esriLicenseAvailable)
            {
                aoi.Initialize(productCode);
            }
        }


        #endregion

        #region 窗体事件

        /// <summary>
        ///作用：窗体加载后事件
        ///备注：在Window_Loaded实现TOC与MapControl控件、ToolBar与MapControl之间的绑定  
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _axtocCtrl.SetBuddyControl(_axmapCtrl);
            _axtoolbarCtrl.SetBuddyControl(_axmapCtrl);
            AddToolbarItems();
            AddEngineEventHandler();
            AddEngineToolBarMenus();
            _axmapCtrl.LoadMxFile(AppDomain.CurrentDomain.BaseDirectory + @"DataFiles\DefaultMap\Default.mxd");//地图服务
        }


        #endregion

        #region 界面控件响应事件

        #region 应用程序菜单

        //设置数据连接
        private void OnItemClick_setDbConnStr(object sender, ItemClickEventArgs e)
        {
            this.SetDbConnection();
        }


        #endregion

        #region 主页

        #region 文件

        /// <summary>
        /// 作用：加载数据库中的钻孔数据及工程区域数据
        /// 时间：mym-2018-10-30
        /// </summary>
        private void OnItemClick_loadDatas(object sender, ItemClickEventArgs e)
        {
            //若无数据库连接，则打开数据库连接界面
            if (_dbHelper == null)
            {
            IL01:
                if (!this.SetDbConnection())
                {
                    if (System.Windows.MessageBox.Show("数据库连接失败，是否重新设置？", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        goto IL01;
                    }
                    else
                    {
                        return;
                    }
                }
            }
            //二次加载删除重复图层
            int numLayer = this._axmapCtrl.LayerCount;
            for (int m = 0; m < numLayer; m++)
            {
                if (this._axmapCtrl.get_Layer(m).Name == "工程区域" || this._axmapCtrl.get_Layer(m).Name == "钻孔")
                {
                    this._axmapCtrl.DeleteLayer(m);
                    m--;
                    numLayer--;
                }
            }
            int zkCount = Convert.ToInt32(_dbHelper.QueryScalar(string.Format("select count(*) from {0}", SdeGCPolygons.TABLE_NAME)));
            if (zkCount == 0)
            {
                System.Windows.Forms.MessageBox.Show("无数据！");
                return;
            }
            else
            {
                IFeatureWorkspace pSdeFeatureWorkspace = (IFeatureWorkspace)this._bizEsriWS;
                IFeatureClass zkFeatureClass = pSdeFeatureWorkspace.OpenFeatureClass(SdeZKPoints.TABLE_NAME);
                IFeatureLayer pftrlyr = new FeatureLayerClass();
                pftrlyr.FeatureClass = zkFeatureClass;
                pftrlyr.Name = "钻孔";//图层显示为非别名;
                this._axmapCtrl.AddLayer(pftrlyr);//ZXX
                this._axmapCtrl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                IFeatureLayer gcFeatrueClass = new FeatureLayerClass();
                gcFeatrueClass.FeatureClass = pSdeFeatureWorkspace.OpenFeatureClass(SdeGCPolygons.TABLE_NAME);
                gcFeatrueClass.Name = "工程区域";//图层显示为非别名; 
                this._axmapCtrl.AddLayer(gcFeatrueClass);
                this._axmapCtrl.MoveLayerTo(1, 0);
                this._axmapCtrl.MousePointer = esriControlsMousePointer.esriPointerDefault;
                System.Windows.Forms.MessageBox.Show("数据加载完成");
            }


        }

        /// <summary>
        /// 新建工程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemClick_newProject(object sender, ItemClickEventArgs e)
        {
            //DataTable dt = _dbHelper.DoQueryEx("select * from Z_ZUANKONG t");
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemClick_addDatas(object sender, ItemClickEventArgs e)
        {
            ICommand pcmd = new ControlsAddDataCommand();
            pcmd.OnCreate(_axmapCtrl.Object);
            _axmapCtrl.CurrentTool = pcmd as ITool;
            pcmd.OnClick();
        }

        //导入工程数据到数据库
        private void OnItemClick_ImportProject(object sender, ItemClickEventArgs e)
        {
            WndProjectImport wndImport = new WndProjectImport(_dbHelper, this._bizEsriWS);
            wndImport.Show();
        }



        #endregion

        #region 工具
        /// <summary>
        /// 作用：坐标平移
        /// 备注：将钻孔/工程区域，根据目标点的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemClick_CoordTranslate(object sender, ItemClickEventArgs e)
        {

        }
        #endregion


        #endregion

        #region 私有方法

        #region  添加ArcEngine控件事件

        /// <summary>
        /// 添加ArcEngine事件绑定
        /// </summary>
        private void AddEngineEventHandler()
        {
            //地图控件的事件绑定
            _axmapCtrl.OnMouseDown += new IMapControlEvents2_Ax_OnMouseDownEventHandler(OnMouseDown_AxMapCtrl);
            _axmapCtrl.OnMouseMove += new IMapControlEvents2_Ax_OnMouseMoveEventHandler(OnMouseMove_AxMapCtrl);

            //内容目录树控件的事件绑定
            _axtocCtrl.OnMouseDown += new ITOCControlEvents_Ax_OnMouseDownEventHandler(OnMouseDown_AxTocCtrl);
        }

        /// <summary>
        /// 地图控件--鼠标按下事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown_AxMapCtrl(object sender, IMapControlEvents2_OnMouseDownEvent e)
        {
            int flag = e.button;
            if (flag == 4)
            {
                //axmapCtrl.Pan();
            }

            switch (flag)
            {
                case 1://单击左键
                    {
                        break;
                    }
                case 2: //单击右键
                    {
                        _menuOfMap.PopupMenu(e.x, e.y, _mapCtrlObject.hWnd);
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4://拖动鼠标中轴
                    {
                        _axmapCtrl.Pan();
                        break;
                    }
                case 5:
                    {
                        break;
                    }
                case 6:
                    {
                        break;
                    }
                case 7:
                    {
                        break;
                    }

            }
        }

        /// <summary>
        /// 地图控件--鼠标移动事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseMove_AxMapCtrl(object sender, IMapControlEvents2_OnMouseMoveEvent e)
        {

        }

        /// <summary>
        /// 目录树控件--鼠标按下事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseDown_AxTocCtrl(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            esriTOCControlItem item1 = esriTOCControlItem.esriTOCControlItemNone;

            int flag = e.button;
            switch (flag)
            {

                case 1://单击左键
                    {
                        IBasicMap map1 = null;
                        ILayer layer1 = null;
                        object other1 = null;
                        object index1 = null;
                        _tocCtrlControl.HitTest(e.x, e.y, ref item1, ref map1, ref layer1, ref other1, ref index1);
                        if (item1 == esriTOCControlItem.esriTOCControlItemLayer)
                        {
                            if (layer1 is IAnnotationSublayer)
                                return;
                            else
                            {
                                _lyrActive = layer1;

                            }

                        }
                        break;
                    }
                case 2:
                    {
                        esriTOCControlItem item = esriTOCControlItem.esriTOCControlItemNone;
                        IBasicMap map = null;
                        ILayer layer = null;
                        object other = null;
                        object index = null;

                        //判断所选菜单的类型
                        _tocCtrlObject.HitTest(e.x, e.y, ref item, ref map, ref layer, ref other, ref index);

                        //确定选定的菜单类型，Map或是图层菜单
                        if (item == esriTOCControlItem.esriTOCControlItemMap)
                            _tocCtrlObject.SelectItem(map, null);
                        else
                            _tocCtrlObject.SelectItem(layer, null);
                        //设置CustomProperty为layer (用于自定义的Layer命令)                  
                        _mapCtrlObject.CustomProperty = layer;
                        if (item == esriTOCControlItem.esriTOCControlItemMap)
                            _menuOfMap.PopupMenu(e.x, e.y, _tocCtrlObject.hWnd);
                        if (item == esriTOCControlItem.esriTOCControlItemLayer)
                        {
                            _menuOfToc.RemoveAll();
                            IGeoFeatureLayer geofLayer = layer as IGeoFeatureLayer;
                            if (geofLayer != null)
                            {
                                _menuOfToc.AddItem(new OpenAttrTable(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);

                                _menuOfToc.AddItem(new AddAnnotation(), 1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
                            }
                            _menuOfToc.AddItem(new RemoveLayer(), 1, -1, true, esriCommandStyles.esriCommandStyleTextOnly);
                            _menuOfToc.AddItem(new ZoomToLayer(), 1, 0, false, esriCommandStyles.esriCommandStyleTextOnly);

                            _menuOfToc.PopupMenu(e.x, e.y, _tocCtrlObject.hWnd);
                        }
                        break;
                    }
                default:
                    break;

            }
        }



        #endregion

        #region 添加ArcEngine工具控件

        /// <summary>
        /// 添加AE工具条控件
        /// </summary>
        private void AddToolbarItems()
        {
            _axtoolbarCtrl.AddItem(new ControlsOpenDocCommandClass(), -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
            _axtoolbarCtrl.AddItem(new ControlsSaveAsDocCommandClass(), -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
            _axtoolbarCtrl.AddItem(new ControlsAddDataCommandClass(), -1, -1, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
            _axtoolbarCtrl.AddItem("esriControls.ControlsMapNavigationToolbar");
            _axtoolbarCtrl.AddItem("esriControls.ControlsMapIdentifyTool");
            _axtoolbarCtrl.Orientation = esriToolbarOrientation.esriToolbarOrientationVertical;
        }

        /// <summary>
        /// 添加右键菜单，包括toc目录树控件、map地图控件的右键菜单
        /// </summary>
        private void AddEngineToolBarMenus()
        {
            _tocCtrlControl = (ITOCControl)_axtocCtrl.Object;
            _mapCtrlObject = (IMapControl3)_axmapCtrl.Object;
            _tocCtrlObject = (ITOCControl2)_axtocCtrl.Object;
            _menuOfMap = new ToolbarMenuClass();
            _menuOfToc = new ToolbarMenuClass();
            _menuOfMap.AddItem(new LayerVisibility(), 2, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.AddItem(new LayerVisibility(), 1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.AddItem(new RemoveAll(), 1, 2, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.AddItem(new ControlsMapZoomToLastExtentBackCommandClass(), 1, 3, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.AddItem(new ControlsMapZoomToLastExtentForwardCommandClass(), 1, 4, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.AddItem(new ControlsMapFullExtentCommandClass(), 1, 5, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.AddItem(new ControlsMapZoomInFixedCommandClass(), 1, 6, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.AddItem(new ControlsMapZoomOutFixedCommandClass(), 1, 7, false, esriCommandStyles.esriCommandStyleTextOnly);
            _menuOfMap.SetHook(_mapCtrlObject);

            _menuOfToc.SetHook(_mapCtrlObject);
        }



        #endregion

        #region 私有过程方法


        /// <summary>
        /// 作用：设置数据库连接，获得全局DBHelper和全局WorkFactory
        /// 时间：mym-2018-20-30
        /// </summary>
        private bool SetDbConnection()
        {
            bool blResult = false;
            WndDbConnect wndConn = new WndDbConnect();
            wndConn.ShowDialog();
            if (wndConn.ConnState)
            {
                _dbHelper = SysParams.GlobalDBHelper;
                _bizEsriWS = SysParams.BizEsriWS;
                AppRoot.DB = SysParams.GlobalDBHelper;
                AppRoot.BizEsriWS = SysParams.BizEsriWS;
                blResult = true;
            }
            return blResult;
        }


        #endregion

        #endregion

        #endregion


        #region Test
        private void TestEasy()
        {
        }

        #endregion


    }

}
