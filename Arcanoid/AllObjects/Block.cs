using System.Windows;

namespace Arcanoid
{
    public class Block : AbstractRectangle
    {
        /// <summary>
        /// Отступ следующего блока от текущего
        /// при условии полностью заполненного массива
        /// </summary>
        public Point Indent { get; set; }
    }             

}
