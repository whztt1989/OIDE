using MahApps.Metro.Controls;
using System.Windows;
using Wide.Splash;

namespace OIDE
{
    /// <summary>
    /// Interaction logic for AppSplash.xaml
    /// </summary>
    internal partial class AppSplash : MetroWindow, ISplashView
    {
        public AppSplash(SplashViewModel model)
        {
            InitializeComponent();
            DataContext = model;
        }
    }
}