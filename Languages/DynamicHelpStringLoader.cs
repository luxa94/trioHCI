using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using HCI.AttachProperties;
using HCI.Languages;

namespace HCI.Languages
{
    public class DynamicHelpStringLoader
    {
        private const string HelpStringReferenceFolder = "DynamicHelpReference";
        private const string UsFileName = "DynamicHelp_EN_US.xml";
        private const string DefaultFileName = "DynamicHelp_EN_US.xml";

        /// <summary>
        /// This is the collection where all the JerichoMessage objects
        /// will be stored.
        /// </summary>
        private static readonly Dictionary<string, DynamicHelpModel> HelpMessages;

        private static Languages _languageType;

        /// <summary>
        /// The static constructor.
        /// </summary>
        static DynamicHelpStringLoader()
        {
            HelpMessages = new Dictionary<string, DynamicHelpModel>();
            _languageType = Languages.None;
        }
        /// <summary>
        /// Generates the collection of JerichoMessage objects as if the provided language.
        /// </summary>
        /// <param name="languages">The Languages enum. Represents the user's choice of language.</param>
        public static void GenerateCollection(Languages languages)
        {
            if (_languageType == languages)
            {
                return;
            }
            _languageType = languages;
            string startUpPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
            string fileName;
            switch (languages)
            {
                case Languages.English:
                    fileName = UsFileName;
                    break;
                
                default:
                    fileName = DefaultFileName;
                    break;
            }

            Task.Factory.StartNew(() =>
            {
                LoadXmlFile(Path.Combine(startUpPath,
                                         string.Format(@"{0}\{1}", HelpStringReferenceFolder,
                                                       fileName)));
            });
        }
        /// <summary>
        /// Load the provided xml file and populate the dictionary.
        /// </summary>
        /// <param name="fileName"></param>
        private static void LoadXmlFile(string fileName)
        {
            XDocument doc = null;
            try
            {
                //Load the XML Document                
                doc = XDocument.Load(fileName);
                //clear the dictionary
                HelpMessages.Clear();

                var helpCodeTypes = doc.Descendants("item");
                //now, populate the collection with JerichoMessage objects
                foreach (XElement message in helpCodeTypes)
                {
                    var key = message.Attribute("element_name").Value;
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        var index = 0;
                        //get all Message elements under the help type
                        //create a JerichoMessage object and insert appropriate values
                        var dynamicHelp = new DynamicHelpModel
                        {
                            Title = message.Element("title").Value,
                            HelpText = message.Element("helptext").Value,
                            URL = message.Element("moreURL").Value,
                            ShortCut = message.Element("shortcut").Value,
                            FlowIndex = (int.TryParse(message.Element("flowindex").Value, out index)) ? index : 0
                        };
                        //add the JerichoMessage into the collection
                        HelpMessages.Add(key.TrimStart().TrimEnd(), dynamicHelp);
                    }
                }

            }
            catch (FileNotFoundException)
            {
                throw new Exception(LanguageLoader.GetText("HelpCodeFileNotFound"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Returns mathced string from the xml.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static DynamicHelpModel GetDynamicHelp(string name)
        {

            if (!string.IsNullOrWhiteSpace(name))
            {
                var key = name.TrimStart().TrimEnd();
                if (HelpMessages.ContainsKey(key))
                    return HelpMessages[key];
            }
            return new DynamicHelpModel();
        }


    }
}
