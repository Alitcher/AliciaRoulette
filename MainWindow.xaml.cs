using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Roulette
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddNumberBorders();
            CreateUniformGridBot();
        }

        private void AddNumberBorders()
        {
            int rows = 3, cols = 4;
            int slots = 3;


            for (int slot = 0; slot < slots; slot++)
            {

            }
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int i = col * rows + (rows - row);

                    Border border = GetBorder(i);
                    border.Child = GetTextBlock(i);
                    NumberGrid.Children.Add(border);
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                }
            }



            Border GetBorder(int i) 
            {
                Border border = new Border
                {
                    Background = (i % 2 == 0) ? Brushes.Black : Brushes.Red,
                    BorderThickness = new Thickness(0.5),
                    BorderBrush = Brushes.Green,
                    CornerRadius = new CornerRadius(2),
                    Margin = new Thickness(0.5, 1, 0, 0)
                };

                return border;
            }

            TextBlock GetTextBlock(int i) 
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = i.ToString(),
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 5
                };

                return textBlock;
            }


        }

        private void CreateUniformGridBot()
        {
            // Create the first UniformGrid
            UniformGrid uniformGrid1 = new UniformGrid { Rows = 1, Columns = 1 };
            Border border1 = new Border { Style = (Style)FindResource("BorderBotStyle") };
            TextBlock textBlock1 = new TextBlock
            {
                Style = (Style)FindResource("UniformBotTextBlack"),
                Text = "1st 12"
            };
            border1.Child = textBlock1;
            uniformGrid1.Children.Add(border1);
            Grid.SetRow(uniformGrid1, 1);
            MainGrid.Children.Add(uniformGrid1);

            // Create the second UniformGrid
            UniformGrid uniformGrid2 = new UniformGrid { Rows = 1, Columns = 2, Margin = new Thickness(0, 5, 0, 0) };
            Border border2 = new Border { Style = (Style)FindResource("BorderBotStyle") };
            TextBlock textBlock2 = new TextBlock
            {
                Style = (Style)FindResource("UniformBotTextBlack"),
                Text = "1 to 18"
            };
            border2.Child = textBlock2;
            uniformGrid2.Children.Add(border2);

            Border border3 = new Border { Style = (Style)FindResource("BorderBotStyle") };
            TextBlock textBlock3 = new TextBlock
            {
                Style = (Style)FindResource("UniformBotTextBlack"),
                Text = "Even"
            };
            border3.Child = textBlock3;
            uniformGrid2.Children.Add(border3);
            Grid.SetRow(uniformGrid2, 2);
            MainGrid.Children.Add(uniformGrid2);
        }
    }


}
