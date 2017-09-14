using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arcanoid
{
    public abstract class AbstractRectangle:PlayObject
    {
        public Rectangle GetRect()
        {
            var colorBrush = new SolidColorBrush(Color);
            var rectangle = new Rectangle()
            {
                Width = Size.Width,
                Height = Size.Height,
                Fill = colorBrush
            };

            Canvas.SetLeft(rectangle, Position.X);
            Canvas.SetTop(rectangle, Position.Y);

            return rectangle;
        }
                
    }
}
