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
        public ObservableCollection<ShiftOverview> ShiftOverviews { get; set; }
        public ObservableCollection<EmployeeOverview> EmployeeOverviews { get; set; }
        public EmployeeOverview SelectedEmployee { get; set; }

        public ObservableCollection<LoginOverview> LoginOverviews { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public RelayCommand PostLoginOverviewCommand { get; set; }
        public RelayCommand GetLoginOverviewCommand { get; set; }

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

        public TestViewModel()
        {
            ShiftOverviews = new ObservableCollection<ShiftOverview>();
            EmployeeOverviews = new ObservableCollection<EmployeeOverview>();

            LoginOverviews = new ObservableCollection<LoginOverview>();
            PostLoginOverviewCommand = new RelayCommand(PostLoginOverview);
            GetLoginOverviewCommand = new RelayCommand(GetLoginOverview);

            TimeFrom = DateTime.Now.TimeOfDay;
            TimeTo = DateTime.Now.TimeOfDay;
            DateFrom = DateTime.Today;
            DateTo = CombineDateTime(DateFrom, TimeTo);
            PostEmployeeOverviewCommand = new RelayCommand(PostEmployeeOverview);
            GetEmployeeOverviewCommand = new RelayCommand(GetEmployeeOverview);
            PostShiftOverviewCommand = new RelayCommand(PostShiftOverview);
            GetShiftOverviewCommand = new RelayCommand(GetShiftOverview);
        }
        private async void PostEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(EmployeeID, Name, Address, PhoneNumber);
            await Database.Post<EmployeeOverview>(employee);
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

        private async void PostShiftOverview()
        {
            DateTime from = CombineDateTime(DateFrom, TimeFrom);
            DateTime to = CombineDateTime(DateTo, TimeTo);
            ShiftOverview shift = new ShiftOverview(ShiftID, SelectedEmployee.EmployeeID, from, to);
            await Database.Post<ShiftOverview>(shift);
        }


        private async void GetShiftOverview()
        {
            ShiftOverviews.Clear();
            var shifts = Database.Get<ShiftOverview>();
            foreach (var e in await shifts)
            {
                ShiftOverviews.Add(e);
            }
        }

        public DateTime CombineDateTime(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return dateTimeOffset.Date + timeSpan;
        }

        private async void PostLoginOverview()
        {
            LoginOverview login = new LoginOverview(Username, Password);
            await LogInDatabase.Post<LoginOverview>(login);
        }

        private async void GetLoginOverview()
        {
            LoginOverviews.Clear();
            var login = LogInDatabase.Get<LoginOverview>();
            foreach (var e in await login)
            {
                LoginOverviews.Add(e);
            }
        }

    }
}
