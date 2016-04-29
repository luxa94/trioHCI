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
    /// Interaction logic for TypeTableView.xaml
    /// </summary>
    public partial class TypeTableView : Window
    {
        public ObservableCollection<Model.Type> Types { get; set; }

        public TypeTableView()
        {
            InitializeComponent();
            this.DataContext = this;
            Types = Globals.Types;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbName.IsEnabled = true;
            tbDescription.IsEnabled = true;
            button.IsEnabled = true;
            button1.IsEnabled = true;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            Types.Remove(Types[dgrMain.SelectedIndex]); 
        }
    }
}
