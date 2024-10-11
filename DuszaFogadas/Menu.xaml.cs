using DuszaFogadas.Models;
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

namespace DuszaFogadas
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        User usr;
        public Menu(User user)
        {
            InitializeComponent();
            usr = user;
        }

        private void btn_CreateGame(object sender, RoutedEventArgs e)
        {
            JatekLetrehozasa createGame = new JatekLetrehozasa();
            createGame.Show();
        }
    }
}
