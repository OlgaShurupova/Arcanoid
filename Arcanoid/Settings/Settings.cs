using System.Windows;
using System.Windows.Media;

namespace Arcanoid
{  
    public class Settings
    {
        #region Свойства, скрытые для редактирования. Определяются сложностью уровня     
        /// <summary>
        /// Количество блоков
        /// </summary>
        public int BlocksCount { get; set; } = 15;
        
        /// <summary>
        /// Количество блоков в строке
        /// при условии отсутствия пропусков 
        /// </summary>
        public int ColumnsCount { get; set; } = 8;

        /// <summary>
        /// Число строк 
        /// </summary>
        public int RowsCount { get; set; } = 4;
        #endregion

        #region Свойства, частично доступные пользователю для редактирования 

        /// <summary>
        /// Свойства мяча
        /// </summary>
        public Ball Ball { get; set; } = new Ball()
        {
            Size = new Size(10, 10),
            Color = Color.FromRgb(20, 47, 54),
            Speed = new Point(1, 1)

        };

        /// <summary>
        /// Свойства поля
        /// </summary>
        public Field Field { get; set; } = new Field() { Color = Color.FromRgb(168, 209, 230) };

        /// <summary>
        /// Свойства платформы
        /// </summary>
        public Platform Platform { get; set; } = new Platform()
        {
            Color = Color.FromRgb(11, 78, 34),
            Size = new Size(100, 20),
            Position = new Point(0, 300),
            Speed = 7
        };

        /// <summary>
        /// Свойства блока
        /// </summary>
        public Block Block { get; set; } = new Block()
        {
            Size = new Size(80, 30),
            Color = Color.FromRgb(98, 28, 43),
            Indent = new Point(20, 20)
        };

        public int[] PositionsArray{ get; set; }
        #endregion
 
    }
}
