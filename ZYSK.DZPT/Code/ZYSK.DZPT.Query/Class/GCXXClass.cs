using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.Query.Class
{
    public class GCXXClass
    {
        

        #region 字段
        private string _prjno = "";
        private string _prjname = "";
        private string _coordsys = "";
        private string _elevationsys = "";
        private string _prjheader = "";
        private string _prjdate = "";
        private string _prjunit = "";
        private string _fileno = "";
        private string _wzxx = "";
        private string _dxxx = "";
        private string _dmxx = "";
        private string _kzqd = "";
        private string _dzjsd = "";
        private string _dzfz = "";
        private string _jqbs = "";
        private string _fghd = "";
        private string _cdlb = "";
        private string _tzzq = "";
        private string _djsd = "";
        private string _dxsqk = "";

        #endregion

        #region 属性

        /// <summary>
        /// 工程编号
        /// </summary>
        public string ProjectNo
        {
            get { return _prjno; }
            set { _prjno = value; }
        }

        /// <summary>
        /// 工程名称
        /// </summary>
        public string ProjectName
        {
            get { return _prjname; }
            set { _prjname = value; }
        }

        /// <summary>
        /// 坐标系统
        /// </summary>
        public string CoordSystem
        {
            get { return _coordsys; }
            set { _coordsys = value; }
        }

        /// <summary>
        /// 高程系统
        /// </summary>
        public string ElevationSystem
        {
            get { return _elevationsys; }
            set { _elevationsys = value; }
        }

        /// <summary>
        /// 工程负责人
        /// </summary>
        public string ProjectHeader
        {
            get { return _prjheader; }
            set { _prjheader = value; }
        }

        /// <summary>
        /// 建立时间
        /// </summary>
        public string ProjectDate
        {
            get { return _prjdate; }
            set { _prjdate = value; }
        }

        /// <summary>
        /// 建设单位
        /// </summary>
        public string ProjectUnit
        {
            get { return _prjunit; }
            set { _prjunit = value; }
        }

        /// <summary>
        /// 档案编号
        /// </summary>
        public string FileNo
        {
            get { return _fileno; }
            set { _fileno = value; }
        }

        /// <summary>
        /// 场地位置信息
        /// </summary>
        public string WZXX
        {
            get { return _wzxx; }
            set { _wzxx = value; }
        }

        /// <summary>
        /// 场地地形信息
        /// </summary>
        public string DXXX
        {
            get { return _dxxx; }
            set { _dxxx = value; }
        }

        /// <summary>
        /// 场地地貌信息
        /// </summary>
        public string DMXX
        {
            get { return _dmxx; }
            set { _dmxx = value; }
        }

        /// <summary>
        /// 抗震设防强度
        /// </summary>
        public string KZQD
        {
            get { return _kzqd; }
            set { _kzqd = value; }
        }

        /// <summary>
        /// 地震分组
        /// </summary>
        public string DZFZ
        {
            get { return _dzfz; }
            set { _dzfz = value; }
        }

        /// <summary>
        /// 基本地震加速度值
        /// </summary>
        public string DZJSD
        {
            get { return _dzjsd; }
            set { _dzjsd = value; }
        }

        /// <summary>
        /// 等效剪切波速
        /// </summary>
        public string JQBS
        {
            get { return _jqbs; }
            set { _jqbs = value; }
        }

        /// <summary>
        /// 场地覆盖厚度
        /// </summary>
        public string FGHD
        {
            get { return _fghd; }
            set { _fghd = value; }
        }

        /// <summary>
        /// 场地类别
        /// </summary>
        public string CDLB
        {
            get { return _cdlb; }
            set { _cdlb = value; }
        }

        /// <summary>
        /// 设计特征周期
        /// </summary>
        public string TZZQ
        {
            get { return _tzzq; }
            set { _tzzq = value; }
        }

        /// <summary>
        /// 标准冻结深度
        /// </summary>
        public string DJSD
        {
            get { return _djsd; }
            set { _djsd = value; }
        }

        /// <summary>
        /// 地下水情况介绍
        /// </summary>
        public string DXSQK
        {
            get { return _dxsqk; }
            set { _dxsqk = value; }
        }



        #endregion

    }
}
