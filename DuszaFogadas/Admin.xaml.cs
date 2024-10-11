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
using System.Collections.ObjectModel;

using DuszaFogadas.Helpers;
using DuszaFogadas.Models;

namespace DuszaFogadas
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        ObservableCollection<User> users = new ObservableCollection<User>();

        public Admin()
        {
            InitializeComponent();

            dgData.ItemsSource = users;
            LoadPage();
        }

        public void LoadPage()
        {
            MySqlConnection conn = GetMysqlConnection.getMysqlConnection();
            conn.Open();

            MySqlCommand com = new MySqlCommand("SELECT * FROM felhasznalok", conn);
            MySqlDataReader reader = com.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    User user = new User(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetInt32(3),
                        UserEnum.ConvertEnum(reader.GetString(4))
                    );
                    users.Add(user);
                }
            }
            conn.Close();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedUser = dgData.SelectedItem as User;

            if (selectedUser != null) 
            {
                txtUsername.Text = selectedUser.Name;

                txtPoints.Text = selectedUser.Points.ToString();

                if (selectedUser.Role.ToString() == "A") { cboUserRole.Text = "Admin"; }
                else if (selectedUser.Role.ToString() == "SZ") { cboUserRole.Text = "Szervező"; }
                else if (selectedUser.Role.ToString() == "F") { cboUserRole.Text = "Felhasználó"; }
            }
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            Regisztracio regisztracio = new Regisztracio();
            regisztracio.Closed += RegisterWindow_Closed;
            regisztracio.Show();
        }

        private void btnModifyUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgData.SelectedItem as User;

            users[dgData.SelectedIndex].Name = txtUsername.Text;
            users[dgData.SelectedIndex].Points = Convert.ToInt32(txtPoints.Text);
            users[dgData.SelectedIndex].Role = UserEnum.ConvertEnum(convertRole(cboUserRole.Text.ToString()));

            dgData.Items.Refresh();

            MySqlConnection conn = GetMysqlConnection.getMysqlConnection();
            conn.Open();
            MySqlCommand com = new MySqlCommand($"UPDATE felhasznalok SET nev=@nev, pontok=@pontok, szerepkor=@szerepkor WHERE id=@id", conn);
            com.Parameters.AddWithValue("@nev", txtUsername.Text);
            com.Parameters.AddWithValue("@pontok", int.Parse(txtPoints.Text));
            com.Parameters.AddWithValue("@szerepkor", convertRole(cboUserRole.Text.ToString()));
            com.Parameters.AddWithValue("@id", selectedUser.Id);
            com.ExecuteNonQuery();
            conn.Close();

            clearTextboxes();
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgData.SelectedItem as User;
            users.RemoveAt(dgData.SelectedIndex);

            MySqlConnection conn = GetMysqlConnection.getMysqlConnection();
            conn.Open();
            MySqlCommand com = new MySqlCommand($"DELETE FROM felhasznalok WHERE id=@id", conn);
            com.Parameters.AddWithValue("@id", selectedUser.Id);
            com.ExecuteNonQuery();
            conn.Close();

            clearTextboxes();
        }

        private void RegisterWindow_Closed(object sender, EventArgs e)
        {
            users.Clear();
            LoadPage();
        }

        private string convertRole(string role)
        {
            if (role == "Admin")
            {
                return "A";
            }
            else if (role == "Szervező")
            {
                return "SZ";
            }
            else if (role == "Felhasználó")
            {
                return "F";
            }
            else
            {
                return "";
            }
        }
    
        private void clearTextboxes()
        {
            txtUsername.Clear();
            txtPoints.Clear();
            cboUserRole.Text = "";
        }
    }
}