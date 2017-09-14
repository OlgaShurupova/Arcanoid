using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Arcanoid
{
    public abstract class PlayObject
    {
        #region Свойства
        /// <summary>
        /// Размер объекта
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Координаты объекта
        /// </summary>
        public Point Position { get; set; }

        /// <summary>
        /// Цвет объекта
        /// </summary>
        public Color Color { get; set; }     
        
        #endregion

        #region Методы
        /// <summary>
        /// Рисование объекта
        /// </summary>
        public abstract void Draw();
        #endregion
    }
}
