using HCI.Model.Global;
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
using System.Windows.Shapes;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for TypeTableView.xaml
    /// </summary>
    public partial class TypeTableView : Window
    {
        public ObservableCollection<HCI.Model.Type> Types { get; set; }
        public HCI.Model.Type Selected { get;  set;}

        public TypeTableView()
        {
            InitializeComponent();
            Selected = new HCI.Model.Type();
            this.DataContext = this;
            tbId.DataContext = Selected;
            tbName.DataContext = Selected;
            tbDescription.DataContext = Selected;
            Types = Globals.Types;
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                //deep copy
                Selected.Id = Types[dgrMain.SelectedIndex].Id;
                Selected.Name = Types[dgrMain.SelectedIndex].Name;
                Selected.Description = Types[dgrMain.SelectedIndex].Description;
            }
            else
            {
                //deep copy
                Selected.Id = "";
                Selected.Name = "";
                Selected.Description = "";
            }
        }

        private void enableFields(bool e)
        {
            tbName.IsEnabled = e;
            tbDescription.IsEnabled = e;
            btnCancel.IsEnabled = e;
            btnDelete.IsEnabled = e;
            btnSave.IsEnabled = e;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
   
            if (dgrMain.SelectedIndex != -1)
            {
                setSelected();
            }
            enableFields(true);
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Types.Remove(Types[dgrMain.SelectedIndex]);
                setSelected();
                enableFields(false);
            }
            else {
                MessageBox.Show("You have to select one premise from table!");
            } 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            setSelected();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int sIndex = dgrMain.SelectedIndex;
            Types[dgrMain.SelectedIndex] = Selected;
            dgrMain.SelectedIndex = sIndex;
        }
    }
}
