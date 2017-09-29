using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Arcanoid
{
    /// <summary>
    /// Логика взаимодействия для PlayWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {     
        private Field _field = new Field();      
        private GameHelper _gameHelper = new GameHelper();
        private Gamer _gamer = new Gamer()
        {
            LifeCount=5
        };
        private System.Timers.Timer _timer;
        private bool _isGame;
        public Settings Settings { get; set; }

        public GameWindow(Settings settings)
        {
            InitializeComponent();
            DataContext = _gamer;
            Settings = settings;          
            InitializeGameObjects();
            SetTimer();        
        }

        /// <summary>
        /// Установка таймера
        /// </summary>
        private void SetTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Interval = 0.005 * 1000;
            _timer.Elapsed += _timer_Elapsed;
        }

        /// <summary>
        /// Работа таймера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate ()
            {
                CheckLoss();
                CheckWin();
                PlayField.Children.Clear();
                _field.Ball.Move();
                CheckCollision();
                UpdateCanvas();
                _field.Ball.Acceleration = 1;
            }));
        }
        /// <summary>
        /// Инициализация объектов игры
        /// </summary>
        public void InitializeGameObjects()
        {
            PlayField.Background = new SolidColorBrush(Settings.Field.Color);
            _field.Block = Settings.Block;
            _field.Ball = Settings.Ball;       
            _field.Platform = Settings.Platform;           
            _field.Block.Indent = GetIndent(500, 600);
            _gameHelper.InitializeBlocksArray(_field.Block, Settings);           
        }

        /// <summary>
        /// Обработка изменения размера окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void WindowChangeHandler(object sender, SizeChangedEventArgs e)
        {     
            PlayField.Children.Clear();
            var platformPosition = new Point();
            platformPosition.X = PlayField.ActualWidth / 2 - _field.Platform.Size.Width / 2;
            platformPosition.Y = PlayField.ActualHeight - _field.Platform.Size.Height - 20;
            _field.Platform.Position = platformPosition;
            var ballPosition = new Point();
            ballPosition.X = _field.Platform.Position.X + _field.Platform.Size.Width / 2;
            ballPosition.Y = _field.Platform.Position.Y - _field.Ball.Size.Height;
            _field.Ball.Position = ballPosition;
            UpdateCanvas();
        }
      
        /// <summary>
        /// Получение отступа в зависимости от размеров окна
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Point GetIndent(double width, double height)
        {
            var position = new Point();
            position.X = (width - Settings.ColumnsCount * _field.Block.Size.Width) / (Settings.ColumnsCount + 1);
            position.Y = (height / 3 - Settings.RowsCount * _field.Block.Size.Height) / (Settings.RowsCount + 1);
            return position;
        }

        /// <summary>
        /// Отрисовка блоков
        /// </summary>
        private void DrawBlocks()
        {
            _field.Block.Indent = GetIndent(PlayField.ActualWidth, PlayField.ActualHeight);
            _gameHelper.ChangeBlocksArray(Settings, _field.Block);
            for (var i = 0; i < Settings.RowsCount; i++)
                for (var j = 0; j < Settings.ColumnsCount; j++)
                    if (_gameHelper.BlocksArray[i, j] != null)
                        PlayField.Children.Add(_gameHelper.BlocksArray[i, j].GetRect());                        
        }

        /// <summary>
        /// Отрисовка мяча
        /// </summary>
        private void DrawBall()
        {
            var ball = _field.Ball.GetBall();
            PlayField.Children.Add(ball);
        }

        /// <summary>
        /// Отрисовка платформы
        /// </summary>
        private void DrawPlatform()
        {
            var rectangle = _field.Platform.GetRect();
            PlayField.Children.Add(rectangle);
        }

        /// <summary>
        /// Пауза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseClick(object sender, RoutedEventArgs e)
        {
            _isGame = false;
            _timer.Stop();        
        }

        /// <summary>
        /// Старт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartClic(object sender, RoutedEventArgs e)
        {
            _isGame = true;
            _timer.Start();  
        }

        /// <summary>
        /// Обработка нажатия клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyDownHandler(object sender, KeyEventArgs e)
        {
            var isMove = false;        
            switch (e.Key)
            {
                case Key.Left:             
                    isMove = true;
                    if (_field.Platform.Speed >= 0) _field.Platform.Speed *= -1;                  
                    break;

                case Key.Right:                 
                    isMove = true;                  
                    if (_field.Platform.Speed <= 0) _field.Platform.Speed *= -1;
                    break;

                case Key.Space:
                    ControlGame();
                    break;
            }
            if (isMove)
            {               
                _field.Size = GetFieldSize();
                _gameHelper.MovePlatform(_field.Platform, _field.Size);
                _field.Ball.Acceleration = 1.2;
            }
            else _field.Ball.Acceleration = 1;
        }

        /// <summary>
        /// Обработка клика 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            ControlGame();
        }

        /// <summary>
        /// Старт/остановка
        /// </summary>
        private void ControlGame()
        {
            if (!_isGame) StartClic(null, null);
            else PauseClick(null, null);
        }

        /// <summary>
        /// Обновление холста
        /// </summary>
        private void UpdateCanvas()
        {   
            DrawBlocks();            
            DrawBall();
            DrawPlatform();           
        }

        /// <summary>
        /// Проверка столкновений
        /// </summary>
        private void CheckCollision()
        {
            var collision = new Collision();
            collision.CheckBlocks(_field.Ball, _gameHelper.BlocksArray, Settings, _gamer, _field.Platform);
            collision.CheckPlatform(_field.Ball, _field.Platform);
            collision.CheckBoder(_field.Ball, PlayField.ActualWidth);
        }
        private void CheckLoss()
        {
            if (_field.Ball.Position.Y>PlayField.ActualHeight)
            {
                _timer.Stop();
                _isGame = false;        
                _gamer.LifeCount--;
                _field.Ball.Move();
                _field.Platform = Settings.Platform;
                _field.Ball = Settings.Ball;
                WindowChangeHandler(null, null);

                UpdateCanvas();
                if (_gamer.LifeCount == 0)
                {
                    _timer.Stop();
                    var endWindow = new EndWindow(this, _gamer, false);
                    endWindow.Show();
                }
            }
        }

        private void CheckWin()
        {
            if (Settings.BlocksCount*100==_gamer.Score)
            {
                _timer.Stop();
                var endWindow = new EndWindow(this, _gamer, true);
                endWindow.Show();
            }
        }

        /// <summary>
        /// Получение размеров игрового поля
        /// </summary>
        /// <returns></returns>
        private Size GetFieldSize()
        {
            var fieldSize = new Size();
            fieldSize.Width = PlayField.ActualWidth;
            fieldSize.Height = PlayField.ActualHeight;
            return fieldSize;
        }       
    }
}
