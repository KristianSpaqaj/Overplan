using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Model
{
    public class EmployeeOverview : IModel
    {
        public EmployeeOverview(int employeeID, string name, string address, string phonenumber)
        {
            ID = employeeID;
            Name = name;
            Address = address;
            PhoneNumber = phonenumber;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        
    }
}
