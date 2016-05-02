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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HCI.Model;
using HCI.Model.Global;
using Microsoft.Win32;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for TypeDialog.xaml
    /// </summary>
    public partial class TypeDialog : Window
    {
        private HCI.Model.Type type;

        public TypeDialog()
        {
            InitializeComponent();
            type = new HCI.Model.Type();
            tbDescription.DataContext = type;
            tbId.DataContext = type;
            tbName.DataContext = type;
            imgIcon.DataContext = type;
            imgIcon.Source = new BitmapImage(new Uri(type.PathImage, UriKind.Relative));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Globals.Types.Add(type);
            Close();
        }

        private void btnBrowse_Click_1(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog chooseImage = new OpenFileDialog();
            chooseImage.Filter = "Image files (*.png; *.jpeg; *.ico)| *.png; *.jpeg; *.ico|All files(*.*)|*.*";

            if (chooseImage.ShowDialog() == true)
            {
                type.PathImage = chooseImage.FileName;
                imgIcon.Source = new BitmapImage(new Uri(type.PathImage, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
