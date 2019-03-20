using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.Base.DbBase
{
    /// <summary>
    /// 数据库字段信息结构
    /// </summary>
    [DebuggerDisplay("{Name},{FieldType},{Value}")]
    public class DBFieldItem
    {
        private string a;

        private object b;

        private EnumDBFieldType c;

        /// <summary>
        /// 字段名
        /// </summary>
        public string Name
        {
            get
            {
                return this.a;
            }
            set
            {
                this.a = value;
            }
        }

        /// <summary>
        /// 字段值
        /// </summary>
        public object Value
        {
            get
            {
                return this.b;
            }
            set
            {
                this.b = value;
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public EnumDBFieldType FieldType
        {
            get
            {
                return this.c;
            }
            set
            {
                this.c = value;
            }
        }

        public DBFieldItem()
        {
        }

        public DBFieldItem(string fieldName, object fieldValue, EnumDBFieldType fieldType)
        {
            this.a = fieldName;
            this.b = fieldValue;
            this.c = fieldType;
        }

        public override string ToString()
        {
            return string.Format("{{0}:{1}}", this.a, this.b);
        }

        public static implicit operator DBFieldItem(object[] array)
        {
            if (true)
            {
            }
            return new DBFieldItem
            {
                a = array[0].ToString(),
                b = array[1],
                FieldType = (EnumDBFieldType)array[2]
            };
        }
    }

    /// <summary>
    /// 内部生成Sql语句时使用的字段类型枚举
    /// </summary>
    public enum EnumDBFieldType
    {
        UnKnown = -1,
        /// <summary>
        /// 数字
        /// </summary>
        FTNumber = 1,
        FTString,
        FTDate,
        FTDatetime,
        [Obsolete("请不要使用该枚举值")]
        FTServerNowDatetime,
        FTBlob = 7
    }
}
