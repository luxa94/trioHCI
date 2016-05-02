using HCI.Model;
using HCI.Model.Global;
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
    /// Interaction logic for PremisesTableView.xaml
    /// </summary>
    public partial class PremisesTableView : Window
    {
        public ObservableCollection<Premises> Premises { get; set; }
        public Premises Selected { get; set; }

        public PremisesTableView()
        {
            InitializeComponent();
            Selected = new Premises();
            this.DataContext = this;
            tbId.DataContext = Selected;
            cbAlcohol.ItemsSource = Globals.AlcoholServings;
            cbAlcohol.DataContext = this;
            tbCapa.DataContext = Selected;
            tbDesc.DataContext = Selected;
            tbHand.DataContext = Selected;
            tbName.DataContext = Selected;
            tbOpen.DataContext = Selected;
            cbPrice.ItemsSource = Globals.PriceCategories;
            tbReser.DataContext = Selected;
            tbSmok.DataContext = Selected;
            cbType.DataContext = Selected;
            cbType.ItemsSource = Globals.Types;
            Premises = Globals.Premisses;
            enableFields(false);
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                //deep copy
                Selected.Id = Premises[dgrMain.SelectedIndex].Id;
                Selected.Name = Premises[dgrMain.SelectedIndex].Name;
                Selected.Description = Premises[dgrMain.SelectedIndex].Description;
                Selected.AlcoholServing = Premises[dgrMain.SelectedIndex].AlcoholServing;
                cbAlcohol.SelectedItem = Selected.AlcoholServing;
                Selected.Price = Premises[dgrMain.SelectedIndex].Price;
                cbPrice.SelectedItem = Selected.Price;
                Selected.IsSmokingAlowed = Premises[dgrMain.SelectedIndex].IsSmokingAlowed;
                Selected.IsHandicapable = Premises[dgrMain.SelectedIndex].IsHandicapable;
                Selected.IsReservingAvailable = Premises[dgrMain.SelectedIndex].IsReservingAvailable;
                Selected.Capacity = Premises[dgrMain.SelectedIndex].Capacity;
                Selected.OpeningDate = Premises[dgrMain.SelectedIndex].OpeningDate;
                Selected.Type = Premises[dgrMain.SelectedIndex].Type;
                cbType.SelectedItem = Selected.Type.Id;
            }
           
            else
            {
                //deep copy
                Selected.Id = "";
                Selected.Name = "";
                Selected.Description = "";
                Selected.IsHandicapable = false;
                Selected.IsReservingAvailable = false;
                Selected.IsSmokingAlowed = false;
                Selected.Capacity = 0;
                Selected.OpeningDate = new DateTime();
                
            }
   
        }

        private void enableFields(bool e)
        {
            tbName.IsEnabled = e;
            tbDesc.IsEnabled = e;
            cbAlcohol.IsEnabled = e;
            cbPrice.IsEnabled = e;
            tbHand.IsEnabled = e;
            tbSmok.IsEnabled = e;
            tbReser.IsEnabled = e;
            tbCapa.IsEnabled = e;
            tbOpen.IsEnabled = e;
            cbType.IsEnabled = e;
            btnCancel.IsEnabled = e;
            btnDelete.IsEnabled = e;
            btnAddNewType.IsEnabled = e;
            btnSave.IsEnabled = e;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Console.WriteLine("***** indeks selektovanog: " + dgrMain.SelectedIndex);
                setSelected();
                Console.WriteLine("******* id selektovanog: " + Selected.Id);
                Console.WriteLine("******* tip selektovanog: " + Selected.Type);
            }
            enableFields(true);

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //mali hack jer se izgubi selektovani jedino ovde
            int sIndex = dgrMain.SelectedIndex;
            Premises[dgrMain.SelectedIndex] = Selected;
            dgrMain.SelectedIndex = sIndex;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Premises.Remove(Premises[dgrMain.SelectedIndex]);
                setSelected();
                enableFields(false);
            }
            else{
                MessageBox.Show("You have to select one premise from table!");
            }
        }

        private void btnAddNewType_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TypeDialog();
            w.ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            setSelected();
        }
    }
}
