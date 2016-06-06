using HCI.Model.Global;
using HCI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using Microsoft.Win32;
using Type = HCI.Model.Type;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for TypeTableView.xaml
    /// </summary>
    public partial class TypeTableView : Window
    {
        public ObservableCollection<HCI.Model.Type> Types { get; set; }
        public HCI.Model.Type Selected { get; set; }

        public TypeTableView()
        {
            InitializeComponent();
            Selected = new HCI.Model.Type();
            this.DataContext = this;
            using (var ctx = new DatabaseModel())
            {
                Types = new ObservableCollection<HCI.Model.Type>(ctx.Types);
            }
            tbId.DataContext = Selected;
            tbName.DataContext = Selected;
            tbDescription.DataContext = Selected;
            imgIcon.DataContext = Selected;
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                //deep copy
                Selected.Id = Types[dgrMain.SelectedIndex].Id;
                Selected.Name = Types[dgrMain.SelectedIndex].Name;
                Selected.Description = Types[dgrMain.SelectedIndex].Description;
                Selected.PathImage = Types[dgrMain.SelectedIndex].PathImage;
            }
            else
            {
                //deep copy
                Selected.Id = "";
                Selected.Name = "";
                Selected.Description = "";
                Selected.PathImage = "photo1.png";
            }
        }

        private void enableFields(bool e)
        {
            tbName.IsEnabled = e;
            tbDescription.IsEnabled = e;
            btnCancel.IsEnabled = e;
            btnDelete.IsEnabled = e;
            btnSave.IsEnabled = e;
            btnBrowse.IsEnabled = e;
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
                using (var ctx = new DatabaseModel())
                {
                    var deletable = true;
                    if (ctx.Premises.Include(p => p.Type).Any(p => p.Type.Id == Selected.Id))
                    {
                        var result =
                            MessageBox.Show("There are premises connected to that type.\nDelete the premises as well?",
                                "Worning", MessageBoxButton.YesNo);
                        deletable = result == MessageBoxResult.Yes;
                    }
                    if (deletable)
                    {
                        var result2 =
                           MessageBox.Show("You are trying to delete selected type. Are you sure?",
                               "Worning", MessageBoxButton.YesNo);

                        if (result2 == MessageBoxResult.Yes)
                        {
                            ctx.Entry(Types[dgrMain.SelectedIndex]).State = EntityState.Deleted;
                            foreach (var p in ctx.Premises.Include(p => p.Type).Where(p => p.Type.Id == Selected.Id))
                            {
                                ctx.Entry(p).State = EntityState.Deleted;
                            }
                            ctx.SaveChanges();
                            Types = new ObservableCollection<Type>(ctx.Types);
                            dgrMain.ItemsSource = Types;
                            Globals.UpdatePremises();
                        }
                    }
                }
                dgrMain.SelectedIndex = -1;
                setSelected();
                enableFields(false);
            }
            else
            {
                MessageBox.Show("You have to select one type from table!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            setSelected();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Changes are successfuly saved.");
            int sIndex = dgrMain.SelectedIndex;
            Types[dgrMain.SelectedIndex].Id = Selected.Id;
            Types[dgrMain.SelectedIndex].Name = Selected.Name;
            Types[dgrMain.SelectedIndex].Description = Selected.Description;
            Types[dgrMain.SelectedIndex].PathImage = Selected.PathImage;
            using (var ctx = new DatabaseModel())
            {
                ctx.Entry(Types[dgrMain.SelectedIndex]).State = EntityState.Modified;
                ctx.SaveChanges();
            }
            dgrMain.SelectedIndex = sIndex;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseImage = new OpenFileDialog();
            chooseImage.Filter =
                "Image files (*.png; *.jpg; *.jpeg; *.ico)| *.png; *.jpeg; *.jpg; *.ico|All files(*.*)|*.*";

            if (chooseImage.ShowDialog() == true)
            {
                Selected.PathImage = chooseImage.FileName;
            }
        }
    }
}