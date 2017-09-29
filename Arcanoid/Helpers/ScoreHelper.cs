using Newtonsoft.Json;
using System.IO;

namespace Arcanoid
{
    class ScoreHelper
    {
        /// <summary>
        /// Получение текста касательно счёта
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public string GetText(int score)
        {
            return CompareScore(score) ? score.ToString()+ " - новый рекорд:)" : "Не грустите, " + score.ToString() + " - не так уж и мало";
        }

        /// <summary>
        /// Суммирование очков и бонусов в итоговый счёт
        /// </summary>
        /// <param name="gamer"></param>
        /// <returns></returns>
        public int SumScore(Gamer gamer)
        {
            return gamer.Score + gamer.LifeCount * 100;
        }

        /// <summary>
        /// Сравнение текущего счёта с максимальным
        /// </summary>
        /// <param name="currentScore"></param>
        /// <returns></returns>
        private bool CompareScore(int currentScore)
        {
            if (currentScore > ReadMaxScore())
            {
                SaveNewMaxScore(currentScore);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Сохранение нового максимального счёта
        /// </summary>
        /// <param name="score"></param>
        private void SaveNewMaxScore(int score)
        {
            var path = GetPath();        
            using (var file = new StreamWriter(path))
            {
                var serScore = JsonConvert.SerializeObject(score);
                file.Write(serScore);
            }
        }

        /// <summary>
        /// Считывание прежнего максимального счёта
        /// </summary>
        /// <returns></returns>
        private int ReadMaxScore()
        {
            var score = 0;
            var path = GetPath();
            if (File.Exists(path))
                using (var file = new StreamReader(path))
                {
                    var json = file.ReadLine();
                    score = JsonConvert.DeserializeObject<int>(json);
                }
            return score;
        }

        /// <summary>
        /// Получение пути к файлику с максимальным счётом
        /// </summary>
        /// <returns></returns>
        private string GetPath()
        {
            var fileName = "MaxScore";
            var format = ".txt";
            return Path.Combine(fileName + format);
        }     
        
    }
}
