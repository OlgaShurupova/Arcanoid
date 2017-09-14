using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcanoid
{
    public class Collision
    {
        /// <summary>
        /// Проверка столкновения мяча с границами поля
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="playFieldWidth"></param>
        public void CheckBoder(Ball ball, double playFieldWidth)
        {
            if (ball.Position.X + ball.Size.Width >= playFieldWidth) ball.DirectionX = -1;
            if (ball.Position.X - ball.Size.Width <= 0) ball.DirectionX = 1;
            if (ball.Position.Y <= 0) ball.DirectionY = 1;
        }

        /// <summary>
        /// Проверка столкновения мяча с платформой
        /// </summary>
        public void CheckPlatform(Ball ball, Platform platform)
        {
            if (ball.Position.Y + ball.Size.Height >= platform.Position.Y)
                CheckRect(ball, platform);
        }

        /// <summary>
        /// Проверка столкновения мяча с блоком
        /// </summary>
        public void CheckBlocks(Ball ball, Block [,] blocksArray, Settings settings, Player player, Platform platform)
        {
            for (var i = 0; i < settings.RowsCount; i++)
                for (var j = 0; j < settings.ColumnsCount; j++)
                {
                    var block = blocksArray[i, j];             
                    if (block!= null && ball.Position.Y <= block.Position.Y + block.Size.Height)
                        if (CheckRect(ball, block))
                        {
                            ball.DirectionY *= -1;
                            ball.SpeedY += 0.1;
                            platform.Speed += 0.1;
                            player.Score += 100;
                            blocksArray[i, j] = null;
                        }
                }
        }

        /// <summary>
        /// Проверка на столкновение мяча с прямоугольным объектом
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public bool CheckRect(Ball ball, AbstractRectangle rect)
        {
            var ballX1 = ball.Position.X;
            var ballX2 = ball.Position.X + ball.Size.Width;
            var ballX0 = (ballX1 + ballX2) / 2;

            //Мяч ударился о платформу __о___
            if (ballX0 >= rect.Position.X && ballX0 <= rect.Position.X + rect.Size.Width)
            {
                ball.DirectionY = -1;                
                return true;
            }
            else
            //Мяч ударился о левый угол о____
            if (ballX2 >= rect.Position.X + ball.Size.Width && ballX2 <= rect.Position.X + rect.Size.Width)
            {
                ball.DirectionY = -1;
                ball.DirectionX = -ball.DirectionX;
                ball.SpeedX+= 0.1;
                return true;
            }
            else
            //Мяч ударился о правый угол ____о
            if (ballX1 <= rect.Position.X && ballX1 - ball.Size.Width <= rect.Position.X + rect.Size.Width)
            {
                ball.DirectionY = -1;
                ball.DirectionX = -ball.DirectionX;
                ball.SpeedX +=0.1;
                return true;
            }
            return false;
        }
    }
}
