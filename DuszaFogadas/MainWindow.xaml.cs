using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

using DuszaFogadas.Helpers;

namespace DuszaFogadas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MySqlConnection connection = GetMysqlConnection.getMysqlConnection();

            connection.Open();
        }

        //private void btnTeszt_Click(object sender, RoutedEventArgs e)
        //{
        //    Regisztracio window = new();
        //    window.Show();
        //}

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}