using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace HCI.Model
{
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
            Premises = new List<Premises>();
        }

        [InverseProperty("Type")]
        public ICollection<Premises> Premises { get; set; }

        private String id;
        private String name;
        private String description;

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
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
    }
}
