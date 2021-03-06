﻿using OverplanUWP.Commands;
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
    public class LoginViewModel
    {
        public ObservableCollection<LoginOverview> LoginOverviews { get; set; }
        public LoginOverview SelectedUsername { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool Leader { get; set; }
        public bool LoginSucc { get; set; }
        public string Usernamevalue { get; set; }
        public string Passwordvalue { get; set; }
        private LoginOverview _LoginHelp;
        public RelayCommand PostLoginOverviewCommand { get; set; }
        public RelayCommand GetLoginOverviewCommand { get; set; }
        public RelayCommand LoginCheckCommand { get; set; }
        public LoginOverview SelectedLoginHelp { get => _LoginHelp; set { _LoginHelp = value; LoginFrame_Navigated(); } }
        public RelayCommand DeleteLoginOverviewCommand { get; set; }

        public LoginViewModel()
        {
            LoginOverviews = new ObservableCollection<LoginOverview>();
            PostLoginOverviewCommand = new RelayCommand(PostLoginOverview);
            GetLoginOverviewCommand = new RelayCommand(GetLoginOverview);
            LoginCheckCommand = new RelayCommand(LoginFrame_Navigated);
            DeleteLoginOverviewCommand = new RelayCommand(DeleteLoginOverview);
        }
        // Vi har en get metode til at refreshe listen
        private async void PostLoginOverview()
        {
            LoginOverview login = new LoginOverview(Username, Password, Leader);
            await LogInDatabase.Post<LoginOverview>(login);
            GetLoginOverview();
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
        private async void DeleteLoginOverview()
        {
            LoginOverview login = new LoginOverview(SelectedUsername.Username, Password, Leader);
            await LogInDatabase.Delete<LoginOverview>(login);
            GetLoginOverview();
        }
        // dette void er lavet til at filtrere og checke logins om de er rigtige og navigere leader eller employee til deres pages
        public async void LoginFrame_Navigated()
        {
            var login = LogInDatabase.Get<LoginOverview>();
            foreach (var p in await login)
            {
                var UsernameV = p.Username;
                var PasswordV = p.Password;
                if (UsernameV == Usernamevalue && PasswordV == Passwordvalue)
                {
                    if (p.Leader == true)
                    {
                        Navigation.GoToPage("LeaderHomeView", SelectedLoginHelp);
                        LoginSucc = true;
                    }
                    else
                    {
                        Navigation.GoToPage("EmployeeHomeView", SelectedLoginHelp);
                        LoginSucc = true;
                    }
                    break;
                }
            }
            if (LoginSucc != true)
            {
                DisplayWrongLogin();
            }
        }

        public async void DisplayWrongLogin()
        {
            ContentDialog WrongLogin = new ContentDialog
            {
                Title = "Error!",
                Content = "Username / Password is invalid!",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await WrongLogin.ShowAsync();
        }
    }
}
