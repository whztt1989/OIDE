using System.Windows.Controls;
using System;
using System.Windows;
using System.IO;

namespace ADock.View.ViewTemplates
{
    /// <summary>
    /// Interaktionslogik für UCT_FontSetting.xaml
    /// </summary>
    public partial class FontSettingView : UserControl
    {
        public FontSettingView()
        {
            InitializeComponent();
        }

        private void btnGenFont_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo();
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                 String basePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
          
                psi.FileName = basePath + @"\FontGen\fontgen.exe";
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.Arguments = "-f gorilla -t 512";
                System.Diagnostics.Process proz = System.Diagnostics.Process.Start(psi);

                proz.StandardInput.WriteLine("-o 2 -i 2 -s 18 -c \"1 1 0\" -C \"1 0 0\" Segoe_UI.ttf");
                proz.StandardInput.WriteLine("-o 1 -i 2 -s 24 -c \"0 0 1\" -C \"1 0 1\" Segoe_UI.ttf");
                proz.StandardInput.WriteLine("-o 2 -i 3 -s 36 -c \"1 1 1\" -C \"0 0 0\" Segoe_UI.ttf");

                proz.StandardInput.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
