using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OverplanUWP.Commands
{
    public static class Navigation
    {
        // denne funktion navigere til den angivende side
        // den første parameter angiver hvilken side man går til og den anden parameter er den data man ville sende til den side
        public static void GoToPage(string dest, object param = null)
        {
            Frame rootFrame = GetRootFrame();
            Type type = GetPageType(dest);
            rootFrame.Navigate(type);
        }

        // den returner framen
        private static Frame GetRootFrame()
        {
            return Window.Current.Content as Frame;
        }

        // Her kan den skifte hvad for en slags frame
        private static Type GetPageType(string page, string pageNameSpace = "OverplanUWP.View")
        {
            Type type = Type.GetType(pageNameSpace + "." + page);

            return type;
        }

    }



}