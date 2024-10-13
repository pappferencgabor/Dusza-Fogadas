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
using DuszaFogadas.Models;

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

            if (usr.Role.ToString() == "F")
            {
                btnCreateGame.Visibility = Visibility.Collapsed;
                btnCloseGame.Visibility = Visibility.Collapsed;
                btnManageUsers.Visibility = Visibility.Collapsed;
            }
            if (usr.Role.ToString() == "SZ")
            {
                btnManageUsers.Visibility = Visibility.Collapsed;
            }
        }

        private void btnCreateGame_Click(object sender, RoutedEventArgs e)
        {
            JatekLetrehozasa createGame = new JatekLetrehozasa();
            createGame.Show();
        }
        
        private void btnSendBet_Click(object sender, RoutedEventArgs e)
        {
            FogadasLeadasa sendBet = new FogadasLeadasa(usr);
            sendBet.Show();
        }

        private void btnCloseGame_Click(object sender, RoutedEventArgs e)
        {
            JatekLezarasa jatekLezarasa = new JatekLezarasa(usr);
            jatekLezarasa.Show();
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            Admin admin = new Admin();
            admin.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
