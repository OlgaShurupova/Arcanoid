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
    public class Platform : AbstractRectangle
    {        
        /// <summary>
        /// Cкорость движения платформы 
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Направление движения платформы
        /// </summary>
        public int Direction { get; set; } = 0;


        #region Методы

        /// <summary>
        /// Отрисовка платформы
        /// </summary>
        public override void Draw()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Перемещение платформы
        /// </summary>
        /// <param name="x"></param>
        public void Move(double x)
        {
            var position = new Point();
            position.X = Position.X + x;
            position.Y = Position.Y;
            Position = position;
        }

       

        #endregion
    }
}
