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
            tbId.DataContext = tag;
            colorPicker.DataContext = tag;
            tbDescription.DataContext = tag;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var closeable = true;
            using (var ctx = new DatabaseModel())
            {
                var tt = new List<Tag>(ctx.Tags.Where(t => t.Id == tag.Id));
                if (string.IsNullOrEmpty(tag.Id))
                {
                    closeable = false;
                    MessageBox.Show("Id must be set!");
                }
                else if (tt.Count > 0)
                {
                    closeable = false;
                    MessageBox.Show("Id already exists!");
                }
                else
                {
                    ctx.Tags.Add(tag);
                    ctx.SaveChanges();
                }
            }
            if (closeable)
            {
                Close();
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            tag.Color = colorPicker.SelectedColor.ToString();
            Console.WriteLine(tag.Color);
        }
    }
}
