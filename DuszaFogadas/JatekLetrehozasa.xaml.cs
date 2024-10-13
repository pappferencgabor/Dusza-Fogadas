using DuszaFogadas.Helpers;
using DuszaFogadas.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZstdSharp.Unsafe;

namespace DuszaFogadas
{
    /// <summary>
    /// Interaction logic for JatekLetrehozasa.xaml
    /// </summary>
    public partial class JatekLetrehozasa : Window
    {
        public JatekLetrehozasa()
        {
            InitializeComponent();
        }

        private void btnAddNewParticipant_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewParticipant.Text.Trim() != "")
            {
                lbParticipants.Items.Add(txtNewParticipant.Text);
                txtNewParticipant.Clear();
            }
        }

        private void lbParticipants_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbParticipants.SelectedIndex;

            if (index > -1)
            {
                lbParticipants.Items.RemoveAt(index);
            }
        }


        private void btnAddNewEvent_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewEvent.Text.Trim() != "")
            {
                lbEvents.Items.Add(txtNewEvent.Text);
                txtNewEvent.Clear();
            }
        }

        private void lbEvents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = lbEvents.SelectedIndex;

            if (index > -1)
            {
                lbEvents.Items.RemoveAt(index);
            }
        }

        private void btnCreateGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection conn = GetMysqlConnection.getMysqlConnection();
                conn.Open();
                MySqlCommand com = new MySqlCommand("SELECT * FROM jatekok WHERE nev = (@nev)", conn);
                com.Parameters.AddWithValue("@nev", txtGamename.Text);
                MySqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    throw new Exception("GameNameExists");
                }
                reader.Close();
                com = new MySqlCommand("INSERT INTO jatekok (szervezonev, nev, alanyokszama, status) VALUES (@szervezoNev, @nev, @alanyokSzama, @status)", conn);
                com.Parameters.AddWithValue("@szervezoNev", txtHostname.Text);
                com.Parameters.AddWithValue("@nev", txtGamename.Text);
                com.Parameters.AddWithValue("@alanyokSzama", lbParticipants.Items.Count);
                com.Parameters.AddWithValue("@status", "Aktiv");
                com.ExecuteNonQuery();

                com = new MySqlCommand("SELECT id FROM jatekok WHERE nev = (@nev)", conn);
                com.Parameters.AddWithValue("@nev", txtGamename.Text);
                reader = com.ExecuteReader();
                reader.Read();
                int lastGameID = reader.GetInt32("id");
                reader.Close();

                //I am fairly sure there is a nicer way to do this, but as of now this will do.
                for (int i = 0; i < lbParticipants.Items.Count; i++)
                {
                    com = new MySqlCommand("INSERT INTO alanyok (nev, jatekId) VALUES (@nev, @jatekId)", conn);
                    com.Parameters.AddWithValue("@nev", lbParticipants.Items[i]);
                    com.Parameters.AddWithValue("@jatekId", lastGameID);
                    com.ExecuteNonQuery();
                }

                for (int i = 0; i < lbEvents.Items.Count; i++)
                {
                    com = new MySqlCommand("INSERT INTO esemenyek (nev, jatekId) VALUES (@nev, @jatekId)", conn);
                    com.Parameters.AddWithValue("@nev", lbEvents.Items[i]);
                    com.Parameters.AddWithValue("@jatekId", lastGameID);
                    com.ExecuteNonQuery();
                }

                MessageBox.Show("Játék sikeresen felvéve!", "Sikeres művelet", MessageBoxButton.OK, MessageBoxImage.Information);
                conn.Close();
                this.Close();
            } catch
            {
                MessageBox.Show("Sikertelen a mentés!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
