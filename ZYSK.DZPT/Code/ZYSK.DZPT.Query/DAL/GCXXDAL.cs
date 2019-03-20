using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYSK.DZPT.Base.DbBase;
using ZYSK.DZPT.Base.Enum;
using ZYSK.DZPT.Base.Public;
using ZYSK.DZPT.Query.Class;

namespace ZYSK.DZPT.Query.DAL
{
    public class GCXXDAL
    {
        #region 常量
        public const string TABLE_NAME = "GCXX";
        public const string FEILD_F_PROJECTNO = "F_ProjectNo";
        public const string FEILD_F_PROJECTNAME = "F_ProjectName";
        public const string FEILD_F_PROJECTHEADER = "F_ProjectHeader";
        public const string FEILD_F_PROJRCTDATE = "F_ProjectDate";
        public const string FEILD_F_PROJECTUNIT = "F_ProjectUnit";
        public const string FEILD_F_FILENO = "F_FileNo";
        public const string FEILD_F_COORDSYSTEM = "F_CoordSystem";
        public const string FEILD_F_ELEVATIONSYSTEM = "F_ElevationSystem";
        public const string FEILD_F_WZXX = "F_wzxx";
        public const string FEILD_F_DXXX = "F_dxxx";
        public const string FEILD_F_DMXX = "F_dmxx";
        public const string FEILD_F_KZQD = "F_kzqd";
        public const string FEILD_F_DZJSD = "F_dzjsd";
        public const string FEILD_F_DZFZ = "F_dzfz";
        public const string FEILD_F_JQBS = "F_jqbs";
        public const string FEILD_F_FGHD = "F_fghd";
        public const string FEILD_F_CDLB = "F_cdlb";
        public const string FEILD_F_TZZQ = "F_tzzq";
        public const string FEILD_F_DJSD = "F_djsd";
        public const string FEILD_F_DXSQK = "F_dxsqk";
        #endregion

        #region 字段
        private IDBHelper _dbHelper = null;
        #endregion

        #region 初始化

        public GCXXDAL(IDBHelper db)
        {
            this._dbHelper = db;
        }

        #endregion

        #region public 方法

        public bool Insert(GCXXClass gcxxobject)
        {
            string sql = this.GetInsertSQLString(gcxxobject);
            return _dbHelper.DoSQL(sql)>0;
        }



        #endregion


        #region private 方法

        private string GetInsertSQLString(GCXXClass gcxxobject)
        {
            string insertSQLstring = "";
            List<DBFieldItem> insertfields = new List<DBFieldItem>();
            insertfields.Add(new DBFieldItem(FEILD_F_PROJECTNO, gcxxobject.ProjectNo, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_PROJECTNAME, gcxxobject.ProjectName, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_PROJECTHEADER, gcxxobject.ProjectHeader, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_PROJECTUNIT, gcxxobject.ProjectUnit, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_PROJRCTDATE, gcxxobject.ProjectDate, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_FILENO, gcxxobject.FileNo, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_COORDSYSTEM, gcxxobject.CoordSystem, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_ELEVATIONSYSTEM, gcxxobject.ElevationSystem, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_WZXX, gcxxobject.WZXX, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_DXXX, gcxxobject.DXXX, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_DMXX, gcxxobject.DMXX, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_KZQD, gcxxobject.KZQD, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_DZJSD, gcxxobject.DZJSD, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_DZFZ, gcxxobject.DZFZ, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_JQBS, gcxxobject.JQBS, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_FGHD, gcxxobject.FGHD, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_CDLB, gcxxobject.CDLB, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_TZZQ, gcxxobject.TZZQ, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_DJSD, gcxxobject.DJSD, EnumDBFieldType.FTString));
            insertfields.Add(new DBFieldItem(FEILD_F_DXSQK, gcxxobject.DXSQK, EnumDBFieldType.FTString));
            insertSQLstring = SQLStringUtility.GetInsertSQL(TABLE_NAME, insertfields, this._dbHelper);
            return insertSQLstring;
        }



        #endregion
    }
}
