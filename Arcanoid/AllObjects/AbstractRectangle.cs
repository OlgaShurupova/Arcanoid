using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arcanoid
{   
    public abstract class AbstractRectangle:PlayObject
    {
        /// <summary>
        /// Отрисовка прямоугольного объекта
        /// </summary>
        /// <returns></returns>
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
