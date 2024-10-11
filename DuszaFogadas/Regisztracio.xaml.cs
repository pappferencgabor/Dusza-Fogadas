using DuszaFogadas.Helpers;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public string errorMessage = "";

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == txtPasswordAgain.Password)
            {
                if (ValidatePassword(txtPassword.Password))
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
            else
            {
                MessageBox.Show("Az általad megadott két jelszó nem egyezik!", "Érvénytelen jelszó", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool ValidatePassword(string password)
        {
            if (password.Length < 8)
            {
                errorMessage += "A jelszónak legalább 8 karakter hosszúnak kell lennie.\n";
            }

            if (password.Contains(" "))
            {
                errorMessage += "A jelszó nem tartalmazhat szóközt.\n";
            }

            if (!password.Any(char.IsUpper) || !password.Any(char.IsLower) || !password.Any(char.IsDigit) || !Regex.IsMatch(password, @"[\W_]"))
            {
                errorMessage += "A jelszónak tartalmaznia kell legalább egy: ";

                if (!password.Any(char.IsUpper))
                {
                    errorMessage += "nagybetűt, ";
                }

                if (!password.Any(char.IsLower))
                {
                    errorMessage += "kisbetűt, ";
                }

                if (!password.Any(char.IsDigit))
                {
                    errorMessage += "számot, ";
                }

                if (!Regex.IsMatch(password, @"[\W_]"))
                {
                    errorMessage += "speciális karaktert, ";
                }
            }

            errorMessage = errorMessage.Trim();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage.Substring(0, errorMessage.Length-1), "Érvénytelen jelszó", MessageBoxButton.OK, MessageBoxImage.Warning);
                errorMessage = "";
                return false;
            }

            errorMessage = "";
            return true;
        }
    }
}
