using OverplanUWP.Commands;
using OverplanUWP.Common;
using OverplanUWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverplanUWP.ViewModel
{
    public class LeaderRosterViewModel
    {
        public ObservableCollection<EmployeeOverview> EmployeeOverviews { get; set; }
        public ObservableCollection<ShiftOverview> ShiftOverviews { get; set; }
        public EmployeeOverview SelectedEmployee { get; set; }

        public int ShiftID { get; set; }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }

        public RelayCommand GetEmployeeOverviewCommand { get; set; }
        public RelayCommand PostEmployeeOverviewCommand { get; set; }
        public RelayCommand GetShiftOverviewCommand { get; set; }
        public RelayCommand PostShiftOverviewCommand { get; set; }
        

        public LeaderRosterViewModel()
        {
            TimeFrom = DateTime.Now.TimeOfDay;
            TimeTo = DateTime.Now.TimeOfDay;
            DateFrom = DateTime.Today;
            DateTo = CombineDateTime(DateFrom, TimeTo);

            EmployeeOverviews = new ObservableCollection<EmployeeOverview>();
            ShiftOverviews = new ObservableCollection<ShiftOverview>();

            GetEmployeeOverviewCommand = new RelayCommand(GetEmployeeOverview);
            PostEmployeeOverviewCommand = new RelayCommand(PostEmployeeOverview);
            PostShiftOverviewCommand = new RelayCommand(PostShiftOverview);
            GetShiftOverviewCommand = new RelayCommand(GetShiftOverview);
        }

        private async void GetEmployeeOverview()
        {
            EmployeeOverviews.Clear();
            var employees = Database.Get<EmployeeOverview>();
            foreach (var e in await employees)
            {
                EmployeeOverviews.Add(e);
            }
        }

        //Uses Post method from Database Class using EmployeeOverview. It posts an Employee to the database.
        private async void PostEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(EmployeeID, Name, Address, PhoneNumber);
            await Database.Post<EmployeeOverview>(employee);
        }

        //uses Get method from Database Class using ShiftOverview. It gets all the registered shifts.
        private async void GetShiftOverview()
        {
            ShiftOverviews.Clear();
            var shifts = Database.Get<ShiftOverview>();
            foreach (var e in await shifts)
            {
                ShiftOverviews.Add(e);
            }
        }

        //Uses Post method from Database Class using ShiftOverview. It posts a shift to the database.
        private async void PostShiftOverview()
        {
            DateTime from = CombineDateTime(DateFrom, TimeFrom);
            DateTime to = CombineDateTime(DateTo, TimeTo);
            ShiftOverview shift = new ShiftOverview(ShiftID, SelectedEmployee.ID, from, to);
            await Database.Post<ShiftOverview>(shift);
        }

        //Combines DateTimeOffset and Timespan for a working DateTime for PostShiftOverview.
        public DateTime CombineDateTime(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return dateTimeOffset.Date + timeSpan;
        }
    }
}
