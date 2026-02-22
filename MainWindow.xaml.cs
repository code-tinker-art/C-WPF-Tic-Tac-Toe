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

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] winningConditions =
        {
            {1,2,3 },
            {4,5,6 },
            {7,8,9 },
            {1,4,7 },
            {2,5,8 },
            {3,6,9 },
            { 1,5,9 },
            {3,5,7 }
        };

        List<Button> buttons;
        public MainWindow()
        {
            InitializeComponent();

            buttons = TTT_Grid.Children.OfType<Button>().ToList();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        bool isX = true;
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            if(e.OriginalSource is Button btn)
            {
                if (string.IsNullOrEmpty(btn.Content?.ToString()))
                {
                    btn.Content = isX ? "X" : "O";
                    btn.Background = isX ? Brushes.CadetBlue : Brushes.PaleVioletRed;
                    isX = !isX;

                    btn.IsEnabled = false;
                }

                if (checkWinner())
                {
                    WinnerText.Text = isX ? "O is the winner." : "X is the winner.";
                    disableAllBtn();
                }else if(!checkWinner() && allCellsFilled())
                {
                    WinnerText.Text =  "Draw";
                }
            }
        }

        private bool allCellsFilled()
        {
            return buttons.All(b => !string.IsNullOrEmpty(b.Content?.ToString()));
        }

        private void disableAllBtn()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].IsEnabled = false;
            }
        }

        private bool checkWinner()
        {
            bool winnerFound = false;
            for (int i = 0; i < winningConditions.GetLength(0); i++) { 
                int a = winningConditions[i, 0] - 1;
                int b = winningConditions[i, 1] - 1;
                int c = winningConditions[i, 2] - 1;

                string 
                    valA = buttons[a].Content?.ToString(),
                    valB = buttons[b].Content?.ToString(),
                    valC = buttons[c].Content?.ToString();

                if (!string.IsNullOrEmpty(valA))
                {
                    if (string.Equals(valA, valB) && string.Equals(valB, valC))
                    {
                        winnerFound = true;
                        buttons[a].Background = Brushes.LightGreen;
                        buttons[b].Background = Brushes.LightGreen;
                        buttons[c].Background = Brushes.LightGreen;
                    }
                }
            }

            return winnerFound;
        }

        private void Rst_Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var child in TTT_Grid.Children)
            {
                if (child is Button btn)
                {
                    btn.IsEnabled = true;
                    btn.Content = "";
                    btn.Background = (Brush)new BrushConverter().ConvertFrom("#111");
                }
            }

            WinnerText.Text = "__ is the winner.";
            isX = true;
        }
    }
}
