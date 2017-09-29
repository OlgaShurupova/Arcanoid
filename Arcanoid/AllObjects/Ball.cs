using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arcanoid
{
    public class Ball : GameObject
    {
        /// <summary>
        /// Скорость мяча по х
        /// </summary>
        public Point Speed { get; set; }

        public double Acceleration { get; set; } = 1;      
     
        /// <summary>
        /// Установка свойств мяча как объекта на холсте 
        /// </summary>
        /// <returns></returns>
        public Ellipse GetBall()
        {
            var colorBrush = new SolidColorBrush(Color);
            var ball = new Ellipse()
            {
                Fill = colorBrush,
                Width=Size.Width,
                Height=Size.Height     
            };

            Canvas.SetLeft(ball, Position.X);
            Canvas.SetTop(ball, Position.Y);

            return ball;
        }

        /// <summary>
        /// Передвижение мяча
        /// </summary>
        public void Move()
        {
            var position = new Point();
            position.X = Position.X + Speed.X;
            position.Y = Position.Y + Speed.Y;
            Position = position;
        }
    }
}
