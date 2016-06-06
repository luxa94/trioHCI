using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HCI.Model;
using Microsoft.Maps.MapControl.WPF;
using HCI.Model.Global;
using HCI.GUI;

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
        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            ContextMenu menu = new ContextMenu();
            MenuItem editItem = new MenuItem();
            MenuItem izbrisiLokal = new MenuItem();
            editItem.Header = "Edit";
            izbrisiLokal.Header = "Delete";

            menu.Items.Add(editItem);
            menu.Items.Add(izbrisiLokal);

            editItem.Click += Context_Edit;
            izbrisiLokal.Click += Context_Delete;

            menu.PlacementTarget = this;
            menu.IsOpen = true;

        }

        private void Context_Edit(object sender, RoutedEventArgs e)
        {
            Window w = new PremisesTableView(_p);
            w.ShowDialog();
        }

        private void Context_Delete(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("You are trying to delete a premises. Please confirm.",
                                "Delete premises?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using (var ctx = new DatabaseModel())
                {
                    ctx.Entry(_p).State = System.Data.Entity.EntityState.Deleted;
                    ctx.SaveChanges();
                    
                }
                _map.Children.Remove(this);
                Globals.pushpins.Remove(_p.Id);
                Globals.UpdatePremises();
            }
        }
    }
}
