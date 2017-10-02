﻿using System;
using System.Windows;

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
            var speed = ball.Speed;           
            if (ball.Position.X + ball.Size.Width >= playFieldWidth) speed.X *= -1;
            if (ball.Position.X - ball.Size.Width <= 0) speed.X *= -1;
            if (ball.Position.Y <= 0) speed.Y *= -1;
            ball.Speed = speed;
        }

        /// <summary>
        /// Проверка столкновения мяча с платформой
        /// </summary>
        public void CheckPlatform(Ball ball, Platform platform)
        {
            if (CheckRect(ball, platform))
                HandleCollision(ball, platform);
        }

        /// <summary>
        /// Проверка столкновения мяча с блоком
        /// </summary>
        public void CheckBlocks(Ball ball, Block[,] blocksArray, Settings settings, Gamer player, Platform platform)
        {
            for (var i = 0; i < settings.RowsCount; i++)
                for (var j = 0; j < settings.ColumnsCount; j++)
                {
                    var block = blocksArray[i, j];
                    if (block != null && CheckRect(ball, block))
                    {
                        HandleCollision(ball, block);                                       
                        player.Score += 100;
                        blocksArray[i, j] = null;
                    }
                }
        }

        /// <summary>
        /// Проверка на столкновение мяча  с прямоугольным объектом
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private bool CheckRect(Ball ball, AbstractRectangle rect)
        {
            var position = new Point();
            position.X = ball.Position.X + ball.Size.Width;
            position.Y = ball.Position.Y + ball.Size.Height;
            var rectangle = new Rect(rect.Position, rect.Size);
            return rectangle.Contains(position);
        }

        /// <summary>
        /// Проверка характера столкновения мяча с прямоугольным объектом
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private void HandleCollision(Ball ball, AbstractRectangle rect)
        {           
            var speed = ball.Speed;     
            speed.X = GetBallSpeedX(ball, rect);          
            speed.Y *= -1;        
            ball.Speed = speed;
            if (rect is Platform) ChangeSpeed(ball, rect as Platform);
        }

        /// <summary>
        /// Получение скорости мяча по Х
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private double GetBallSpeedX(Ball ball, AbstractRectangle rect)
        {
            var ballX1 = ball.Position.X;
            var ballX2 = ball.Position.X + ball.Size.Width;
            var ballX0 = (ballX1 + ballX2) / 2;

            //Мяч ударился об угол о______ || _______о
            if (!(ballX0 >= rect.Position.X && ballX0 <= rect.Position.X + rect.Size.Width)) return ball.Speed.X*(-1);
            // __o____ || _______ 
            if (ballX0 >= rect.Position.X && ballX0 <= rect.Position.X + rect.Size.Width / 3) return Math.Abs(ball.Speed.X)*(-1)-0.2;
            // ______ || _____o__ 
            if (ballX0>= rect.Position.X + rect.Size.Width / 3*2 && ballX0<=rect.Position.X+rect.Size.Width) return Math.Abs(ball.Speed.X) +0.2;
            return ball.Speed.X;
        }

        /// <summary>
        /// Изменение скорости мяча и платформы
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="platform"></param>
        private void ChangeSpeed(Ball ball, Platform platform)
        {
            var speed = ball.Speed;           
            if (speed.X * platform.Speed > 0)
            {
                speed.X *= ball.Acceleration;
                speed.Y *= ball.Acceleration;
                platform.Speed *= 1.1;
            }
            else
            {
                speed.X /= ball.Acceleration;
                speed.Y /= ball.Acceleration;
                platform.Speed /= 1.1;
            }
            ball.Speed = speed;           
        }

    }
}
