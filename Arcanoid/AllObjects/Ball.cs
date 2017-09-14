using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Arcanoid
{
    public class Ball : PlayObject
    {
        /// <summary>
        /// Скорость мяча по х
        /// </summary>
        public double SpeedX { get; set; }

        /// <summary>
        /// Скорость мяча по y
        /// </summary>
        public double SpeedY { get; set; }

        /// <summary>
        /// Направление изменения координат по х
        /// </summary>
        public int DirectionX { get; set; } = 1;

        /// <summary>
        /// Направление изменения координат по y
        /// </summary>
        public int DirectionY { get; set; } = -1;

        /// <summary>
        /// Сдвиг объекта
        /// </summary>
       // public Point Shift { get; set; }
        public override void Draw()
        {
            throw new NotImplementedException();
        }
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

        public void Move()
        {
            var position = new Point();
            position.X = Position.X + SpeedX * DirectionX;
            position.Y = Position.Y + SpeedY * DirectionY;
            Position = position;
        }
    }
}
