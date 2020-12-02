﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace OverplanUWP.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeeHomeView : Page
    {
        public EmployeeHomeView()
        {
            this.InitializeComponent();
        }
        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            

            // set the initial SelectedItem 
            foreach (NavigationViewItemBase item in SideMenu.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "My Shifts")
                {
                    SideMenu.SelectedItem = item;
                    break;
                }
            }
        }
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                EmployeeView.Navigate(typeof(MyShiftsEmployeeView));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);
            }
        }
        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "myShifts":
                    EmployeeView.Navigate(typeof(MyShiftsEmployeeView));
                    break;

                case "roster":
                    EmployeeView.Navigate(typeof(EmployeeRosterView));
                    break;


            }
        }
    }
}
