using DuszaFogadas.Helpers;
using MySql.Data.MySqlClient;
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
    /// Interaction logic for Regisztracio.xaml
    /// </summary>
    public partial class Regisztracio : Window
    {
        public Regisztracio()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == txtPasswordAgain.Password)
            {
                MySqlConnection conn = GetMysqlConnection.getMysqlConnection();
                conn.Open();
                MySqlCommand com = new MySqlCommand("INSERT INTO felhasznalok (nev, jelszo, pontok, szerepkor) VALUES (@nev, @jelszo, @pontok, @szerepkor)", conn);
                com.Parameters.AddWithValue("@nev", txtUsername.Text);
                com.Parameters.AddWithValue("@jelszo", PasswordHelper.HashPassword(txtPassword.Password));
                com.Parameters.AddWithValue("@pontok", 0);
                com.Parameters.AddWithValue("@szerepkor", "F");
                com.ExecuteNonQuery();
                conn.Close();
                this.Close();
            }
            
        }
    }
}
