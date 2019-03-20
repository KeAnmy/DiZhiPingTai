using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;


namespace ZYSK.DZPT.Base.Utility
{

    #region 公共静态方法
    #endregion
    public static class GridControlUtility
    {
       /// <summary>
       /// GridControl添加新的一列
       /// </summary>
       /// <param name="gridView"></param>
       /// <param name="caption"></param>
       /// <param name="fieldName"></param>
       /// <param name="visibleIndex"></param>
       /// <param name="allowSort"></param>
       /// <param name="allowEdit"></param>
       /// <returns></returns>
       public static GridColumn GridViewAddNewColumn(GridView gridView, string caption, string fieldName, int visibleIndex, DefaultBoolean allowSort, bool allowEdit)
       {
           GridColumn col = gridView.Columns.Add();
           try
           {
               col.Caption = caption;
               col.FieldName = fieldName;
               col.VisibleIndex = visibleIndex;
               col.OptionsColumn.AllowSort = allowSort;
               col.OptionsColumn.AllowEdit = allowEdit;
           }
           catch (Exception ex)
           {
               LogManager.WriteToError(ex.Message);
                XtraMessageBox.Show(ex.Message);
           }
           return col;
       }

       /// <summary>
       /// 初始化GridView
       /// </summary>
       /// <param name="gridControl"></param>
       /// <param name="columnNames"></param>
       /// <returns></returns>
       public static DataTable InitGridView(GridControl gridControl, Dictionary<string, Type> columnNames)
       {
           DataTable dtItems = new DataTable();
           if (columnNames == null || columnNames.Count == 0)
               throw new Exception("没有设置列名！");
           try
           {
               GridView gridView = gridControl.MainView as GridView;
               foreach (KeyValuePair<string, Type> name in columnNames)
               {
                   dtItems.Columns.Add(new DataColumn(name.Key, name.Value));
               }
               gridView.BeginInit();
               gridControl.DataSource = dtItems;
               gridView.EndInit();
               gridView.OptionsBehavior.Editable = true;
               gridView.OptionsView.ColumnAutoWidth = true;
               gridView.OptionsView.ShowGroupPanel = false;
               gridView.OptionsMenu.EnableColumnMenu = false;
               gridView.OptionsCustomization.AllowFilter = false;

           }
           catch (Exception ex)
           {
               LogManager.WriteToError(ex.Message);
               XtraMessageBox.Show(ex.Message);

           }

           return dtItems;

       }
    }
}
