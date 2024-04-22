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

namespace ConnectFour
{
    public partial class MainGame : Window
    {
        public MainGame()
        {
            InitializeComponent();

            // Button loop to generate the grid
            for (int row = 0; row < 6; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    Button button = new Button();
                    button.Content = $"Cell ({row},{col})";
                    Grid.SetRow(button, row);
                    Grid.SetColumn(button, col);
                    gameGrid.Children.Add(button);
                }
            }
        }
    }
}
