using OverplanUWP.Commands;
using OverplanUWP.Common;
using OverplanUWP.Model;
using OverplanUWP.View;
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
        public ObservableCollection<ShiftOverview> ShiftOverviews { get; set; }
        public ObservableCollection<EmployeeOverview> EmployeeOverviews { get; set; }
        public EmployeeOverview SelectedEmployee { get; set; }

        public int FilterID { get; set; }
        public string FilterName { get; set; }

        public int ShiftID { get; set; }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public RelayCommand PostEmployeeOverviewCommand { get; set; }
        public RelayCommand GetEmployeeOverviewCommand { get; set; }
        public RelayCommand PostShiftOverviewCommand { get; set; }
        public RelayCommand GetShiftOverviewCommand { get; set; }

        public RelayCommand GetMyShiftOverviewCommand { get; set; }
        public RelayCommand GetSpecificEmployeeOverviewCommand { get; set; }

        public TestViewModel()
        {
            ShiftOverviews = new ObservableCollection<ShiftOverview>();
            EmployeeOverviews = new ObservableCollection<EmployeeOverview>();

            TimeFrom = DateTime.Now.TimeOfDay;
            TimeTo = DateTime.Now.TimeOfDay;
            DateFrom = DateTime.Today;
            DateTo = CombineDateTime(DateFrom, TimeTo);
            PostEmployeeOverviewCommand = new RelayCommand(PostEmployeeOverview);
            GetEmployeeOverviewCommand = new RelayCommand(GetEmployeeOverview);
            PostShiftOverviewCommand = new RelayCommand(PostShiftOverview);
            GetShiftOverviewCommand = new RelayCommand(GetShiftOverview);

            GetMyShiftOverviewCommand = new RelayCommand(GetMyShiftOverview);
            GetSpecificEmployeeOverviewCommand = new RelayCommand(GetSpecificEmployeeOverview);

        }

        private async void GetMyShiftOverview()
        {
            ShiftOverviews.Clear();
            var shifts = Database.Get<ShiftOverview>();
            
            foreach (var e in await shifts)
            {
                var id = e.EmployeeID;
                if (id == FilterID)
                {
                    ShiftOverviews.Add(e);
                }
                
            }
        }

        private async void GetSpecificEmployeeOverview()
        {
            EmployeeOverviews.Clear();
            var employees = Database.Get<EmployeeOverview>();
            foreach (var e in await employees)
            {
                var id = e.Name;
                if (id.Contains(FilterName))
                {
                    EmployeeOverviews.Add(e);
                }
                
            }
        }
        //Uses Post method from Database Class using EmployeeOverview. It posts an Employee to the database.
        private async void PostEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(EmployeeID, Name, Address, PhoneNumber);
            await Database.Post<EmployeeOverview>(employee);
        }
        //Uses Get method from Database Class using EmployeeOverview. It gets all of the Employees from the database.
        private async void GetEmployeeOverview()
        {
            EmployeeOverviews.Clear();
            var employees = Database.Get<EmployeeOverview>();
            foreach (var e in await employees)
            {
                EmployeeOverviews.Add(e);
            }
        }
        //Uses Post method from Database Class using ShiftOverview. It posts a shift to the database.
        private async void PostShiftOverview()
        {
            DateTime from = CombineDateTime(DateFrom, TimeFrom);
            DateTime to = CombineDateTime(DateTo, TimeTo);
            ShiftOverview shift = new ShiftOverview(ShiftID, SelectedEmployee.EmployeeID, from, to);
            await Database.Post<ShiftOverview>(shift);
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
        //Combines DateTimeOffset and Timespan for a working DateTime for PostShiftOverview.
        public DateTime CombineDateTime(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return dateTimeOffset.Date + timeSpan;
        }

    }
}
