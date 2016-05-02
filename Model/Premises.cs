﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace HCI.Model
{
    [DataContract]
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
        public string TypeId { get; set; }

        [ForeignKey("TypeId")]
        public Type Type {
            get { return _type; }
            set
            {
                _type = value;
                if (value != null)
                {
                    TypeId = value.Id;
                }
            }
        }
        public ObservableCollection<Tag> Tags { get; set; }
        private Type _type;
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
        
        [Key]
        [DataMember]
        public string Id
        {
            get { return id; }

            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }

        [DataMember]
        public string Name
        {
            get { return name; }

            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        [DataMember]
        public string Description
        {
            get { return description; }

            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        [DataMember]
        public string AlcoholServing
        {
            get { return alcoholServing; }

            set
            {
                if (value != alcoholServing)
                {
                    alcoholServing = value;
                    OnPropertyChanged("AlcoholServing2");
                }
            }
        }

        [DataMember]
        public string Price
        {
            get { return price; }

            set
            {
                if (value != price)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        [DataMember]
        public bool IsHandicapable
        {
            get { return isHandicapable; }

            set
            {
                if (value != isHandicapable)
                {
                    isHandicapable = value;
                    OnPropertyChanged("IsHandicapable");
                }
            }
        }

        [DataMember]
        public bool IsSmokingAlowed
        {
            get { return isSmokingAlowed; }

            set
            {
                if (value != isSmokingAlowed)
                {
                    isSmokingAlowed = value;
                    OnPropertyChanged("IsSmokingAlowed");
                }
            }
        }

        [DataMember]
        public bool IsReservingAvailable
        {
            get { return isReservingAvailable; }

            set
            {
                if (value != isReservingAvailable)
                {
                    isReservingAvailable = value;
                    OnPropertyChanged("isReservingAvailable");
                }
            }
        }

        [DataMember]
        public int Capacity
        {
            get { return capacity; }

            set
            {
                if (value != capacity)
                {
                    capacity = value;
                    OnPropertyChanged("Capacity");
                }
            }
        }

        [DataMember]
        public DateTime OpeningDate
        {
            get { return openingDate; }

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
            openingDate = DateTime.Now;
        }

        public Premises(String id, String name, String description, String alcoholServing, String price,
            bool isHandicapable, bool isSmokingAlowed, bool isReservingAvailable, int capacity, DateTime openingDate)
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
        }
    }
}