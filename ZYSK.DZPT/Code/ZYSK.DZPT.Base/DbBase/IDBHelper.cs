using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.Base.DbBase
{
    public interface IDBHelper
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        string DBName
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        string DBPwd
        {
            get;
            set;
        }

        /// <summary>
        /// 路径（Access有效）
        /// </summary>
        string DBPath
        {
            get;
            set;
        }

        /// <summary>
        /// 服务器IP
        /// </summary>
        string DBServer
        {
            get;
            set;
        }

        /// <summary>
        /// 服务名称/实例名称
        /// </summary>
        string DBServiceName
        {
            get;
            set;
        }

        /// <summary>
        /// 设置在每个操作结束后是否断开数据库连接。默认false。
        /// </summary>
        bool CloseConnectionAfterAction
        {
            set;
        }

        /// <summary>
        /// 数据库实例ID，Oracle有效
        /// </summary>
        string DBSid
        {
            get;
            set;
        }

        /// <summary>
        /// 标识
        /// </summary>
        string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        [Obsolete("建议不要使用该属性,推荐使用ActuralDBType属性")]
        DBHelper.enumDBType DBType
        {
            get;
            set;
        }

        DBHelper.enumDBType ActuralDBType
        {
            get;
        }

        /// <summary>
        /// 数据库驱动，Oracle有效
        /// 请不要设置DBDriver属性!!!
        /// </summary>
        [Obsolete("建议不要使用该属性")]
        DBHelper.enumDBDriver DBDriver
        {
            get;
            set;
        }

        /// <summary>
        /// 登录用户
        /// </summary>
        string DBUser
        {
            get;
            set;
        }

        /// <summary>
        /// 端口，Oracle有效
        /// </summary>
        string DBPort
        {
            get;
            set;
        }

        /// <summary>
        /// 指示是否向日志输出SQL语句
        /// </summary>
        //bool EnableLog
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 是否支持Oracle数据库sys账户连接
        /// </summary>
        //[Obsolete("不要使用该属性", true)]
        //bool EnableOracleSys
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 错误描述，预留属性
        /// </summary>
        //[Obsolete("不要使用该属性")]
        //string errDescription
        //{
        //    get;
        //    set;
        //}

        /// <summary>
        /// 当前活动的数据库连接对象
        /// </summary>
        IDbConnection ActiveConnection
        {
            get;
        }

        IDbCommand ActiveCommand
        {
            get;
        }

        /// <summary>
        /// 连接参数，支持直接设置参数
        /// </summary>
        string ConnectString
        {
            get;
            set;
        }

        [Obsolete("不要使用该属性，已废弃")]
        DataSet ActiveDataSet
        {
            get;
            set;
        }

        int CommandTimeout
        {
            get;
            set;
        }

        /// <summary>
        /// DDTek License 文件路径（DDTek.lic绝对路径）
        /// </summary>
        string DDTekLicensePath
        {
            get;
            set;
        }

        //IDBHelper Clone();

        /// <summary>
        /// 开启事务，仅支持DMS,不支持DDL
        /// </summary>
        //void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        //void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        //void Rollback();

        /// <summary>
        /// 如果连接不为空，则状态不为中断，则不创建连接，重用现有连接
        /// 如果连接状态为中断，则先关闭连接并建立新的连接。
        /// </summary>
        /// <returns>连接创建成功由返回为true,否则抛出异常:数据库连接失败</returns>    
        bool connect();

        /// <summary>
        /// 尝试数据库连接，任何情况下都不返回异常
        /// </summary>
        /// <returns></returns>
        bool TryConnect();

        /// <summary>
        /// 断开数据库连接
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 检查连接状态
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 记录是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="whereCluse">条件</param>
        /// <returns></returns>
       // bool Exist(string tableName, string whereCluse);

        /// <summary>
        /// 执行指定SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回受影响的行数，如果SQL语句引发错误，将抛出一个异常</returns>
        int DoSQL(string sql);

        /// <summary>
        /// 执行指定SQL语句
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paraList">参数列表</param>
        /// <returns>返回受影响的行数，如果SQL语句引发错误，将抛出一个异常</returns>
        //int DoSQL(string sql, IList paraList);

        /// <summary>
        /// 执行sql语句。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>当受影响的行数大于等于0时返回true,否则返回false。sql语句错误，将抛出异常。DDL一般返回0。</returns>
        //[Obsolete("不要再使用了，请使用DoSQL方法")]
       // bool ExcuteUpdate(string sql);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <returns>返回IDataReader对象</returns>
       // IDataReader DoQuery(string sql);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="paraList">参数列表</param>
        /// <returns></returns>
        //IDataReader DoQuery(string sql, IList paraList);

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="table">返回的DataTable的Name属性</param>
        /// <param name="sql"></param>
        /// <param name="release">是否在保留该DataTable以复用。true不保存，false保存</param>
        /// <returns></returns>
        [Obsolete("推荐使用DoQueryEx(string)方法")]
        DataTable DoQueryEx(string table, string sql, bool release);

        /// <summary>
        /// 执行查询,返回名为QueryResult的DataTable
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        DataTable DoQueryEx(string queryString);

        /// <summary>
        /// 通过传入参数查询。
        /// 注意：改查询结果不保留在查询缓存中。
        /// </summary>
        /// <param name="queryString">sql语句</param>
        /// <param name="parameters">参数的集合</param>
        /// <returns></returns>
        DataTable DoQueryEx(string queryString, params IDbDataParameter[] parameters);

        /// <summary>
        /// 执行存储过程，返回输出参数列表
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="spParams"></param>
        /// <returns></returns>
        //List<IDataParameter> ExcuteStoreProcedure(string spName, params IDataParameter[] spParams);

        /// <summary>
        /// 保存内存中的DataTable数据。
        /// 该DataTable必须是通过查询得到的，并且有严格的要求，比如主键。
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        //[Obsolete("请不要使用该方法，已不再提供对该方法的支持。")]
        //bool SaveTable(string tbName);

        /// <summary>
        /// 释放内存中指定名称DataTable的数据
        /// </summary>
       // [Obsolete("请不要使用该方法，已不再提供对该方法的支持。")]
       // bool ReleaseTable(string tbName, bool isSave);

        /// <summary>
        /// 释放内存中指定名称DataTable的数据
        /// </summary>
        /// <param name="tbName"></param>
        /// <returns></returns>
        //bool ReleaseTable(string tbName);

        /// <summary>
        /// 关闭数据库读游标。
        /// </summary>
        void CloseReader();

       // int GetNextValidID(string tblName, string fldName, int StartValue, int StepValue);

        //int GetNextValidID(string tblName, string fldName);

        /// <summary>
        /// 数据库插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="para">一个存储字段、值的二维数组。格式: Name Value Type</param>
        /// <returns></returns>
        //int InsertRow(string tableName, object[][] para);

        /// <summary>
        /// 根据指定条件删除一行数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="strfilter"></param>
        /// <returns></returns>
        //int DeleteRow(string tableName, string strfilter);

        /// <summary>
        /// 更新一行数据。
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="para">一个存储字段、值的二维数组。格式: Name Value Type</param>
        /// <param name="strfilter">过滤条件</param>
        /// <returns></returns>
        int UpdateRow(string tableName, object[][] para, string strfilter);

        /// <summary>
        /// 检查DataParameter是否有效。
        /// </summary>
        /// <param name="dp"></param>
        //[Obsolete("禁用", true)]
        //void CheckDpValidate(IDataParameter dp);

        /// <summary>
        /// 获取系统参数默认值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
       // string GetSysParamDefaultValue(string key);

        /// <summary>
        /// 保存系统参数默认值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
       // bool SaveSysParamDefaultValue(string key, string value);

        /// <summary>
        /// 获取连接信息字符串，非ConnectString。eg：Oracle:Server/DBName/DBUser
        /// </summary>
        /// <returns></returns>
        string GetConnInfoString();

        string GetDateString(DateTime dt);

        string GetDateTimeString(DateTime dt);

        [Obsolete("请不要使用该方法。请使用GetEscapedString方法进行转义。")]
        string getEscapeString();

        DateTime getServerDate();

        string GetServerDateTimeString();

        /// <summary>
        /// 获取表的指定列的单一值。select distint([colname]) from table where filter
        /// </summary>
        /// <param name="tblName"></param>
        /// <param name="colName"></param>
        /// <param name="whereClause"></param>
        /// <returns></returns>
        //IList GetTableColList(string tblName, string colName, string whereClause);

        /// <summary>
        /// 获得数据库中的表列表。
        /// 说明：对于ACCESS数据库，返回表(TABLE)和视图(VIEW)
        ///     对于Oracle数据库，返回登录用户的用户表(User TABLE)
        ///     对于SQLServer数据库，返回基本表(BASE TABLE)和视图(VIEW)
        /// </summary>
        /// <returns>表列表</returns>
        //IList GetTableList();

       // string getUniversalString(string strQueryText);

        /// <summary>
        /// 判断Blob字段的数据是否为空
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="tblName"></param>
        /// <param name="blobField"></param>
        /// <returns></returns>
        //bool IsBlobNull(string condition, string tblName, string blobField);

        /// <summary>
        /// 判断指定的表是否存在
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        bool IsTableExist(string tableName);

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        object QueryScalar(string sql);

      //  byte[] ReadBlob2Bytes(int id, string idfield, string tblName, string blobField);

        //byte[] ReadBlob2Bytes(string condition, string tblName, string blobField);

       // void ReadBlob2File(string filename, string condition, string tblName, string blobField);

       // object ReadBlob2Object(string tblName, string condition, string blobField);

        //IList ReadMultiBlob2Bytes(string condition, string tblName, string blobField);

       // void SaveBytes2Blob(int id, string idfield, string tblName, string blobField, ref byte[] content);

       // void SaveBytes2Blob(string condition, string tblName, string blobField, ref byte[] content);

        //void SaveFile2Blob(string path, int id, string idfield, string tblName, string blobField, bool advanceMode);

       // void SaveFile2Blob(string path, string condition, string tblName, string blobField, bool advanceMode);

        /// <summary>
        /// 获取用户创建的表集合
        /// </summary>
        /// <returns></returns>
        //List<string> GetUserTables();

        /// <summary>
        /// 持久化数据库连接对象到一个文件
        /// </summary>
        /// <param name="fileName"></param>
       // void SaveAs(string fileName);

      //  string GetLikeFieldDecorate(string fieldName, bool ignoreCase = true);

     //   string GetLikeString(bool allMatch, int count);

        /// <summary>
        /// 获取一个简单Like从句。
        /// eg: field like ('%value%')
        /// 更复杂的like语句，请使用GetLikeFieldDecorate和GetLikeString的组合进行处理。
        /// </summary>
        /// <param name="fieldName">字段名称</param>
        /// <param name="key">待匹配的关键字</param>
        /// <param name="pre">是否加前缀限定</param>
        /// <param name="after">是否加后缀限定</param>
        /// <returns></returns>
        //string GetLikeClause(string fieldName, string key, bool pre = true, bool after = true);


        //bool TableExists(string tableName);

        /// <summary>
        /// 获取加密的连接字符串。
        /// 格式如：dbtype=type;Name=name;...
        /// </summary>
        /// <returns></returns>
        //string GetEncryptedConnectionString();


        IDbDataParameter CreateDbParameter();


        /// <summary>
        /// 返回转义特殊字符后的字符串。eg：'''转义为''''。
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string GetEscapedString(string input);

        /// <summary>
        /// 格式化sql语句中的参数名称，eg:name--&gt;@name
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
      //  string FormatParameterName(string parameterName);

        /// <summary>
        /// 清除指定表的主键约束。
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name">主键名字</param>
        /// <returns></returns>
        //bool ClearPrimaryKeys(string table, string name = null);

        /// <summary>
        /// 设置数据库表的主键。
        /// 注意：不检查是否已存在。可以先调用ClearPrimaryKeys方法清除主键。
        ///     设置主键后需要重新维护数据库表，以保证DML（数据操作）的高效。
        /// </summary>
        /// <param name="table"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        //bool SetPrimaryKeys(string table, string[] fields);

       // string getLowerString(string F_DATASOURCEKEY);
    }
}
