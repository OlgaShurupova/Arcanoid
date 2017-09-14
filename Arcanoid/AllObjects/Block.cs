using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
              

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
