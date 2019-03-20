using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.Base.Utility
{
   public static class FileReader
    {

       /// <summary>
       /// 将XML文件读取为一整个字符串
       /// </summary>
       /// <param name="strFilePath">XML文件路径</param>
       /// <returns></returns>
       public static string XMLReader(string strFilePath)
       {
           string strReturn = "";
           try
           {
               using (StreamReader sr = new StreamReader(strFilePath, Encoding.Default,true))
               {
                 strReturn=  sr.ReadToEnd();
               }
               return strReturn;
           }
           catch (Exception ex)
           {
               LogManager.WriteToError(ex.Message);
               return "";
           }
       }
    }
}
