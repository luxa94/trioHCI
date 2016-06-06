using HCI.Model.Global;
using HCI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Interaction logic for TagTableView.xaml
    /// </summary>
    public partial class TagTableView : Window
    {
        public ObservableCollection<Tag> Tags { get; set; }
        public Tag Selected { get; set; }
  

        public TagTableView()
        {
            InitializeComponent();
            Selected = new Tag();
            this.DataContext = this;
            using (var ctx = new DatabaseModel())
            {
                Tags = new ObservableCollection<Tag>(ctx.Tags);
            }
            tbId.DataContext = Selected;
            colorPicker.DataContext = Selected;
            tbDescription.DataContext = Selected;
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                //deep copy
                Selected.Id = Tags[dgrMain.SelectedIndex].Id;
                Selected.Color = Tags[dgrMain.SelectedIndex].Color;
                Selected.Description = Tags[dgrMain.SelectedIndex].Description;
            }
            else
            {
                //deep copy
                Selected.Id = "";
                Selected.Color = "FFFFFFFF";
                Selected.Description = "";
            }
        }

        private void enableFields(bool e)
        {
            colorPicker.IsEnabled = e;
            tbDescription.IsEnabled = e;
            btnCancel.IsEnabled = e;
            btnDelete.IsEnabled = e;
            btnSave.IsEnabled = e;
        }
        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                setSelected();
            }
            enableFields(true);
        }

        private void ColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Selected.Color = colorPicker.SelectedColor.ToString();
                Console.WriteLine(Tags[dgrMain.SelectedIndex].Color);
            }
        }


        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                using (var ctx = new DatabaseModel())
                {
                    var result =
                            MessageBox.Show("You are trying to delete selected tag. Are you sure?",
                                "Worning", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {

                        ctx.Entry(Tags[dgrMain.SelectedIndex]).State = EntityState.Deleted;
                        ctx.SaveChanges();
                        Tags = new ObservableCollection<Tag>(ctx.Tags);
                        dgrMain.ItemsSource = Tags;
                    }
                    setSelected();
                    dgrMain.SelectedIndex = -1;
                    enableFields(false);
                }
            }
            else {
                MessageBox.Show("You have to select one tag from table!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            setSelected();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Changes are successfuly saved.");
            int sIndex = dgrMain.SelectedIndex;
            Tags[dgrMain.SelectedIndex].Id = Selected.Id;
            Tags[dgrMain.SelectedIndex].Description = Selected.Description;
            Tags[dgrMain.SelectedIndex].Color = Selected.Color;
            using (var ctx = new DatabaseModel())
            {
                ctx.Entry(Tags[dgrMain.SelectedIndex]).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            dgrMain.SelectedIndex = sIndex;
        }
    }
}
