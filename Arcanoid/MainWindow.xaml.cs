using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            var playWindow = new PlayWindow(_settings);
            playWindow.Show();
        }

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow(this) {DataContext=_settings };
            settingsWindow.Show();
        }

        public void SaveSettings(Settings settings)
        {
            _settings = settings;
        }
    }
}
