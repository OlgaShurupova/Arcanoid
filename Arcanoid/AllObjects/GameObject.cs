using System.Windows;
using System.Windows.Media;

namespace Arcanoid
{
    public abstract class GameObject
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
    }
}
