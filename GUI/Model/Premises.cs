using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace HCI.Model
{
    public class Premises : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Type Type { get; set; }
        public ObservableCollection<Tag> Tags { get; set; }
        private String id;
        private String name;
        private String description;
        private String alcoholServing;
        private String price;
        private Boolean isHandicapable;
        private Boolean isSmokingAlowed;
        private Boolean isReservingAvailable;
        public int capacity;
        public DateTime openingDate;

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }

            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if(value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public string AlcoholServing
        {
            get
            {
                return alcoholServing;
            }

            set
            {
                if (value != alcoholServing)
                {
                    alcoholServing = value;
                    OnPropertyChanged("AlcoholServing");
                }
            }
        }

        public string Price
        {
            get
            {
                return price;
            }

            set
            {
                if (value != price)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public bool IsHandicapable
        {
            get
            {
                return isHandicapable;
            }

            set
            {
                if (value != isHandicapable)
                {
                    isHandicapable = value;
                    OnPropertyChanged("IsHandicapable");
                }
            }
        }

        public bool IsSmokingAlowed
        {
            get
            {
                return isSmokingAlowed;
            }

            set
            {
                if (value != isSmokingAlowed)
                {
                    isSmokingAlowed = value;
                    OnPropertyChanged("IsSmokingAlowed");
                }
            }
        }

        public bool IsReservingAvailable
        {
            get
            {
                return isReservingAvailable;
            }

            set
            {
                if (value != isReservingAvailable)
                {
                    isReservingAvailable = value;
                    OnPropertyChanged("isReservingAvailable");
                }
            }
        }

        public int Capacity
        {
            get
            {
                return capacity;
            }

            set
            {
                if (value != capacity)
                {
                    capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }

        public DateTime OpeningDate
        {
            get
            {
                return openingDate;
            }

            set
            {
                if (value != openingDate)
                {
                    openingDate = value;
                    OnPropertyChanged("OpeningDate");
                }
            }
        }

        public Premises()
        {
            Tags = new ObservableCollection<Tag>();
        }

        /*       public Premises(String id, String name, String description, String alcoholServing, String price, bool isHandicapable, bool isSmokingAlowed, bool isReservingAvailable, int capacity, DateTime openingDate)
               {
                   this.id = id;
                   this.name = name;
                   this.description = description;
                   this.alcoholServing = alcoholServing;
                   this.price = price;
                   this.isHandicapable = isHandicapable;
                   this.isSmokingAlowed = IsSmokingAlowed;
                   this.isReservingAvailable = isReservingAvailable;
                   this.capacity = capacity;
                   this.openingDate = openingDate;
               }*/
    }
}
