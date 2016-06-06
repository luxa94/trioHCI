using HCI.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI
{
    class PinTemplateFactory
    {
        public static ControlTemplate getTemplate(Premises p)
        {
            ControlTemplate product = new ControlTemplate();

            product.VisualTree = new FrameworkElementFactory(typeof(Grid));
            var lblOpis = new FrameworkElementFactory(typeof(Label));
            product.VisualTree.AppendChild(lblOpis);
            lblOpis.SetValue(Label.ContentProperty, "");

            var imgSlidza = new FrameworkElementFactory(typeof(Image));
            var rectangle = new FrameworkElementFactory(typeof(Rectangle));

            rectangle.SetValue(Rectangle.WidthProperty, 50.0);
            rectangle.SetValue(Rectangle.HeightProperty, 50.0);

            BitmapImage bitmap = new BitmapImage(new Uri(string.IsNullOrEmpty(p.PathImage) || p.PathImage == "photo1.png" ? p.Type.PathImage : p.PathImage, UriKind.RelativeOrAbsolute));
            rectangle.SetValue(Rectangle.FillProperty, new ImageBrush(bitmap));

            product.VisualTree.AppendChild(rectangle);

            product.VisualTree.AppendChild(imgSlidza);

            return product;
        }
    }
}
