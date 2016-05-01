using System;
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
using HCI.Model;
using HCI.Model.Global;

namespace HCI.GUI
{
    /// <summary>
    /// Interaction logic for TypeDialog.xaml
    /// </summary>
    public partial class TypeDialog : Window
    {
        private Model.Type type;

        public TypeDialog()
        {
            InitializeComponent();
            type = new Model.Type();

            tbDescription.DataContext = type;
            tbId.DataContext = type;
            tbName.DataContext = type;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {    
            using (var ctx = new DatabaseModel())
            {
                ctx.Types.Add(type);
                ctx.SaveChanges();
            }
            Close();
        }
    }
}
