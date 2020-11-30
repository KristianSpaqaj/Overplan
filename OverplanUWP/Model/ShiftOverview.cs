using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Model
{
    public class ShiftOverview
    {
        public ShiftOverview(int shiftID, int employeeID, DateTime dateFrom, DateTime dateTo)
        {
            ShiftID = shiftID;
            EmployeeID = employeeID;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }

        public int ShiftID { get; set; }
        public int EmployeeID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
