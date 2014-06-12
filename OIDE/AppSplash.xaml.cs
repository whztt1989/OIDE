﻿using System.Windows;
using Wide.Splash;

namespace OIDE
{
    /// <summary>
    /// Interaction logic for AppSplash.xaml
    /// </summary>
    public partial class AppSplash : Window, ISplashView
    {
        public AppSplash(SplashViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}