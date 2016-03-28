using HCI.Model;
using HCI.Model.Global;
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

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for TagsDialog.xaml
    /// </summary>
    public partial class TagsDialog : Window
    {
        private Tag tag;

        public TagsDialog()
        {
            InitializeComponent();
            tag = new Tag();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Globals.Tags.Add(tag);
            Close();
        }

        private void ColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            tag.Color = colorPicker.SelectedColor.ToString();
            Console.WriteLine(tag.Color);
        }
    }
}
