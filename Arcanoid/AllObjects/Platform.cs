﻿using System.Windows;

namespace Arcanoid
{
    public class Platform : AbstractRectangle
    {        
        /// <summary>
        /// Cкорость движения платформы 
        /// </summary>
        public double Speed { get; set; }

        #region Методы

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
