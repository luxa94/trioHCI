using HCI.GUI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<HCI.Model.Type> AllTypes { get; set; }
        public ObservableCollection<HCI.Model.Premises> AllPremises { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            using (var ctx = new DatabaseModel())
            {
                AllTypes = new ObservableCollection<HCI.Model.Type>(ctx.Types);
                AllPremises = new ObservableCollection<Model.Premises>(ctx.Premises);
            }
            foreach (Premises p in AllPremises)
            {
                foreach(Model.Type t in AllTypes)
                {
                    if (p.Type == t)
                    {
                        t.Members.Add(p);
                        break;
                    }
                }
            }

            this.DataContext = this;
           
        }

        private void btnAddBusiness_Click(object sender, RoutedEventArgs e)
        {
            Window w = new PremisesDialog();
            w.ShowDialog();
        }

        private void btnAddType_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TypeDialog();
            w.ShowDialog();
        }

        private void btnAddTag_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TagsDialog();
            w.ShowDialog();
        }

        private void btnViewAllPremises_Click(object sender, RoutedEventArgs e)
        {
            Window w = new PremisesTableView();
            w.ShowDialog();
        }

        private void btnViewAllTypes_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TypeTableView();
            w.ShowDialog();
        }

        private void btnViewAllTags_Click(object sender, RoutedEventArgs e)
        {
            Window w = new TagTableView();
            w.ShowDialog();
        }
    }
}
