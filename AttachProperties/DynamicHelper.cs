using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Automation;
using System.Windows.Controls;
using System.ComponentModel;
using HCI.Languages;

namespace HCI.AttachProperties
{
    public class DynamicHelper
    {
        public static event EventHandler<HelperPublishEventArgs> OnHelpMessagePublished;
        public static event EventHandler<HelperModeEventArgs> OnHelpModeSelection;

        private static bool HelpActive { get; set; }

        public static void SetDynamicHelp(UIElement element, bool value)
        {
            element.SetValue(DynamicHelpProperty, value);
        }
        public static bool GetDynamicHelp(UIElement element)
        {
            return (Boolean)element.GetValue(DynamicHelpProperty);
        }

        public static readonly DependencyProperty DynamicHelpProperty =
          DependencyProperty.RegisterAttached("DynamicHelp", typeof(bool), typeof(UIElement),
                                              new PropertyMetadata(false, DynamicHelpChanged));

        private static readonly List<HelpGroup> HelpGroups = new List<HelpGroup>();

        public static HelpGroup Current
        {
            get
            {
                return HelpGroups.LastOrDefault();
            }
        }

        private static void DynamicHelpChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as UIElement;

            if (null != element)
            {
                if (null != HelpGroups && !HelpGroups.Any(g => null != g.Element && g.Element.Equals(element)))
                {
                    UIElement window = null;
                    if (element is Window)
                        window = (Window)element;
                    else
                        window = Window.GetWindow(element);

                    //Note: Use below code if you have used any custom window class other than child of Window (for example WindowBase is base of your custom window)
                    //if (window == null)
                    //{
                    //    if (element is WindowBase)
                    //        window = (WindowBase)element;
                    //    else
                    //        window = element.TryFindParent<WindowBase>();
                    //}

                    if (null != window)
                    {
                        var currentGroup = new HelpGroup { Screen = window, Element = element, ScreenAdorner = new HelpTextAdorner(element) };
                        var newVal = (bool)e.NewValue;
                        var oldVal = (bool)e.OldValue;

                        // Register Events
                        if (newVal && !oldVal)
                        {
                            if (currentGroup.Screen != null)
                            {
                                if (!currentGroup.Screen.CommandBindings.OfType<CommandBinding>().Any(c => c.Command.Equals(ApplicationCommands.Help)))
                                {
                                    if (currentGroup._helpCommandBind == null)
                                    {
                                        currentGroup._helpCommandBind = new CommandBinding(ApplicationCommands.Help, HelpCommandExecute);
                                    }
                                    currentGroup.Screen.CommandBindings.Add(currentGroup._helpCommandBind);
                                }

                                if (currentGroup._helpHandler == null)
                                {
                                    currentGroup._helpHandler = new MouseButtonEventHandler(ElementMouse);
                                }
                                currentGroup.Screen.PreviewMouseLeftButtonDown += currentGroup._helpHandler;
                                if (window is Window)
                                {
                                    ((Window)currentGroup.Screen).Closing += WindowClosing;
                                    ((Window)currentGroup.Screen).LocationChanged += WindowLocationChanged;
                                    ((Window)currentGroup.Screen).StateChanged += WindowStateChanged;
                                }
                                //else
                                //    ((WindowBase)currentGroup.Screen).Closed += new EventHandler<WindowClosedEventArgs>(RadWindowClosed);
                            }
                        }
                        HelpGroups.Add(currentGroup);
                    }
                }
            }

        }

        static void WindowStateChanged(object sender, EventArgs e)
        {
            if (sender is Window)
            {
                var current = HelpGroups.Where(g => g.Screen.Equals(sender)).FirstOrDefault();

                if ((sender as Window).WindowState == WindowState.Normal
                    || (sender as Window).WindowState == WindowState.Maximized)
                {
                    if (null != OnHelpModeSelection)
                    {
                        OnHelpModeSelection(Current.Element, new HelperModeEventArgs() { IsHelpActive = HelpActive, Current = Current });
                    }
                    UpdateScreen(current);
                }
       //         else
        //            DynamicHelperViewer.ToggoleDetailMode(true);
            }
        }

        static void WindowLocationChanged(object sender, EventArgs e)
        {
            var current = HelpGroups.Where(g => g.Screen.Equals(sender)).FirstOrDefault();
            UpdateScreen(current, (sender is Window && (sender as Window).WindowState == WindowState.Minimized));
        }

        static void UpdateScreen(HelpGroup current, bool forceClose = false)
        {
            if (HelpActive)
            {
                if (current != null)
                {
                    foreach (var item in current._popUps.Values)
                    {
                        if (null != item && null != item.Data)
                        {
                            item.Data.IsVisible = false;
                            item.Data.IsVisible = forceClose ? false : !current.IsDetailShown;
                        }
                    }
                }
            }
        }

        static void WindowClosing(object sender, CancelEventArgs e)
        {
            OnClosingScreen(sender);
        }

        private static void OnClosingScreen(object sender)
        {
            var group = HelpGroups.FirstOrDefault(g => g.Screen.Equals(sender));
            if (null != group)
            {
                HelpGroups.Remove(group);
                if (null != group._helpCommandBind)
                    group.Screen.CommandBindings.Remove(group._helpCommandBind);
                group.Screen.PreviewMouseLeftButtonDown -= group._helpHandler;
                if (group.Screen is Window)
                {
                    ((Window)group.Screen).Closing -= WindowClosing;
                    ((Window)group.Screen).LocationChanged -= WindowLocationChanged;
                    ((Window)group.Screen).StateChanged -= WindowStateChanged;
                }
                //Note: Use Below Code if you have used any custom window class other than child of Window (for example WindowBase is base of your custom window)
                //else
                //    ((WindowBase)group.Screen).Closed -= WindowBaseClosed;
                group._helpHandler = null;
                group._helpCommandBind = null;
                group.Screen = null;
                group.Element = null;
                group.HelpDO = null;
                group._popUps.Clear();
                group._cacheText.Clear();
            }
        }

        static void ElementMouse(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ButtonState != MouseButtonState.Pressed
                || e.ClickCount != 1)
                return;

            var element = sender as DependencyObject;
            if (null != element)
            {
                UIElement window = null;
                if (element is Window)
                    window = (Window)element;
                else
                    window = Window.GetWindow(element);

                //Note:  Use bellow code if you have used any custom window class other than child of Window (for example WindowBase is base of your custom window)
                //if (window == null)
                //{
                //    if (element is WindowBase)
                //        window = (WindowBase) element;
                //    else
                //        window = element.TryFindParent<WindowBase>();
                //}

                if (null != window)
                {
                    // Walk up the tree in case a parent element has help defined
                    var hitElement = (DependencyObject)window.InputHitTest(e.GetPosition(window));

                    var checkHelpDo = hitElement;
                    string helpText = Current.FetchHelpText(checkHelpDo);
                    while (string.IsNullOrWhiteSpace(helpText) && checkHelpDo != null &&
                            !Equals(checkHelpDo, Current.Element) &&
                            !Equals(checkHelpDo, window))
                    {
                        checkHelpDo = (checkHelpDo is Visual) ? VisualTreeHelper.GetParent(checkHelpDo) : null;
                        helpText = Current.FetchHelpText(checkHelpDo);
                    }
                    if (string.IsNullOrWhiteSpace(helpText))
                    {
                        Current.HelpDO = null;
                    }
                    else if (!string.IsNullOrWhiteSpace(helpText) && Current.HelpDO != checkHelpDo)
                    {
                        Current.HelpDO = checkHelpDo;
                    }

                    if (null != OnHelpMessagePublished)
                        OnHelpMessagePublished(checkHelpDo, new HelperPublishEventArgs() { HelpMessage = helpText, Sender = hitElement });

                }
            }
        }

        static void HelpCommandExecute(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ToggleHelp();
        }


        public static void ToggleHelp()
        {
            // Turn the current help off
            Current.HelpDO = null;
            // Toggle current state; add/remove mouse handler
            HelpActive = !HelpActive;

            if (null != OnHelpModeSelection)
            {
                OnHelpModeSelection(Current.Element, new HelperModeEventArgs() { IsHelpActive = HelpActive, Current = Current });
            }
        }

    }

    public class HelperPublishEventArgs : EventArgs
    {
        public string HelpMessage { get; set; }
        public DependencyObject Sender { get; set; }
    }

    public class HelperModeEventArgs : EventArgs
    {
        public bool IsHelpActive { get; set; }
        public HelpGroup Current { get; set; }
    }

    public class HelpModeViewData
    {
        public ControlTemplate Template { get; set; }
        public DynamicHelpModel Data { get; set; }
    }

    public class HelpGroup
    {
        public bool IsDetailShown { get; set; }
        public HelpTextAdorner ScreenAdorner { get; set; }
        public DependencyObject HelpDO { get; set; }
        public UIElement Screen { get; set; }
        public UIElement Element { get; set; }
        public MouseButtonEventHandler _helpHandler = null;
        public CommandBinding _helpCommandBind = null;

        public Dictionary<int, HelpModeViewData> _popUps = new Dictionary<int, HelpModeViewData>();
        public Dictionary<int, string> _cacheText = new Dictionary<int, string>();

        public string FetchHelpText(DependencyObject element)
        {
            if (null != element)
            {
                if (_cacheText.ContainsKey(element.GetHashCode()))
                {
                    return _cacheText[element.GetHashCode()];
                }
                else
                {
                    string helpText = AutomationProperties.GetHelpText(element);
                    if (!string.IsNullOrWhiteSpace(helpText))
                    {
                        _cacheText.Add(element.GetHashCode(), helpText);
                    }
                    return helpText;
                }
            }
            return null;
        }
    }

    public class HelpTextAdorner : Adorner
    {
        public HelpTextAdorner(UIElement adornedElement)
            : base(adornedElement)
        { }

        protected override int VisualChildrenCount
        {
            get
            {
                return 1;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new ArgumentOutOfRangeException();
            return _child;
        }

        private Control _child;
        public Control Child
        {
            get { return _child; }
            set
            {
                if (_child != null)
                {
                    RemoveVisualChild(_child);
                }
                _child = value;
                if (_child != null)
                {
                    AddVisualChild(_child);
                }
            }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (null != Child)
            {
                _child.Measure(constraint);
                return _child.DesiredSize;
            }
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (null != Child)
            {
                _child.Arrange(new Rect(new Point(0, 0), finalSize));
                return new Size(_child.ActualWidth, _child.ActualHeight);
            }
            return base.ArrangeOverride(finalSize);
        }

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    var segment = (this.AdornedElement as UIElement);
        //    if (segment != null)
        //    {
        //        var adornedElementRect = new Rect(this.AdornedElement.DesiredSize);
        //        //Some arbitrary drawing implements.
        //        var renderBrush = new SolidColorBrush(Colors.WhiteSmoke) { Opacity = 0.1 };
        //        drawingContext.DrawRectangle(renderBrush, null, new Rect(RenderSize));
        //        var ft = new FormattedText(
        //            LanguageLoader.GetText("HelpTextMode"), Thread.CurrentThread.CurrentCulture,
        //            System.Windows.FlowDirection.LeftToRight,
        //            new Typeface(new FontFamily("Arial"), FontStyles.Normal, FontWeights.Bold, FontStretches.Normal),
        //            25, Brushes.SlateGray);

        //        drawingContext.DrawText(ft, new Point(adornedElementRect.TopLeft.X + 20, adornedElementRect.TopLeft.Y + 20));
        //    }
        //}
    }
}
