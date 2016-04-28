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
using System.IO;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for PremisesDialog.xaml
    /// </summary>

    public partial class PremisesDialog : Window
    {
        private Premises premises;

        public Premises Premises
        {
            get
            {
                return premises;
            }
            set
            {
                premises = value;
            }
        }

        public PremisesDialog()
        {
            InitializeComponent();
            premises = new Premises();

            tbId.DataContext = premises;
            tbName.DataContext = premises;
            tbDescription.DataContext = premises;
            cbHandicapable.DataContext = premises;
            cbReservations.DataContext = premises;
            cbSmoking.DataContext = premises;

            cbAlcohol.ItemsSource = Globals.AlcoholServings;
            cbAlcohol.DataContext = premises;

            cbPrice.ItemsSource = Globals.PriceCategories;
            cbPrice.DataContext = premises;

            tbCapacity.DataContext = premises;
            dpOpeningDate.DataContext = premises;

            cbType.ItemsSource = Globals.Types;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("saved");
       //     Window w = new TableView();
       //     w.ShowDialog();
       //     w.Close();

        }
        private void btnAddNewType_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TypeDialog();
            w.ShowDialog();
        }
    }
}
