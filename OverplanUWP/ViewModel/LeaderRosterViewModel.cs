using OverplanUWP.Commands;
using OverplanUWP.Common;
using OverplanUWP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace OverplanUWP.ViewModel
{
    public class LeaderRosterViewModel
    {
        public ObservableCollection<EmployeeOverview> EmployeeOverviews { get; set; }
        public ObservableCollection<ShiftOverview> ShiftOverviews { get; set; }
        public EmployeeOverview SelectedEmployee { get; set; }
        public ShiftOverview SelectedShift { get; set; }

        public int ShiftID { get; set; }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }
        public TimeSpan TimeFrom { get; set; }
        public TimeSpan TimeTo { get; set; }
        public int FilterID { get; set; }
        public string FilterName { get; set; }

        public RelayCommand GetEmployeeOverviewCommand { get; set; }
        public RelayCommand PostEmployeeOverviewCommand { get; set; }
        public RelayCommand DeleteEmployeeOverviewCommand { get; set; }
        public RelayCommand GetShiftOverviewCommand { get; set; }
        public RelayCommand PostShiftOverviewCommand { get; set; }
        public RelayCommand DeleteShiftOverviewCommand { get; set; }
        public RelayCommand GetSpecificShiftOverviewCommand { get; set; }
        public RelayCommand GetSpecificEmployeeOverviewCommand { get; set; }

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
            DeleteEmployeeOverviewCommand = new RelayCommand(DeleteEmployeeOverview);

            PostShiftOverviewCommand = new RelayCommand(PostShiftOverview);
            GetShiftOverviewCommand = new RelayCommand(GetShiftOverview);
            DeleteShiftOverviewCommand = new RelayCommand(DeleteShiftOverview);
            GetSpecificShiftOverviewCommand = new RelayCommand(GetSpecificShiftOverview);
            GetSpecificEmployeeOverviewCommand = new RelayCommand(GetSpecificEmployeeOverview);
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

        private async void DeleteEmployeeOverview()
        {
            EmployeeOverview employee = new EmployeeOverview(SelectedEmployee.ID, Name, Address, PhoneNumber);
            await Database.Delete<EmployeeOverview>(employee);
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
        //This method uses ContentDialog Class to make a Pop-up window for errors
        private async void DisplayNoAccessDialog()
        {
            ContentDialog noAccessDialog = new ContentDialog
            {
                Title = "Error! Date and time is wrong",
                Content = "The end of the shift can't be before the start!",
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noAccessDialog.ShowAsync();
        }

        //Combines DateTimeOffset and Timespan for a working DateTime for PostShiftOverview.
        public DateTime CombineDateTime(DateTimeOffset dateTimeOffset, TimeSpan timeSpan)
        {
            return dateTimeOffset.Date + timeSpan;
        }

        private async void DeleteShiftOverview()
        {
            DateTime from = CombineDateTime(DateFrom, TimeFrom);
            DateTime to = CombineDateTime(DateTo, TimeTo);

            ShiftOverview shift = new ShiftOverview(SelectedShift.ID, EmployeeID, from, to);
            await Database.Delete<ShiftOverview>(shift);
        }
        private async void GetSpecificShiftOverview()
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
    }
}
