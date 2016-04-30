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
        public static String[] AlcoholServings = { "does not serve", "serve before 23:00", "serve all the time" };
        public static String[] PriceCategories = { "low prices", "medium prices", "high prices", "extremely high pricesd" };

        public static ObservableCollection<Premises> Premisses = new ObservableCollection<Premises>();
        public static ObservableCollection<Type> Types = new ObservableCollection<Type>();
        public static ObservableCollection<Tag> Tags = new ObservableCollection<Tag>();

       
    }
}
