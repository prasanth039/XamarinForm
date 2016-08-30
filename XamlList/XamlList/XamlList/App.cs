using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Xamarin.Forms;

namespace XamlList
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
           MainPage = new NavigationPage(new Page1());

        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }
    }
}
