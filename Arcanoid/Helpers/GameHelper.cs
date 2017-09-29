using System;
using System.Linq;
using System.Windows;

namespace Arcanoid
{
    public class GameHelper
    {
        /// <summary>
        /// Массив блоков
        /// </summary>
        public Block[,] BlocksArray { get; set; }           

        /// <summary>
        /// Инициализация массива блоков
        /// </summary>
        /// <param name="block"></param>
        /// <param name="settings"></param>
        public void InitializeBlocksArray(Block block, Settings settings)
        {
            var currentBlockNumber = 0;
            //var positionsArray = GeneratePositionsArray(settings);
            
            BlocksArray = new Block[settings.RowsCount, settings.ColumnsCount];

            for (var i = 0; i < settings.RowsCount; i++)
            {
                block.Position = ChangeBlockPosition(block, false, i);
                block.Position = new Point(0, block.Position.Y);

                for (var j = 0; j < settings.ColumnsCount; j++)
                {
                    block.Position = ChangeBlockPosition(block, true, j);
                    //Если номер текущего блока содержится в массиве номеров блоков на заполнение, создание новго блока 
                    if (settings.PositionsArray.Any(x => x == currentBlockNumber)) BlocksArray[i, j] = GetBlock(block);
                    currentBlockNumber++;
                }
            }
        }

        /// <summary>
        /// Генерация массива, содержащего номера подлежащих заполнению блоков
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public void GeneratePositionsArray(Settings settings)
        {
            var random = new Random();
            settings.PositionsArray = new int[settings.BlocksCount];
            for (var i = 0; i < settings.BlocksCount; i++)
            {
                int item;
                do
                {
                    item = random.Next(0, settings.ColumnsCount * settings.RowsCount);
                } while (settings.PositionsArray.Any(x => x == item));
                settings.PositionsArray[i] = item;
            }        
        }

        /// <summary>
        /// Получение нового блока массива
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private Block GetBlock(Block block)
        {
            return new Block()
            {
                Color = block.Color,
                Size = block.Size,
                Indent = block.Indent,
                Position = block.Position
            };
        }

        /// <summary>
        /// Изменение расположения блоков
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="block"></param>
        public void ChangeBlocksArray(Settings settings, Block block)
        {
            for (var i = 0; i < settings.RowsCount; i++)
            {
                block.Position = ChangeBlockPosition(block, false, i);
                block.Position = new Point(0, block.Position.Y);
                for (var j = 0; j < settings.ColumnsCount; j++)
                    if (BlocksArray[i, j] != null) BlocksArray[i, j].Position = ChangeBlockPosition(block, true, j);
            }
        }

        /// <summary>
        /// Изменение координат блока
        /// </summary>
        /// <param name="block"></param>
        /// <param name="isX"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public Point ChangeBlockPosition(Block block, bool isX, int i)
        {
            var newPosition = new Point();
            if (isX)
            {
                newPosition.Y = block.Position.Y;
                newPosition.X = block.Indent.X * (i + 1) + block.Size.Width * i;
            }
            else
            {
                newPosition.Y = block.Indent.Y * (i + 1) + block.Size.Height * i;
                newPosition.X = block.Position.X;
            }
            return newPosition;
        }

        /// <summary>
        /// Передвижение платформы
        /// </summary>
        /// <param name="x"></param>
        public void MovePlatform( Platform platform, Size fieldSize)
        {
            var x = platform.Speed;
            if (platform.Position.X + x >= 0 && platform.Position.X + x + platform.Size.Width <= fieldSize.Width)
                platform.Move(x);
        }
    }
}
