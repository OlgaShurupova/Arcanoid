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
using System.Windows.Shapes;

namespace Arcanoid
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly MainWindow _mainWindow;
        private Settings _settings;
        public SettingsWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
           var result= MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButton.YesNoCancel);
            switch(result)
            {
                case MessageBoxResult.Yes:
                    _mainWindow.SaveSettings(_settings);
                    Close();
                    break;
                case MessageBoxResult.No:
                    Close();
                    break;
                case MessageBoxResult.Cancel:
                    break;
            }
            
        }
    }
}
