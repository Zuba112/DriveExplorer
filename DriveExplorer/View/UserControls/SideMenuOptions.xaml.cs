using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.DirectoryServices;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static DriveExplorer.MainWindow;

namespace DriveExplorer.View.UserControls
{
    /// <summary>
    /// Interaction logic for SideMenuOptions.xaml
    /// </summary>
    public partial class SideMenuOptions : UserControl
    {
        public SideMenuOptions()
        {
            InitializeComponent();
            UserName.Text = System.Windows.Forms.SystemInformation.UserName;
        }

        private string GetFolderPath(Environment.SpecialFolder localApplicationData)
        {
            throw new NotImplementedException();
        }
    }
}
