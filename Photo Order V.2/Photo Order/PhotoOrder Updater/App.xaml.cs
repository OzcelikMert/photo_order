﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PhotoOrder_Updater
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static String[] mArgs;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0) {
                mArgs = e.Args;
            }
        }
    }
}
