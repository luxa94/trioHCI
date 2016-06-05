using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HCI.Commands
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand HelloWorld = new RoutedUICommand(
            "Hello World",
            "HelloWorld",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.H, ModifierKeys.Control),
                new KeyGesture(Key.H, ModifierKeys.Alt | ModifierKeys.Control)
            }
        );
    }
}
