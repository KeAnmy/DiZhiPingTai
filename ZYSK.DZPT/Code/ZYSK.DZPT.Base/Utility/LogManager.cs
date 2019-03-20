using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ZYSK.DZPT.Base.Utility
{
    public static class LogManager
    {
        #region 字段
        static string errorMessage = "";
        #endregion

        #region 属性
        public static string ErrorMessage
        {
            set
            {
                errorMessage = value;
            }
            get
            {
                return errorMessage;
            }
        }
        #endregion


        /// <summary>
        ///PC端： 写调试日志
        /// </summary>
        /// <param name="contentStr">内容字符串</param>
        /// <returns></returns>
        public static bool WriteToDebug(string contentStr)
        {
            try
            {
                string configPath = Application.StartupPath + @"\Logs\debug.txt";
                if (!Directory.Exists(Application.StartupPath + @"\Logs"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\Logs");
                }
                using (StreamWriter sw = new StreamWriter(configPath, true, Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ":");
                    sw.WriteLine(contentStr + "\r\n");
                    sw.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = DateTime.Now.ToString() + ":" + ex.Message + "\r\n";
                return false;
            }
        }
        
        /// <summary>
        /// PC端：写错误日志
        /// </summary>
        /// <param name="contentStr"></param>
        /// <returns></returns>
        public static bool WriteToError(string contentStr)
        {
            try
            {
                string configPath = Application.StartupPath + @"\Logs\error.txt";
                if (!Directory.Exists(Application.StartupPath + @"\Logs"))
                {
                    Directory.CreateDirectory(Application.StartupPath + @"\Logs");
                }
                using (StreamWriter sw = new StreamWriter(configPath, true, Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ":");
                    sw.WriteLine(contentStr + "\r\n");
                    sw.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = DateTime.Now.ToString() + ":" + ex.Message + "\r\n";
                return false;
            }
        }


        /// <summary>
        /// Web端：写调试日志文件
        /// </summary>
        /// <param name="contentStr">内容字符串</param>
        /// <param name="logFolderPath">Logs所在文件夹路径</param>
        /// <returns></returns>
        public static bool WriteToDebug(string contentStr, string logFolderPath)
        {
            string debugFilePath = logFolderPath + @"\Logs\debug.txt";
            try
            {
                if (!Directory.Exists(logFolderPath+ @"\Logs"))
                {
                    Directory.CreateDirectory(logFolderPath+ @"\Logs");
                }
                using (StreamWriter sw = new StreamWriter(debugFilePath, true, Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ":");
                    sw.WriteLine(contentStr + "\r\n");
                    sw.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = DateTime.Now.ToString() + ":" + ex.Message + "\r\n";
                return false;
            }
        }

        /// <summary>
        /// Web端：写错误日志
        /// </summary>
        /// <param name="contentStr">内容字符串</param>
        /// <param name="logFolderPath">Logs所在文件夹路径</param>
        /// <returns></returns>
        public static bool WriteToError(string contentStr, string logFolderPath)
        {
            try
            {
                string configPath = logFolderPath + @"\Logs\error.txt";
                if (!Directory.Exists(logFolderPath + @"\Logs"))
                {
                    Directory.CreateDirectory(logFolderPath + @"\Logs");
                }
                using (StreamWriter sw = new StreamWriter(configPath, true, Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString() + ":");
                    sw.WriteLine(contentStr + "\r\n");
                    sw.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = DateTime.Now.ToString() + ":" + ex.Message + "\r\n";
                return false;
            }
        }


    }
}
