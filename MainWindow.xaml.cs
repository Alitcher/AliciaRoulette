using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace Roulette
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
       // private Popup winningPopup = new Popup();

        public MainWindow()
        {
            SetTheme();
            InitializeComponent();
            AddFirstSlot();
            CreateUniformGrid(1, ref MainGrid1);
            CreateUniformGrid(2, ref MainGrid2);
            CreateUniformGrid(3, ref MainGrid3);
            AddFourthColumn();

            //CreatePopup();

            //  myTextBlock.Text = $"{Width / Height}";

        }



        private void SetTheme()
        {
            ResourceDictionary resources = new ResourceDictionary();
            ResourceDictionary mergedDictionary = new ResourceDictionary();
            mergedDictionary.Source = new Uri("Theme/RouletteTheme.xaml", UriKind.Relative);
            resources.MergedDictionaries.Add(mergedDictionary);
            this.Resources = resources;
        }

        private void AddFirstSlot()
        {
            // Create the parent Grid
            MainGrid0 = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                Margin = new Thickness(10, 131, 10.2, 131)
            };

            AddRowDefinition(ref MainGrid0);

            Grid childGrid = new Grid
            {
                Background = Brushes.Green

            };

            MainGrid0.Children.Add(childGrid);
            Grid.SetRow(childGrid, 0);
            Grid.SetColumn(childGrid, 0);

            TextBlock textBlock = new TextBlock
            {
                Text = "0",
                FontSize = 55
            };

            childGrid.Children.Add(textBlock);
            Grid.SetRow(textBlock, 0);
            Grid.SetColumn(textBlock, 0);

        }

        Border GetBorder(int i, bool isBlack)
        {
            Border border = new Border
            {
                Style = (Style)FindResource("BorderBotStyle"),
                Background = (isBlack) ? Brushes.Black : Brushes.Red,
                BorderThickness = new Thickness(5),
                BorderBrush = Brushes.Green,
                CornerRadius = new CornerRadius(8),
                //Height = 100,
                //Width = 80,
                Margin = new Thickness(2)
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
                FontSize = 35
            };

            return textBlock;
        }

        bool isBlackBG(int num, int slot)
        {
            if (slot == 1)
            {
                if ((num % 2 == 0 || num == 11) && num != 12)
                {
                    return true;
                }
            }
            else if (slot == 2)
            {
                if (num == 13 || num == 15 || num == 17 || num == 20 || num == 22 || num == 24)
                {
                    return true;
                }
            }
            else if (slot == 3)
            {
                if (num == 26 || num == 28 || num == 29 || num == 31 || num == 33 || num == 35)
                {
                    return true;
                }
            }
            return false;
        }

        private void CreateUniformGrid(int whichSlot, ref Grid botGrid)
        {

            UniformGrid numberGrid = CreateGridNumber(whichSlot);
            Grid.SetRow(numberGrid, 0);
            botGrid.Children.Add(numberGrid);

            UniformGrid uniformGrid1 = new UniformGrid { Rows = 1, Columns = 1, Margin = new Thickness(0, 20, 0, 0) };
            Border border1 = new Border
            {
                Style = (Style)FindResource("BorderBotStyle")
            };
            TextBlock textBlock1 = new TextBlock
            {
                Style = (Style)FindResource("UniformBotTextBlack"),
                Text = whichSlot == 1 ? "1st 12" : whichSlot == 2 ? "2nd 12" : "3rd 12"
            };
            border1.Child = textBlock1;
            uniformGrid1.Children.Add(border1);
            Grid.SetRow(uniformGrid1, 1);
            botGrid.Children.Add(uniformGrid1);

            UniformGrid uniformGrid2 = new UniformGrid { Rows = 1, Columns = 2, Margin = new Thickness(0, 15, 0, 0) };
            Border border2 = new Border { Style = (Style)FindResource("BorderBotStyle") };
            TextBlock textBlock2 = new TextBlock
            {
                Style = (Style)FindResource("UniformBotTextBlack"),
                Text = whichSlot == 1 ? "1 to 18" : whichSlot == 2 ? "" : "Odd"
            };
            border2.Child = textBlock2;
            uniformGrid2.Children.Add(border2);

            Border border3 = new Border { Style = (Style)FindResource("BorderBotStyle") };
            TextBlock textBlock3 = new TextBlock
            {
                Style = (Style)FindResource("UniformBotTextBlack"),
                Text = whichSlot == 1 ? "Even" : whichSlot == 2 ? "" : "19 to 36"
            };
            border3.Child = textBlock3;
            if (whichSlot == 2) border3.Background = Brushes.Red;

            uniformGrid2.Children.Add(border3);
            Grid.SetRow(uniformGrid2, 2);
            botGrid.Children.Add(uniformGrid2);
            AddRowDefinition(ref botGrid);
        }

        private UniformGrid CreateGridNumber(int whichSlot)
        {
            UniformGrid numberGrid = new UniformGrid { Rows = 3, Columns = 4 };

            int rows = 3, cols = 4;
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    int i = col * rows + (rows - row);
                    if (whichSlot > 1) i += 12 * (whichSlot - 1);
                    Border border = GetBorder(i, isBlackBG(i, whichSlot));
                    border.Child = GetTextBlock(i);
                    numberGrid.Children.Add(border);
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                }
            }


            return numberGrid;
        }




        private void AddRowDefinition(ref Grid grid)
        {
            // Create the RowDefinitions
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
        }

        private void AddFourthColumn()
        {
            Grid grid = new Grid();
            Grid.SetColumn(grid, 4);
            Margin = new Thickness(10, 140, 10.2, 100);

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            // grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            AddRowDefinition(ref grid);

            Viewbox viewbox = new Viewbox();
            Grid.SetRow(viewbox, 0);
            Margin = new Thickness(0, -30, 0.2, 50);
            Grid.SetColumnSpan(viewbox, 2);

            grid.Children.Add(viewbox);

            // Create the UniformGrid
            UniformGrid uniformGrid = new UniformGrid();
            Grid.SetRow(uniformGrid, 0);
            uniformGrid.Rows = 3;
            uniformGrid.Width = 120;
            uniformGrid.HorizontalAlignment = HorizontalAlignment.Left;
            viewbox.Child = uniformGrid;

            for (int i = 0; i < 3; i++)
            {
                Border border = new Border
                {
                    Style = (Style)FindResource("BorderBotStyle"),
                    Background = Brushes.Black,
                    CornerRadius = new CornerRadius(10),
                    Height = 130,
                    Width = 100,
                    Margin = new Thickness(0, 0, 0, 0)
                };
                TextBlock textBlock = new TextBlock
                {
                    Style = (Style)FindResource("Rotated90Text"),
                    Text = "2 to 1"
                };
                border.Child = textBlock;
                uniformGrid.Children.Add(border);
            }

            MainGrid4.Children.Add(grid);
        }

        //private void CreatePopup()
        //{
        //    winningPopup.Placement = PlacementMode.Bottom;
        //    winningPopup.CustomPopupPlacementCallback = PopupPlacementCallback;

        //    Border outerBorder = new Border
        //    {
        //        BorderBrush = Brushes.Gray,
        //        BorderThickness = new Thickness(2),
        //        CornerRadius = new CornerRadius(5)
        //    };

        //    Grid grid = new Grid();
        //    grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        //    grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        //    Border headerBorder = new Border
        //    {
        //        Background = Brushes.Black,
        //        Height = 50,
        //        CornerRadius = new CornerRadius(5)
        //    };
        //    Grid.SetRow(headerBorder, 0);

        //    TextBlock headerText = new TextBlock
        //    {
        //        Text = "22",
        //        Foreground = Brushes.White,
        //        FontSize = 20,
        //        Margin = new Thickness(10)
        //    };

        //    headerBorder.Child = headerText;

        //    Border contentBorder = new Border
        //    {
        //        Background = Brushes.Gray,
        //        CornerRadius = new CornerRadius(5),
        //        Padding = new Thickness(10)
        //    };
        //    Grid.SetRow(contentBorder, 1);

        //    StackPanel stackPanel = new StackPanel();

        //    for (int i = 1; i <= 5; i++)
        //    {
        //        TextBlock contentText = new TextBlock
        //        {
        //            Text = $"Random text {i}",
        //            Foreground = Brushes.White,
        //            FontSize = 18,
        //            Margin = new Thickness(0, 0, 0, 5)
        //        };
        //        stackPanel.Children.Add(contentText);
        //    }

        //    contentBorder.Child = stackPanel;

        //    grid.Children.Add(headerBorder);
        //    grid.Children.Add(contentBorder);

        //    outerBorder.Child = grid;
        //    winningPopup.Child = outerBorder;
        //}

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            winningPopup.IsOpen = true;
        }

        public CustomPopupPlacement[] PopupPlacementCallback(Size popupSize, Size targetSize, Point offset)
        {
            CustomPopupPlacement placement1 = new CustomPopupPlacement(new Point(targetSize.Width - popupSize.Width, targetSize.Height - popupSize.Height), PopupPrimaryAxis.None);
            CustomPopupPlacement[] ttplaces = new CustomPopupPlacement[] { placement1 };
            return ttplaces;
        }
    }


}
