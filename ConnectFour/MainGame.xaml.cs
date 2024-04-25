using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ConnectFour;
public partial class MainGame
{
    //Enum used to store player states for when there is no player and for player 1 and 2
    private enum Player
    {
        None,
        Red,
        Blue
    }

    private bool _currentPlayerIsRed = true;
    private readonly Player[,] _gameBoard = new Player[6, 7];

    //This entire function is used to generate the grid for the game, may be changed in time.
    public MainGame()
    {
        InitializeComponent();
        //Button loop to generate the grid using 6 rows and 7 columns
        for (var row = 0; row < 6; row++)
        {
            for (var col = 0; col < 7; col++)
            {
                var button = new Button();
                button.Content = $"Cell ({row},{col})"; //debugging
                button.HorizontalAlignment = HorizontalAlignment.Stretch;
                button.VerticalAlignment = VerticalAlignment.Stretch;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);
                gameGrid.Children.Add(button);

                button.Click += Button_Click;
            }
        }
    }

    //This is the function for whenever you click a button on the grid, the big fucntion.
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var clickedButton = (Button)sender;
        var column = Grid.GetColumn(clickedButton);
        var row = GetLowestEmptyRow(column);

        if (row == -1)
        {
            //If the column is full, you can't place anymore in that column.
            return;
        }

        //This is to check if the clicked button is already colored
        if (_gameBoard[row, column] != Player.None)
        {
            row = GetLowestEmptyRow(column);
            if (row == -1)
            {
                return;
            }
        }

        //variable to set the colour for each player, red and blue
        var colorBrush = _currentPlayerIsRed ? Brushes.Red : Brushes.Blue;


        //This updates the game's state, making sure the lowest empty row is checked aswell.
        var lowestButton = gameGrid.Children
            .OfType<Button>()
            .FirstOrDefault(b => Grid.GetRow(b) == GetLowestEmptyRow(column) && Grid.GetColumn(b) == column);

        if (lowestButton != null)
        {
            lowestButton.Background = colorBrush;
            _gameBoard[GetLowestEmptyRow(column), column] = _currentPlayerIsRed ? Player.Red : Player.Blue;
        }

        //Below this is all checks for win conditions, it's quite long aswell.
        if (CheckForConnectFourVertically(row, column))
        {
            Console.WriteLine("Connect Four vertically!");
        }

        if (CheckForConnectFourHorizontally(row, column))
        {
            Console.WriteLine("Connect Four horizontally!");
        }

        if (CheckForConnectFourDiagonally(row, column))
        {
            Console.WriteLine("Connect Four diagonally!");
        }

        //Switch to the next player
        _currentPlayerIsRed = !_currentPlayerIsRed;
    }

    //This function is what calcualates where the lowest empty row is in that column
    private int GetLowestEmptyRow(int columnIndex)
    {
        for (var row = _gameBoard.GetLength(0) - 1; row >= 0; row--)
        {
            if (_gameBoard[row, columnIndex] == Player.None)
            {
                return row;
            }
        }
        return -1; //the column is full
    }

    //Function to check if the win condition has been achieved vertically
    private bool CheckForConnectFourVertically(int row, int column)
    {
        var currentPlayer = _currentPlayerIsRed ? Player.Red : Player.Blue;
        var count = 0;

        //Check downwards
        for (var i = row; i >= 0; i--)
        {
            if (_gameBoard[i, column] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }

        //Check upwards
        for (var i = row + 1; i < 6; i++)
        {
            if (_gameBoard[i, column] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }
        return false;
    }

    //Function to check if the win condition has been achieved horizontally
    private bool CheckForConnectFourHorizontally(int row, int column)
    {
        var currentPlayer = _currentPlayerIsRed ? Player.Red : Player.Blue;
        var count = 0;

        //Check to the left
        for (var i = column; i >= 0; i--)
        {
            if (_gameBoard[row, i] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }

        //Check to the right
        for (var i = column + 1; i < 7; i++)
        {
            if (_gameBoard[row, i] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }
        return false;
    }

    //Function to check if the win condition has been achieved diagonally
    private bool CheckForConnectFourDiagonally(int row, int column)
    {
        return CheckForConnectFourDiagonallyUpward(row, column) ||
               CheckForConnectFourDiagonallyDownward(row, column);
    }

    //Function to check if its been won diagonally upwards
    private bool CheckForConnectFourDiagonallyUpward(int row, int column)
    {
        var currentPlayer = _currentPlayerIsRed ? Player.Red : Player.Blue;
        var count = 0;

        //Check upwards diagonally
        for (int i = row, j = column; i >= 0 && j >= 0; i--, j--)
        {
            if (_gameBoard[i, j] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }

        //Check downwards diagonally
        for (int i = row + 1, j = column + 1; i < 6 && j < 7; i++, j++)
        {
            if (_gameBoard[i, j] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }

        return false;
    }

    //Function to check if its been won diagonallh downward
    private bool CheckForConnectFourDiagonallyDownward(int row, int column)
    {
        var currentPlayer = _currentPlayerIsRed ? Player.Red : Player.Blue;
        var count = 0;

        //Check upwards diagonally
        for (int i = row, j = column; i >= 0 && j < 7; i--, j++)
        {
            if (_gameBoard[i, j] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }

        //Check downwards diagonally
        for (int i = row + 1, j = column - 1; i < 6 && j >= 0; i++, j--)
        {
            if (_gameBoard[i, j] == currentPlayer)
            {
                count++;
                if (count >= 4)
                {
                    return true;
                }
            }
            else
            {
                break;
            }
        }
        return false;
    }
}