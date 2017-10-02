using System;
using System.Windows;

namespace Arcanoid
{
    /// <summary>
    /// Логика взаимодействия для LossWindow.xaml
    /// </summary>
    public partial class EndWindow : Window
    {
        private GameWindow _gameWindow;
        public EndWindow(GameWindow gameWindow, Gamer gamer, bool isWin)
        {
            InitializeComponent();
            _gameWindow = gameWindow;
            ChooseText(gamer, isWin);
        }

        /// <summary>
        /// Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
            _gameWindow.Close();
        }
      
        /// <summary>
        /// Повторное прохождение с той же расстановкой блоков
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewGameClick(object sender, RoutedEventArgs e)
        {                        
            var gameWindow = new GameWindow(_gameWindow.Settings);
            _gameWindow.Close();
            gameWindow.Show();
            Close();
        }

        /// <summary>
        /// Выбор текста в зависимости от типа окончания игры и счёта
        /// </summary>
        /// <param name="gamer"></param>
        /// <param name="isWin"></param>
        private void ChooseText(Gamer gamer, bool isWin)
        {
            var scoreHelper = new ScoreHelper();
            LossText.Text = isWin ? "Победа^^" : "Проигрыш";
            LossText.Text += Environment.NewLine;
            LossText.Text += scoreHelper.GetText(scoreHelper.SumScore(gamer));
        }
    }
}
