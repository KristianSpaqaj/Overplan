﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.Model
{
    public class EmployeeOverview
    {
        public EmployeeOverview(int employeeID, string name, string address, string number)
        {
            EmployeeID = employeeID;
            Name = name;
            Adress = address;
            Number = number;
        }

        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
    }
}
