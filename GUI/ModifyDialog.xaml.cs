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

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for ModifyDialog.xaml
    /// </summary>
    public partial class ModifyDialog : Window
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

        public ModifyDialog()
        {
            InitializeComponent();
            premises = new Premises();

            tbId.DataContext = premises;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("saved");
            this.Close();
            
        }
    }
}
