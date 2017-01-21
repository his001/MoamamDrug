using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moamam.Lib
{
    public class DbConStr
    {
        public static string getDbConstr()
        {
            string _conStr = @"server=FLEX3\MSSQL2014; database=moamam; uid=moamam; pwd=gkgk1234";
			//string _conStr = @"server=localhost; database=moamam; uid=moamam; pwd=gkgk1234";
            return _conStr;
        }
    }
}
