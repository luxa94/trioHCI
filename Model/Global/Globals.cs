using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI.Model.Global
{
    public class Globals
    {
        public static String[] AlcoholServings = { "ne služi", "služi samo do 23:00", "služi i kasno noću" };
        public static String[] PriceCategories = { "niske cene", "srednje cene", "visoke cene", "izuzetno visoke cene" };

        public static ObservableCollection<Premises> Premisses = new ObservableCollection<Premises>();
        public static ObservableCollection<Type> Types = new ObservableCollection<Type>();
        public static ObservableCollection<Tag> Tags = new ObservableCollection<Tag>();

       
    }
}
