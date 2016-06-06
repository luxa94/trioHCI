using HCI.GUI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Maps.MapControl.WPF;

namespace HCI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool Tutorial = false;
        public ObservableCollection<HCI.Model.Type> AllTypes { get; set; }
        public ObservableCollection<HCI.Model.Premises> AllPremises { get; set; }
        private Dictionary<Premises, DraggablePushpin> pushpins;

        private Point startPoint;

        public MainWindow()
        {
            InitializeComponent();

            pushpins = new Dictionary<Premises, DraggablePushpin>();

            using (var ctx = new DatabaseModel())
            {
                AllTypes = new ObservableCollection<HCI.Model.Type>(ctx.Types);
                AllPremises = new ObservableCollection<Model.Premises>(ctx.Premises);
            }

            foreach (var p in AllPremises)
            {
                if (p.MapNumber != 0)
                {
                    Map m = intToMap(p.MapNumber);
                    Location l = new Location(p.Latitude, p.Longitude);
                    var pin = new DraggablePushpin(m, l, p);
                    pushpins.Add(p, pin);
                    m.Children.Add(pin);
                }
            }

            this.DataContext = this;

        }

        private void HelloWorld_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Hello world!");
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
            if (!Tutorial)
            {
                Window w = new TagsDialog();
                w.ShowDialog();
            }
            else
            {
                Window w = new TagsDialog(true);
                w.ShowDialog();
                EnableAll(true);
                btnAddTag.Background = null;
            }
            
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

        private void treeview_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void treeview_MouseMove(object sender, MouseEventArgs e)
        {

            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Premises p = listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem) as Premises;

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("premises", p);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
                
            }
        }

        private void map_Drop(object sender, DragEventArgs e)
        {
            var m = sender as Map;
            if (e.Data.GetDataPresent("premises"))
            {
                Premises p = e.Data.GetData("premises") as Premises;
                Point mousePosition = e.GetPosition(this);
                Point point = m.TransformToAncestor(this).Transform(new Point(0, 0));
                mousePosition.X -= point.X;
                mousePosition.Y -= point.Y;
                Location pinLocation = m.ViewportPointToLocation(mousePosition);
                p.Latitude = pinLocation.Latitude;
                p.Longitude = pinLocation.Longitude;
                p.MapNumber = mapToInt(m);

                DraggablePushpin pin = null;

                if (pushpins.ContainsKey(p))
                {
                    pin = pushpins[p];
                    pin.Map.Children.Remove(pin);
                    pin.Map = m;
                }
                else
                {
                    pin = new DraggablePushpin(m, pinLocation, p);
                    pushpins.Add(p, pin);
                }
                pin.Location = pinLocation;

                using (var ctx = new DatabaseModel())
                {
                    ctx.Entry(p).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
                
                m.Children.Add(pin);
            }
            e.Handled = true;
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
        
        private int mapToInt(Map map)
        {
            if (map == myMap1)
            {
                return 1;
            }
            else if (map == myMap2)
            {
                return 2;
            }
            else if (map == myMap3)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        private Map intToMap(int i)
        {
            switch (i)
            {
                case 1:
                    return myMap1;
                case 2:
                    return myMap2;
                case 3:
                    return myMap3;
                case 4:
                    return myMap;
            }
            return null;
        }

        private void EnableAll(bool b) {

                btnViewAllPremises.IsEnabled = b;
                btnViewAllTypes.IsEnabled = b;
                btnViewAllTags.IsEnabled = b;
                btnAddBusiness.IsEnabled = b;
                btnAddType.IsEnabled = b;
                listView.IsEnabled = b;
                myMap.IsEnabled = b;
                myMap1.IsEnabled = b;
                myMap2.IsEnabled = b;
                myMap3.IsEnabled = b;
                meniMeni.IsEnabled = b;
            // sve je enableovano sem btnAddTag posto on uvek mora biti dostupan
        }
        private void InteractivTutorial_Click(object sender, RoutedEventArgs e)
        {
            Tutorial = true;
            MessageBox.Show("Welcome to Interactiv Tutorial for adding new tag. Folow next steps.");
            EnableAll(false);
            btnAddTag.Background = Brushes.LightCoral;
            MessageBox.Show("Click on button \"Add new tag.\".");
   
        }
    }
}

