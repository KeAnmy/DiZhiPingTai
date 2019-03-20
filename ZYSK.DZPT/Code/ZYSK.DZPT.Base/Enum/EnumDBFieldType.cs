using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYSK.DZPT.Base.Enum
{
    public enum EnumDBFieldType
    {
        UnKnown = -1,
        FTNumber = 1,
        FTString,
        FTDate,
        FTDatetime,
        FTServerNowDatetime,
        FTBlob = 7
    }
}
