using ServiceLayer.Models;
using ServiceLayer.Services;
using System.Windows;
using System.Windows.Controls;

namespace FragrantWorld.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        private readonly UsersService _userService = new();

       
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы вошли как гость", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            App.CurrentFrame.Navigate(new MarketPage());
        }

        private async void AuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                bool isUserExist = await _userService.IsUserExist(LoginTextBox.Text, PasswordTextBox.Password);

                if (isUserExist)
                {
                    var user = await _userService.GetUserNameByLogin(LoginTextBox.Text);
                    MessageBox.Show($"Добро пожаловать {user.Name}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    App.CurrentFrame.Navigate(new MarketPage(user));
                }
                else
                {
                    MessageBox.Show("Логин или пароль введены не верно", "Ошикбка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошикбка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }
}
