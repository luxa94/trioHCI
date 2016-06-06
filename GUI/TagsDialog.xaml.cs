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
        private bool Tutorial = false;

        public TagsDialog()
        {
            InitializeComponent();
            tag = new Tag();
            tbId.DataContext = tag;
            colorPicker.DataContext = tag;
            tbDescription.DataContext = tag;
        }

        private void EnableAll(bool b) {

            Tutorial = true;
            tbId.IsEnabled = b;
            colorPicker.IsEnabled = b;
            tbDescription.IsEnabled= b;
            btnSave.IsEnabled = b;
            btnCancel.IsEnabled = b;
            

        }
        
        public TagsDialog(bool b)
        {
            InitializeComponent();
            tag = new Tag();
            tbId.DataContext = tag;
            colorPicker.DataContext = tag;
            tbDescription.DataContext = tag;

            MessageBox.Show("Now enter your tag id. \nClick Enter when you finish for next step.");
            EnableAll(false);
            tbId.IsEnabled = true;     

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Tutorial)
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
            else
            {
                Close();
                MessageBox.Show("Well done! Now you know how to add new tag.");
            }   
        }

        private void ColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            tag.Color = colorPicker.SelectedColor.ToString();
            Console.WriteLine(tag.Color);
        }

        private void tbDescription_KeyUp(object sender, KeyEventArgs e)
        {
            if (Tutorial)
            {
                if (e.Key == Key.Enter)
                {
                    
                    MessageBox.Show("Now, if you want to save this tag click on button \"Save\".\nIf you do not want to save this tag click button \"Cancel\". \nNOTICE:This is only demonstration and this tag does not exist for real in application.");
                    tbId.IsEnabled = false;
                    colorPicker.IsEnabled = false;
                    tbDescription.IsEnabled = false;
                    btnSave.IsEnabled = true;
                    btnCancel.IsEnabled = true;
                    

                }
            }
        }

        private void tbColor_KeyUp(object sender, KeyEventArgs e)
        {
            if (Tutorial)
            {
                if (e.Key == Key.Enter)
                {
                    tbId.IsEnabled = false;
                    colorPicker.IsEnabled = false;
                    tbDescription.IsEnabled = true;
                    MessageBox.Show("Now enter tags description.\nClick Enter when you finish for next step.");
                    
                }
            }
        }

        private void tbId_KeyUp(object sender, KeyEventArgs e)
        {
            if (Tutorial)
            {
                if (e.Key == Key.Enter)
                {
                    if (tbId.Text == "")
                        MessageBox.Show("Text box \"Id\" can not be empty.");
                    else { 
                        MessageBox.Show("Now select color for tag.\nClick Enter when you finish for next step.");
                        tbId.IsEnabled = false;
                        colorPicker.IsEnabled = true;
                    }
                }
            }
        }
    }
}
