
using System;
using System.Collections.Generic;
using System.Text;

namespace ZYSK.DZPT.Base.Utility
{
    /// <summary>
    /// WGS84参考椭球 参数(全球GPS监测网采用此参考椭球)
    /// </summary>
    public class WGS84 : GaussPrjBase
    {
        public WGS84()
        {
            _a = 6378137.0;
            _f = 1.0 / 298.257223563;

        }
    }
}
