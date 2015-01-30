using System.Windows.Controls;
using System;
using System.Windows;
using System.IO;

namespace OIDE.Gorilla
{
    /// <summary>
    /// Interaktionslogik für UCT_Font.xaml
    /// </summary>
    public partial class FontView : UserControl
    {
        public FontView()
        {
            InitializeComponent();
        }

        private void btnGenFont_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            (this.DataContext as FontModel).GenFont();
           

        }
    }
}
