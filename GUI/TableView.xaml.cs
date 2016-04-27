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
            Premises = new ObservableCollection<Premises>();
            Premises.Add(new Premises( "1", "Pingvin", "Najbolji!", "yes", "low", true, true, true, 10, new DateTime(2011, 6, 10)));
            Premises.Add(new Premises("2", "Gusan", "Best beer!", "yes", "low", false, true, true, 50, new DateTime(2011, 7, 10)));
            Premises.Add(new Premises("3", "Popaj", "Najbolji!", "yes", "low", true, true, true, 20, new DateTime(2011, 8, 10)));
            Console.WriteLine();
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
