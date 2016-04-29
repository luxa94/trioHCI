using System;
using HCI.Model;
using HCI.Model.Global;
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
using System.Collections.ObjectModel;


namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for TableView.xaml
    /// </summary>
    public partial class TableView : Window
    {
        public ObservableCollection<Premises> Premises{ get; set;}
        public bool isSelected = false;

        public TableView()
        {
            InitializeComponent();
            this.DataContext = this;
            Premises = Globals.Premisses;

        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isSelected = true;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (isSelected == true)
            {
                Premises.Remove(Premises[dgrMain.SelectedIndex]);
            }
            else
            {
                MessageBox.Show("You must select one item!");
            }
        }
    }
}
