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
        public EmployeeOverview selectedEmployee { get; set; }

        public int shiftID { get; set; }
        public int employeeID { get; set; }
        public string name { get; set; }
        public string adress { get; set; }
        public string number { get; set; }
        public DateTimeOffset dateFrom { get; set; } 
        public DateTimeOffset dateTo { get; set; } 
        public TimeSpan timeFrom { get; set; }
        public TimeSpan timeTo { get; set; }

        

        public RelayCommand AddMedarbejder { get; set; }
        public RelayCommand HentMedarbejder { get; set; }

        public RelayCommand postShift { get; set; }
        public RelayCommand getShift { get; set; }

        public TestViewModel()
        {

            getShiftoverview = new ObservableCollection<ShiftOverview>();
            getMedarbejdersplan = new ObservableCollection<EmployeeOverview>();

            timeFrom = DateTime.Now.TimeOfDay;
            timeTo = DateTime.Now.TimeOfDay.Add(new TimeSpan(8,0,0));
            dateFrom = DateTime.Today;
            dateTo = DateTime.Today + timeTo;

            AddMedarbejder = new RelayCommand(PostEmployeeOverview);
            HentMedarbejder = new RelayCommand(GetEmployeeOverview);
            postShift = new RelayCommand(PostShiftOverview);
            getShift = new RelayCommand(GetShiftOverview);
        }
        private async void PostEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(employeeID, name, adress, number);
            await Database.Post<EmployeeOverview>(employee);
        }

        private async void GetEmployeeOverview()
        {
            getMedarbejdersplan.Clear();
            var employees = Database.Get<EmployeeOverview>();
            foreach(var e in await employees)
            {
                getMedarbejdersplan.Add(e);
            }
        }

        private async void PostShiftOverview()
        {
            DateTime from = CombineDateTime(dateFrom, timeFrom);
            DateTime to = CombineDateTime(dateTo, timeTo);
            ShiftOverview shift = new ShiftOverview(shiftID, selectedEmployee.EmployeeID, from, to);
            await Database.Post<ShiftOverview>(shift);
        }

        public DateTime CombineDateTime(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return dateTimeOffset.Date + timeSpan;
        }

        private async void GetShiftOverview()
        {
            getShiftoverview.Clear();
            var shifts = Database.Get<ShiftOverview>();
            foreach (var e in await shifts)
            {
                getShiftoverview.Add(e);
            }
        }

    }
}
