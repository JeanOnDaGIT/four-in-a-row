using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace ConnectFour
{
    public partial class MainGame : Window
    {
        public enum Player
        {
            None, Red, Blue
        }
        private bool currentPlayerIsRed = true;
        private Player[,] gameBoard = new Player[6, 7];
        public MainGame()
        {
            InitializeComponent();

            //Button loop to generate the grid
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Button button = new Button();
                    //button.Content = $"Cell ({row},{col})"; (This tells the user which column it is)
                    button.HorizontalAlignment = HorizontalAlignment.Stretch;
                    button.VerticalAlignment = VerticalAlignment.Stretch;
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    gameGrid.Children.Add(button);
                    button.Click += Button_Click;

                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;

            int column = Grid.GetColumn(clickedButton);

            //Finds the lowest available row in the column
            for (int row = 5; row >= 0; row--)
            {
                if (!IsCellOccupied(row, column))
                {
                    // Set the background color of the button
                    SolidColorBrush colorBrush = currentPlayerIsRed ? Brushes.Red : Brushes.Blue;
                    clickedButton.Background = colorBrush;

                    // Update the game board state
                    gameBoard[row, column] = currentPlayerIsRed ? Player.Red : Player.Blue;

                    // Switch to the next player
                    currentPlayerIsRed = !currentPlayerIsRed;
                    break;
                }
            }
        }

        private bool IsCellOccupied(int row, int column)
        {
            //Checks if the cell at the specified row and column is occupied
            return gameBoard[row, column] != Player.None;
        }
    }
}