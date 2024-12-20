using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ServiceLayer.Models;
using ServiceLayer.Services;

namespace FragrantWorld.Pages
{
    /// <summary>
    /// Логика взаимодействия для MarketPage.xaml
    /// </summary>
    public partial class MarketPage : Page
    {
        private readonly ProductsService _productsService = new();
        private readonly UsersService _usersService = new();

        public MarketPage(User user = null)
        {
            InitializeComponent();
            if (user == null)
            {
                UserTextBlock.Text = "Гость";
            }
            else
            {
                UserTextBlock.Text = user.Surname + " " + user.Name + " " + user.Patronymic;
            }
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadProductsAsync();
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                var product = await _productsService.GetProductsAsync();
                foreach (Product productItem in product)
                    CreatProductConteiner(productItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CreatProductConteiner(Product productItem)
        {
            try
            {
                StackPanel panel = new()
                {
                    Width = 700,
                    Margin = new Thickness(20),
                    Background = new SolidColorBrush(Color.FromRgb(255, 204, 153)),

                };



                Grid grid = new() { };
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());

                TextBlock ProductTextBlock = new TextBlock()
                {
                    Text = productItem.Name,
                    FontWeight = FontWeights.Bold
                };
                Grid.SetRow(ProductTextBlock, 0);
                grid.Children.Add(ProductTextBlock);

                TextBlock DescriptionTextBlock = new TextBlock
                {
                    Text = productItem.Description
                };
                Grid.SetRow(DescriptionTextBlock, 1);
                grid.Children.Add(DescriptionTextBlock);

                TextBlock ManufacturerTextBlock = new TextBlock
                {
                    Text = $"Производитель: {productItem.Manufacturer}",
                };
                Grid.SetRow(ManufacturerTextBlock, 2);
                grid.Children.Add(ManufacturerTextBlock);

                TextBlock PriceTextBlock = new TextBlock
                {
                    Text = $"Цена: {productItem.Cost}",
                };
                Grid.SetRow(PriceTextBlock, 3);
                grid.Children.Add(PriceTextBlock);

                panel.Children.Add(grid);
                ProductStackPanel.Children.Add(panel);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentFrame.CanGoBack)
                App.CurrentFrame.GoBack();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
