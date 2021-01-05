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
using Windows.UI.Xaml.Controls;

namespace OverplanUWP.ViewModel
{

    public class TestViewModel
    {
        public ObservableCollection<ShiftOverview> ShiftOverviews { get; set; }
        public ObservableCollection<EmployeeOverview> EmployeeOverviews { get; set; }
        public EmployeeOverview SelectedEmployee { get; set; }
        

        public int FilterID { get; set; }
        public string FilterName { get; set; }

        public ObservableCollection<LoginOverview> LoginOverviews { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Leader { get; set; }
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
        public RelayCommand DeleteEmployeeOverviewCommand { get; set; }
        public RelayCommand GetMyShiftOverviewCommand { get; set; }
        public RelayCommand GetSpecificEmployeeOverviewCommand { get; set; }
        public RelayCommand PopUpCommand { get; set; }

        public TestViewModel()
        {
            ShiftOverviews = new ObservableCollection<ShiftOverview>();
            EmployeeOverviews = new ObservableCollection<EmployeeOverview>();

            LoginOverviews = new ObservableCollection<LoginOverview>();
            PostLoginOverviewCommand = new RelayCommand(PostLoginOverview);
            GetLoginOverviewCommand = new RelayCommand(GetLoginOverview);
            PopUpCommand = new RelayCommand(DisplayNoAccessDialog);

            TimeFrom = DateTime.Now.TimeOfDay;
            TimeTo = DateTime.Now.TimeOfDay;
            DateFrom = DateTime.Today;
            DateTo = CombineDateTime(DateFrom, TimeTo);
            PostEmployeeOverviewCommand = new RelayCommand(PostEmployeeOverview);
            GetEmployeeOverviewCommand = new RelayCommand(GetEmployeeOverview);
            PostShiftOverviewCommand = new RelayCommand(PostShiftOverview);
            GetShiftOverviewCommand = new RelayCommand(GetShiftOverview);
            DeleteEmployeeOverviewCommand = new RelayCommand(DeleteEmployeeOverview);
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
            if (to < from)
            {
                DisplayNoAccessDialog();
            }
            else
            {
                ShiftOverview shift = new ShiftOverview(ShiftID, SelectedEmployee.ID, from, to);
                await Database.Post<ShiftOverview>(shift);
            }
        }



        private async void DisplayNoAccessDialog()
        {
            ContentDialog noAccessDialog = new ContentDialog
            {
                Title = "Error! Date and time is wrong",
                Content = "Date and time to cant be before from!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noAccessDialog.ShowAsync();
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

        private async void DeleteEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(SelectedEmployee.ID, Name, Address, PhoneNumber);
            await Database.Delete<EmployeeOverview>(employee);
        }
        //Combines DateTimeOffset and Timespan for a working DateTime for PostShiftOverview.
        public DateTime CombineDateTime(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return dateTimeOffset.Date + timeSpan;
        }

        private async void PostLoginOverview()
        {
            LoginOverview login = new LoginOverview(Username, Password, Leader);
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
