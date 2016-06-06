using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using HCI.Model;
using Microsoft.Maps.MapControl.WPF;

namespace HCI
{
    public class DraggablePushpin : Pushpin
    {
        private Map _map;
        private bool isDragging = false;
        Location _center;
        private Premises _p;

        public Map Map
        {
            get { return _map; }
            set { _map = value; }
        }
       

        public DraggablePushpin(Map map, Location center, Premises p)
        {
            this.Location = center;
            _map = map;
            _center = center;
            _p = p;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {

            DataObject dragData = new DataObject("premises", _p);
            DragDrop.DoDragDrop(this, dragData, DragDropEffects.Move);
            // Enable Dragging
            this.isDragging = true;
            e.Handled = true;
        }

        void ParentMap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.isDragging = false;
        }

    }
}
