using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace OverplanUWP.Commands
{
    public static class Navigation
    {
        private static Stack<object> _history = new Stack<object>();

        // denne funktion navigere til den angivende side
        // den første parameter angiver hvilken side man går til og den anden parameter er den data man ville sende til den side
        public static void GoToPage(string dest, object param = null)
        {
            Frame rootFrame = GetRootFrame();
            Type type = GetPageType(dest);

            _history.Push(param);
            rootFrame.Navigate(type);
        }
        // denne funktion sender en tilbage til den forrige side
        public static void GoBack()
        {
            Frame rootFrame = GetRootFrame();

            if (rootFrame.CanGoBack)
            {
                _history.Pop();
                rootFrame.GoBack();
            }
        }

        public static void GoBack(object param = null)
        {
            Frame rootFrame = GetRootFrame();

            if (rootFrame.CanGoBack)
            {
                _history.Pop();
                _history.Pop();
                _history.Push(param);

                rootFrame.GoBack();
            }
        }

        // denne funktion kalder på den data der er blevet sendt til den nuværende side
        public static T GetParameter<T>()
        {

            T converted = (T)_history.Peek();
            return converted;
        }


        private static Frame GetRootFrame()
        {
            return Window.Current.Content as Frame;
        }


        private static Type GetPageType(string page, string pageNameSpace = "OverplanUWP.View")
        {
            Type type = Type.GetType(pageNameSpace + "." + page);

            return type;
        }

    }



}