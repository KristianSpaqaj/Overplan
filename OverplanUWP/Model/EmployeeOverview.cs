using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Model
{
    public class EmployeeOverview : IModel
    {
        public EmployeeOverview(int employeeID, string name, string address, string number)
        {
            ID = employeeID;
            Name = name;
            Adress = address;
            Number = number;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        
    }
}
