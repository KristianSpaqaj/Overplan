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
        public DateTime dateFrom { get; set; } = new DateTime(2020, 5, 3,10,10,10,1);
        public DateTime dateTo { get; set; } = new DateTime(2020, 6, 3,10,10,10,2);

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
        private async void PostEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(employeeID, name, adress, number);
            await Database.PostEmployeeOverview(employee);
        }

        private async void GetEmployeeOverview()
        {
            getMedarbejdersplan.Clear();
            var employees = Database.GetEmployeeOverview();
            foreach(var e in await employees)
            {
                getMedarbejdersplan.Add(e);
            }
        }

        private async void PostShiftOverview()
        {
            ShiftOverview shift = new ShiftOverview(1, employeeID, dateFrom, dateTo);
            await Database.PostShiftOverview(shift);
        }

        private async void GetShiftOverview()
        {
            getShiftoverview.Clear();
            var shifts = Database.GetShiftOverview();
            foreach (var e in await shifts)
            {
                getShiftoverview.Add(e);
            }
        }

    }
}
