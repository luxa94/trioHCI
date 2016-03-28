using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI.Model
{
    public class Premises
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String AlcoholServing { get; set; }
        public String Price { get; set; }
        public bool IsHandicapable { get; set; }
        public bool IsSmokingAlowed { get; set; }
        public bool IsReservingAvailable { get; set; }
        public int Capacity { get; set; }
        public DateTime OpeningDate { get; set; }

        public Type Type { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }

        public Premises()
        {
            Tags = new ObservableCollection<Tag>();
        }
    }
}
