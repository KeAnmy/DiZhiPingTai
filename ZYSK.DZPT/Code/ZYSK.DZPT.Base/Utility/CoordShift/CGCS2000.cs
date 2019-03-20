
using System;
using System.Collections.Generic;
using System.Text;

namespace ZYSK.DZPT.Base.Utility
{
    /// <summary>
    /// IUGG1975参考椭球 参数(北京54坐标系采用此参考椭球)
    /// </summary>
    public class CGCS2000 : GaussPrjBase
    {
        public CGCS2000()
        {
            _a = 6378137.0;
            _f = 1.0 / 298.257222101;
        }
    }
}
