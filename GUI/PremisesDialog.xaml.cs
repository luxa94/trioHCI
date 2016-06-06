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
using HCI.GUI.Model.Global;
using Microsoft.Win32;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for PremisesDialog.xaml
    /// </summary>

    public partial class PremisesDialog : Window
    {
        Point startPoint = new Point();
        private Premises premises;
        private ObservableCollection<Type> Types;
        public ObservableCollection<Tag> AllTags { get; set; }
        public ObservableCollection<Tag> SelectedTags { get; set; }

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
            SelectedTags = new ObservableCollection<Tag>();

            using (var ctx = new DatabaseModel())
            {
                Types = new ObservableCollection<Type>(ctx.Types);
                AllTags = new ObservableCollection<Tag>(ctx.Tags);
                cbType.ItemsSource = Types;
                cbType.SelectedItem = Premises.Type;
                lvAllTags.ItemsSource = AllTags;
                
            }
            lvSelected.ItemsSource = SelectedTags;

            imgIcon.DataContext = premises;
            imgIcon.Source = new BitmapImage(new Uri(premises.PathImage, UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
         
            Console.WriteLine("saved");
            Console.WriteLine("******* datum sacuvanog: " + premises.OpeningDate);
            premises.Tags = new ObservableCollection<Tag>(SelectedTags);
            var closeable = true;
            using (var ctx = new DatabaseModel())
            {

                var p = new List<Premises>(ctx.Premises.Where(t => t.Id == premises.Id));
                if (string.IsNullOrEmpty(premises.Id))
                {
                    closeable = false;
                    MessageBox.Show("Premises id must be set!");
                }
                else if (p.Count > 0)
                {
                    closeable = false;
                    MessageBox.Show("Premises id already exists!");
                }
                else if (string.IsNullOrEmpty(premises.Name))
                {
                    closeable = false;
                    MessageBox.Show("Premises name must be set!");
                }
                else if (premises.Type == null || string.IsNullOrEmpty(premises.Type.Id))
                {
                    closeable = false;
                    MessageBox.Show("Type must be chosen!");
                }
                else
                {
                    ctx.AddPremises(premises);
                    Globals.UpdatePremises();
                    MessageBox.Show("New premises is successfuly saved.");
                }
               

            }
            // Serialization.serialize
            if (premises.PathImage == "photo1.png")
            {
                premises.PathImage = premises.Type.PathImage.ToString();
            }
            else
            {
                premises.PathImage = premises.PathImage.ToString();
            }

            if (closeable)
            {
                Close();
            }
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

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog chooseImage = new OpenFileDialog();
            chooseImage.Filter = "Image files (*.png; *.jpg; *.jpeg; *.ico)| *.png; *.jpeg; *.jpg; *.ico|All files(*.*)|*.*";

            if (chooseImage.ShowDialog() == true)
            {
                premises.PathImage = chooseImage.FileName;
                imgIcon.Source = new BitmapImage(new Uri(premises.PathImage, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
