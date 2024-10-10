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
using DuszaFogadas.Models;
using System.Data;
using static DuszaFogadas.Models.UserEnum;

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
        }

        private void btnTeszt_Click(object sender, RoutedEventArgs e)
        {
            Admin window = new();
            window.Show();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = GetMysqlConnection.getMysqlConnection();
            conn.Open();
            MySqlCommand com = new MySqlCommand("SELECT * FROM felhasznalok where nev = @nev", conn);
            com.Parameters.AddWithValue("@nev", txtUsername.Text);
            MySqlDataReader reader = com.ExecuteReader();
            reader.Read();
            if (PasswordHelper.VerifyPassword(reader.GetString("jelszo"), txtPassword.Password))
            { 
                User usr = new User(reader.GetInt32("id"), reader.GetString("nev"), reader.GetInt32("pontok"), ConvertEnum(reader.GetString("szerepkor")));
                conn.Close();
                Menu menu = new Menu(usr);
                menu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed");
                conn.Close();
            }
        }
    }
}