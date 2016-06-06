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
using System.Windows.Navigation;
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
           
            var closeable = true;
            using (var ctx = new DatabaseModel())
            {
                var t = new List<HCI.Model.Type>(ctx.Types.Where(tt => tt.Id == type.Id));
                if (string.IsNullOrEmpty(type.Id))
                {
                    closeable = false;
                    MessageBox.Show("Id mus be set!");
                }
                else if (t.Count > 0)
                {
                    closeable = false;
                    MessageBox.Show("Id already exists!");
                }
                else if (string.IsNullOrEmpty(type.Name))
                {
                    closeable = false;
                    MessageBox.Show("Id mus be set!");
                }
                else if (string.IsNullOrEmpty(type.PathImage) || type.PathImage == "photo1.png")
                {
                    closeable = false;
                    MessageBox.Show("Image must be set!");
                }
                else
                {
                    ctx.Types.Add(type);
                    ctx.SaveChanges();
                    MessageBox.Show("New type is successfuly saved.");
                }
            }
            if (closeable)
            {
                Close();
            }
        }

        private void btnBrowse_Click_1(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog chooseImage = new OpenFileDialog();
            chooseImage.Filter = "Image files (*.png; *.jpg; *.jpeg; *.ico)| *.png; *.jpeg; *.jpg; *.ico|All files(*.*)|*.*";

            if (chooseImage.ShowDialog() == true)
            {
                type.PathImage = chooseImage.FileName;
                imgIcon.Source = new BitmapImage(new Uri(type.PathImage, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
