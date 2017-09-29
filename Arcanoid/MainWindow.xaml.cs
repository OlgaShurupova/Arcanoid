using System.Windows;

namespace Arcanoid
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Settings _settings=new Settings();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartClick(object sender, RoutedEventArgs e)
        {
            var gameHelper = new GameHelper();
            gameHelper.GeneratePositionsArray(_settings);
            var playWindow = new GameWindow(_settings);
            playWindow.Show();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        { 
            var settingsWindow = new SettingsWindow(this);
            settingsWindow.Show();
        }

        public void SaveSettings(Settings settings)
        {
            _settings = settings;
        }
    }
}
