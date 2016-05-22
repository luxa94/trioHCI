using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

namespace HCI.Model
{
    [DataContract]
    public class Type : INotifyPropertyChanged
    {
        public override bool Equals(object obj)
        {
            Type other = obj as Type;
            if (other == null)
            {
                return false;
            }
            return this.Id.Equals(other.id);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public Type()
        {
            pathImage = "photo1.png";
            this.Members = new ObservableCollection<Model.Premises>();
        }


        private String id;
        private String name;
        private String description;
        private String pathImage;
        //private ObservableCollection<Premises> Premises;
       
        [DataMember]
        [Key]
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
        [DataMember]
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
        [DataMember]
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public string PathImage
        {
            get
            {
                return pathImage;
            }

            set
            {
                pathImage = value;
                OnPropertyChanged("PathImage");
            }
        }
        public ObservableCollection<Premises> Members
        {
            get;
            set;
        }

    }
}
