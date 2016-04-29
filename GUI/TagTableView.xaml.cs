using HCI.Model.Global;
using HCI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Model.Tag> Tags { get; set; }
        public bool isSelected = false;

        public TagTableView()
        {
            InitializeComponent();
            this.DataContext = this;
            Tags = Globals.Tags;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSelected = true;
        }



        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected == true)
            {
                Tags.Remove(Tags[dgrMain.SelectedIndex]);
            }
            else
            {
                MessageBox.Show("You must select one item!");
            }
        }
    }
}
