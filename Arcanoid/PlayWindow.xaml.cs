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
    public partial class PlayWindow : Window
    {
        private Ball _ball;
        private Block _block;
        private Field _field = new Field();
        private Platform _platform;
        private PlayHelper _playHelper = new PlayHelper();
        private Settings _settings;
        private Player _player=new Player();
        private System.Timers.Timer _timer;
        private bool _isPlay;

        public PlayWindow(Settings settings)
        {
            InitializeComponent();
            DataContext = _player;
            _settings = settings;
            PlayField.Focusable = true;
            InitializePlayObjects();
            SetTimer();
            PlayField.Focus();
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
                PlayField.Children.Clear();
                _ball.Move();
                CheckCollision();
                UpdateCanvas();
            }));
        }
        /// <summary>
        /// Инициализация объектов игры
        /// </summary>
        private void InitializePlayObjects()
        {
            PlayField.Background = new SolidColorBrush(_settings.Field.Color);
            _block = _settings.Block;
            _ball = _settings.Ball;
            _platform = _settings.Platform;
            _block.Indent = GetIndent(500, 600);
            _playHelper.InitializeBlocksArray(_block, _settings);
        }

        /// <summary>
        /// Обработка изменения размера окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void WindowChangeHandler(object sender, SizeChangedEventArgs e)
        {
            var platformPosition = new Point();
            platformPosition.X = PlayField.ActualWidth / 2 - _platform.Size.Width / 2;
            platformPosition.Y = PlayField.ActualHeight - _platform.Size.Height - 20;
            _platform.Position = platformPosition;
            var ballPosition = new Point();
            ballPosition.X = _platform.Position.X + _platform.Size.Width / 2;
            ballPosition.Y = _platform.Position.Y - _ball.Size.Height;
            _ball.Position = ballPosition;
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
            position.X = (width - _settings.ColumnsCount * _block.Size.Width) / (_settings.ColumnsCount + 1);
            position.Y = (height / 3 - _settings.RowsCount * _block.Size.Height) / (_settings.RowsCount + 1);
            return position;
        }

        /// <summary>
        /// Отрисовка блоков
        /// </summary>
        private void DrawBlocks()
        {
            _block.Indent = GetIndent(PlayField.ActualWidth, PlayField.ActualHeight);
            _playHelper.ChangeBlocksArray(_settings, _block);
            for (var i = 0; i < _settings.RowsCount; i++)
                for (var j = 0; j < _settings.ColumnsCount; j++)
                    if (_playHelper.BlocksArray[i, j] != null)
                        PlayField.Children.Add(_playHelper.BlocksArray[i, j].GetRect());                        
        }

        /// <summary>
        /// Отрисовка мяча
        /// </summary>
        private void DrawBall()
        {
            var ball = _ball.GetBall();
            PlayField.Children.Add(ball);
        }

        /// <summary>
        /// Отрисовка платформы
        /// </summary>
        private void DrawPlatform()
        {
            var rectangle = _platform.GetRect();
            PlayField.Children.Add(rectangle);
        }

        /// <summary>
        /// Пауза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PauseClick(object sender, RoutedEventArgs e)
        {
            _isPlay = false;
            _timer.Stop();
            PlayField.Focus();
        }

        /// <summary>
        /// Старт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartClic(object sender, RoutedEventArgs e)
        {
            _isPlay = true;
            _timer.Start();
            PlayField.Focus();
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
                    _platform.Direction = -1;
                    isMove = true;
                    break;
                case Key.Right:
                    _platform.Direction = 1;
                    isMove = true;
                    break;
                case Key.Space:
                    if (!_isPlay) StartClic(null, null);
                    else PauseClick(null, null);
                    break;
            }

            if (isMove)
            {
                _field.Size = GetFieldSize();
                _playHelper.MovePlatform(_platform, _field.Size);
            }
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
            collision.CheckBlocks(_ball, _playHelper.BlocksArray, _settings, _player, _platform);
            collision.CheckPlatform(_ball, _platform);
            collision.CheckBoder(_ball, PlayField.ActualWidth);
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
