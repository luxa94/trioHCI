using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI.Model.Global
{
    class Globals
    {
        public static String[] AlcoholServings = { "", "does not serve", "serve before 23:00", "serve all the time" };
        public static String[] PriceCategories = { "", "low prices", "medium prices", "high prices", "extremely high pricesd" };

        public static ObservableCollection<Premises> AllPremises = new ObservableCollection<Premises>();
        public static Dictionary<String, DraggablePushpin> pushpins = new Dictionary<String, DraggablePushpin>();


        public static void UpdatePremises()
        {
            using (var ctx = new DatabaseModel())
            {
                AllPremises.Clear();
                foreach (var premises in ctx.Premises.Include(p => p.Type))
                {
                    AllPremises.Add(premises);
                    if (pushpins.ContainsKey(premises.Id))
                    {
                        DraggablePushpin pin = pushpins[premises.Id];
                        pin.Template = PinTemplateFactory.getTemplate(premises);
                        pin.Map.Children.Remove(pin);
                        pin.Map.Children.Add(pin);

                    }
                }
            }
        }

        public static void updateForFilter(String filter)
        {
            using (var ctx = new DatabaseModel())
            {
                AllPremises.Clear();

                foreach (var premises in ctx.Premises.Include(p => p.Type))
                {
                    bool m = matches(filter, premises);
                    if (m)
                    {
                        AllPremises.Add(premises);
                    }
                    if (pushpins.ContainsKey(premises.Id))
                    {
                        DraggablePushpin pin = pushpins[premises.Id];
                        pin.Template = PinTemplateFactory.getTemplate(premises);
                        pin.Map.Children.Remove(pin);
                        if (m)
                        {
                            pin.Map.Children.Add(pin);
                        }

                    }
                }
            }
        }

        private static bool matches(string filter, Premises premises)
        {
            return (premises.Id?.Contains(filter) ?? false)             ||
                   (premises.Name?.Contains(filter) ?? false)           || 
                   (premises.Type.Name?.Contains(filter) ?? false)      || 
                   (premises.Type.Id?.Contains(filter) ?? false)        ||
                   (premises.Description?.Contains(filter) ?? false)    || 
                   (premises.AlcoholServing?.Contains(filter) ?? false) || 
                   (premises.Price?.Contains(filter) ?? false);

        }

    }
}
