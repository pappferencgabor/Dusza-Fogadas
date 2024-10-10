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

            MessageBox.Show(reader.HasRows.ToString());

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
            }
        }
    }
}
