
using System;
using System.Collections.Generic;
using System.Text;

namespace ZYSK.DZPT.Base.Utility
{

    /// <summary>
    /// Krasovsky参考椭球 参数(北京54坐标系采用此参考椭球)
    /// </summary>
    public class Beijing54 : GaussPrjBase
    {
        public Beijing54()
        {
            _a = 6378245.0;
            _f = 1.0 / 298.3;
        }
    }
}
