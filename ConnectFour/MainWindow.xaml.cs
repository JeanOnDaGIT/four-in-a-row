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

namespace ConnectFour
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        // handling buttons that send the user to a new window or show instructions/quit out of the application - Tom
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Window MainGame = new MainGame();
            MainGame.Show();
        }

        private void Inst_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("How to Play:" + Environment.NewLine +
                "- Players start by placing counters" + Environment.NewLine +
                "- Make 4 in a row to win (across, diagnoally or upwards)" + Environment.NewLine +
                "- Use your tokens to successfully block your opponent from winning" + Environment.NewLine +
                "Enjoy!");
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
