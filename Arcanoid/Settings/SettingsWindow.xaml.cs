using System.Windows;

namespace Arcanoid
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly MainWindow _mainWindow;
        private Settings _settings=new Settings();
      
        public SettingsWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
            DataContext = _settings;
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
