using HCI.Model;
using HCI.Model.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using Microsoft.Win32;
using Xceed.Wpf.DataGrid.Views;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for PremisesTableView.xaml
    /// </summary>
    public partial class PremisesTableView : Window
    {
        Point startPoint = new Point();
        public ObservableCollection<HCI.Model.Type> Types { get; set; }
        public ObservableCollection<Premises> Premises { get; set; }
        public ObservableCollection<Tag> AllTags { get; set; }
        public ObservableCollection<Tag> SelectedTags { get; set; }
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
            cbHandicaple.DataContext = Selected;
            tbName.DataContext = Selected;
            dpOpeningDate.DataContext = Selected;
            cbPrice.ItemsSource = Globals.PriceCategories;
            cbReservations.DataContext = Selected;
            cbSmoking.DataContext = Selected;
            cbType.DataContext = Selected;
            imgIcon.DataContext = Selected;
            using (var ctx = new DatabaseModel())
            {
                Premises = new ObservableCollection<Premises>(ctx.Premises.Include(p => p.Tags).Include(p => p.Type));
                Types = new ObservableCollection<HCI.Model.Type>(ctx.Types);
                cbType.ItemsSource = Types;
            }
            enableFields(false);
            this.DataContext = this;
        }

        private void setSelected()
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Selected.Copy(Premises[dgrMain.SelectedIndex]);
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
                Selected.Price = "";
                Selected.AlcoholServing = "";
                Selected.PathImage = "photo1.png";
                Selected.OpeningDate = new DateTime();
                Selected.Tags = new ObservableCollection<Tag>();
            }

        }

        private void enableFields(bool e)
        {
            tbName.IsEnabled = e;
            tbDesc.IsEnabled = e;
            cbAlcohol.IsEnabled = e;
            cbPrice.IsEnabled = e;
            cbHandicaple.IsEnabled = e;
            cbSmoking.IsEnabled = e;
            cbReservations.IsEnabled = e;
            tbCapa.IsEnabled = e;
            dpOpeningDate.IsEnabled = e;
            cbType.IsEnabled = e;
            btnCancel.IsEnabled = e;
            btnDelete.IsEnabled = e;
            cbType.IsEnabled = e;
            btnAddNewType.IsEnabled = e;
            btnSave.IsEnabled = e;
            lvAllTags.IsEnabled = e;
            lvSelected.IsEnabled = e;
            button.IsEnabled = e;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                Console.WriteLine("***** indeks selektovanog: " + dgrMain.SelectedIndex);
                setSelected();
                Console.WriteLine("******* id selektovanog: " + Selected.Id);
                Console.WriteLine("******* tip selektovanog: " + Selected.Type);
                if (Selected.PathImage == "photo1.png")
                {
                    Console.WriteLine("uso");
                    Selected.PathImage = Selected.Type.PathImage.ToString();
                }
                else {
                    Selected.PathImage = Selected.PathImage.ToString();
                }

                Console.WriteLine("Slika selektovanog: " + Selected.PathImage);

                using (var ctx = new DatabaseModel())
                {
                    AllTags = new ObservableCollection<Tag>(ctx.Tags);
                    SelectedTags = new ObservableCollection<Tag>(Selected.Tags);
                    foreach (Tag tag in SelectedTags)
                    {
                        AllTags.Remove(tag);
                    }

                    lvAllTags.ItemsSource = AllTags;
                    lvAllTags2.ItemsSource = AllTags;
                    lvSelected.ItemsSource = SelectedTags;
                    //                cbType.ItemsSource = new ObservableCollection<HCI.Model.Type>(ctx.Types);
                }

                enableFields(true);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //mali hack jer se izgubi selektovani jedino ovde
            if (Selected.Type == null || string.IsNullOrEmpty(Selected.Type.Id))
            {
                MessageBox.Show("Type must be set!");
                return;
            }
            int sIndex = dgrMain.SelectedIndex;
            Selected.Tags = SelectedTags;

            Premises[dgrMain.SelectedIndex].Copy(Selected);

            using (var ctx = new DatabaseModel())
            {
                ctx.UpdatePremises(Premises[dgrMain.SelectedIndex]);
            }
            dgrMain.SelectedIndex = sIndex;
            Globals.UpdatePremises();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgrMain.SelectedIndex != -1)
            {
                //                Premises.Remove(Premises[dgrMain.SelectedIndex]);

                using (var ctx = new DatabaseModel())
                {
                    ctx.Entry(Premises[dgrMain.SelectedIndex]).State = EntityState.Deleted;
                    ctx.SaveChanges();
                    Premises = new ObservableCollection<Premises>(ctx.Premises.Include(p => p.Tags).Include(p => p.Type).ToList());
                    //                    foreach (Premises p in Premises)
                    //                    {
                    //                        p.Type = ctx.Types.Single(t => t.Id == p.TypeId);
                    //                    }
                    dgrMain.ItemsSource = Premises;
                }
                dgrMain.SelectedIndex = -1;
                setSelected();
                enableFields(false);
                Globals.UpdatePremises();
            }
            else {
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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseImage = new OpenFileDialog();
            chooseImage.Filter = "Image files (*.png; *.jpg; *.jpeg; *.ico)| *.png; *.jpeg; *.jpg; *.ico|All files(*.*)|*.*";

            if (chooseImage.ShowDialog() == true)
            {
                Selected.PathImage = chooseImage.FileName;
                imgIcon.Source = new BitmapImage(new Uri(Selected.PathImage, UriKind.RelativeOrAbsolute));
            }
        }

        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Tag t = listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem) as Tag;

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", t);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
        }

        private void ListView_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void ListView_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Tag student = e.Data.GetData("myFormat") as Tag;
                AllTags.Remove(student);
                SelectedTags.Remove(student);
                SelectedTags.Add(student);
                Console.WriteLine(SelectedTags.ToString());
            }
        }

        private void AllList_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Tag student = e.Data.GetData("myFormat") as Tag;
                SelectedTags.Remove(student);
                AllTags.Remove(student);
                AllTags.Add(student);
                Console.WriteLine(SelectedTags.ToString());
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
