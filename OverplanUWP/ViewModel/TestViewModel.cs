﻿using OverplanUWP.Commands;
using OverplanUWP.Common;
using OverplanUWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.ViewModel
{
    
    public class TestViewModel
    {

        public ObservableCollection<EmployeeOverview> postMedarbejdersplan { get; set; }
        public ObservableCollection<Business> postVirksomhed { get; set; }
        public ObservableCollection<EmployeeOverview> getMedarbejdersplan { get; set; }

        public int employeeID { get; set; }
        public string name { get; set; }
        public string adress { get; set; }
        public string number { get; set; }

        public RelayCommand AddMedarbejder { get; set; }
        public RelayCommand AddVirksomhed { get; set; }
        public RelayCommand HentMedarbejder { get; set; }


        public TestViewModel()
        {
            postMedarbejdersplan = new ObservableCollection<EmployeeOverview>();
            postVirksomhed = new ObservableCollection<Business>();
            getMedarbejdersplan = new ObservableCollection<EmployeeOverview>();

            AddMedarbejder = new RelayCommand(PostEmployeeOverview);
            //AddVirksomhed = new RelayCommand(PostVirksomhed);
            HentMedarbejder = new RelayCommand(GetEmployeeOverview);

        }
        private void PostEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(employeeID, name, adress, number);
            Database.PostEmployeeOverview(employee);
        }

        private void GetEmployeeOverview()
        {
            var employees = Database.GetEmployeeOverview();
            foreach(var e in employees)
            {
                getMedarbejdersplan.Add(e);
            }
        }
    }
}
