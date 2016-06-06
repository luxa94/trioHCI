using System;
using System.ComponentModel;

namespace HCI.AttachProperties
{
    public class DynamicHelpModel : INotifyPropertyChanged
    {
        private bool _isVisible;

        public bool IsVisible
        {
            get { return _isVisible; }
            set { _isVisible = value; OnPropertyChanged("IsVisible"); }
        }
        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged("Title"); }
        }
        private string _helpText;

        public string HelpText
        {
            get { return _helpText; }
            set { _helpText = value; OnPropertyChanged("HelpText"); }
        }
        private string _uRL;

        public string URL
        {
            get { return _uRL; }
            set { _uRL = value; OnPropertyChanged("URL"); }
        }
        private string _shortCut;

        public string ShortCut
        {
            get { return _shortCut; }
            set { _shortCut = value; OnPropertyChanged("ShortCut"); }
        }
        private int _flowIndex;

        public int FlowIndex
        {
            get { return _flowIndex; }
            set { _flowIndex = value; OnPropertyChanged("FlowIndex"); }
        }

        public string DisplaybleFlowIndex
        {
            get { return _flowIndex > 0 ? Convert.ToString(_flowIndex) : string.Empty; }
        }

        // Declare the event 
        public event PropertyChangedEventHandler PropertyChanged;
        // Create the OnPropertyChanged method to raise the event 
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
