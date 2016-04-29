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

        public TableView()
        {
            InitializeComponent();
            this.DataContext = this;
            Premises = Globals.Premisses;

        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbName.IsEnabled = true;
            tbDesc.IsEnabled = true;
            tbAlc.IsEnabled = true;
            tbPrice.IsEnabled = true;
            tbHand.IsEnabled = true;
            tbSmok.IsEnabled = true;
            tbReser.IsEnabled = true;
            tbCapa.IsEnabled = true;
            tbOpen.IsEnabled = true;
            button.IsEnabled = true;
            button1.IsEnabled = true;
            cbType.IsEnabled = true;
            btnAddNewType.IsEnabled = true;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Premises.Remove(Premises[dgrMain.SelectedIndex]);
        }

        private void btnAddNewType_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TypeDialog();
            w.ShowDialog();
        }
    }
}
