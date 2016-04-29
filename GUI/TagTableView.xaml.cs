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
    /// Interaction logic for TagTableView.xaml
    /// </summary>
    public partial class TagTableView : Window
    {
        public ObservableCollection<Model.Tag> Tags { get; set; }
        public TagTableView()
        {
            InitializeComponent();
            this.DataContext = this;
            Tags = Globals.Tags;
        }
    }
}
