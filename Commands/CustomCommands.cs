using HCI.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Diagnostics;

namespace HCI.Commands
{
    public static class CustomCommands
    {
        // EXIT KOMANDA
        public static readonly ICommand testKomanda = new OpenPremisesCommand();
        public static readonly ICommand testKomanda2 = new OpenTypeCommand();
        public static readonly ICommand testKomanda3 = new OpenTagCommand();

        public static readonly ICommand editPCom = new EditPremisesCommand();
        public static readonly ICommand editTypeCom = new EditTypeCommand();
        public static readonly ICommand editTagCom = new EditTagCommand();

        public static readonly ICommand helpCom = new HelpCommand();
        public static readonly ICommand aboutCom = new AboutCommand();
        public static readonly ICommand tutorialCom = new TutorialCommand();
        public static readonly ICommand exitCom = new ExitCommand();

    }

    public class ExitCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            MainWindow.Close();
        }
    }

    public class OpenPremisesCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            PremisesDialog w = new PremisesDialog();
            w.Show();
        }
    }

    public class OpenTypeCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            TypeDialog w = new TypeDialog();
            w.Show();
        }
    }

    public class OpenTagCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            TagsDialog w = new TagsDialog();
            w.Show();
        }
    }

    public class EditPremisesCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            PremisesTableView w = new PremisesTableView();
            w.Show();
        }
    }

    public class EditTypeCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            TypeTableView w = new TypeTableView();
            w.Show();
        }
    }

    public class EditTagCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            TagTableView w = new TagTableView();
            w.Show();
        }
    }
    public class HelpCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Process.Start("..\\..\\HelpSystem\\Helper\\Helper.chm");
        }
    }
    public class AboutCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            Process.Start("..\\..\\HelpSystem\\About\\Helper.chm");
        }
    }

    public class TutorialCommand : ICommand //da hocem da radimo i sa precicama, trebalo bi da nasledom RoutedUICommand, recimo
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            //InteractivTutorial_Click();

        }
    }
}
