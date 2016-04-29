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
        public Premises Selected { get; set; }

        public TableView()
        {
            InitializeComponent();
            Selected = new Premises();
            this.DataContext = this;
            tbId.DataContext = Selected;
            tbAlc.DataContext = Selected;
            tbCapa.DataContext = Selected;
            tbDesc.DataContext = Selected;
            tbHand.DataContext = Selected;
            tbName.DataContext = Selected;
            tbOpen.DataContext = Selected;
            tbPrice.DataContext = Selected;
            tbReser.DataContext = Selected;
            tbSmok.DataContext = Selected;
            Premises = Globals.Premisses;

        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Console.WriteLine("***** indeks selektovanog: " + dgrMain.SelectedIndex);
                Selected = Premises[dgrMain.SelectedIndex];
                //deep copy
                Selected = new Premises(
                    Premises[dgrMain.SelectedIndex].Id,
                    Premises[dgrMain.SelectedIndex].Name,
                    Premises[dgrMain.SelectedIndex].Description,
                    Premises[dgrMain.SelectedIndex].AlcoholServing,
                    Premises[dgrMain.SelectedIndex].Price,
                    Premises[dgrMain.SelectedIndex].IsHandicapable,
                    Premises[dgrMain.SelectedIndex].IsSmokingAlowed,
                    Premises[dgrMain.SelectedIndex].IsReservingAvailable,
                    Premises[dgrMain.SelectedIndex].Capacity,
                    Premises[dgrMain.SelectedIndex].OpeningDate
                    );
                Console.WriteLine("******* id selektovanog: " + Selected.Id);
            }
            
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

        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Premises.Remove(Premises[dgrMain.SelectedIndex]);
            tbName.IsEnabled = false;
            tbDesc.IsEnabled = false;
            tbAlc.IsEnabled = false;
            tbPrice.IsEnabled = false;
            tbHand.IsEnabled = false;
            tbSmok.IsEnabled = false;
            tbReser.IsEnabled = false;
            tbCapa.IsEnabled = false;
            tbOpen.IsEnabled = false;
            button.IsEnabled = false;
            button1.IsEnabled = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
          

        }

        private void btnAddNewType_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TypeDialog();
            w.ShowDialog();
        }
    }
}
