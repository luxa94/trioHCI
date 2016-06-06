using System;
using System.Linq;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using HCI.AttachProperties;
using HCI.Languages;


namespace IntegratedHelpInWPF
{
    public static class DynamicHelperViewer
    {
        public static event EventHandler<HelpElementArgs> OnHelpTextShown;
        public static event EventHandler<HelpElementArgs> OnHelpTextCollaped;

        private static List<HelpElementArgs> _helpElements;

        private static Timer _helpTimer;

        private const int Interval = 800;

        internal static readonly DropShadowEffect HelpGlow =
        new DropShadowEffect() { ShadowDepth = 0, Color = Colors.YellowGreen, BlurRadius = 10 };

        public static void GenerateHelpControl(DependencyObject dependObj, HelperModeEventArgs e)
        {
            DynamicHelper.Current.IsDetailShown = false;
            //_textAnimationStory = null;
            if (_helpTimer == null)
            {
                _helpTimer = new Timer(Interval);
                _helpTimer.Elapsed += HelpTimerTick;
            }

            if (_helpElements == null)
                _helpElements = new List<HelpElementArgs>();
            _helpElements.Clear();

            DoGenerateHelpControl(dependObj, e);
            _helpTimer.Enabled = e.IsHelpActive;
        }

        private static void HelpTimerTick(object sender, ElapsedEventArgs args)
        {
            if (null != _helpElements && _helpElements.Count > 0)
            {
                int idx = _helpElements.Min(e => e.HelpData.Data.FlowIndex);
                var data = _helpElements.Where(e => e.HelpData.Data.FlowIndex.Equals(idx));

                foreach (var helpElementArgse in data.ToList())
                {
                    if (null != helpElementArgse)
                    {
                        if (helpElementArgse.HelpData != null
                             && helpElementArgse.HelpData.Data != null)
                            helpElementArgse.HelpData.Data.IsVisible = !DynamicHelper.Current.IsDetailShown;

                        _helpElements.Remove(helpElementArgse);
                        if (null != OnHelpTextShown)
                        {
                            OnHelpTextShown(sender, helpElementArgse);
                        }
                    }
                }
            }
            else
            {
                _helpTimer.Enabled = false;
            }
        }

        public static void ToggoleDetailMode(bool showDetail)
        {
            var current = DynamicHelper.Current;
            if (current != null)
            {
                current.IsDetailShown = showDetail;
                foreach (var item in current._popUps.Values)
                {
                    if (null != item && null != item.Data)
                        item.Data.IsVisible = !current.IsDetailShown;
                }
            }
        }

        private static void DoGenerateHelpControl(DependencyObject dependObj, HelperModeEventArgs e)
        {
            // Continue recursive toggle. Using the VisualTreeHelper works nicely.
            for (int x = 0; x < VisualTreeHelper.GetChildrenCount(dependObj); x++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(dependObj, x);
                DoGenerateHelpControl(child, e);
            }

            // BitmapEffect is defined on UIElement so our DependencyObject 
            // must be a UIElement also
            if (dependObj is UIElement)
            {
                var element = (UIElement)dependObj;
                if (e.IsHelpActive)
                {
                    var helpText = e.Current.FetchHelpText(element);
                    if (!string.IsNullOrWhiteSpace(helpText) && element.IsVisible
                        && !IsWindowAdornerItem(element))
                    {
                        // Any effect can be used, I chose a simple yellow highlight
                        _helpElements.Add(new HelpElementArgs()
                        {
                            Element = element,
                            HelpData = DynamicHelperViewer.GetPopUpTemplate(element, helpText, e.Current),
                            Group = e.Current
                        });
                    }
                }
                else if (element.Effect == HelpGlow)
                {
                    if (null != OnHelpTextCollaped)
                        OnHelpTextCollaped(null, new HelpElementArgs() { Element = element, Group = e.Current });
                }
            }
        }

        public static HelpModeViewData GetPopUpTemplate(UIElement element, string helpText, HelpGroup Current)
        {
            //if (IsWindowAdornerItem(element))
            //    return null;

            HelpModeViewData popUp = null;
            if (Current._popUps.ContainsKey(element.GetHashCode()))
            {
                popUp = Current._popUps[element.GetHashCode()];
            }
            else
            {
                var helpModel = DynamicHelpStringLoader.GetDynamicHelp(helpText);
                if (null != helpModel)
                {
                    popUp = new HelpModeViewData { Data = helpModel, Template = CreatePopUpTemplate(element) };
                    Current._popUps.Add(element.GetHashCode(), popUp);
                }
            }
            return popUp;
        }

        private static bool IsWindowAdornerItem(UIElement element)
        {
            if (element is TabItem)
            {
                return true;
            }
            return false;
        }

        private static ControlTemplate CreatePopUpTemplate(UIElement element)
        {
            if (element is FrameworkElement)
            {
                var template = ((FrameworkElement)element).TryFindResource("DynamicHelpControl") as ControlTemplate;
                return template;
            }
            return null;
        }
    }

    public class HelpElementArgs : EventArgs
    {
        public UIElement Element { get; set; }
        public HelpModeViewData HelpData { get; set; }
        public HelpGroup Group { get; set; }
    }
}
