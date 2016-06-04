using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HCI.Commands
{
    public static class CustomCommands
    {
        public static readonly ICommand testKomanda = new OpenDialogCommand();
    }

    public class OpenDialogCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MessageBox.Show("Hello, world!");
        }
    }
}
