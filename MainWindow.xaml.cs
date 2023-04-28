using System;
using System.Collections.Generic;
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
        private RouletteTcpListener _rouletteTcpListener = new RouletteTcpListener();

        private Popup winningPopup = new Popup();
        private TextBlock winningText;
        private TextBlock[] contentText = new TextBlock[5];
        public MainWindow()
        {
            SetTheme();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            // AddFirstSlot();
            BoardSetup();
            CreateUniformGrid(1, ref MainGrid1);
            CreateUniformGrid(2, ref MainGrid2);
            CreateUniformGrid(3, ref MainGrid3);
            AddFourthColumn();

            CreatePopup();

            _rouletteTcpListener.OnDataReceived += RouletteTcpListener_OnDataReceived;
            _rouletteTcpListener.StartListening();

        }

        List<Border> numberBorder = new List<Border>();
        List<Border> colorBorder = new List<Border>();
        List<Border> oddEvenBorder = new List<Border>();
        List<Border> dozenBorder = new List<Border>();
        List<Border> columnBorder = new List<Border>();
        List<Border> lowHighBorder = new List<Border>();

        List<TextBlock> numberTextBlock = new List<TextBlock>();
        List<TextBlock> oddEvenTextBlock = new List<TextBlock>();
        List<TextBlock> dozenTextBlock = new List<TextBlock>();
        List<TextBlock> columnTextBlock = new List<TextBlock>();
        List<TextBlock> lowHighTextBlock = new List<TextBlock>();

        private void BoardSetup()
        {
            for (int i = 0; i < 36; i++)
            {
                numberBorder.Add(new Border());
                numberTextBlock.Add(new TextBlock());
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _rouletteTcpListener.StartListening();
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

        bool isBlackBG(int num)
        {
            if (num > 0 && num <=12 )
            {
                if ((num % 2 == 0 || num == 11) && num != 12)
                {
                    return true;
                }
            }
            else if (num > 12 && num <= 24)
            {
                if (num == 13 || num == 15 || num == 17 || num == 20 || num == 22 || num == 24)
                {
                    return true;
                }
            }
            else
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
            dozenBorder.Add(border1);
            dozenTextBlock.Add(textBlock1);
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
            if (whichSlot == 2)
            {
                border2.Background = Brushes.Black;
                colorBorder.Add(border2);
            }
            else
            {
                if (whichSlot == 1)
                {
                    lowHighBorder.Add(border2);
                    lowHighTextBlock.Add(textBlock2);
                }
                else
                {
                    oddEvenBorder.Add(border2);
                    oddEvenTextBlock.Add(textBlock2);
                }
            }

            border2.Child = textBlock2;
            uniformGrid2.Children.Add(border2);

            Border border3 = new Border { Style = (Style)FindResource("BorderBotStyle") };
            TextBlock textBlock3 = new TextBlock
            {
                Style = (Style)FindResource("UniformBotTextBlack"),
                Text = whichSlot == 1 ? "Even" : whichSlot == 2 ? "" : "19 to 36"
            };
            border3.Child = textBlock3;
            if (whichSlot == 2)
            {
                border3.Background = Brushes.Red;
                colorBorder.Add(border3);
            }
            else
            {
                if (whichSlot == 1)
                {

                    oddEvenBorder.Add(border3);
                    oddEvenTextBlock.Add(textBlock3);
                }
                else
                {
                    lowHighBorder.Add(border3);
                    lowHighTextBlock.Add(textBlock3);
                }
                colorBorder.Add(border3);
            }
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
                    numberBorder[i - 1] = border;
                    numberTextBlock[i - 1] = GetTextBlock(i);
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
                columnBorder.Add(border);
                TextBlock textBlock = new TextBlock
                {
                    Style = (Style)FindResource("Rotated90Text"),
                    Text = "2 to 1"
                };
                columnTextBlock.Add(textBlock);
                border.Child = textBlock;
                uniformGrid.Children.Add(border);
            }

            MainGrid4.Children.Add(grid);
        }

        private void CreatePopup()
        {

            double screenWidth = SystemParameters.WorkArea.Width;
            double screenHeight = SystemParameters.WorkArea.Height;

            winningPopup.Placement = PlacementMode.AbsolutePoint;
            winningPopup.HorizontalOffset = screenWidth - 200; // Adjust the value based on my popup width
            winningPopup.VerticalOffset = screenHeight - 200;

            // winningPopup.Placement = PlacementMode.Bottom;
            winningPopup.CustomPopupPlacementCallback = PopupPlacementCallback;

            Border outerBorder = new Border
            {
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(2),
                CornerRadius = new CornerRadius(5)
            };

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            Border headerBorder = new Border
            {
                Background = Brushes.Black,
                Height = 50,
                CornerRadius = new CornerRadius(5)
            };
            Grid.SetRow(headerBorder, 0);

            winningText = new TextBlock
            {
                Text = "ZZ",
                Foreground = Brushes.White,
                FontSize = 20,
                Margin = new Thickness(10)
            };

            headerBorder.Child = winningText;

            Border contentBorder = new Border
            {
                Background = Brushes.Gray,
                CornerRadius = new CornerRadius(5),
                Padding = new Thickness(10)
            };
            Grid.SetRow(contentBorder, 1);

            StackPanel stackPanel = new StackPanel();

            for (int i = 0; i < 5; i++)
            {
                contentText[i] = new TextBlock
                {
                    Text = $"Random text {i}",
                    Foreground = Brushes.White,
                    FontSize = 18,
                    Margin = new Thickness(0, 0, 0, 5)
                };
                stackPanel.Children.Add(contentText[i]);
            }

            contentBorder.Child = stackPanel;

            grid.Children.Add(headerBorder);
            grid.Children.Add(contentBorder);

            outerBorder.Child = grid;
            winningPopup.Child = outerBorder;
        }



        public CustomPopupPlacement[] PopupPlacementCallback(Size popupSize, Size targetSize, Point offset)
        {
            double x = SystemParameters.WorkArea.Width - popupSize.Width;
            double y = SystemParameters.WorkArea.Height - popupSize.Height;

            CustomPopupPlacement placement = new CustomPopupPlacement(new Point(x, y), PopupPrimaryAxis.None);
            return new[] { placement };
        }

        private void RouletteTcpListener_OnDataReceived(object sender, RouletteData rouletteData)
        {
            winningText.Text = rouletteData.Data.WinningNumber.ToString();

            ShowPopupAndCloseAfterDelay(winningPopup, TimeSpan.FromSeconds(10));
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            SetWinning(rand.Next(0, 38));
            ShowPopupAndCloseAfterDelay(winningPopup, TimeSpan.FromSeconds(2));
        }



        private void SetWinning(int index)
        {
            winningText.Text = index.ToString();

            if (index == 0)
            {
                contentText[0].Text = WinningNumberTexts[0][1];
                for (int i = 1; i < contentText.Length; i++)
                {
                    contentText[i].IsEnabled = false;
                }
            }

            else
            {
                for (int i = 0; i < contentText.Length; i++)
                {
                    contentText[i].IsEnabled = true;
                    contentText[i].Text = WinningNumberTexts[index][i + 1];
                }
            }
            UpdateBoard(index);

        }

        private void ResetBoard()
        {
            for (int i = 0; i < numberTextBlock.Count; i++)
            {
                numberTextBlock[i].Foreground = isBlackBG(int.Parse(numberTextBlock[i].Text)) ? Brushes.White : Brushes.Black;

                numberBorder[i].Background = isBlackBG(int.Parse(numberTextBlock[i].Text)) ? Brushes.Black : Brushes.Red;
                numberBorder[i].Child = numberTextBlock[i];
            }



            colorBorder[0].Background = Brushes.Black;
            colorBorder[1].Background = Brushes.Red;


            for (int i = 0; i < dozenBorder.Count; i++)
            {
                dozenBorder[i].Background = Brushes.Black;
                dozenTextBlock[i].Foreground = Brushes.White;
                dozenBorder[i].Child = dozenTextBlock[i];

                columnBorder[i].Background = Brushes.Black;
                columnTextBlock[i].Foreground = Brushes.White;
                columnBorder[i].Child = columnTextBlock[i];
            }

            for (int i = 0; i < lowHighBorder.Count; i++)
            {
                lowHighBorder[i].Background = Brushes.Black;
                lowHighTextBlock[i].Foreground = Brushes.White;
                lowHighBorder[i].Child = lowHighTextBlock[i];

                oddEvenBorder[i].Background = Brushes.Black;
                oddEvenTextBlock[i].Foreground = Brushes.White;
                oddEvenBorder[i].Child = oddEvenTextBlock[i];

            }


        }

        private void UpdateBoard(int winningNumber)
        {
            ResetBoard();
            //Number
            if (winningNumber > 0)
            {
                numberBorder[winningNumber - 1].Background = Brushes.Cyan;
                numberTextBlock[winningNumber - 1].Foreground = Brushes.Black;
                numberBorder[winningNumber - 1].Child = numberTextBlock[winningNumber - 1];

            }

            // Color
            if (WinningNumberTexts[winningNumber][1] == "Black")
            {
                colorBorder[1].Background = Brushes.Cyan;
            }
            else
            {
                colorBorder[1].Background = Brushes.Cyan;
            }


            //Odd Even
            if (WinningNumberTexts[winningNumber][2] == "Even")
            {
                oddEvenBorder[0].Background = Brushes.Cyan;
                oddEvenTextBlock[0].Foreground = Brushes.Black;
                oddEvenBorder[0].Child = oddEvenTextBlock[0];
            }
            else
            {
                oddEvenBorder[1].Background = Brushes.Cyan;
                oddEvenTextBlock[1].Foreground = Brushes.Black;
                oddEvenBorder[1].Child = oddEvenTextBlock[1];
            }

            //Dozen
            if (WinningNumberTexts[winningNumber][4].Contains("1st"))
            {
                dozenBorder[0].Background = Brushes.Cyan;
                dozenTextBlock[0].Foreground = Brushes.Black;
                dozenBorder[0].Child = dozenTextBlock[0];
            }
            else if (WinningNumberTexts[winningNumber][4].Contains("2nd"))
            {
                dozenBorder[1].Background = Brushes.Cyan;
                dozenTextBlock[1].Foreground = Brushes.Black;
                dozenBorder[1].Child = dozenTextBlock[1];
            }
            else
            {
                dozenBorder[2].Background = Brushes.Cyan;
                dozenTextBlock[2].Foreground = Brushes.Black;
                dozenBorder[2].Child = dozenTextBlock[2];
            }


            //Column
            if (WinningNumberTexts[winningNumber][4].Contains("ColumnA"))
            {
                columnBorder[0].Background = Brushes.Cyan;
                columnTextBlock[0].Foreground = Brushes.Black;
                columnBorder[0].Child = columnTextBlock[0];
            }
            else if (WinningNumberTexts[winningNumber][4].Contains("ColumnB"))
            {
                columnBorder[1].Background = Brushes.Cyan;
                columnTextBlock[1].Foreground = Brushes.Black;
                columnBorder[1].Child = columnTextBlock[1];

            }
            else
            {
                columnBorder[2].Background = Brushes.Cyan;
                columnTextBlock[2].Foreground = Brushes.Black;
                columnBorder[2].Child = columnTextBlock[2];
            }

            if (WinningNumberTexts[winningNumber][3].Contains("Low"))
            {
                lowHighBorder[0].Background = Brushes.Cyan;
                lowHighTextBlock[0].Foreground = Brushes.Black;
                lowHighBorder[0].Child = lowHighTextBlock[0];

            }
            else
            {
                lowHighBorder[1].Background = Brushes.Cyan;
                lowHighTextBlock[1].Foreground = Brushes.Black;
                lowHighBorder[1].Child = lowHighTextBlock[1];
            }

        }



        private void ShowPopupAndCloseAfterDelay(Popup popup, TimeSpan delay)
        {
            popup.IsOpen = true;

            DispatcherTimer timer = new DispatcherTimer { Interval = delay };
            timer.Tick += (s, e) =>
            {
                popup.IsOpen = false;
                timer.Stop();
            };
            timer.Start();
        }

        private List<List<string>> WinningNumberTexts = new List<List<string>>
        {
            new List<string>{"0", "0"},
            new List<string>{"1", "Red", "Odd", "Low", "ColumnA", "1st dozen"},
            new List<string>{"2", "Black", "Even", "Low", "ColumnB", "1st dozen"},
            new List<string>{"3", "Red", "Odd", "Low", "ColumnC", "1st dozen"},
            new List<string>{"4", "Black", "Even", "Low", "ColumnA", "1st dozen"},
            new List<string>{"5", "Red", "Odd", "Low", "ColumnB", "1st dozen"},
            new List<string>{"6", "Black", "Even", "Low", "ColumnC", "1st dozen"},
            new List<string>{"7", "Red", "Odd", "Low", "ColumnA", "1st dozen"},
            new List<string>{"8", "Black", "Even", "Low", "ColumnB", "1st dozen"},
            new List<string>{"9", "Red", "Odd", "Low", "ColumnC", "1st dozen"},
            new List<string>{"10", "Black", "Even", "Low", "ColumnA", "1st dozen"},
            new List<string>{"11", "Black", "Odd", "Low", "ColumnB", "1st dozen"},
            new List<string>{"12", "Red", "Even", "Low", "ColumnC", "1st dozen"},
            new List<string>{"13", "Black", "Odd", "Low", "ColumnA", "2nd dozen"},
            new List<string>{"14", "Red", "Even", "Low", "ColumnB", "2nd dozen"},
            new List<string>{"15", "Black", "Odd", "Low", "ColumnC", "2nd dozen"},
            new List<string>{"16", "Red", "Even", "Low", "ColumnA", "2nd dozen"},
            new List<string>{"17", "Black", "Odd", "Low", "ColumnB", "2nd dozen"},
            new List<string>{"18", "Red", "Even", "Low", "ColumnC", "2nd dozen"},
            new List<string>{"19", "Red", "Odd", "High", "ColumnA", "2nd dozen"},
            new List<string>{"20", "Black", "Even", "High", "ColumnB", "2nd dozen"},
            new List<string>{"21", "Red", "Odd", "High", "ColumnC", "2nd dozen"},
            new List<string>{"22", "Black", "Even", "High", "ColumnA", "2nd dozen"},
            new List<string>{"23", "Red", "Odd", "High", "ColumnB", "2nd dozen"},
            new List<string>{"24", "Black", "Even", "High", "ColumnC", "2nd dozen"},
            new List<string>{"25", "Red", "Odd", "High", "ColumnA", "3rd dozen"},
            new List<string>{"26", "Black", "Even", "High", "ColumnB", "3rd dozen"},
            new List<string>{"27", "Red", "Odd", "High", "ColumnC", "3rd dozen"},
            new List<string>{"28", "Black", "Even", "High", "ColumnA", "3rd dozen"},
            new List<string>{"29", "Black", "Odd", "High", "ColumnB", "3rd dozen"},
            new List<string>{"30", "Red", "Even", "High", "ColumnC", "3rd dozen"},
            new List<string>{"31", "Black", "Odd", "High", "ColumnA", "3rd dozen"},
            new List<string>{"32", "Red", "Even", "High", "ColumnB", "3rd dozen"},
            new List<string>{"33", "Black", "Odd", "High", "ColumnC", "3rd dozen"},
            new List<string>{"34", "Red", "Even", "High", "ColumnA", "3rd dozen"},
            new List<string>{"35", "Black", "Odd", "High", "ColumnB", "3rd dozen"},
            new List<string>{"36", "Red", "Even", "High", "ColumnC", "3rd dozen"},
            new List<string>{"37", "Black", "Odd", "High", "ColumnA", "3rd dozen" }
           };

    }


}
