using HCI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using HCI.Model.Global;

namespace HCI.GUI.Model.Global
{
    class Serialization
    {
        public static void serialize()
        {
            XmlSerializer serializerPremises = new XmlSerializer(typeof(ObservableCollection<Premises>));
            XmlSerializer serializerTags = new XmlSerializer(typeof(ObservableCollection<Tag>));
            XmlSerializer serializerTypes = new XmlSerializer(typeof(ObservableCollection<HCI.Model.Type>));

            Console.WriteLine("Serijalizujem...");

            using (TextWriter twPremises = new StreamWriter("../../Serialization/premises.xml"))
            {
                serializerPremises.Serialize(twPremises, Globals.Premisses);
            }
           
            using (TextWriter twTags = new StreamWriter("../../Serialization/tags.xml"))
            {
                serializerTags.Serialize(twTags, Globals.Tags);
            }

            using (TextWriter twTypes = new StreamWriter("../../Serialization/types.xml"))
            {
                serializerTypes.Serialize(twTypes, Globals.Types);
            }
            
        }
    }
}
