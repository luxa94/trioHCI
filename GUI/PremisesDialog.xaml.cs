using HCI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
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
using System.Xml.Linq;
using HCI.Model.Global;
using Type = HCI.Model.Type;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for PremisesDialog.xaml
    /// </summary>

    public partial class PremisesDialog : Window
    {
        private Premises premises;
        private ObservableCollection<Model.Type> Types;

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

            cbType.DataContext = premises;
            using (var ctx = new DatabaseModel())
            {
                Types = new ObservableCollection<Type>(ctx.Types);
                cbType.ItemsSource = Types;
            }            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("saved");
            Console.WriteLine("******* datum sacuvanog: " + premises.OpeningDate);
            using (var ctx = new DatabaseModel())
            {
                premises.Type.Premises.Add(premises);
                ctx.Premises.AddOrUpdate(premises);
                ctx.Types.Attach(premises.Type);
                ctx.SaveChanges();
            }
            Close();
        }
        private void btnAddNewType_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TypeDialog();
            w.ShowDialog();
            // repopulate the type list if new type was added
            using (var ctx = new DatabaseModel())
            {
                Types = new ObservableCollection<Type>(ctx.Types);
                cbType.ItemsSource = Types;
            }
        }
    }
}
