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
            Regisztracio window = new();
            window.Show();
        }

        //private void btnTeszt_Click(object sender, RoutedEventArgs e)
        //{
        //    Regisztracio window = new();
        //    window.Show();
        //}

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = GetMysqlConnection.getMysqlConnection();
            conn.Open();
            MySqlCommand com = new MySqlCommand("SELECT * FROM felhasznalok where nev = @nev", conn);
            com.Parameters.AddWithValue("@nev", txtUsername.Text);
            com.Parameters.AddWithValue("@jelszo", PasswordHelper.HashPassword(txtPassword.Text));
            MySqlDataReader reader = com.ExecuteReader();
            conn.Close();
            if (reader.HasRows)
            {
                MessageBox.Show("Succesful login");
            } else
            {
                MessageBox.Show("Login failed");
            }
        }
    }
}