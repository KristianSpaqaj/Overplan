using OverplanUWP.Commands;
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
        public ObservableCollection<ShiftOverview> getShiftoverview { get; set; }
        public ObservableCollection<EmployeeOverview> getMedarbejdersplan { get; set; }

        public int shiftID { get; set; }
        public int employeeID { get; set; }
        public string name { get; set; }
        public string adress { get; set; }
        public string number { get; set; }
        public DateTime datefrom { get; set; }
        public DateTime dateto { get; set; }

        public RelayCommand AddMedarbejder { get; set; }
        public RelayCommand HentMedarbejder { get; set; }

        public RelayCommand postShift { get; set; }
        public RelayCommand getShift { get; set; }

        public TestViewModel()
        {

            getShiftoverview = new ObservableCollection<ShiftOverview>();
            getMedarbejdersplan = new ObservableCollection<EmployeeOverview>();

            AddMedarbejder = new RelayCommand(PostEmployeeOverview);
            HentMedarbejder = new RelayCommand(GetEmployeeOverview);
            postShift = new RelayCommand(PostShiftOverview);
            getShift = new RelayCommand(GetShiftOverview);
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

        private void PostShiftOverview()
        {
            ShiftOverview shift = new ShiftOverview(shiftID, employeeID, datefrom, dateto);
            Database.PostShiftOverview(shift);
        }

        private void GetShiftOverview()
        {
            var shifts = Database.GetShiftOverview();
            foreach (var e in shifts)
            {
                getShiftoverview.Add(e);
            }
        }

    }
}
