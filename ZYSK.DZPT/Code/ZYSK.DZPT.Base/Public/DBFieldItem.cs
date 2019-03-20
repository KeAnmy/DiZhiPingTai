using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYSK.DZPT.Base.Enum;

namespace ZYSK.DZPT.Base.Public
{
    [DebuggerDisplay("{Name},{FieldType},{Value}")]
    public class DBFieldItem
    {
        private string a;
        private object b;
        private EnumDBFieldType c;
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
}
